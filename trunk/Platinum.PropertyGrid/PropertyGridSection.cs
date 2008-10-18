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

namespace Platinum
{
    internal partial class PropertyGridSection : UserControl, IPropertyGridSection
    {
        const int DEFAULT_ITEM_HEIGHT = 20;

        #region ItemEx class
        struct ItemEx
        {
            public ItemEx( PropertyGridItem item, Label nameLabel )
            {
                Item = item;
                NameLabel = nameLabel;
            }

            public readonly PropertyGridItem Item;
            public readonly Label NameLabel;
        }
        #endregion

        #region ItemCollection struct
        struct ItemCollection : IPropertyGridItemCollection
        {
            PropertyGridSection _owner;

            public ItemCollection( PropertyGridSection owner )
            {
                _owner = owner;
            }

            public PropertyGridItem this[string name]
            {
                get 
                { 
                    int index = _owner._items.FindIndex( 
                        delegate( ItemEx item )
                        {
                            return item.Item.Name == name;
                        } 
                    );

                    return index >= 0 ? _owner._items[index].Item : null;
                }
            }

            public PropertyGridItem this[int index]
            {
                get { return _owner._items[index].Item; }
            }

            public PropertyGridItem Add( String name, PropertyEditorBase propertyEditor )
            {
                return _owner._addItem( name, propertyEditor );
            }

            public void Remove( String name )
            {
                PropertyGridItem item = this[name];
                _owner._removeItem( item );
            }

            public void Clear()
            {
                _owner._clearItems();
            }

            public IEnumerator<PropertyGridItem> GetEnumerator()
            {
                foreach ( ItemEx itemEx in _owner._items )
                {
                    yield return itemEx.Item;
                }
            }
            
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                foreach ( ItemEx itemEx in _owner._items )
                {
                    yield return itemEx.Item;
                }
            }

            public int Count
            {
                get { return _owner._items.Count; }
            }
        }
        #endregion

        #region Variables
        bool _isExpanded = true;
        List<ItemEx> _items = new List<ItemEx>();
        PropertyGridItem _selectedItem;
        bool _splitterMoving;
        PropertyGrid _owner;
        bool _updatingSelectedItem;
        #endregion

        #region Events
        internal event SplitterCancelEventHandler SplitterMoving;
        internal event EventHandler SelectedItemChanged;
        internal event EventHandler ExpandStateChanged;
        #endregion

        #region Constructor
        internal PropertyGridSection()
        {
            InitializeComponent();

            Disposed += delegate { if ( Disposing ) _clearItems(); };
        }
        #endregion

        #region Properties
        public String SectionName
        {
            get { return _titelLabel.Text; }
            set { _titelLabel.Text = value; }
        }

        public int SplitterDistance
        {
            get { return _splitContainer.SplitterDistance; }
            set 
            {
                if ( !_splitterMoving )
                {
                    _splitContainer.SplitterDistance = value;
                }
            }
        }

        public IPropertyGridItemCollection Items
        {
            get { return new ItemCollection( this ); }
        }

        public PropertyGrid Owner
        {
            get { return _owner; }
            set
            {
                if ( _owner != null )
                {
                    _owner.SectionBackColorChanged -= _owner_SectionBackColorChanged;
                }

                _owner = value;

                if ( _owner != null )
                {
                    _owner.SectionBackColorChanged += _owner_SectionBackColorChanged;
                }

                _updateSectionBackColor();
            }
        }

