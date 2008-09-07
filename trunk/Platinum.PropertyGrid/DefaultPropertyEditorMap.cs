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
                IEnumerable<Type> classes = a.GetTypes().Where( x => x.IsClass );
                IEnumerable<Type> editors = classes.Where( x => x.GetCustomAttributes( attributeType, true ).Length > 0 );

                foreach ( Type editor in editors )
                {
                    var attributes = editor.GetCustomAttributes( attributeType, true ).Cast<IsDefaultPropertyEditorOfAttribute>();

                    foreach ( var attribute in attributes )
                    {
                        _map.Add( attribute.TargetType, editor );
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
