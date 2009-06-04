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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Platinum.PropertyEditors
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
        bool _suppressEvents;
        #endregion

        #region Constructor
        public ValidatingStringEditor()
        {
            InitializeComponent();

            _propertyGridItemAssigned += _handlePropertyGridItemAssigned;
        }
        #endregion

        #region Properties
        [Browsable( false )]
        public IValidator Validator
        {
            get { return _validator; }
            set
            {
                _validator = value;

                if ( _validator != null )
                {
                    RefreshProperty();
                }
                else
                {
                    _currentValue = null;
                    _lastValue = null;
                    _lastCommittedValue = null;
                    _textBox.Text = String.Empty;
                }
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
                Debug.Assert( _validator != null );

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

        #region Methods
        public override void RefreshProperty()
        {
            if ( !_propertyDescriptor.IsEmpty )
            {
                _currentValue = _propertyDescriptor.PropertyDescriptor.GetValue(
                    _propertyDescriptor.PropertyOwner
                    );

                _suppressEvents = true;
                _textBox.Text = _validator.ConvertTo<String>( _currentValue );
                _suppressEvents = false;

                _lastValue = _currentValue;
                _lastCommittedValue = _currentValue;
            }
            else
            {
                _currentValue = null;
                _lastCommittedValue = null;
                _lastValue = null;
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
                    SendKeys.Send( "{Tab}" );
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
                    _textBox.ForeColor = ErrorForeColor;
                    _textBox.BackColor = ErrorBackColor;
                    BackColor = ErrorBackColor;
                    _isCurrentTextValid = false;
                }
                else
                {
                    _textBox.ForeColor = Color.FromKnownColor( KnownColor.ControlText );
                    _textBox.BackColor = Color.FromKnownColor( KnownColor.Window );
                    BackColor = Color.FromKnownColor( KnownColor.Window );
                    _isCurrentTextValid = true;

                    // Do not raise the CHANGING event if there is no change
                    if ( _valuesEqual( _currentValue, value ) )
                        return;

                    _lastValue = _currentValue;
                    _currentValue = value;

                    if ( !_suppressEvents )
                    {
                        _raisePropertyChangingEvent(
                            new PropertyChangeEventArgs( _lastValue, _currentValue ) 
                            );
                    }
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

            if ( !_valuesEqual( _lastCommittedValue, _currentValue ) )
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
                 !_valuesEqual( _lastCommittedValue, _currentValue ) ||
                 !_valuesEqual( _lastCommittedValue, _lastValue ) )
            {
                PropertyChangeRevertedEventArgs e =
                    new PropertyChangeRevertedEventArgs( _lastCommittedValue );

                _raisePropertyChangeRevertedEvent( e );

                _lastValue = _lastCommittedValue;
                _currentValue = _lastCommittedValue;
                _textBox.Text = _validator.ConvertTo<String>( _lastCommittedValue );
            }
        }

        static bool _valuesEqual( Object value1, Object value2 )
        {
            if ( value1 != null )
            {
                return value2 != null ? value1.Equals( value2 ) : false;
            }
            else
            {
                return value2 == null;
            }
        }
        #endregion
    }
}
