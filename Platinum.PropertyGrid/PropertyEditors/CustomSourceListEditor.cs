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
using System.Diagnostics;
using System.Collections;

namespace Platinum.PropertyEditors
{
    public partial class CustomSourceListEditor : PropertyEditorBase
    {
        #region IItemSource interface
        public interface IItemSource
        {
            event Action SourceChanged;
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

                _updateItemList();

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
                _updateItemList();
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
            _textLabel.Visible = true;
        }

        void _comboBox_DropDownClosed( object sender, EventArgs e )
        {
            _revertValue();
            _textLabel.Visible = true;
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
            _textLabel.Visible = true;
        }

        void _comboBox_SelectionChangeCommitted( object sender, EventArgs e )
        {
            _commitValue();
        }
        #endregion

        #region Private Methods
        void _updateItemList()
        {
            Object selectedItem = _comboBox.SelectedIndex >= 0 ?
                _items[_comboBox.SelectedIndex] : null;

            _comboBox.Items.Clear();

            if ( _itemSource != null )
            {
                _items.Clear();
                _items.AddRange( _itemSource.Items.Cast<Object>() );

                if ( _nameProvider != null )
                {
                    _comboBox.Items.AddRange( _items.Select( x => _nameProvider( x ) ).ToArray() );
                }
                else
                {
                    _comboBox.Items.AddRange( _items.Select( x => x.ToString() ).ToArray() );
                }
            }

            if ( selectedItem != null && !_items.Any( x => x.Equals( selectedItem ) ) )
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
            int index = _items.FindIndex( x => x.Equals( item ) );
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
