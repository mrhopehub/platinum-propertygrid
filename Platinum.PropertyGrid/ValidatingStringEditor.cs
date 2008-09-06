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
    public partial class ValidatingStringEditor : PropertyEditorBase
    {
        #region Variables
        IValidator _validator;
        BoundPropertyDescriptor _propertyDescriptor;
        bool _isCurrentTextValid;
        Object _lastValue;
        Object _currentValue;
        Object _lastCommittedValue;
        #endregion

        #region Constructor
        public ValidatingStringEditor()
        {
            InitializeComponent();

            _propertyGridItemAssigned += _handlePropertyGridItemAssigned;
        }
        #endregion

        #region Properties
        public IValidator Validator
        {
            get { return _validator; }
            set 
            {
                Debug.Assert( _propertyDescriptor.IsEmpty,
                    "PropertyDescriptor must not be set when the validator is changed." 
                    );

                _validator = value;
                _currentValue = _validator.DefaultValue;
                _lastValue = _currentValue;
                _lastCommittedValue = _currentValue;
                _textBox.Text = _validator.ConvertTo<String>( _currentValue );
            }
        }

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

                if ( !_propertyDescriptor.IsEmpty )
                {
                    _currentValue = _propertyDescriptor.PropertyDescriptor.GetValue(
                        _propertyDescriptor.PropertyOwner
                        );

                    _lastValue = _currentValue;
                    _lastCommittedValue = _currentValue;
                    _textBox.Text = _validator.ConvertTo<String>( _currentValue );
                }
            }
        }
        #endregion

        #region Event Handlers
        void _textBox_KeyDown( object sender, KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return )
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                if ( _isCurrentTextValid )
                {
                    _commit();
                }
                else
                {
                    System.Media.SystemSounds.Beep.Play();
                }
            }
            else if ( e.KeyCode == Keys.Escape )
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                _revert();
            }
            else
            {
                e.Handled = false;
            }
        }

        void _textBox_TextChanged( object sender, EventArgs e )
        {
            if ( _validator != null )
            {
                Object value = _validator.ValidateValue( _textBox.Text );

                if ( value == null )
                {
                    _textBox.ForeColor = ValidatorInvalidColor.ForeGround;
                    _textBox.BackColor = ValidatorInvalidColor.BackGround;
                    BackColor = ValidatorInvalidColor.BackGround;
                    _isCurrentTextValid = false;
                }
                else
                {
                    _textBox.ForeColor = Color.FromKnownColor( KnownColor.ControlText );
                    _textBox.BackColor = Color.FromKnownColor( KnownColor.Window );
                    BackColor = Color.FromKnownColor( KnownColor.Window );
                    _isCurrentTextValid = true;

                    // Do not raise the CHANGE event if there is no change
                    if ( _currentValue.Equals( value ) )
                        return;

                    _lastValue = _currentValue;
                    _currentValue = value;

                    PropertyChangeEventArgs args =
                            new PropertyChangeEventArgs( _lastValue, _currentValue );

                    _raisePropertyChangingEvent( args );
                }
            }
        }

        void _textBox_Leave( object sender, EventArgs e )
        {
            if ( _isCurrentTextValid )
            {
                _commit();
            }
            else 
            {
                _revert();
            }
        }

        void _handlePropertyGridItemAssigned( PropertyGridItem item )
        {
            item.IsSelectedChanged += new EventHandler( _item_IsSelectedChanged );
        }

        void _item_IsSelectedChanged( object sender, EventArgs e )
        {
            if ( PropertyGridItem.IsSelected )
            {
                _textBox.Focus();
            }
        }
        #endregion

        #region Private Methods

        void _commit()
        {
            Debug.Assert( _isCurrentTextValid );

            if ( !_lastCommittedValue.Equals( _currentValue ) )
            {
                PropertyChangeEventArgs e =
                        new PropertyChangeEventArgs( _lastCommittedValue, _currentValue );

                _raisePropertyChangeCommittedEvent( e );

                _lastCommittedValue = _currentValue;
                _lastValue = _currentValue;
            }
        }

        void _revert()
        {
            if ( !_isCurrentTextValid ||
                 !_lastCommittedValue.Equals( _currentValue ) ||
                 !_lastCommittedValue.Equals( _lastValue ) )
            {
                PropertyChangeRevertedEventArgs e =
                    new PropertyChangeRevertedEventArgs( _lastCommittedValue );

                _raisePropertyChangeRevertedEvent( e );

                _lastValue = _lastCommittedValue;
                _currentValue = _lastCommittedValue;
                _textBox.Text = _validator.ConvertTo<String>( _lastCommittedValue );
            }
        }

        #endregion
    }
}
