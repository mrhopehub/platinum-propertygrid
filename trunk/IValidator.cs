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
using System.Drawing;

namespace Platinum
{
    public interface IValidator
    {
        /// <summary>
        /// Returns the validated value or null if the value was not valid.
        /// </summary>
        /// <param name="o">The Object to validate</param>
        /// <returns>The type of the returned object must be the same as ValidatedType</returns>
        Object ValidateValue( Object o );

        /// <summary>
        /// The type validated by this validator.
        /// </summary>
        Type ValidatedType { get; }

        /// <summary>
        /// An error message specifying the last error. Empty if the last validation
        /// was successful.
        /// </summary>
        String Message { get; }

        /// <summary>
        /// A default value of the validated type. This default value must always
        /// pass validation.
        /// </summary>
        Object DefaultValue { get; }
    }

    public static class ValidatorInvalidColor
    {
        public static readonly Color BackGround = Color.Salmon;
        public static readonly Color ForeGround = Color.Black;
    }
}
