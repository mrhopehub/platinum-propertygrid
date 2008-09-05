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

namespace Platinum
{
    public partial class PropertyGrid : UserControl
    {
        #region SectionCollection struct
        struct SectionCollection : IPropertyGridSectionCollection
        {
            PropertyGrid _owner;

            public SectionCollection( PropertyGrid owner )
            {
                _owner = owner;
            }

            public IPropertyGridSection this[string name]
            {
                get 
                { 
                    return _owner._sections.SingleOrDefault( x => x.SectionName == name ); 
                }
            }

            public IPropertyGridSection this[int index]
            {
                get 
                { 
                    return _owner._sections[index];
                }
            }

            public IPropertyGridSection Add( String name )
            {
                return _owner._addSection( name );
            }

            public void Remove( String name )
            {
                _owner._removeSection( name );
            }

            public void Clear()
            {
                _owner._clearSections();
            }

            public int Count
            {
                get { return _owner._sections.Count; }
            }
            
            public IEnumerator<IPropertyGridSection> GetEnumerator()
            {
                return _owner._sections.Cast<IPropertyGridSection>().GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return _owner._sections.GetEnumerator();
            }
        }
        #endregion

        #region Variables
        List<PropertyGridSection> _sections = new List<PropertyGridSection>();
        Color _sectionBackColor = Color.FromArgb( 233, 236, 250 );
        bool _updatingSelection;
        #endregion

        #region Events
        public event EventHandler<ColorChangedEventArgs> SectionBackColorChanged;
        #endregion

        #region Properties
        [Browsable( true )]
        [Description("The background color of all contained sections.")]
        [Category("Appearance")]
        public Color SectionBackColor
        {
            get { return _sectionBackColor; }
            set 
            {
                Color oldColor = _sectionBackColor;
                _sectionBackColor = value;

                if ( SectionBackColorChanged != null )
                {
                    ColorChangedEventArgs e = new ColorChangedEventArgs( oldColor, value );
                    SectionBackColorChanged( this, e );
                }
            }
        }

        [Browsable( false )]
        public IPropertyGridSectionCollection Items
        {
            get { return new SectionCollection( this ); }
        }
        #endregion

        #region Constructor
        public PropertyGrid()
        {
            InitializeComponent();

            SetStyle( ControlStyles.UserPaint, true );
            SetStyle( ControlStyles.AllPaintingInWmPaint, true );
            SetStyle( ControlStyles.OptimizedDoubleBuffer, true );
        }
        #endregion
        
        #region EventHandler
        void _section_SizeChanged( object sender, EventArgs e )
        {
            _updateSectionPositions();
        }

        void _section_SelectedItemChanged( object sender, EventArgs e )
        {
            if ( !_updatingSelection )
            {
                _updatingSelection = true;

                _helpTextTitleLabel.Text = "";
                _helpTextLabel.Text = "";

                PropertyGridSection selectedSection = (PropertyGridSection) sender;

                foreach ( PropertyGridSection section in _sections )
                {
                    if ( selectedSection != section )
                    {
                        section.ResetSelectedItem();
                    }
                    else
                    {
                        PropertyGridItem selectedItem = section.SelectedItem;

                        if ( selectedItem != null && selectedItem.Description != null )
                        {
                            _helpTextTitleLabel.Text = selectedItem.Name;
                            _helpTextLabel.Text = selectedItem.Description;
                        }
                    }
                }

                _updatingSelection = false;
            }
        }

        void section_ExpandStateChanged( object sender, EventArgs e )
        {
            _updateSectionPositions();
        }

        void _section_SplitterMoving( object sender, SplitterCancelEventArgs e )
        {
            foreach ( PropertyGridSection section in _sections )
            {
                section.SplitterDistance = e.SplitX;
            }
        }

        void _splitContainer_Panel1_SizeChanged( object sender, EventArgs e )
        {
            _updateSectionPositions();
        }

