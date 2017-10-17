namespace Experiments.PluginTest
{
    partial class PluginTestForm
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
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnDll = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRed = new System.Windows.Forms.Button();
            this.btnBlue = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // btnDefault
            //
            this.btnDefault.Font = new System.Drawing.Font("江戸勘亭流", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDefault.Location = new System.Drawing.Point(6, 18);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(120, 50);
            this.btnDefault.TabIndex = 0;
            this.btnDefault.Text = "デフォルト";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.selectButton_Click);
            //
            // btnDll
            //
            this.btnDll.Font = new System.Drawing.Font("江戸勘亭流", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDll.Location = new System.Drawing.Point(151, 18);
            this.btnDll.Name = "btnDll";
            this.btnDll.Size = new System.Drawing.Size(120, 50);
            this.btnDll.TabIndex = 1;
            this.btnDll.Text = "DLL";
            this.btnDll.UseVisualStyleBackColor = true;
            this.btnDll.Click += new System.EventHandler(this.selectButton_Click);
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.btnDefault);
            this.groupBox1.Controls.Add(this.btnDll);
            this.groupBox1.Font = new System.Drawing.Font("江戸勘亭流", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 82);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "パネルセット";
            //
            // btnRed
            //
            this.btnRed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRed.Font = new System.Drawing.Font("ふみゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRed.Location = new System.Drawing.Point(18, 134);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(120, 50);
            this.btnRed.TabIndex = 5;
            this.btnRed.Text = "赤ボタン";
            this.btnRed.UseVisualStyleBackColor = true;
            //
            // btnBlue
            //
            this.btnBlue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBlue.Font = new System.Drawing.Font("ふみゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnBlue.Location = new System.Drawing.Point(163, 134);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(120, 50);
            this.btnBlue.TabIndex = 6;
            this.btnBlue.Text = "青ボタン";
            this.btnBlue.UseVisualStyleBackColor = true;
            //
            // lblInfo
            //
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("麗流隷書", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblInfo.Location = new System.Drawing.Point(16, 109);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(70, 13);
            this.lblInfo.TabIndex = 7;
            this.lblInfo.Text = "- - - - -";
            //
            // PluginTestForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 196);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnBlue);
            this.Controls.Add(this.btnRed);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PluginTestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "プラグインテスト";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnDll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRed;
        private System.Windows.Forms.Button btnBlue;
        private System.Windows.Forms.Label lblInfo;
    }
}
