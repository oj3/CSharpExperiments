namespace Experiments
{
    partial class MainForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnImageTest = new System.Windows.Forms.Button();
            this.btnPluginTest = new System.Windows.Forms.Button();
            this.btnMiscTest = new System.Windows.Forms.Button();
            this.btnChildTest = new System.Windows.Forms.Button();
            this.btnEventTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("HGS創英角ﾎﾟｯﾌﾟ体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnClose.Location = new System.Drawing.Point(151, 143);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 50);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.Button_Click);
            //
            // btnImageTest
            //
            this.btnImageTest.Font = new System.Drawing.Font("HGS創英角ﾎﾟｯﾌﾟ体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnImageTest.Location = new System.Drawing.Point(12, 12);
            this.btnImageTest.Name = "btnImageTest";
            this.btnImageTest.Size = new System.Drawing.Size(110, 50);
            this.btnImageTest.TabIndex = 0;
            this.btnImageTest.Text = "画像";
            this.btnImageTest.UseVisualStyleBackColor = true;
            this.btnImageTest.Click += new System.EventHandler(this.Button_Click);
            //
            // btnPluginTest
            //
            this.btnPluginTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPluginTest.Font = new System.Drawing.Font("HGS創英角ﾎﾟｯﾌﾟ体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnPluginTest.Location = new System.Drawing.Point(151, 12);
            this.btnPluginTest.Name = "btnPluginTest";
            this.btnPluginTest.Size = new System.Drawing.Size(110, 50);
            this.btnPluginTest.TabIndex = 1;
            this.btnPluginTest.Text = "プラグイン";
            this.btnPluginTest.UseVisualStyleBackColor = true;
            this.btnPluginTest.Click += new System.EventHandler(this.Button_Click);
            //
            // btnMiscTest
            //
            this.btnMiscTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMiscTest.Font = new System.Drawing.Font("HGS創英角ﾎﾟｯﾌﾟ体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnMiscTest.Location = new System.Drawing.Point(12, 143);
            this.btnMiscTest.Name = "btnMiscTest";
            this.btnMiscTest.Size = new System.Drawing.Size(110, 50);
            this.btnMiscTest.TabIndex = 3;
            this.btnMiscTest.Text = "その他";
            this.btnMiscTest.UseVisualStyleBackColor = true;
            this.btnMiscTest.Click += new System.EventHandler(this.Button_Click);
            //
            // btnChildTest
            //
            this.btnChildTest.Font = new System.Drawing.Font("HGS創英角ﾎﾟｯﾌﾟ体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnChildTest.Location = new System.Drawing.Point(12, 77);
            this.btnChildTest.Name = "btnChildTest";
            this.btnChildTest.Size = new System.Drawing.Size(110, 50);
            this.btnChildTest.TabIndex = 2;
            this.btnChildTest.Text = "継承";
            this.btnChildTest.UseVisualStyleBackColor = true;
            this.btnChildTest.Click += new System.EventHandler(this.Button_Click);
            //
            // btnEventTest
            //
            this.btnEventTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEventTest.Font = new System.Drawing.Font("HGS創英角ﾎﾟｯﾌﾟ体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnEventTest.Location = new System.Drawing.Point(151, 77);
            this.btnEventTest.Name = "btnEventTest";
            this.btnEventTest.Size = new System.Drawing.Size(110, 50);
            this.btnEventTest.TabIndex = 5;
            this.btnEventTest.Text = "イベント";
            this.btnEventTest.UseVisualStyleBackColor = true;
            this.btnEventTest.Click += new System.EventHandler(this.Button_Click);
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 205);
            this.Controls.Add(this.btnEventTest);
            this.Controls.Add(this.btnChildTest);
            this.Controls.Add(this.btnMiscTest);
            this.Controls.Add(this.btnPluginTest);
            this.Controls.Add(this.btnImageTest);
            this.Controls.Add(this.btnClose);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "テスト選択";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnImageTest;
        private System.Windows.Forms.Button btnPluginTest;
        private System.Windows.Forms.Button btnMiscTest;
        private System.Windows.Forms.Button btnChildTest;
        private System.Windows.Forms.Button btnEventTest;
    }
}
