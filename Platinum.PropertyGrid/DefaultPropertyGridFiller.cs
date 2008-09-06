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
using System.Windows.Forms;

namespace Platinum
{
    public class DefaultPropertyGridFiller : IPropertyGridFiller
    {
        public void FillPropertyGrid( PropertyGrid grid, object o )
        {
            PropertyDescriptorCollection props = 
                TypeDescriptor.GetProperties( o );

            Dictionary<String, IPropertyGridSection> _sections =
                new Dictionary<string, IPropertyGridSection>();

            foreach ( PropertyDescriptor desc in props )
            {
                String sectionName;

                IEnumerable<CategoryAttribute> categoryAttributes =
                    desc.Attributes.OfType<CategoryAttribute>();

                if ( categoryAttributes.Any() )
                {
                    sectionName = categoryAttributes.Single().Category;
                }
                else
                {
                    sectionName = "Misc";
                }

                IPropertyGridSection section;

                if ( !_sections.TryGetValue( sectionName, out section ) )
                {
                    section = grid.Items.Add( sectionName );
                    _sections.Add( sectionName, section );
                }

                IEnumerable<DescriptionAttribute> descriptionAttributes =
                    desc.Attributes.OfType<DescriptionAttribute>();

                PropertyEditorBase editor = 
                    DefaultPropertyEditorMap.GetEditor( desc.PropertyType );

                BoundPropertyDescriptor propDesc = new BoundPropertyDescriptor();
                propDesc.PropertyDescriptor = desc;
                propDesc.PropertyOwner = o;

                editor.PropertyDescriptor = propDesc;

                PropertyGridItem item = section.Items.Add( desc.Name, editor );

                item.EditorPanel.Height = editor.Height;
                editor.Width = item.EditorPanel.Width;
                editor.Anchor = AnchorStyles.Left | AnchorStyles.Right;

                if ( descriptionAttributes.Any() )
                {
                    item.Description = descriptionAttributes.Single().Description;
                }

                item.EditorPanel.Controls.Add( editor );
            }
        }
    }
}
