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
using System.Text;
using System.ComponentModel;

namespace Platinum
{
    public struct BoundPropertyDescriptor
    {
        public PropertyDescriptor PropertyDescriptor;
        public Object PropertyOwner;

        public bool IsEmpty
        {
            get { return PropertyDescriptor == null || PropertyOwner == null; }
        }

        public static bool operator ==( BoundPropertyDescriptor d1,
                                       BoundPropertyDescriptor d2 )
        {
            return d1.PropertyOwner == d2.PropertyOwner &&
                   d1.PropertyDescriptor == d2.PropertyDescriptor;
        }

        public static bool operator !=( BoundPropertyDescriptor d1,
                                       BoundPropertyDescriptor d2 )
        { 
            return !(d1 == d2);
        }

        public override bool Equals( object obj )
        {
            if ( obj is BoundPropertyDescriptor )
            {
                return this == (BoundPropertyDescriptor) obj;
            }

            return false;
        }

        public override int GetHashCode()
        {
            throw new NotSupportedException();
        }
    }
}
