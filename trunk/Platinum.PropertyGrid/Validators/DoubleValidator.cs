#region MIT
/*
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
using System.Globalization;
using System.ComponentModel;

namespace Platinum.Validators
{
    internal class DoubleValidator : IValidator
    {
        #region Variables
        String _message;
        readonly IFormatProvider _formatProvider;
        readonly TypeConverter _typeConverter;
        #endregion

        #region Constructor
        public DoubleValidator()
        {
            _message = "";
            _formatProvider = CultureInfo.GetCultureInfo( "en-US" ).NumberFormat;
            _typeConverter = TypeDescriptor.GetConverter( typeof( double ) );
        }
        #endregion

        #region Methods
        public Object ValidateValue( Object o )
        {
            double val;

            if ( o is String )
            {
                if ( double.TryParse( o.ToString(), NumberStyles.Float,
                    _formatProvider, out val ) )
                {
                    return val;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if ( !_typeConverter.CanConvertFrom( o.GetType() ) )
                    return null;

                try
                {
                    return _typeConverter.ConvertFrom( o );
                }
                catch
                {
                    return null;
                }
            }
        }

        public T ConvertTo<T>( Object o ) where T : class
        {
            if ( typeof( T ) == typeof( String ) )
            {
                Object ret = ( ( (double) o ).ToString( _formatProvider ) );
                return (T) ret;
            }
            else
            {
                try
                {
                    return (T) _typeConverter.ConvertTo( o, typeof( T ) );
                }
                catch
                {
                    _message = "Could not convert " + o.ToString() + " to " + typeof( T ).ToString();
                    return null;
                }
            }
        }
        #endregion

        #region Properties
        public string Message
        {
            get { return _message; }
        }

        public Object DefaultValue
        {
            get { return 0.0; }
        }

        public Type ValidatedType
        {
            get { return typeof( double ); }
        }
        #endregion
    }
}
