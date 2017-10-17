namespace Experiments
{
    partial class EventTestForm
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
            this.visibleButton = new System.Windows.Forms.Button();
            this.checkButton = new System.Windows.Forms.Button();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.enableButton = new System.Windows.Forms.Button();
            this.visibleLabel = new System.Windows.Forms.Label();
            this.enabledLabel = new System.Windows.Forms.Label();
            this.checkedLabel = new System.Windows.Forms.Label();
            this.eventLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // visibleButton
            //
            this.visibleButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.visibleButton.Location = new System.Drawing.Point(142, 98);
            this.visibleButton.Name = "visibleButton";
            this.visibleButton.Size = new System.Drawing.Size(75, 23);
            this.visibleButton.TabIndex = 0;
            this.visibleButton.Text = "Hide";
            this.visibleButton.UseVisualStyleBackColor = true;
            this.visibleButton.Click += new System.EventHandler(this.visibleButton_Click);
            //
            // checkButton
            //
            this.checkButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.checkButton.Location = new System.Drawing.Point(142, 40);
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(75, 23);
            this.checkButton.TabIndex = 1;
            this.checkButton.Text = "Check";
            this.checkButton.UseVisualStyleBackColor = true;
            this.checkButton.Click += new System.EventHandler(this.checkButton_Click);
            //
            // checkBox
            //
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(12, 12);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(89, 16);
            this.checkBox.TabIndex = 2;
            this.checkBox.Text = "CHECK BOX";
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            //
            // closeButton
            //
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(142, 142);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            //
            // enableButton
            //
            this.enableButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.enableButton.Location = new System.Drawing.Point(142, 69);
            this.enableButton.Name = "enableButton";
            this.enableButton.Size = new System.Drawing.Size(75, 23);
            this.enableButton.TabIndex = 5;
            this.enableButton.Text = "Disable";
            this.enableButton.UseVisualStyleBackColor = true;
            this.enableButton.Click += new System.EventHandler(this.enableButton_Click);
            //
            // visibleLabel
            //
            this.visibleLabel.AutoSize = true;
            this.visibleLabel.Location = new System.Drawing.Point(27, 103);
            this.visibleLabel.Name = "visibleLabel";
            this.visibleLabel.Size = new System.Drawing.Size(74, 12);
            this.visibleLabel.TabIndex = 7;
            this.visibleLabel.Text = "Visible = true";
            //
            // enabledLabel
            //
            this.enabledLabel.AutoSize = true;
            this.enabledLabel.Location = new System.Drawing.Point(27, 74);
            this.enabledLabel.Name = "enabledLabel";
            this.enabledLabel.Size = new System.Drawing.Size(79, 12);
            this.enabledLabel.TabIndex = 8;
            this.enabledLabel.Text = "Enabled = true";
            //
            // checkedLabel
            //
            this.checkedLabel.AutoSize = true;
            this.checkedLabel.Location = new System.Drawing.Point(27, 45);
            this.checkedLabel.Name = "checkedLabel";
            this.checkedLabel.Size = new System.Drawing.Size(88, 12);
            this.checkedLabel.TabIndex = 9;
            this.checkedLabel.Text = "Checked = false";
            //
            // eventLabel
            //
            this.eventLabel.AutoSize = true;
            this.eventLabel.Location = new System.Drawing.Point(119, 13);
            this.eventLabel.Name = "eventLabel";
            this.eventLabel.Size = new System.Drawing.Size(23, 12);
            this.eventLabel.TabIndex = 10;
            this.eventLabel.Text = "---";
            //
            // EventTestForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 168);
            this.Controls.Add(this.eventLabel);
            this.Controls.Add(this.checkedLabel);
            this.Controls.Add(this.enabledLabel);
            this.Controls.Add(this.visibleLabel);
            this.Controls.Add(this.enableButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.checkButton);
            this.Controls.Add(this.visibleButton);
            this.Name = "EventTestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EventTestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button visibleButton;
        private System.Windows.Forms.Button checkButton;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button enableButton;
        private System.Windows.Forms.Label visibleLabel;
        private System.Windows.Forms.Label enabledLabel;
        private System.Windows.Forms.Label checkedLabel;
        private System.Windows.Forms.Label eventLabel;
    }
}
