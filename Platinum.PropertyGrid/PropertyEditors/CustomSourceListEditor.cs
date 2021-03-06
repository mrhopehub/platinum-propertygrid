﻿#region MIT
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;

namespace Platinum.PropertyEditors
{
    public partial class CustomSourceListEditor : PropertyEditorBase
    {
        #region IItemSource interface
        public interface IItemSource
        {
            event EventHandler SourceChanged;
            IEnumerable Items { get; }
        }
        #endregion

        #region ItemNameProvider interface
        public delegate String ItemNameProviderDelegate( Object item );
        #endregion

        #region Events
        public event EventHandler<ItemEventArgs> SelectedItemRemovedFromSource;
        #endregion

        #region Variables
        IItemSource _itemSource;
        ItemNameProviderDelegate _nameProvider;
        List<Object> _items = new List<Object>();
        BoundPropertyDescriptor _propertyDescriptor;
        Object _lastCommittedValue;
        Object _lastValue;
        bool _suppressEvents;
        #endregion

        #region Properties
        [Browsable( false )]
        public IItemSource ItemSource
        {
            get { return _itemSource; }
            set 
            {
                if ( _itemSource != null )
                {
                    _itemSource.SourceChanged -= _updateItemList;
                }

                _itemSource = value;

                if ( _itemSource != null )
                {
                    _itemSource.SourceChanged += _updateItemList;
                }

                _updateItemList( null, EventArgs.Empty );

                if ( _propertyDescriptor.PropertyOwner != null )
                {
                    Debug.Assert( _propertyDescriptor.PropertyDescriptor != null );
                    _selectItem(
                        _propertyDescriptor.PropertyDescriptor.GetValue(
                            _propertyDescriptor.PropertyOwner )
                            );
                }
            }
        }

        [Browsable(false)]
        public ItemNameProviderDelegate ItemNameProvider
        {
            get { return _nameProvider; }
            set 
            { 
                _nameProvider = value;
                _updateItemList( null, EventArgs.Empty );
            }
        }

        [Browsable( false )]
        public override BoundPropertyDescriptor PropertyDescriptor
        {
            get
            {
                return _propertyDescriptor;
            }
            set
            {
                if ( value != _propertyDescriptor )
                {
                    _propertyDescriptor = value;
                    RefreshProperty();

                    _raisePropertyDescriptorChangedEvent(
                        new PropertyDescriptorChangedEventArgs( value )
                    );
                }
            }
        }
        #endregion

        #region Constructor
        public CustomSourceListEditor()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        public override void RefreshProperty()
        {
            _suppressEvents = true;
            if ( !_propertyDescriptor.IsEmpty )
            {
                _lastCommittedValue =
                    _propertyDescriptor.PropertyDescriptor.GetValue(
                        _propertyDescriptor.PropertyOwner
                        );

                _lastValue = _lastCommittedValue;

                
                _selectItem( _lastCommittedValue );
                
                Debug.Assert( _comboBox.SelectedIndex >= 0 );
            }
            else
            {
                _comboBox.SelectedIndex = -1;
            }
            _suppressEvents = false;
        }
        #endregion

        #region Event Handlers
        void _comboBox_DrawItem( object sender, DrawItemEventArgs e )
        {
            if ( _comboBox.Items.Count == 0 )
                return;

            if ( ( e.State & DrawItemState.ComboBoxEdit ) == 0 )
            {
                if ( e.Index < 0 )
                    return;

                e.DrawBackground();

                using ( SolidBrush brush = new SolidBrush( e.ForeColor ) )
                {
                    e.Graphics.DrawString( _comboBox.Items[e.Index].ToString(),
                        e.Font, brush, 0, e.Index * _comboBox.ItemHeight );
                }

                e.DrawFocusRectangle();
            }
            else
            {
                if ( _comboBox.SelectedItem == null )
                    return;

                e.DrawBackground();

                using ( SolidBrush brush = new SolidBrush( e.ForeColor ) )
                {
                    e.Graphics.DrawString( _comboBox.SelectedItem.ToString(),
                        e.Font, brush, e.Bounds );
                }

                e.DrawFocusRectangle();
            }
        }

        void _textLabel_MouseEnter( object sender, EventArgs e )
        {
            _textLabel.Visible = false;
        }

        void _comboBox_MouseLeave( object sender, EventArgs e )
        {
            if ( !_comboBox.ContainsFocus )
            {
                _textLabel.Visible = true;
            }
        }

