#region MIT
/*
Platinum.PropertyGrid (http://code.google.com/p/platinum-propertygrid/)
Copyright (c) 2008 Michael Woerister

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;

namespace Platinum.Validators
{
    public delegate Object DefaultValueFactoryDelegate();

    public class DefaultValidator : IValidator
    {
        #region Variables
        static Dictionary<Type, Type> _defaultValidators;
        static Dictionary<Type, DefaultValueFactoryDelegate> _defaultValueFactories;

        TypeConverter _typeConverter;
        String _message = "";
        Type _validatedType;
        #endregion

        #region Properties
        public string Message
        {
            get { return _message; }
        }

        public Type ValidatedType
        {
            get { return _validatedType; }
        }

        public Object DefaultValue
        {
            get
            {
                DefaultValueFactoryDelegate factory;

                if ( _defaultValueFactories.TryGetValue( _validatedType, out factory ) )
                {
                    return factory();
                }

                return TypeDescriptor.CreateInstance( null, _validatedType, null, null );
            }
        }
        #endregion

        #region Constructor (private)
        DefaultValidator( Type t )
        {
            Debug.Assert( t != null );

            _typeConverter = TypeDescriptor.GetConverter( t );
            _validatedType = t;
        }
        #endregion

        #region Public Methods
        public Object ValidateValue( Object o )
        {
            if ( _typeConverter.CanConvertFrom( o.GetType() ) )
            {
                try
                {
                    Object ret = _typeConverter.ConvertFrom( o );

                    _message = "";
                    return ret;
                }
                catch
                {
                    _message = "Cannot convert " + o.ToString() + " to " + _validatedType.FullName;
                    return null;
                }
            }
            else
            {
                _message = "Cannot convert " + o.ToString() + " to " + _validatedType.FullName;
                return null;
            }
        }

        public T ConvertTo<T>( Object o ) where T : class
        {
            try
            {
                return (T) _typeConverter.ConvertTo( o, typeof( T ) );
            }
            catch
            {
                _message = "Cannot convert object to type " + typeof( T ).ToString();
                return null;
            }
        }
        #endregion

        #region Public Static Methods
        public static IValidator CreateFor( Type type )
        {
            Type vType;

            if ( _defaultValidators.TryGetValue( type, out vType ) )
            {
                return (IValidator) TypeDescriptor.CreateInstance(
                    null, vType, null, null
                    );
            }

            return new DefaultValidator( type );
        }

        public static void RegisterDefaultValidator( Type type, Type validatorType )
        {
            Debug.Assert( ContainsInterface( validatorType.GetInterfaces(), 
                typeof( IValidator ) ) );
            Debug.Assert( !_defaultValidators.ContainsKey( type ) );

            _defaultValidators[type] = validatorType;
        }

        public static void RegisterDefaultValueFactory( Type type, 
            DefaultValueFactoryDelegate factory )
        {
            Debug.Assert( !_defaultValueFactories.ContainsKey( type ) );

            _defaultValueFactories.Add( type, factory );
        }
        #endregion

        #region Static Initializer
        static DefaultValidator()
        {
            _defaultValidators = new Dictionary<Type, Type>();
            _defaultValueFactories = new Dictionary<Type, DefaultValueFactoryDelegate>();

            RegisterDefaultValidator( typeof( float ), typeof( FloatValidator ) );
            RegisterDefaultValueFactory( typeof( String ), delegate { return ""; } );
        }
        #endregion

        #region Private Methods
        static bool ContainsInterface( Type[] types, Type iface )
        {
            foreach ( Type type in types )
            {
                if ( type == iface )
                    return true;
            }

            return false;
        }
        #endregion
    }
}
