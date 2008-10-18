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
using System.Diagnostics;
using System.Collections;

namespace Platinum.PropertyEditors
{
    /// <summary>
    /// PropertyEditor for all non-flag enums.
    /// </summary>
    [IsDefaultPropertyEditorOf( typeof( Enum ) )]
    public partial class EnumEditor : PropertyEditorBase
    {
        #region EnumItemSource class
        class EnumItemSource : CustomSourceListEditor.IItemSource
        {
            static Dictionary<Type, Array> _typeMap =
                new Dictionary<Type, Array>();

            Array _values;

            public EnumItemSource( Type type )
            {
                if ( !_typeMap.ContainsKey( type ) )
                {
                    _typeMap.Add( type, Enum.GetValues( type ) );
                }

                _values = _typeMap[type];
            }

            public event EventHandler SourceChanged
            {
                add { }
                remove { }
            }

            public IEnumerable Items
            {
                get 
                {
                    return _values;
                }
            }
        }
        #endregion

        #region Construtor
        public EnumEditor()
        {
            InitializeComponent();

            _customSourceListEditor.PropertyDescriptorChanged += 
                _customSourceListEditor_PropertyDescriptorChanged;

            _propertyGridItemAssigned += new Action<PropertyGridItem>( _handlePropertyGridItemAssigned );
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

        #region Properties
        [Browsable( false )]
        public override BoundPropertyDescriptor PropertyDescriptor
        {
            get
            {
                return _customSourceListEditor.PropertyDescriptor;
            }
            set
            {
                if ( !value.IsEmpty )
                {
                    Debug.Assert( value.PropertyDescriptor.PropertyType.IsEnum );

                    _customSourceListEditor.ItemSource =
                        new EnumItemSource( value.PropertyDescriptor.PropertyType );

                    _customSourceListEditor.PropertyDescriptor = value;
                }
            }
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
        #endregion
    }
}