        void _comboBox_DropDownClosed( object sender, EventArgs e )
        {
            _revertValue();
        }

        void _comboBox_Enter( object sender, EventArgs e )
        {
            _textLabel.Visible = false;
        }

        void _comboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if ( _comboBox.SelectedIndex >= 0 )
            {
                _textLabel.Text = _items[_comboBox.SelectedIndex].ToString();

                if ( _lastValue.Equals( _items[_comboBox.SelectedIndex] ) )
                    return;

                if ( !_suppressEvents )
                {
                    _raisePropertyChangingEvent(
                        new PropertyChangeEventArgs(
                            _lastValue,
                            _items[_comboBox.SelectedIndex]
                            )
                        );
                }

                _lastValue = _items[_comboBox.SelectedIndex];
            }
            else
            {
                _textLabel.Text = "";
            }
        }

        void _comboBox_Leave( object sender, EventArgs e )
        {
            if ( !_textLabel.ContainsFocus )
            {
                _textLabel.Visible = true;
            }
        }

        void _comboBox_SelectionChangeCommitted( object sender, EventArgs e )
        {
            _commitValue();
        }

        void _comboBox_KeyDown( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Enter )
            {
                SendKeys.Send( "{Tab}" );
            }
        }
        #endregion

        #region Private Methods
        void _updateItemList( Object sender, EventArgs e )
        {
            Object selectedItem = _comboBox.SelectedIndex >= 0 ?
                _items[_comboBox.SelectedIndex] : null;

            _comboBox.Items.Clear();

            if ( _itemSource != null )
            {
                _items.Clear();
                foreach ( Object item in _itemSource.Items )
                {
                    _items.Add( item );
                }

                if ( _nameProvider != null )
                {
                    foreach ( Object item in _items )
                    {
                        _comboBox.Items.Add( _nameProvider( item ) );
                    }
                }
                else
                {
                    foreach ( Object item in _items )
                    {
                        _comboBox.Items.Add( item.ToString() );
                    }
                }
            }

            if ( selectedItem == null )
                return;

            bool selectedItemContainedNow = false;

            foreach ( Object item in _items )
            {
                if ( item.Equals( selectedItem ) )
                {
                    selectedItemContainedNow = true;
                }
            }

            if ( !selectedItemContainedNow )
            {
                if ( SelectedItemRemovedFromSource != null )
                { 
                    SelectedItemRemovedFromSource( this, 
                        new ItemEventArgs( selectedItem )
                        );
                }

                if ( _comboBox.Items.Count > 0 )
                {
                    _comboBox.SelectedIndex = 0;
                }
                else
                {
                    _comboBox.SelectedIndex = -1;
                }
            }
            else
            {
                _selectItem( selectedItem );
            }
        }

        void _selectItem( Object item )
        {
            int index = _items.FindIndex( delegate( Object o ) { return o.Equals( item ); } );
            _comboBox.SelectedIndex = index;
        }

        void _commitValue()
        {
            if ( !_lastCommittedValue.Equals( _items[_comboBox.SelectedIndex] ) )
            {
                if ( !_suppressEvents )
                {
                    _raisePropertyChangeCommittedEvent(
                        new PropertyChangeEventArgs(
                            _lastCommittedValue,
                            _items[_comboBox.SelectedIndex]
                            )
                        );
                }

                _lastCommittedValue = _items[_comboBox.SelectedIndex];
                _lastValue = _lastCommittedValue;
            }
            else
            {
                if ( !_lastCommittedValue.Equals( _lastValue ) )
                {
                    _revertValue();
                }
            }
        }

        void _revertValue()
        {
            if ( !_lastCommittedValue.Equals( _items[_comboBox.SelectedIndex] ) ||
                 !_lastValue.Equals( _items[_comboBox.SelectedIndex] ) )
            {
                if ( !_suppressEvents )
                {
                    _raisePropertyChangeRevertedEvent(
                        new PropertyChangeRevertedEventArgs( _lastCommittedValue )
                        );
                }

                _lastValue = _lastCommittedValue;

                _selectItem( _lastCommittedValue );
            }
        }
        #endregion
    }

    public class ItemEventArgs : EventArgs
    { 
        public readonly Object Item;

        public ItemEventArgs( Object item )
        {
            Item = item;
        }
    }
}
