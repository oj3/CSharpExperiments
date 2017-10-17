namespace Experiments
{
    partial class ImageTestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.helpButton = new System.Windows.Forms.Button();
            this.helpPathTextBox = new System.Windows.Forms.TextBox();
            this.panelSizeCaptionLabel = new System.Windows.Forms.Label();
            this.panelCSizeCaptionLabel = new System.Windows.Forms.Label();
            this.panelSizeLabel = new System.Windows.Forms.Label();
            this.panelCSizeLabel = new System.Windows.Forms.Label();
            this.testPanel = new System.Windows.Forms.Panel();
            this.testPictureBox = new System.Windows.Forms.PictureBox();
            this.widthPlusButton = new System.Windows.Forms.Button();
            this.widthMinusButton = new System.Windows.Forms.Button();
            this.heightMinusButton = new System.Windows.Forms.Button();
            this.heightPlusButton = new System.Windows.Forms.Button();
            this.widthCaptionLabel = new System.Windows.Forms.Label();
            this.heightCaptionLabel = new System.Windows.Forms.Label();
            this.picSizeLabel = new System.Windows.Forms.Label();
            this.picSizeCaptionLabel = new System.Windows.Forms.Label();
            this.picCaptionLabel = new System.Windows.Forms.Label();
            this.panelCaptionLabel = new System.Windows.Forms.Label();
            this.unitLabel = new System.Windows.Forms.Label();
            this.changeUnitButton = new System.Windows.Forms.Button();
            this.dispRectSizeLabel = new System.Windows.Forms.Label();
            this.dispRectCaptionLabel = new System.Windows.Forms.Label();
            this.mousePostionLabel = new System.Windows.Forms.Label();
            this.testPanel.SuspendLayout();
((System.ComponentModel.ISupportInitialize)(this.testPictureBox)).BeginInit();
            this.SuspendLayout();
            //
            // helpButton
            //
            this.helpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpButton.Location = new System.Drawing.Point(385, 12);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(75, 23);
            this.helpButton.TabIndex = 0;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            //
            // helpPathTextBox
            //
            this.helpPathTextBox.Location = new System.Drawing.Point(12, 14);
            this.helpPathTextBox.Name = "helpPathTextBox";
            this.helpPathTextBox.Size = new System.Drawing.Size(300, 19);
            this.helpPathTextBox.TabIndex = 1;
            this.helpPathTextBox.Text = "Help\\Help.txt";
            //
            // panelSizeCaptionLabel
            //
            this.panelSizeCaptionLabel.AutoSize = true;
            this.panelSizeCaptionLabel.Location = new System.Drawing.Point(360, 105);
            this.panelSizeCaptionLabel.Name = "panelSizeCaptionLabel";
            this.panelSizeCaptionLabel.Size = new System.Drawing.Size(28, 12);
            this.panelSizeCaptionLabel.TabIndex = 6;
            this.panelSizeCaptionLabel.Text = "Size:";
            //
            // panelCSizeCaptionLabel
            //
            this.panelCSizeCaptionLabel.AutoSize = true;
            this.panelCSizeCaptionLabel.Location = new System.Drawing.Point(360, 123);
            this.panelCSizeCaptionLabel.Name = "panelCSizeCaptionLabel";
            this.panelCSizeCaptionLabel.Size = new System.Drawing.Size(58, 12);
            this.panelCSizeCaptionLabel.TabIndex = 7;
            this.panelCSizeCaptionLabel.Text = "ClientSize:";
            //
            // panelSizeLabel
            //
            this.panelSizeLabel.AutoSize = true;
            this.panelSizeLabel.Location = new System.Drawing.Point(424, 105);
            this.panelSizeLabel.Name = "panelSizeLabel";
            this.panelSizeLabel.Size = new System.Drawing.Size(31, 12);
            this.panelSizeLabel.TabIndex = 8;
            this.panelSizeLabel.Text = "(x, y)";
            //
            // panelCSizeLabel
            //
            this.panelCSizeLabel.AutoSize = true;
            this.panelCSizeLabel.Location = new System.Drawing.Point(424, 123);
            this.panelCSizeLabel.Name = "panelCSizeLabel";
            this.panelCSizeLabel.Size = new System.Drawing.Size(31, 12);
            this.panelCSizeLabel.TabIndex = 9;
            this.panelCSizeLabel.Text = "(x, y)";
            //
            // testPanel
            //
            this.testPanel.AutoScroll = true;
            this.testPanel.BackColor = System.Drawing.SystemColors.Control;
            this.testPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.testPanel.Controls.Add(this.testPictureBox);
            this.testPanel.Location = new System.Drawing.Point(12, 50);
            this.testPanel.Name = "testPanel";
            this.testPanel.Size = new System.Drawing.Size(135, 87);
            this.testPanel.TabIndex = 10;
            //
            // testPictureBox
            //
            this.testPictureBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.testPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.testPictureBox.Location = new System.Drawing.Point(0, 0);
            this.testPictureBox.Name = "testPictureBox";
            this.testPictureBox.Size = new System.Drawing.Size(110, 72);
            this.testPictureBox.TabIndex = 0;
            this.testPictureBox.TabStop = false;
            //
            // widthPlusButton
            //
            this.widthPlusButton.Location = new System.Drawing.Point(264, 50);
            this.widthPlusButton.Name = "widthPlusButton";
            this.widthPlusButton.Size = new System.Drawing.Size(48, 23);
            this.widthPlusButton.TabIndex = 11;
            this.widthPlusButton.Text = "+";
            this.widthPlusButton.UseVisualStyleBackColor = true;
            this.widthPlusButton.Click += new System.EventHandler(this.widthPlusButton_Click);
            //
            // widthMinusButton
            //
            this.widthMinusButton.Location = new System.Drawing.Point(210, 50);
            this.widthMinusButton.Name = "widthMinusButton";
            this.widthMinusButton.Size = new System.Drawing.Size(48, 23);
            this.widthMinusButton.TabIndex = 12;
            this.widthMinusButton.Text = "-";
            this.widthMinusButton.UseVisualStyleBackColor = true;
            this.widthMinusButton.Click += new System.EventHandler(this.widthMinusButton_Click);
            //
            // heightMinusButton
            //
            this.heightMinusButton.Location = new System.Drawing.Point(210, 79);
            this.heightMinusButton.Name = "heightMinusButton";
            this.heightMinusButton.Size = new System.Drawing.Size(48, 23);
            this.heightMinusButton.TabIndex = 14;
            this.heightMinusButton.Text = "-";
            this.heightMinusButton.UseVisualStyleBackColor = true;
            this.heightMinusButton.Click += new System.EventHandler(this.heightMinusButton_Click);
            //
            // heightPlusButton
            //
            this.heightPlusButton.Location = new System.Drawing.Point(264, 79);
            this.heightPlusButton.Name = "heightPlusButton";
            this.heightPlusButton.Size = new System.Drawing.Size(48, 23);
            this.heightPlusButton.TabIndex = 13;
            this.heightPlusButton.Text = "+";
            this.heightPlusButton.UseVisualStyleBackColor = true;
            this.heightPlusButton.Click += new System.EventHandler(this.heightPlusButton_Click);
            //
            // widthCaptionLabel
            //
            this.widthCaptionLabel.AutoSize = true;
            this.widthCaptionLabel.Location = new System.Drawing.Point(164, 55);
            this.widthCaptionLabel.Name = "widthCaptionLabel";
            this.widthCaptionLabel.Size = new System.Drawing.Size(35, 12);
            this.widthCaptionLabel.TabIndex = 15;
            this.widthCaptionLabel.Text = "Width:";
            //
            // heightCaptionLabel
            //
            this.heightCaptionLabel.AutoSize = true;
            this.heightCaptionLabel.Location = new System.Drawing.Point(164, 84);
            this.heightCaptionLabel.Name = "heightCaptionLabel";
            this.heightCaptionLabel.Size = new System.Drawing.Size(40, 12);
            this.heightCaptionLabel.TabIndex = 16;
            this.heightCaptionLabel.Text = "Height:";
            //
            // picSizeLabel
            //
            this.picSizeLabel.AutoSize = true;
            this.picSizeLabel.Location = new System.Drawing.Point(424, 67);
            this.picSizeLabel.Name = "picSizeLabel";
            this.picSizeLabel.Size = new System.Drawing.Size(31, 12);
            this.picSizeLabel.TabIndex = 18;
            this.picSizeLabel.Text = "(x, y)";
            //
            // picSizeCaptionLabel
            //
            this.picSizeCaptionLabel.AutoSize = true;
            this.picSizeCaptionLabel.Location = new System.Drawing.Point(360, 67);
            this.picSizeCaptionLabel.Name = "picSizeCaptionLabel";
            this.picSizeCaptionLabel.Size = new System.Drawing.Size(28, 12);
            this.picSizeCaptionLabel.TabIndex = 17;
            this.picSizeCaptionLabel.Text = "Size:";
            //
            // picCaptionLabel
            //
            this.picCaptionLabel.AutoSize = true;
            this.picCaptionLabel.Location = new System.Drawing.Point(343, 50);
            this.picCaptionLabel.Name = "picCaptionLabel";
            this.picCaptionLabel.Size = new System.Drawing.Size(73, 12);
            this.picCaptionLabel.TabIndex = 19;
            this.picCaptionLabel.Text = "[Picture Box]";
            //
            // panelCaptionLabel
            //
            this.panelCaptionLabel.AutoSize = true;
            this.panelCaptionLabel.Location = new System.Drawing.Point(343, 88);
            this.panelCaptionLabel.Name = "panelCaptionLabel";
            this.panelCaptionLabel.Size = new System.Drawing.Size(41, 12);
            this.panelCaptionLabel.TabIndex = 20;
            this.panelCaptionLabel.Text = "[Panel]";
            //
            // unitLabel
            //
            this.unitLabel.AutoSize = true;
            this.unitLabel.Location = new System.Drawing.Point(164, 111);
            this.unitLabel.Name = "unitLabel";
            this.unitLabel.Size = new System.Drawing.Size(38, 12);
            this.unitLabel.TabIndex = 21;
            this.unitLabel.Text = "Unit: 1";
            //
            // changeUnitButton
            //
            this.changeUnitButton.Location = new System.Drawing.Point(210, 106);
            this.changeUnitButton.Name = "changeUnitButton";
            this.changeUnitButton.Size = new System.Drawing.Size(48, 23);
            this.changeUnitButton.TabIndex = 22;
            this.changeUnitButton.Text = "Alt";
            this.changeUnitButton.UseVisualStyleBackColor = true;
            this.changeUnitButton.Click += new System.EventHandler(this.changeUnitButton_Click);
            //
            // dispRectSizeLabel
            //
            this.dispRectSizeLabel.AutoSize = true;
            this.dispRectSizeLabel.Location = new System.Drawing.Point(424, 143);
            this.dispRectSizeLabel.Name = "dispRectSizeLabel";
            this.dispRectSizeLabel.Size = new System.Drawing.Size(31, 12);
            this.dispRectSizeLabel.TabIndex = 24;
            this.dispRectSizeLabel.Text = "(x, y)";
            //
            // dispRectCaptionLabel
            //
            this.dispRectCaptionLabel.AutoSize = true;
            this.dispRectCaptionLabel.Location = new System.Drawing.Point(360, 143);
            this.dispRectCaptionLabel.Name = "dispRectCaptionLabel";
            this.dispRectCaptionLabel.Size = new System.Drawing.Size(54, 12);
            this.dispRectCaptionLabel.TabIndex = 23;
            this.dispRectCaptionLabel.Text = "DispRect:";
            //
            // mousePostionLabel
            //
            this.mousePostionLabel.AutoSize = true;
            this.mousePostionLabel.Location = new System.Drawing.Point(10, 198);
            this.mousePostionLabel.Name = "mousePostionLabel";
            this.mousePostionLabel.Size = new System.Drawing.Size(61, 12);
            this.mousePostionLabel.TabIndex = 25;
            this.mousePostionLabel.Text = "X: ##, Y: ##";
            //
            // TestForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 219);
            this.Controls.Add(this.mousePostionLabel);
            this.Controls.Add(this.dispRectSizeLabel);
            this.Controls.Add(this.dispRectCaptionLabel);
            this.Controls.Add(this.changeUnitButton);
            this.Controls.Add(this.unitLabel);
            this.Controls.Add(this.panelCaptionLabel);
            this.Controls.Add(this.picCaptionLabel);
            this.Controls.Add(this.picSizeLabel);
            this.Controls.Add(this.picSizeCaptionLabel);
            this.Controls.Add(this.heightCaptionLabel);
            this.Controls.Add(this.widthCaptionLabel);
            this.Controls.Add(this.heightMinusButton);
            this.Controls.Add(this.heightPlusButton);
            this.Controls.Add(this.widthMinusButton);
            this.Controls.Add(this.widthPlusButton);
            this.Controls.Add(this.testPanel);
            this.Controls.Add(this.panelCSizeLabel);
            this.Controls.Add(this.panelSizeLabel);
            this.Controls.Add(this.panelCSizeCaptionLabel);
            this.Controls.Add(this.panelSizeCaptionLabel);
            this.Controls.Add(this.helpPathTextBox);
            this.Controls.Add(this.helpButton);
            this.HelpButton = true;
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestForm";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TestForm_MouseMove);
            this.testPanel.ResumeLayout(false);
((System.ComponentModel.ISupportInitialize)(this.testPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.TextBox helpPathTextBox;
        private System.Windows.Forms.Label panelSizeCaptionLabel;
        private System.Windows.Forms.Label panelCSizeCaptionLabel;
        private System.Windows.Forms.Label panelSizeLabel;
        private System.Windows.Forms.Label panelCSizeLabel;
        private System.Windows.Forms.Panel testPanel;
        private System.Windows.Forms.PictureBox testPictureBox;
        private System.Windows.Forms.Button widthPlusButton;
        private System.Windows.Forms.Button widthMinusButton;
        private System.Windows.Forms.Button heightMinusButton;
        private System.Windows.Forms.Button heightPlusButton;
        private System.Windows.Forms.Label widthCaptionLabel;
        private System.Windows.Forms.Label heightCaptionLabel;
        private System.Windows.Forms.Label picSizeLabel;
        private System.Windows.Forms.Label picSizeCaptionLabel;
        private System.Windows.Forms.Label picCaptionLabel;
        private System.Windows.Forms.Label panelCaptionLabel;
        private System.Windows.Forms.Label unitLabel;
        private System.Windows.Forms.Button changeUnitButton;
        private System.Windows.Forms.Label dispRectSizeLabel;
        private System.Windows.Forms.Label dispRectCaptionLabel;
        private System.Windows.Forms.Label mousePostionLabel;
    }
}
