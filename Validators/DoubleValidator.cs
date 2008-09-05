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
