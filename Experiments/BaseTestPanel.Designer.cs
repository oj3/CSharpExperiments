namespace Experiments
{
    partial class BaseTestPanel
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testMenu1 = new Experiments.TestMenu();
            this.baseMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.baseMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.testMenu1.SuspendLayout();
            this.SuspendLayout();
            //
            // contextMenuStrip1
            //
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            //
            // testMenu1
            //
            this.testMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.baseMenuItem1,
            this.baseMenuItem2});
            this.testMenu1.Name = "testMenu1";
            this.testMenu1.Size = new System.Drawing.Size(118, 48);
            this.testMenu1.Text = "testMenu1";
            //
            // baseMenuItem1
            //
            this.baseMenuItem1.Name = "baseMenuItem1";
            this.baseMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.baseMenuItem1.Text = "base_1";
            //
            // baseMenuItem2
            //
            this.baseMenuItem2.Name = "baseMenuItem2";
            this.baseMenuItem2.Size = new System.Drawing.Size(117, 22);
            this.baseMenuItem2.Text = "base_2";
            this.testMenu1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        protected TestMenu testMenu1;
        private System.Windows.Forms.ToolStripMenuItem baseMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem baseMenuItem2;

    }
}