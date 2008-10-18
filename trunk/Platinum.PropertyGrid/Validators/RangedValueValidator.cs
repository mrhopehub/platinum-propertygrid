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
    public class RangedValueValidator<T> : IValidator where T : IComparable<T>
    {
        #region Variables
        readonly T _min;
        readonly T _max;
        readonly IValidator _validator;
        String _message = "";
        #endregion


        #region Properties
        public string Message
        {
            get { return _message; }
        }

        public Object DefaultValue
        {
            get { return _min; }
        }

        public Type ValidatedType
        {
            get { return _validator.ValidatedType; }
        }
        #endregion

        #region Constructors
        public RangedValueValidator( T min, T max )
        {
            _min = min;
            _max = max;
            _validator = DefaultValidator.CreateFor( typeof( T ) );
        }

        public RangedValueValidator( T min, T max, IValidator wrappedValidator )
        {
            Debug.Assert( wrappedValidator != null );

            _min = min;
            _max = max;
            _validator = wrappedValidator;
        }
        #endregion

        #region Methods
        public Object ValidateValue( Object o )
        {
            _message = "Invalid value";

            Object value = _validator.ValidateValue( o );

            if ( value == null )
                return null;

            if ( !( value is T ) )
                return null;
            
            T casted = (T) value;
            
            if ( casted.CompareTo( _min ) >= 0 && casted.CompareTo( _max ) <= 0 )
            {
                _message = "";
                return value;
            }
            else
            {
                _message = "Value " + o.ToString() + " is out of range [ " 
                    + _min.ToString() + " : " + _max.ToString() +  " ]";

                return null;
            }
        }

        public U ConvertTo<U>( Object o ) where U : class
        {
            return _validator.ConvertTo<U>( o );
        }
        #endregion
    }
}
