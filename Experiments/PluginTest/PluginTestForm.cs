using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Experiments.PluginTest
{
    /// <summary>
    /// プラグイン利用の検証用画面
    /// </summary>
    public partial class PluginTestForm : Form
    {
        /// <summary>DLLファイル名</summary>
        private const string DLL_NAME = "TestDLL.dll";

        /// <summary>選択可能なプラグイン</summary>
        private enum PluginOption
        {
            /// <summary>アプリケーション規定の処理</summary>
            Default,
            /// <summary>外部プラグイン</summary>
            DLL
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PluginTestForm()
        {
            InitializeComponent();

            SetLabel("");
        }

        /// <summary>
        /// プラグイン選択ボタンクリック
        /// </summary>
        private void selectButton_Click(object sender, EventArgs e)
        {
            var plugin = ((Button)sender==this.btnDefault )?PluginOption.Default :PluginOption .DLL;
            BaseButtonRed rBtn;
            BaseButtonBlue bBtn;

            // プラグインごとのボタンインスタンスを取得
            GetControls(plugin, out rBtn, out bBtn);

            rBtn.Parent = this;
            rBtn.SetDelegate(bBtn);
            if (this.btnRed != null)
            {
                this.btnRed.Dispose();
            }
            this.btnRed = rBtn;

            bBtn.Parent = this;
            bBtn.SetDelegate(rBtn);
            if (this.btnBlue != null)
            {
                this.btnBlue.Dispose();
            }
            this.btnBlue = bBtn;
        }

        /// <summary>
        /// プラグインに応じてコントロールのインスタンスを取得します。
        /// </summary>
        /// <param name="plugin">選択したプラグイン</param>
        /// <param name="rBtn">[out] 取得するコントロールその1 (赤ボタン)</param>
        /// <param name="bBtn">[out] 取得するコントロールその2 (青ボタン)</param>
        private void GetControls(PluginOption plugin, out BaseButtonRed rBtn, out BaseButtonBlue bBtn)
        {
            if (plugin == PluginOption.Default)
            {
                rBtn = new DefaultButtonRed(this, this.btnRed);
                bBtn = new DefaultButtonBlue(this, this.btnBlue);
            }
            else
            {
                string exeDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                string dllPath = System.IO.Path.Combine(exeDir, DLL_NAME);
                var typeRed = Assembly.LoadFile(dllPath).GetExportedTypes()
                                .First(t => t.IsClass && typeof(BaseButtonRed).IsAssignableFrom(t));
                rBtn = (BaseButtonRed)Activator.CreateInstance(typeRed, this, this.btnRed);

                var typeBlue = Assembly.LoadFile(dllPath).GetExportedTypes()
                                .First(t => t.IsClass && typeof(BaseButtonBlue).IsAssignableFrom(t));
                bBtn = (BaseButtonBlue)Activator.CreateInstance(typeBlue, this, this.btnBlue);
            }
        }

        /// <summary>
        /// 情報表示ラベルに指定した音字列を表示します。
        /// </summary>
        /// <param name="text"></param>
        public void SetLabel(string text)
        {
            this.lblInfo.Text = text;
        }
    }
}
