#region MIT
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
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

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
        PropertyEditorBase _propertyEditor;
        #endregion

        #region Constructor / Finalizer
        internal PropertyGridItem( Panel editorPanel, Label nameLabel, PropertyEditorBase editor ) 
        {
            Debug.Assert( editorPanel != null );
            Debug.Assert( nameLabel != null );
            Debug.Assert( editor != null );

            _editorPanel = editorPanel;
            _nameLabel = nameLabel;
            _propertyEditor = editor;
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

        public PropertyEditorBase PropertyEditor
        {
            get { return _propertyEditor; }
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
