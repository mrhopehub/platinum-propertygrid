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
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

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
            get { throw new NotImplementedException(); }
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
