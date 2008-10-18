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

namespace Platinum
{
    partial class PropertyGrid
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
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._sectionPanelScrollBar = new System.Windows.Forms.VScrollBar();
            this._sectionPanel = new System.Windows.Forms.Panel();
            this._helpTextLabel = new System.Windows.Forms.Label();
            this._helpTextTitleLabel = new System.Windows.Forms.Label();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer
            // 
            this._splitContainer.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this._splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._splitContainer.Location = new System.Drawing.Point( 0, 0 );
            this._splitContainer.Name = "_splitContainer";
            this._splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this._splitContainer.Panel1.Controls.Add( this._sectionPanelScrollBar );
            this._splitContainer.Panel1.Controls.Add( this._sectionPanel );
            this._splitContainer.Panel1.SizeChanged += new System.EventHandler( this._splitContainer_Panel1_SizeChanged );
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add( this._helpTextLabel );
            this._splitContainer.Panel2.Controls.Add( this._helpTextTitleLabel );
            this._splitContainer.Size = new System.Drawing.Size( 348, 447 );
            this._splitContainer.SplitterDistance = 316;
            this._splitContainer.TabIndex = 0;
            this._splitContainer.TabStop = false;
            // 
            // _sectionPanelScrollBar
            // 
            this._sectionPanelScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this._sectionPanelScrollBar.LargeChange = 100;
            this._sectionPanelScrollBar.Location = new System.Drawing.Point( 329, 0 );
            this._sectionPanelScrollBar.Maximum = 1000;
            this._sectionPanelScrollBar.Name = "_sectionPanelScrollBar";
            this._sectionPanelScrollBar.Size = new System.Drawing.Size( 17, 314 );
            this._sectionPanelScrollBar.SmallChange = 10;
            this._sectionPanelScrollBar.TabIndex = 1;
            this._sectionPanelScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler( this._sectionPanelScrollBar_Scroll );
            // 
            // _sectionPanel
            // 
            this._sectionPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._sectionPanel.BackColor = System.Drawing.SystemColors.Window;
            this._sectionPanel.Location = new System.Drawing.Point( 0, 0 );
            this._sectionPanel.Name = "_sectionPanel";
            this._sectionPanel.Size = new System.Drawing.Size( 242, 205 );
            this._sectionPanel.TabIndex = 0;
            // 
            // _helpTextLabel
            // 
            this._helpTextLabel.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this._helpTextLabel.Location = new System.Drawing.Point( 3, 20 );
            this._helpTextLabel.Name = "_helpTextLabel";
            this._helpTextLabel.Size = new System.Drawing.Size( 340, 105 );
            this._helpTextLabel.TabIndex = 1;
            this._helpTextLabel.Text = "Help Text";
            // 
            // _helpTextTitleLabel
            // 
            this._helpTextTitleLabel.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this._helpTextTitleLabel.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte) ( 0 ) ) );
            this._helpTextTitleLabel.Location = new System.Drawing.Point( 2, 0 );
            this._helpTextTitleLabel.Name = "_helpTextTitleLabel";
            this._helpTextTitleLabel.Size = new System.Drawing.Size( 341, 17 );
            this._helpTextTitleLabel.TabIndex = 0;
            this._helpTextTitleLabel.Text = "PropertyName";
            this._helpTextTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PropertyGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this._splitContainer );
            this.DoubleBuffered = true;
            this.Name = "PropertyGrid";
            this.Size = new System.Drawing.Size( 348, 447 );
            this._splitContainer.Panel1.ResumeLayout( false );
            this._splitContainer.Panel2.ResumeLayout( false );
            this._splitContainer.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.Label _helpTextLabel;
        private System.Windows.Forms.Label _helpTextTitleLabel;
        private System.Windows.Forms.Panel _sectionPanel;
        private System.Windows.Forms.VScrollBar _sectionPanelScrollBar;
    }
}
