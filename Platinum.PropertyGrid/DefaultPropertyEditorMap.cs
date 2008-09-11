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
using System.Reflection;

namespace Platinum
{
    public static class DefaultPropertyEditorMap
    {
        #region variables
        static Dictionary<Type, Type> _map = new Dictionary<Type, Type>();
        #endregion

        static DefaultPropertyEditorMap()
        {
            Type attributeType = typeof( IsDefaultPropertyEditorOfAttribute );

            foreach ( Assembly a in AppDomain.CurrentDomain.GetAssemblies() )
            {
                foreach ( Type type in a.GetTypes() )
                {
                    if ( !type.IsClass )
                        continue;

                    Object[] attributes = type.GetCustomAttributes( attributeType, true );

                    if ( attributes.Length == 0 )
                        continue;

                    foreach ( Object attribute in attributes )
                    {
                        IsDefaultPropertyEditorOfAttribute dpAttribute =
                            attribute as IsDefaultPropertyEditorOfAttribute;
                        
                        if ( dpAttribute != null )
                        {
                            _map.Add( dpAttribute.TargetType, type );
                        }
                    }
                }
            }
        }

        public static PropertyEditorBase GetEditor( Type propertyType )
        {
            Type editorType;

            for ( Type t = propertyType; t != typeof( Object ); t = t.BaseType )
            {
                if ( _map.TryGetValue( t, out editorType ) )
                {
                    Object o = TypeDescriptor.CreateInstance( null, editorType, null, null );

                    return (PropertyEditorBase) o;
                }
            }

            ValidatingStringEditor defaultEditor = new ValidatingStringEditor();
            defaultEditor.Validator = Platinum.Validators.DefaultValidator.CreateFor( propertyType );

            return defaultEditor;
        }
    }
}
