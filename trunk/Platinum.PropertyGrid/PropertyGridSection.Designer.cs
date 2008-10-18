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

namespace Platinum
{
    partial class PropertyGridSection
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
            this._titelLabel = new System.Windows.Forms.Label();
            this._sidePanel = new System.Windows.Forms.Panel();
            this._expandButton = new System.Windows.Forms.Button();
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._sidePanel.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // _titelLabel
            // 
            this._titelLabel.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this._titelLabel.BackColor = System.Drawing.Color.FromArgb( ( (int) ( ( (byte) ( 233 ) ) ) ), ( (int) ( ( (byte) ( 236 ) ) ) ), ( (int) ( ( (byte) ( 250 ) ) ) ) );
            this._titelLabel.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this._titelLabel.ForeColor = System.Drawing.Color.FromArgb( ( (int) ( ( (byte) ( 173 ) ) ) ), ( (int) ( ( (byte) ( 160 ) ) ) ), ( (int) ( ( (byte) ( 160 ) ) ) ) );
            this._titelLabel.Location = new System.Drawing.Point( 19, 0 );
            this._titelLabel.Name = "_titelLabel";
            this._titelLabel.Size = new System.Drawing.Size( 344, 19 );
            this._titelLabel.TabIndex = 0;
            this._titelLabel.Text = "Titel";
            this._titelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._titelLabel.Click += new System.EventHandler( this._titelLabel_Click );
            // 
            // _sidePanel
            // 
            this._sidePanel.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left ) ) );
            this._sidePanel.BackColor = System.Drawing.Color.FromArgb( ( (int) ( ( (byte) ( 233 ) ) ) ), ( (int) ( ( (byte) ( 236 ) ) ) ), ( (int) ( ( (byte) ( 250 ) ) ) ) );
            this._sidePanel.Controls.Add( this._expandButton );
            this._sidePanel.Location = new System.Drawing.Point( 0, 0 );
            this._sidePanel.Name = "_sidePanel";
            this._sidePanel.Size = new System.Drawing.Size( 19, 399 );
            this._sidePanel.TabIndex = 1;
            // 
            // _expandButton
            // 
            this._expandButton.FlatAppearance.BorderSize = 0;
            this._expandButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb( ( (int) ( ( (byte) ( 233 ) ) ) ), ( (int) ( ( (byte) ( 236 ) ) ) ), ( (int) ( ( (byte) ( 250 ) ) ) ) );
            this._expandButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb( ( (int) ( ( (byte) ( 233 ) ) ) ), ( (int) ( ( (byte) ( 236 ) ) ) ), ( (int) ( ( (byte) ( 250 ) ) ) ) );
            this._expandButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._expandButton.Image = global::Platinum.Properties.Resources.minus;
            this._expandButton.Location = new System.Drawing.Point( 0, 0 );
            this._expandButton.Name = "_expandButton";
            this._expandButton.Size = new System.Drawing.Size( 19, 19 );
            this._expandButton.TabIndex = 0;
            this._expandButton.TabStop = false;
            this._expandButton.UseVisualStyleBackColor = true;
            this._expandButton.Click += new System.EventHandler( this._expandButton_Click );
            // 
            // _splitContainer
            // 
            this._splitContainer.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this._splitContainer.BackColor = System.Drawing.Color.FromArgb( ( (int) ( ( (byte) ( 233 ) ) ) ), ( (int) ( ( (byte) ( 236 ) ) ) ), ( (int) ( ( (byte) ( 250 ) ) ) ) );
            this._splitContainer.Location = new System.Drawing.Point( 19, 19 );
            this._splitContainer.Name = "_splitContainer";
            this._splitContainer.Size = new System.Drawing.Size( 344, 380 );
            this._splitContainer.SplitterDistance = 172;
            this._splitContainer.SplitterWidth = 2;
            this._splitContainer.TabIndex = 2;
            this._splitContainer.TabStop = false;
            this._splitContainer.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler( this._splitContainer_SplitterMoving );
            this._splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler( this._splitContainer_SplitterMoved );
            // 
            // PropertyGridSection
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add( this._splitContainer );
            this.Controls.Add( this._sidePanel );
            this.Controls.Add( this._titelLabel );
            this.Name = "PropertyGridSection";
            this.Size = new System.Drawing.Size( 363, 399 );
            this._sidePanel.ResumeLayout( false );
            this._splitContainer.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Label _titelLabel;
        private System.Windows.Forms.Panel _sidePanel;
        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.Button _expandButton;


    }
}
