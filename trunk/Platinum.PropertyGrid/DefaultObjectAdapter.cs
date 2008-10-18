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

namespace Platinum
{
    public class DefaultObjectAdapter : Component, IPropertyGridObjectAdapter
    {
        Object _selectedObject;
        PropertyGrid _targetPropertyGrid;

        class PropertyAdapter
        {
            BoundPropertyDescriptor _property;
            PropertyEditorBase _editor;

            public PropertyAdapter( BoundPropertyDescriptor property,
                PropertyEditorBase editor )
            {
                _property = property;
                _editor = editor;

                _editor.PropertyChanging += _editor_PropertyChanging;
                _editor.PropertyChangeCommitted += _editor_PropertyChangeCommitted;
                _editor.PropertyChangeReverted += _editor_PropertyChangeReverted;
            }

            void _editor_PropertyChanging( object sender,
                PropertyChangeEventArgs e )
            {
                _property.PropertyDescriptor.SetValue(
                    _property.PropertyOwner,
                    e.NewValue
                    );
            }

            void _editor_PropertyChangeCommitted( object sender,
                PropertyChangeEventArgs e )
            {
                _property.PropertyDescriptor.SetValue(
                    _property.PropertyOwner,
                    e.NewValue
                    );

                _editor.RefreshProperty();
            }

            void _editor_PropertyChangeReverted( object sender, 
                PropertyChangeRevertedEventArgs e )
            {
                _property.PropertyDescriptor.SetValue(
                    _property.PropertyOwner,
                    e.RestoredValue
                    );

                _editor.RefreshProperty();
            }

            
        }

        public object SelectedObject
        {
            get
            {
                return _selectedObject;
            }
            set
            {
                _selectedObject = value;

                if ( _targetPropertyGrid != null )
                {
                    _targetPropertyGrid.Items.Clear();

                    if ( _selectedObject != null )
                    {
                        _fillPropertyGrid( _targetPropertyGrid, _selectedObject );
                    }
                }
            }
        }

        public PropertyGrid TargetPropertyGrid
        {
            get { return _targetPropertyGrid; }
            set
            {
                _targetPropertyGrid = value;

                if ( _targetPropertyGrid != null ) 
                {
                    _targetPropertyGrid.Items.Clear();

                    if ( _selectedObject != null )
                    {
                        _fillPropertyGrid( _targetPropertyGrid, _selectedObject );
                    }
                }
            }
        }

        void _fillPropertyGrid( PropertyGrid grid, Object o )
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties( o );

            Dictionary<String, IPropertyGridSection> _sections =
                new Dictionary<string, IPropertyGridSection>();

            foreach ( PropertyDescriptor desc in props )
            {
                if ( desc.IsReadOnly )
                    continue;

                PropertyEditorBase editor;

                try
                {
                    editor = DefaultPropertyEditorMap.GetEditor( desc.PropertyType );
                }
                catch
                {
                    continue;
                }

                String sectionName = desc.Category;

                IPropertyGridSection section;

                if ( !_sections.TryGetValue( sectionName, out section ) )
                {
                    section = grid.Items.Add( sectionName );
                    _sections.Add( sectionName, section );
                }

                BoundPropertyDescriptor propDesc = new BoundPropertyDescriptor();
                propDesc.PropertyDescriptor = desc;
                propDesc.PropertyOwner = o;

                editor.PropertyDescriptor = propDesc;

                PropertyGridItem item = section.Items.Add( desc.Name, editor );

                item.EditorPanel.Height = editor.Height;
                editor.Width = item.EditorPanel.Width;
                editor.Anchor = 
                    System.Windows.Forms.AnchorStyles.Top |
                    System.Windows.Forms.AnchorStyles.Left | 
                    System.Windows.Forms.AnchorStyles.Right;

                item.Description = desc.Description;
                
                item.EditorPanel.Controls.Add( editor );

                new PropertyAdapter( propDesc, editor );
            }
        }
    }
}
