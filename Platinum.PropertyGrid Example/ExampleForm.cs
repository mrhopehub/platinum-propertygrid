using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Platinum.PropertyGrid_Example
{
    public partial class ExampleForm : Form
    {
        enum ExampleEnum
        { 
            ENUM_VALUE_ONE,
            ENUM_VALUE_TWO,
            ENUM_VALUE_THREE,
            ENUM_VALUE_FOUR
        }

        class ExampleObjectClass
        {
            ExampleForm _form;

            String _textValue = "Change me!";
            ExampleEnum _enumValue;
            int _intValue;
            
            public ExampleObjectClass( ExampleForm form  )
            {
                _form = form;

                Text = Text;
                EnumValue = EnumValue;
                Integer = Integer;
            }

            [Category("TextCategory")]
            [Description("This is a changeable string-property")]
            public String Text
            {
                get { return _textValue; }
                set 
                {
                    _textValue = value;
                    _form._textLabel.Text = value; 
                }
            }

            [Description("Enums are displayed in dropdown combobox")]
            public ExampleEnum EnumValue
            {
                get { return _enumValue; }
                set
                {
                    _enumValue = value;
                    _form._enumLabel.Text = _enumValue.ToString();
                }
            }

            [Category("Some Category")]
            [Description("Integers are an example of types that are validated in real-time by default editor.")]
            public int Integer
            {
                get { return _intValue; }
                set
                {
                    _intValue = value;
                    _form._integerLabel.Text = value.ToString();
                }
            }


            public bool Boolean
            {
                get;
                set;
            }
        }

        public ExampleForm()
        {
            InitializeComponent();
        }

        private void ExampleForm_Load( object sender, EventArgs e )
        {
            _objectAdapter.SelectedObject = new ExampleObjectClass( this );
        }
    }
}