        internal PropertyGridItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            private set 
            {
                if ( value != _selectedItem && !_updatingSelectedItem )
                {
                    _updatingSelectedItem = true;
                    Debug.Assert( value == null || 
                        _items.FindAll( 
                            delegate( ItemEx item )
                            {
                                return item.Item == value;
                            } 
                            ).Count == 1 
                        );
                        
                    if ( _selectedItem != null )
                    {
                        _selectedItem.IsSelected = false;
                    }

                    _selectedItem = value;

                    if ( _selectedItem != null )
                    {
                        _selectedItem.IsSelected = true;
                    }

                    _updateLabelSelection();

                    if ( SelectedItemChanged != null )
                    {
                        SelectedItemChanged( this, EventArgs.Empty );
                    }
                    _updatingSelectedItem = false;
                }
            }
        }
        #endregion

        #region Internal Methods
        internal void ResetSelectedItem()
        {
            SelectedItem = null;
        }
        #endregion

        #region EventHandler
        void _expandButton_Click( object sender, EventArgs e )
        {
            _isExpanded = !_isExpanded;

            _updateExpandState();

            if ( ExpandStateChanged != null )
            {
                ExpandStateChanged( this, EventArgs.Empty );
            }

            SelectedItem = null;
        }

        void _nameTextBox_Click( object sender, EventArgs e )
        {
            PropertyGridItem item =
                _items.Find(
                    delegate( ItemEx itemEx )
                    {
                        return itemEx.NameLabel == sender;
                    }
                    ).Item;

            SelectedItem = item;
        }

        void _titelLabel_Click( object sender, EventArgs e )
        {
            SelectedItem = null;
        }

        void _splitContainer_SplitterMoving( object sender, SplitterCancelEventArgs e )
        {
            _splitterMoving = true;

            if ( SplitterMoving != null )
            {
                SplitterMoving( sender, e );
            }
        }

        void _splitContainer_SplitterMoved( object sender, SplitterEventArgs e )
        {
            _splitterMoving = false;
        }

        void _owner_SectionBackColorChanged( object sender, ColorChangedEventArgs e )
        {
            _updateSectionBackColor();
        }

        void _editorPanel_SizeChanged( object sender, EventArgs e )
        {
            _updatePositions();
            _updateExpandState();
        }
        #endregion

        #region Private Methods
        PropertyGridItem _addItem( String name, PropertyEditorBase propertyEditor )
        {
            Debug.Assert( Items[Name] == null );

            int y = _calculateItemHeight();

            Label nameLabel = new Label();
            _splitContainer.Panel1.Controls.Add( nameLabel );

            nameLabel.AutoSize = false;
            nameLabel.Location = new Point( 0, y );
            nameLabel.Size = new Size( _splitContainer.Panel1.Width, DEFAULT_ITEM_HEIGHT - 1 );
            nameLabel.Text = name;
            nameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            nameLabel.BackColor = Color.White;
            nameLabel.TextAlign = ContentAlignment.MiddleLeft;
            nameLabel.Click += new EventHandler( _nameTextBox_Click );
            nameLabel.Font = new Font( "Segoe UI", 8.25f, FontStyle.Regular );
            nameLabel.UseCompatibleTextRendering = true;

            ToolTip nameLabelToolTip = new ToolTip();
            nameLabelToolTip.SetToolTip( nameLabel, nameLabel.Text );

            Panel editorPanel = new Panel();
            _splitContainer.Panel2.Controls.Add( editorPanel );

            editorPanel.Location = new Point( 0, y );
            editorPanel.Size = new Size( _splitContainer.Panel2.ClientSize.Width, DEFAULT_ITEM_HEIGHT - 1 );
            editorPanel.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            editorPanel.AutoScroll = false;
            editorPanel.BackColor = Color.White;
            editorPanel.SizeChanged += new EventHandler( _editorPanel_SizeChanged );

            PropertyGridItem item = new PropertyGridItem( editorPanel, nameLabel );

            item.Owner = this;
            propertyEditor.PropertyGridItem = item;

            propertyEditor.Enter += delegate( Object sender, EventArgs e )
            {
                SelectedItem = item;
            };

            propertyEditor.Leave += delegate( Object sender, EventArgs e )
            {
                SelectedItem = null;
            };

            _items.Add( new ItemEx( item, nameLabel ) );

            _updateExpandState();

            return item;
        }

        void _removeItem( PropertyGridItem item )
        {
            Debug.Assert( Items[item.Name] != null );

            int index = _items.FindIndex( 
                delegate( ItemEx itemEx ) { return item == itemEx.Item; }
                );

            _splitContainer.Panel1.Controls.Remove( _items[index].NameLabel );
            _items[index].NameLabel.Dispose();

            _splitContainer.Panel2.Controls.Remove( item.EditorPanel );
            item.EditorPanel.Dispose();
            item.Dispose();

            _items.RemoveAt( index );

            _updatePositions();
            _updateExpandState();

            if ( item == SelectedItem )
            {
                SelectedItem = null;
            }
        }

        void _clearItems()
        {
            while ( _items.Count != 0 )
            {
                _removeItem( _items[_items.Count-1].Item );
            }
        }

        void _updateExpandState()
        {
            if ( _isExpanded )
            {
                Height = _calculateExpandedHeight();
                _expandButton.Image = Platinum.Properties.Resources.minus;
            }
            else
            {
                Height = _titelLabel.Height;
                _expandButton.Image = Platinum.Properties.Resources.plus;
            }
        }

        int _calculateExpandedHeight()
        {
            return _titelLabel.Height + _calculateItemHeight();
        }

        int _calculateItemHeight()
        {
            int h = 0;

            foreach ( ItemEx item in _items )
            {
                h += item.Item.EditorPanel.Height + 1;
            }

            return h;
        }

        void _updateLabelSelection()
        {
            foreach ( ItemEx item in _items )
            {
                if ( item.Item == _selectedItem )
                {
                    item.NameLabel.BackColor = Color.FromKnownColor( KnownColor.MenuHighlight );
                    item.NameLabel.ForeColor = Color.FromKnownColor( KnownColor.HighlightText );
                }
                else
                {
                    item.NameLabel.BackColor = Color.White;
                    item.NameLabel.ForeColor = Color.Black;
                }
            }
        }

        void _updatePositions()
        {
            int y = 0;

            foreach ( ItemEx item in _items )
            {
                item.NameLabel.Top = y;
                item.NameLabel.Height = item.Item.EditorPanel.Height;
                item.Item.EditorPanel.Top = y;

                y += item.Item.EditorPanel.Height + 1;
            }
        }

        void _updateSectionBackColor()
        {
            if ( _owner != null )
            {
                _sidePanel.BackColor = _owner.SectionBackColor;
                _titelLabel.BackColor = _owner.SectionBackColor;
                _splitContainer.BackColor = _owner.SectionBackColor;
                _splitContainer.Panel1.BackColor = _owner.SectionBackColor;
                _splitContainer.Panel2.BackColor = _owner.SectionBackColor;
            }
        }
        #endregion
    }
}
