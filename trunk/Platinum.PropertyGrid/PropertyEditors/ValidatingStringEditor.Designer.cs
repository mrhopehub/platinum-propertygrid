namespace Platinum
{
    partial class ValidatingStringEditor 
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
            this._textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _textBox
            // 
            this._textBox.AcceptsReturn = true;
            this._textBox.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this._textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._textBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this._textBox.Font = new System.Drawing.Font( "Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this._textBox.Location = new System.Drawing.Point( 0, 3 );
            this._textBox.Name = "_textBox";
            this._textBox.Size = new System.Drawing.Size( 256, 15 );
            this._textBox.TabIndex = 0;
            this._textBox.Tag = "";
            this._textBox.TextChanged += new System.EventHandler( this._textBox_TextChanged );
            this._textBox.KeyDown += new System.Windows.Forms.KeyEventHandler( this._textBox_KeyDown );
            this._textBox.Leave += new System.EventHandler( this._textBox_Leave );
            // 
            // ValidatingStringEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add( this._textBox );
            this.MaximumSize = new System.Drawing.Size( 10000, 19 );
            this.MinimumSize = new System.Drawing.Size( 0, 19 );
            this.Name = "ValidatingStringEditor";
            this.Size = new System.Drawing.Size( 256, 19 );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _textBox;


    }
}
