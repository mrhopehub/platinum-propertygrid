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
