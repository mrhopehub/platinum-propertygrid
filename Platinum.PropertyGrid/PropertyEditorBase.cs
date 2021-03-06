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
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace Platinum
{
    public class PropertyEditorBase : UserControl
    {
        #region Variables
        PropertyGridItem _propertyGridItem;
        #endregion

        #region Events
        /// <summary>
        /// This event is raised when the property bound to this editor is
        /// changed without being committed.
        /// </summary>
        /// <remarks>
        /// The event is only raised for valid values. No Undo-Action should be
        /// created as a consequence for this event. It exists to support 
        /// real-time visualization of changes.
        /// </remarks>
        public event EventHandler<PropertyChangeEventArgs> PropertyChanging;

        /// <summary>
        /// This event is fired when a property change should be persisted.
        /// </summary>
        /// <remarks>
        /// This event is fired when the user presses Enter after changing a
        /// property or the editor leaves focus while being set to a valid value.
        /// An Undo-Action should be created as a consequence of this event.
        /// </remarks>
        public event EventHandler<PropertyChangeEventArgs> PropertyChangeCommitted;

        /// <summary>
        /// This event is fired when the property bound to this editor is reverted
        /// to the last committed value.
        /// </summary>
        /// <remarks>
        /// A property is reverted if the user presses escape while editing a value.
        /// No Undo-Action should be created by this event.
        /// 
        /// Todo: Should the value also be reverted when the editor loses focus while
        /// in an invalid state?
        /// </remarks>
        public event EventHandler<PropertyChangeRevertedEventArgs> PropertyChangeReverted;

        /// <summary>
        /// This event is fired when the PropertyDescriptor property was changed.
        /// </summary>
        public event EventHandler<PropertyDescriptorChangedEventArgs> PropertyDescriptorChanged;

        /// <summary>
        /// This event is fired when this PropertyEditor is assigned a 
        /// PropertyGridItem.
        /// </summary>
        /// <remarks>
        /// The PropertyGridItem is assigned to the PropertyEditor when the 
        /// PropertyGridItem is created via PropertyGridSection.Items.Add().
        /// Subscribe to this event in the constructor if you need access to the 
        /// PropertyGridItem as soon as it is assigned.
        /// </remarks>
        protected event Action<PropertyGridItem> _propertyGridItemAssigned;
        #endregion

        #region Properties
        /// <summary>
        /// The Property this editor should edit.
        /// </summary>
        /// <remarks>
        /// Must be overridden by implementers.
        /// </remarks>
        public virtual BoundPropertyDescriptor PropertyDescriptor
        {
            get { return new BoundPropertyDescriptor(); }
            set { throw new NotImplementedException(); }
        }
        
        public PropertyGridItem PropertyGridItem
        {
            get { return _propertyGridItem; }
            internal set
            {
                Debug.Assert( _propertyGridItem == null );
                Debug.Assert( value != null );

                _propertyGridItem = value;

                if ( _propertyGridItemAssigned != null )
                {
                    _propertyGridItemAssigned( value );
                }
            }
        }

        public Color ErrorForeColor
        {
            get
            {
                if ( _propertyGridItem != null )
                {
                    return _propertyGridItem.Owner.Owner.ErrorForeColor;
                }
                else
                {
                    return Color.Black;
                }                
            }
        }

        public Color ErrorBackColor
        {
            get
            {
                if ( _propertyGridItem != null )
                {
                    return _propertyGridItem.Owner.Owner.ErrorBackColor;
                }
                else
                {
                    return Color.Salmon;
                }
            }
        }
        #endregion

        #region Methods
        public virtual void RefreshProperty()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Protected Methods
        protected void _raisePropertyChangingEvent( PropertyChangeEventArgs e )
        {
            if ( PropertyChanging != null )
            {
                PropertyChanging( this, e );
            }
        }

        protected void _raisePropertyChangeCommittedEvent( PropertyChangeEventArgs e )
        {
            if ( PropertyChangeCommitted != null )
            {
                PropertyChangeCommitted( this, e );
            }
        }

        protected void _raisePropertyChangeRevertedEvent( PropertyChangeRevertedEventArgs e )
        {
            if ( PropertyChangeReverted != null )
            {
                PropertyChangeReverted( this, e );
            }
        }

        protected void _raisePropertyDescriptorChangedEvent( 
            PropertyDescriptorChangedEventArgs e )
        {
            if ( PropertyDescriptorChanged != null )
            {
                PropertyDescriptorChanged( this, e );
            }
        }
        #endregion
    }

    public class PropertyChangeEventArgs : EventArgs
    {
        public PropertyChangeEventArgs( Object oldValue, Object newValue )
        {
            NewValue = newValue;
            OldValue = oldValue;
        }

        public readonly Object OldValue;
        public readonly Object NewValue;
    }

    public class PropertyChangeRevertedEventArgs : EventArgs
    {
        public PropertyChangeRevertedEventArgs( Object restoredValue )
        {
            RestoredValue = restoredValue;
        }

        public readonly Object RestoredValue;
    }

    public class PropertyDescriptorChangedEventArgs : EventArgs
    {
        public PropertyDescriptorChangedEventArgs( BoundPropertyDescriptor newDescriptor )
        {
            NewDescriptor = newDescriptor;
        }

        public readonly BoundPropertyDescriptor NewDescriptor;
    }
}
