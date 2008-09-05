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

namespace Platinum
{
    public partial class ValidatingStringEditor : PropertyEditorBase
    {
        #region Variables
        IValidator _validator;
        Panel _editorPanel;
        BoundPropertyDescriptor _propertyDescriptor;
        bool _isValueValid;
        Object _oldValue;
        Object _newValue;
        #endregion

        public ValidatingStringEditor()
        {
            InitializeComponent();
        }

        public IValidator Validator
        {
            get { return _validator; }
            set 
            { 
                _validator = value;
                _oldValue = _validator.DefaultValue;
                _newValue = _oldValue;
            }
        }

        public Panel EditorPanel
        {
            get { return _editorPanel; }
            set { _editorPanel = value; }
        }

        private void _textBox_TextChanged( object sender, EventArgs e )
        {
            if ( _validator != null )
            {
                Object value = _validator.ValidateValue( _textBox.Text );

                if ( value == null )
                {
                    _textBox.ForeColor = ValidatorInvalidColor.ForeGround;
                    _textBox.BackColor = ValidatorInvalidColor.BackGround;
                    BackColor = ValidatorInvalidColor.BackGround;
                    _isValueValid = false;
                }
                else
                {
                    _textBox.ForeColor = Color.FromKnownColor( KnownColor.ControlText );
                    _textBox.BackColor = Color.FromKnownColor( KnownColor.Window );
                    BackColor = Color.FromKnownColor( KnownColor.Window );
                    _isValueValid = true;

                    _oldValue = _newValue;
                    _newValue = value;

                    _raisePropertyChangedEvent( _oldValue, _newValue );
                }
            }
        }

        #region IPropertyEditor Members

        public override BoundPropertyDescriptor PropertyDescriptor
        {
            get
            {
                return _propertyDescriptor;
            }
            set
            {
                if ( value == _propertyDescriptor )
                    return;
                
                _propertyDescriptor = value;

                _raisePropertyDescriptorChangedEvent(
                    new PropertyDescriptorChangedEventArgs( value ) 
                    );
            }
        }

        #endregion

        #region Private Methods
        void _raisePropertyChangedEvent( Object oldValue, Object newValue )
        {
            PropertyChangeEventArgs e = 
                    new PropertyChangeEventArgs( oldValue, newValue );

            _raisePropertyChangingEvent( e );
        }

        void _raisePropertyChangeCommittedEvent( Object oldValue, Object newValue )
        {
            PropertyChangeEventArgs e =
                    new PropertyChangeEventArgs( oldValue, newValue );

            _raisePropertyChangeCommittedEvent( e );
        }

        void _raisePropertyChangeRevertedEvent( Object restoredValue )
        {
            PropertyChangeRevertedEventArgs e =
                new PropertyChangeRevertedEventArgs( restoredValue );

            _raisePropertyChangeRevertedEvent( e );
        }

        #endregion

        private void _textBox_KeyPress( object sender, KeyPressEventArgs e )
        {
            if ( _isValueValid )
            {
                _raisePropertyChangeCommittedEvent( _oldValue, _newValue );
            }
            else
            {
                _raisePropertyChangeRevertedEvent( _newValue );
            }
        }
    }
}
