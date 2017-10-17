namespace Experiments
{
    partial class DerivedTestPanel
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.childMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.testMenu1.SuspendLayout();
            this.SuspendLayout();
            //
            // testMenu1
            //
            this.testMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.childMenuItem1});
            //
            // childMenuItem1
            //
            this.childMenuItem1.Name = "childMenuItem1";
            this.childMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.childMenuItem1.Text = "child_1";
            this.testMenu1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem childMenuItem1;
    }
}
