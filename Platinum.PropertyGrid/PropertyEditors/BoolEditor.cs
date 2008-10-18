#region MIT
/*
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace Platinum.PropertyEditors
{
    [IsDefaultPropertyEditorOf( typeof( bool ) )]
    public partial class BoolEditor : PropertyEditorBase
    {
        #region BoolSource class
        class BoolSource : CustomSourceListEditor.IItemSource
        {
            object[] _values = new object[] { false, true };

            public event EventHandler SourceChanged
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
        [Browsable( false )]
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

            _propertyGridItemAssigned += new Action<PropertyGridItem>( _handlePropertyGridItemAssigned );
        }
        #endregion

        #region Methods
        public override void RefreshProperty()
        {
            _customSourceListEditor.RefreshProperty();
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

        void _handlePropertyGridItemAssigned( PropertyGridItem gridItem )
        {
            gridItem.IsSelectedChanged += new EventHandler( _gridItem_IsSelectedChanged );
        }

        void _gridItem_IsSelectedChanged( object sender, EventArgs e )
        {
            if ( PropertyGridItem.IsSelected )
            {
                _customSourceListEditor.Focus();
            }
        }
        #endregion
    }
}
