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
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace Platinum
{
    public class PropertyGridItem : IDisposable
    {
        #region Variables
        bool _disposed;
        bool _isSelected;
        Panel _editorPanel;
        Label _nameLabel;
        String _description;
        IPropertyGridSection _owner;
        #endregion

        #region Constructor / Finalizer
        internal PropertyGridItem( Panel editorPanel, Label nameLabel ) 
        {
            _editorPanel = editorPanel;
            _nameLabel = nameLabel;
        }

        ~PropertyGridItem()
        {
            Dispose( false );
        }
        #endregion

        #region Events
        public event EventHandler ItemDisposed;
        public event EventHandler IsSelectedChanged;
        #endregion

        #region Properties
        public String Name
        {
            get { return _nameLabel.Text; }
            set { _nameLabel.Text = value; }
        }

        public Panel EditorPanel
        {
            get { return _editorPanel; }
        }

        public String Description
        {
            get { return _description; }
            internal set { _description = value; }
        }

        public IPropertyGridSection Owner
        {
            get { return _owner; }
            internal set { _owner = value; }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            internal set
            {
                if ( _isSelected != value )
                {
                    _isSelected = value;

                    if ( IsSelectedChanged != null )
                    { 
                        IsSelectedChanged( this, EventArgs.Empty );
                    }
                }
            }
        }
        #endregion

        #region Methods
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected void Dispose( bool disposing )
        {
            if ( !_disposed )
            {
                if ( disposing )
                {
                    if ( ItemDisposed != null )
                    {
                        ItemDisposed( this, new EventArgs() );
                        ItemDisposed = null;
                    }
                }

                _disposed = true;
            }
        }
        #endregion
    }
}