        void _sectionPanelScrollBar_Scroll( object sender, ScrollEventArgs e )
        {
            float value = e.NewValue;
            int maxValue = _sectionPanelScrollBar.Maximum - _sectionPanelScrollBar.LargeChange + 1;

            value /= ( maxValue - _sectionPanelScrollBar.Minimum );
            value *= -( _sectionPanel2.Height - _splitContainer.Panel1.ClientSize.Height );

            _sectionPanel2.Top = ( (int) value ) - 3;
        }
        #endregion

        #region Private Methods
        PropertyGridSection _addSection( String sectionName )
        {
            Debug.Assert( !_sections.Any( x => x.SectionName == sectionName ) );

            PropertyGridSection section = new PropertyGridSection();
            section.SectionName = sectionName;

            int y = _getSectionsHeight();

            _sections.Add( section );

            _sectionPanel2.Controls.Add( section );

            section.Top = y;
            section.Left = 0;
            section.Width = _sectionPanel2.Width;
            section.Anchor = AnchorStyles.None;
            section.SizeChanged += new EventHandler( _section_SizeChanged );
            section.SplitterMoving += new SplitterCancelEventHandler( _section_SplitterMoving );
            section.SelectedItemChanged += new EventHandler( _section_SelectedItemChanged ); 
            section.Owner = this;
            section.ExpandStateChanged += new EventHandler( section_ExpandStateChanged );
            
            _updateSectionPositions();

            return section;
        }

        void _removeSection( String name )
        {
            PropertyGridSection section =
                _sections.SingleOrDefault( x => x.SectionName == name );

            Debug.Assert( _sections.Contains( section ) );

            section.SizeChanged -= _section_SizeChanged;
            _sections.Remove( section );

            section.Dispose();

            _updateSectionPositions();
        }

        void _clearSections()
        {
            while ( _sections.Any() )
            {
                _removeSection( _sections.Last().SectionName );
            }
        }

        int _getSectionsHeight()
        {
            return _sections.Sum( x => x.Height );
        }

        void _updateSectionPositions()
        {
            int height = _sections.Sum( x => x.Height );

            if ( _sectionPanel2.Height != height )
            {
                _sectionPanel2.Height = height;
            }
            
            if ( _sectionPanel2.Height > _splitContainer.Panel1.Height )
            {
                if ( !_sectionPanelScrollBar.Visible )
                {
                    _sectionPanelScrollBar.Visible = true;
                    _sectionPanelScrollBar.Enabled = true;
                }

                if ( _sectionPanel2.Width != _splitContainer.Panel1.Width - SystemInformation.VerticalScrollBarWidth )
                {
                    _sectionPanel2.Width = _splitContainer.Panel1.Width - SystemInformation.VerticalScrollBarWidth;
                }
            }
            else
            {
                if ( _sectionPanelScrollBar.Visible )
                {
                    _sectionPanelScrollBar.Visible = false;
                    _sectionPanelScrollBar.Enabled = false;
                }

                if ( _sectionPanel2.Width != _splitContainer.Panel1.Width )
                {
                    _sectionPanel2.Width = _splitContainer.Panel1.Width;
                }
            }

            if ( _sectionPanel2.Left != 0 )
            {
                _sectionPanel2.Left = 0;
            }

            if ( _sectionPanel2.Top != 0 )
            {
                _sectionPanel2.Top = 0;
            }

            foreach ( var section in _sections )
            {
                if ( section.Width != _sectionPanel2.Width )
                {
                    section.Width = _sectionPanel2.Width;
                }
            }

            int y = 0;

            foreach ( PropertyGridSection section in _sections )
            {
                if ( section.Left != 0 )
                {
                    section.Left = 0;
                }

                if ( section.Top != y )
                {
                    section.Top = y;
                }

                y += section.Height;
            }
        }
        #endregion
    }

    public class ColorChangedEventArgs : EventArgs
    {
        public readonly Color OldColor;
        public readonly Color NewColor;

        public ColorChangedEventArgs( Color oldColor, Color newColor )
        {
            OldColor = oldColor;
            NewColor = newColor;
        }
    }
}
