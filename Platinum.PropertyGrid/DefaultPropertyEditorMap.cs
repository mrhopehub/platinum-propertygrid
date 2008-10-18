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
