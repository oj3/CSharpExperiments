namespace Experiments
{
    partial class BaseTestForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            //
            // tabControl1
            //
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(200, 100);
            this.tabControl1.TabIndex = 0;
            //
            // tabPage1
            //
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 74);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            //
            // btnSet
            //
            this.btnSet.Location = new System.Drawing.Point(12, 133);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 1;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            //
            // btnReset
            //
            this.btnReset.Location = new System.Drawing.Point(93, 133);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            //
            // btnShow
            //
            this.btnShow.Location = new System.Drawing.Point(174, 133);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 3;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            //
            // txtMsg
            //
            this.txtMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMsg.Location = new System.Drawing.Point(12, 162);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMsg.Size = new System.Drawing.Size(260, 95);
            this.txtMsg.TabIndex = 4;
            //
            // BaseTestForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 269);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.tabControl1);
            this.Name = "BaseTestForm";
            this.Text = "BaseTestForm";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        protected System.Windows.Forms.TabControl tabControl1;
        protected System.Windows.Forms.Button btnSet;
        protected System.Windows.Forms.Button btnReset;
        protected System.Windows.Forms.Button btnShow;
        protected System.Windows.Forms.TextBox txtMsg;
    }
}
