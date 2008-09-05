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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace Platinum.PropertyEditors
{
    [PropertyEditor( typeof( bool ) )]
    public partial class BoolEditor : PropertyEditorBase
    {
        #region BoolSource class
        class BoolSource : CustomSourceListEditor.IItemSource
        {
            object[] _values = new object[] { false, true };

            public event Action SourceChanged
            {
                add { }
                remove { }
            }

            public IEnumerable Items
            {
                get { return _values; }
            }
        }
        #endregion

        #region Variables
        static BoolSource _boolSource = new BoolSource();
        #endregion

        #region Properties
        public override BoundPropertyDescriptor PropertyDescriptor
        {
            get { return _customSourceListEditor.PropertyDescriptor; }
            set 
            {
                _customSourceListEditor.PropertyDescriptor = value; 
            }
        }
        #endregion

        #region Constructor
        public BoolEditor()
        {
            InitializeComponent();
            _customSourceListEditor.ItemSource = _boolSource;
        }
        #endregion

        #region Event Handlers
        void _customSourceListEditor_PropertyChangeCommitted( object sender, PropertyChangeEventArgs e )
        {
            _raisePropertyChangeCommittedEvent( e );
        }

        void _customSourceListEditor_PropertyChangeReverted( object sender, PropertyChangeRevertedEventArgs e )
        {
            _raisePropertyChangeRevertedEvent( e );
        }

        void _customSourceListEditor_PropertyChanging( object sender, PropertyChangeEventArgs e )
        {
            _raisePropertyChangingEvent( e );
        }

        void _customSourceListEditor_PropertyDescriptorChanged( object sender, PropertyDescriptorChangedEventArgs e )
        {
            _raisePropertyDescriptorChangedEvent( e );
        }

        void _customSourceListEditor_SelectedItemRemovedFromSource( object sender, ItemEventArgs e )
        {
            throw new Exception( "This should not happen." );
        }
        #endregion
    }
}
