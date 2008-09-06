namespace Platinum.PropertyEditors
{
    partial class CustomSourceListEditor
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
            this._comboBox = new System.Windows.Forms.ComboBox();
            this._textLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _comboBox
            // 
            this._comboBox.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this._comboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboBox.Font = new System.Drawing.Font( "Segoe UI", 8F );
            this._comboBox.FormattingEnabled = true;
            this._comboBox.ItemHeight = 13;
            this._comboBox.Location = new System.Drawing.Point( 0, 0 );
            this._comboBox.Name = "_comboBox";
            this._comboBox.Size = new System.Drawing.Size( 256, 19 );
            this._comboBox.TabIndex = 0;
            this._comboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler( this._comboBox_DrawItem );
            this._comboBox.SelectionChangeCommitted += new System.EventHandler( this._comboBox_SelectionChangeCommitted );
            this._comboBox.SelectedIndexChanged += new System.EventHandler( this._comboBox_SelectedIndexChanged );
            this._comboBox.Leave += new System.EventHandler( this._comboBox_Leave );
            this._comboBox.Enter += new System.EventHandler( this._comboBox_Enter );
            this._comboBox.MouseLeave += new System.EventHandler( this._comboBox_MouseLeave );
            this._comboBox.DropDownClosed += new System.EventHandler( this._comboBox_DropDownClosed );
            // 
            // _textLabel
            // 
            this._textLabel.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this._textLabel.BackColor = System.Drawing.SystemColors.Window;
            this._textLabel.Font = new System.Drawing.Font( "Segoe UI", 8F );
            this._textLabel.Location = new System.Drawing.Point( 0, 0 );
            this._textLabel.Name = "_textLabel";
            this._textLabel.Padding = new System.Windows.Forms.Padding( 3, 2, 0, 0 );
            this._textLabel.Size = new System.Drawing.Size( 256, 19 );
            this._textLabel.TabIndex = 1;
            this._textLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._textLabel.UseCompatibleTextRendering = true;
            this._textLabel.MouseEnter += new System.EventHandler( this._textLabel_MouseEnter );
            // 
            // CustomSourceListEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this._textLabel );
            this.Controls.Add( this._comboBox );
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font( "Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this.Name = "CustomSourceListEditor";
            this.Size = new System.Drawing.Size( 256, 19 );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.ComboBox _comboBox;
        private System.Windows.Forms.Label _textLabel;
    }
}
