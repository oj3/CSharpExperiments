using System;
using System.Drawing;
using System.Windows.Forms;
using Experiments.PluginTest;

namespace DLL.PluginTest
{
    /// <summary>
    /// DLL側の青ボタンのクラス
    /// </summary>
    public partial class DLLButtonBlue : BaseButtonBlue
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">親フォーム</param>
        /// <param name="src">対応するボタン</param>
        public DLLButtonBlue(PluginTestForm parent, Button src)
            : base(parent, src)
        {
            InitializeComponent();

            this.BackColor = Color.DarkGreen;
        }

        /// <summary>クリック時の処理</summary>
        public override void OnClick(object sender, EventArgs e)
        {
            MessageBox.Show("[DLL] 青ボタンのメソッド");
        }

        /// <summary>マウスが乗った時の処理</summary>
        public override void OnMouseEnter(object sender, EventArgs e)
        {
            this.parentForm.SetLabel(string.Format("[DLL] マウスON: {0}", this.buttonName));
        }

        /// <summary>デリゲートの設定</summary>
        public override void SetDelegate(BaseButton src)
        {
            this.Click += src.OnClick;
            this.MouseEnter += src.OnMouseEnter;
            this.MouseLeave += OnMouseLeave;
        }
    }
}
