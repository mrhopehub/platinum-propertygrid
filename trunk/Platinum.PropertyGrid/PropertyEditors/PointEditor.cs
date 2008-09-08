using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Platinum.PropertyEditors
{
    [IsDefaultPropertyEditorOf( typeof( Point ) )]
    public partial class PointEditor : PropertyEditorBase
    {
        #region Variables
        Point _currentValue;
        Point _lastValue;
        Point _lastCommittedValue;
        BoundPropertyDescriptor _propertyDescriptor;
        IValidator _validator = Validators.DefaultValidator.CreateFor( typeof( int ) );
        bool _xIsValid;
        bool _yIsValid;
        Color _errorForeColor = Color.Black;
        Color _errorBackColor = Color.Salmon;
        bool _suppressEvents;
        #endregion

        #region Properties
        [Browsable( false )]
        public override BoundPropertyDescriptor PropertyDescriptor
        {
            get
            {
                return _propertyDescriptor;
            }
            set
            {
                if ( _propertyDescriptor != value )
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
        public PointEditor()
        {
            InitializeComponent();

            _propertyGridItemAssigned += _pointEditor_propertyGridItemAssigned;
        }
        #endregion

        #region Methods
        public override void RefreshProperty()
        {
            if ( !_propertyDescriptor.IsEmpty )
            {
                _currentValue =
                    (Point) _propertyDescriptor.PropertyDescriptor.GetValue(
                        _propertyDescriptor.PropertyOwner
                    );

                _lastValue = _currentValue;
                _lastCommittedValue = _currentValue;

                _suppressEvents = true;

                _xTextBox.Text = _currentValue.X.ToString();
                _yTextBox.Text = _currentValue.Y.ToString();

                _suppressEvents = false;
            }
            else
            {
                _currentValue = Point.Empty;
                _lastValue = Point.Empty;
                _lastCommittedValue = Point.Empty;

                _xTextBox.Text = "";
                _yTextBox.Text = "";
            }
        }
        #endregion

        #region Event Handlers
        void _pointEditor_propertyGridItemAssigned( PropertyGridItem gridItem )
        {
            gridItem.IsSelectedChanged += _gridItem_IsSelectedChanged;

            BackColor = gridItem.Owner.Owner.SectionBackColor;
        }

        void _gridItem_IsSelectedChanged( object sender, EventArgs e )
        {
            if ( PropertyGridItem.IsSelected )
            {
                if ( !_yTextBox.Focused )
                {
                    _xTextBox.Focus();
                }
            }
        }

        void _xTextBox_TextChanged( object sender, EventArgs e )
        {
            Object value = _validator.ValidateValue( _xTextBox.Text );

            if ( value == null )
            {
                _xIsValid = false;
                _xTextBox.ForeColor = ErrorForeColor;
                _xTextBox.BackColor = ErrorBackColor;
                _xLabel.BackColor = ErrorBackColor;
            }
            else
            {
                _xIsValid = true;
                _xTextBox.ForeColor = Color.FromKnownColor( KnownColor.WindowText );
                _xTextBox.BackColor = Color.FromKnownColor( KnownColor.Window );
                _xLabel.BackColor = Color.FromKnownColor( KnownColor.Window );

                int newX = (int) value;

                if ( newX != _currentValue.X )
                {
                    _lastValue = _currentValue;
                    _currentValue.X = newX;

                    if ( !_suppressEvents )
                    {
                        _raisePropertyChangingEvent(
                            new PropertyChangeEventArgs( _lastValue, _currentValue )
                            );
                    }
                }
            }
        }

        void _yTextBox_TextChanged( object sender, EventArgs e )
        {
            Object value = _validator.ValidateValue( _yTextBox.Text );

            if ( value == null )
            {
                _yIsValid = false;
                _yTextBox.ForeColor = ErrorForeColor;
                _yTextBox.BackColor = ErrorBackColor;
                _yLabel.BackColor = ErrorBackColor;
            }
            else
            {
                _yIsValid = true;
                _yTextBox.ForeColor = Color.FromKnownColor( KnownColor.WindowText );
                _yTextBox.BackColor = Color.FromKnownColor( KnownColor.Window );
                _yLabel.BackColor = Color.FromKnownColor( KnownColor.Window );

                int newY = (int) value;

                if ( newY != _currentValue.Y )
                {
                    _lastValue = _currentValue;
                    _currentValue.Y = newY;

                    if ( !_suppressEvents )
                    {
                        _raisePropertyChangingEvent(
                            new PropertyChangeEventArgs( _lastValue, _currentValue )
                            );
                    }
                }
            }
        }

        void _pointEditor_Leave( object sender, EventArgs e )
        {
            if ( _xIsValid && _yIsValid )
            {
                _commit();
            }
            else
            {
                _revert();
            }
        }

        void _xTextBox_KeyDown( object sender, KeyEventArgs e )
        {
            switch ( e.KeyCode )
            {
                case Keys.Escape:
                    _revert();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;

                case Keys.Enter:
                    if ( _xIsValid && _yIsValid )
                    {
                        _commit();
                        // Go to the next editor
                        SendKeys.Send( "{Tab}" );
                    }
                    else
                    {
                        System.Media.SystemSounds.Beep.Play();
                    }

                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        void _yTextBox_KeyDown( object sender, KeyEventArgs e )
        {
            _xTextBox_KeyDown( sender, e );
        }

        void _xLabel_Click( object sender, EventArgs e )
        {
            _xTextBox.Focus();
            _xTextBox.SelectAll();
        }

        void _yLabel_Click( object sender, EventArgs e )
        {
            _yTextBox.Focus();
            _yTextBox.SelectAll();
        }

        void PointEditor_Click( object sender, EventArgs e )
        {
            _xTextBox.Focus();
            _xTextBox.SelectAll();
        }
        #endregion

        #region Private Methods
        void _commit()
        {
            Debug.Assert( _xIsValid && _yIsValid );

            if ( _currentValue != _lastCommittedValue )
            {
                _raisePropertyChangeCommittedEvent(
                    new PropertyChangeEventArgs( _lastCommittedValue, _currentValue )
                    );

                _lastCommittedValue = _currentValue;
                _lastValue = _currentValue;
            }
        }

        void _revert()
        {
            if ( _currentValue != _lastCommittedValue )
            {
                _raisePropertyChangeRevertedEvent(
                    new PropertyChangeRevertedEventArgs( _lastCommittedValue )
                    );

                _currentValue = _lastCommittedValue;
                _lastValue = _lastCommittedValue;

                _suppressEvents = true;

                _xTextBox.Text = _currentValue.X.ToString();
                _yTextBox.Text = _currentValue.Y.ToString();

                _suppressEvents = false;
            }
        }
        #endregion
    }
}
