namespace Experiments
{
    partial class DerivedTestForm
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            //
            // tabControl1
            //
            this.tabControl1.Controls.Add(this.tabPage2);
this.tabControl1.Controls.SetChildIndex(this.tabPage2, 0);
            //
            // tabPage2
            //
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            //
            // DerivedTestForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "DerivedTestForm";
            this.Text = "DerivedTestForm";
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.btnSet, 0);
            this.Controls.SetChildIndex(this.btnReset, 0);
            this.Controls.SetChildIndex(this.btnShow, 0);
            this.Controls.SetChildIndex(this.txtMsg, 0);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
    }
}
