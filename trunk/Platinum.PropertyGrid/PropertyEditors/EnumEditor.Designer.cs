#region MIT
/*
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

namespace Platinum.PropertyEditors
{
    partial class EnumEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._customSourceListEditor = new Platinum.PropertyEditors.CustomSourceListEditor();
            this.SuspendLayout();
            // 
            // _customSourceListEditor
            // 
            this._customSourceListEditor.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this._customSourceListEditor.Font = new System.Drawing.Font( "Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this._customSourceListEditor.ItemNameProvider = null;
            this._customSourceListEditor.ItemSource = null;
            this._customSourceListEditor.Location = new System.Drawing.Point( 0, 0 );
            this._customSourceListEditor.Name = "_customSourceListEditor";
            this._customSourceListEditor.Size = new System.Drawing.Size( 256, 19 );
            this._customSourceListEditor.TabIndex = 0;
            this._customSourceListEditor.PropertyChanging += new System.EventHandler<Platinum.PropertyChangeEventArgs>( this._customSourceListEditor_PropertyChanging );
            this._customSourceListEditor.PropertyChangeCommitted += new System.EventHandler<Platinum.PropertyChangeEventArgs>( this._customSourceListEditor_PropertyChangeCommitted );
            this._customSourceListEditor.PropertyDescriptorChanged += new System.EventHandler<Platinum.PropertyDescriptorChangedEventArgs>( this._customSourceListEditor_PropertyDescriptorChanged );
            this._customSourceListEditor.PropertyChangeReverted += new System.EventHandler<Platinum.PropertyChangeRevertedEventArgs>( this._customSourceListEditor_PropertyChangeReverted );
            // 
            // EnumEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this._customSourceListEditor );
            this.Name = "EnumEditor";
            this.Size = new System.Drawing.Size( 256, 19 );
            this.ResumeLayout( false );

        }

        #endregion

        private CustomSourceListEditor _customSourceListEditor;
    }
}
