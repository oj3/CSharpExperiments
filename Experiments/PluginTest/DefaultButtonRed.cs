using System;
using System.Windows.Forms;

namespace Experiments.PluginTest
{
    /// <summary>
    /// アプリケーション側の赤ボタンのクラス
    /// </summary>
    public partial class DefaultButtonRed : BaseButtonRed
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">親フォーム</param>
        /// <param name="src">対応するボタン</param>
        public DefaultButtonRed(PluginTestForm parent, Button src)
            : base(parent, src)
        {
            InitializeComponent();
        }

        /// <summary>クリック時の処理</summary>
        public override void OnClick(object sender, EventArgs e)
        {
            MessageBox.Show("赤ボタンのメソッド");
        }

        /// <summary>マウスが乗った時の処理</summary>
        public override void OnMouseEnter(object sender, EventArgs e)
        {
            this.parentForm.SetLabel(string.Format("マウスON: {0}", this.buttonName));
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
