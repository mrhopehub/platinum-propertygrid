namespace Platinum.PropertyEditors
{
    partial class PointEditor
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
            this._xLabel = new System.Windows.Forms.Label();
            this._yLabel = new System.Windows.Forms.Label();
            this._xTextBox = new System.Windows.Forms.TextBox();
            this._yTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _xLabel
            // 
            this._xLabel.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this._xLabel.BackColor = System.Drawing.SystemColors.Window;
            this._xLabel.Font = new System.Drawing.Font( "Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this._xLabel.Location = new System.Drawing.Point( 0, 0 );
            this._xLabel.Name = "_xLabel";
            this._xLabel.Size = new System.Drawing.Size( 256, 19 );
            this._xLabel.TabIndex = 0;
            this._xLabel.Text = "X";
            this._xLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._xLabel.Click += new System.EventHandler( this._xLabel_Click );
            // 
            // _yLabel
            // 
            this._yLabel.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this._yLabel.BackColor = System.Drawing.SystemColors.Window;
            this._yLabel.Font = new System.Drawing.Font( "Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this._yLabel.Location = new System.Drawing.Point( 0, 20 );
            this._yLabel.Name = "_yLabel";
            this._yLabel.Size = new System.Drawing.Size( 256, 19 );
            this._yLabel.TabIndex = 1;
            this._yLabel.Text = "Y";
            this._yLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._yLabel.Click += new System.EventHandler( this._yLabel_Click );
            // 
            // _xTextBox
            // 
            this._xTextBox.AcceptsReturn = true;
            this._xTextBox.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this._xTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._xTextBox.Font = new System.Drawing.Font( "Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this._xTextBox.Location = new System.Drawing.Point( 17, 2 );
            this._xTextBox.Name = "_xTextBox";
            this._xTextBox.Size = new System.Drawing.Size( 239, 15 );
            this._xTextBox.TabIndex = 2;
            this._xTextBox.WordWrap = false;
            this._xTextBox.TextChanged += new System.EventHandler( this._xTextBox_TextChanged );
            this._xTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler( this._xTextBox_KeyDown );
            // 
            // _yTextBox
            // 
            this._yTextBox.AcceptsReturn = true;
            this._yTextBox.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this._yTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._yTextBox.Font = new System.Drawing.Font( "Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this._yTextBox.Location = new System.Drawing.Point( 17, 22 );
            this._yTextBox.Name = "_yTextBox";
            this._yTextBox.Size = new System.Drawing.Size( 239, 15 );
            this._yTextBox.TabIndex = 3;
            this._yTextBox.WordWrap = false;
            this._yTextBox.TextChanged += new System.EventHandler( this._yTextBox_TextChanged );
            this._yTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler( this._yTextBox_KeyDown );
            // 
            // PointEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this._yTextBox );
            this.Controls.Add( this._xTextBox );
            this.Controls.Add( this._yLabel );
            this.Controls.Add( this._xLabel );
            this.MaximumSize = new System.Drawing.Size( 10000, 39 );
            this.MinimumSize = new System.Drawing.Size( 0, 39 );
            this.Name = "PointEditor";
            this.Size = new System.Drawing.Size( 256, 39 );
            this.Click += new System.EventHandler( this.PointEditor_Click );
            this.Leave += new System.EventHandler( this._pointEditor_Leave );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _xLabel;
        private System.Windows.Forms.Label _yLabel;
        private System.Windows.Forms.TextBox _xTextBox;
        private System.Windows.Forms.TextBox _yTextBox;


    }
}
