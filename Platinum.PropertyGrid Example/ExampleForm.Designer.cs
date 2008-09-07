namespace Platinum.PropertyGrid_Example
{
    partial class ExampleForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._textLabel = new System.Windows.Forms.Label();
            this._enumLabel = new System.Windows.Forms.Label();
            this._propertyGrid = new Platinum.PropertyGrid();
            this._objectAdapter = new Platinum.DefaultObjectAdapter();
            this._integerLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem} );
            this.menuStrip1.Location = new System.Drawing.Point( 0, 0 );
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size( 348, 24 );
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem} );
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size( 37, 20 );
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size( 92, 22 );
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // _textLabel
            // 
            this._textLabel.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this._textLabel.AutoSize = true;
            this._textLabel.Location = new System.Drawing.Point( 10, 371 );
            this._textLabel.Name = "_textLabel";
            this._textLabel.Size = new System.Drawing.Size( 56, 13 );
            this._textLabel.TabIndex = 2;
            this._textLabel.Text = "_textLabel";
            // 
            // _enumLabel
            // 
            this._enumLabel.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this._enumLabel.AutoSize = true;
            this._enumLabel.Location = new System.Drawing.Point( 10, 395 );
            this._enumLabel.Name = "_enumLabel";
            this._enumLabel.Size = new System.Drawing.Size( 65, 13 );
            this._enumLabel.TabIndex = 3;
            this._enumLabel.Text = "_enumLabel";
            // 
            // _propertyGrid
            // 
            this._propertyGrid.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this._propertyGrid.Location = new System.Drawing.Point( 12, 27 );
            this._propertyGrid.Name = "_propertyGrid";
            this._propertyGrid.SectionBackColor = System.Drawing.Color.FromArgb( ( (int) ( ( (byte) ( 233 ) ) ) ), ( (int) ( ( (byte) ( 236 ) ) ) ), ( (int) ( ( (byte) ( 250 ) ) ) ) );
            this._propertyGrid.Size = new System.Drawing.Size( 324, 341 );
            this._propertyGrid.TabIndex = 0;
            // 
            // _objectAdapter
            // 
            this._objectAdapter.SelectedObject = null;
            this._objectAdapter.TargetPropertyGrid = this._propertyGrid;
            // 
            // _integerLabel
            // 
            this._integerLabel.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this._integerLabel.AutoSize = true;
            this._integerLabel.Location = new System.Drawing.Point( 10, 416 );
            this._integerLabel.Name = "_integerLabel";
            this._integerLabel.Size = new System.Drawing.Size( 71, 13 );
            this._integerLabel.TabIndex = 4;
            this._integerLabel.Text = "_integerLabel";
            // 
            // ExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 348, 438 );
            this.Controls.Add( this._integerLabel );
            this.Controls.Add( this._enumLabel );
            this.Controls.Add( this._textLabel );
            this.Controls.Add( this._propertyGrid );
            this.Controls.Add( this.menuStrip1 );
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size( 0, 212 );
            this.Name = "ExampleForm";
            this.Text = "Platinum.PropertyGrid Example";
            this.Load += new System.EventHandler( this.ExampleForm_Load );
            this.menuStrip1.ResumeLayout( false );
            this.menuStrip1.PerformLayout();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private PropertyGrid _propertyGrid;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label _textLabel;
        private DefaultObjectAdapter _objectAdapter;
        private System.Windows.Forms.Label _enumLabel;
        private System.Windows.Forms.Label _integerLabel;
    }
}

