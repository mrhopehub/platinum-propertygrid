#region LGPL.
// -----------------------------------------------------------------------------
// This source file is part of Platinum.PropertyGrid
// For the latest info, see http://code.google.com/p/platinum-propertygrid/
//
// Copyright (c) 2008 Michael Woerister
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// or go to http://www.gnu.org/copyleft/lesser.txt.
// -----------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;

namespace Platinum.Validators
{
    public class DefaultValidator : IValidator
    {
        #region Variables
        static Dictionary<Type, Type> _defaultValidators;
        static Dictionary<Type, Func<Object>> _defaultValueFactories;

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
                Func<Object> factory;

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
            Debug.Assert( validatorType.GetInterfaces().Contains( typeof( IValidator ) ) );
            Debug.Assert( !_defaultValidators.ContainsKey( type ) );

            _defaultValidators[type] = validatorType;
        }

        public static void RegisterDefaultValueFactory( Type type, Func<Object> factory )
        {
            Debug.Assert( !_defaultValueFactories.ContainsKey( type ) );

            _defaultValueFactories.Add( type, factory );
        }
        #endregion

        #region Static Initializer
        static DefaultValidator()
        {
            _defaultValidators = new Dictionary<Type, Type>();
            _defaultValueFactories = new Dictionary<Type, Func<Object>>();

            RegisterDefaultValidator( typeof( float ), typeof( FloatValidator ) );
            RegisterDefaultValueFactory( typeof( String ), () => "" );
        }
        #endregion
    }
}
