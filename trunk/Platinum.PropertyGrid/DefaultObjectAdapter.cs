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

                _editor.PropertyChanging += _editor_PropertyChange;
                _editor.PropertyChangeCommitted += _editor_PropertyChange;
                _editor.PropertyChangeReverted += _editor_PropertyChangeReverted;
            }

            void _editor_PropertyChangeReverted( object sender, 
                PropertyChangeRevertedEventArgs e )
            {
                _property.PropertyDescriptor.SetValue(
                    _property.PropertyOwner,
                    e.RestoredValue
                    );
            }

            void _editor_PropertyChange( object sender, 
                PropertyChangeEventArgs e )
            {
                _property.PropertyDescriptor.SetValue(
                    _property.PropertyOwner,
                    e.NewValue
                    );
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
