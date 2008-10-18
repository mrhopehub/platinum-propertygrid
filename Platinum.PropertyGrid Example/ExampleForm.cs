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
using System.Data;
using System.Drawing;
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

            public System.Drawing.Point APoint
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
