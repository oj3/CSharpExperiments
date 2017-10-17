using System;
using System.Drawing;
using System.Windows.Forms;

namespace Experiments.PluginTest
{
    /// <summary>
    /// ボタンの基底クラス
    /// </summary>
    public abstract partial class BaseButton : Button
    {
        /// <summary>基本色</summary>
        protected Color defaultColor;
        /// <summary>親フォーム</summary>
        protected PluginTestForm parentForm;
        /// <summary>表示用のボタン名称</summary>
        protected string buttonName;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">親フォーム</param>
        /// <param name="src">対応するボタン</param>
        public BaseButton(PluginTestForm parent, Button src)
        {
            InitializeComponent();

            this.parentForm = parent;
            this.Location = src.Location;
            this.Size = src.Size;
            this.Text = src.Text;
            this.Font = src.Font;
        }

        /// <summary>クリック時の処理</summary>
        public abstract void OnClick(object sender, EventArgs e);

        /// <summary>マウスが乗った時の処理</summary>
        public abstract void OnMouseEnter(object sender, EventArgs e);

        /// <summary>デリゲートの設定</summary>
        public abstract void SetDelegate(BaseButton src);

        /// <summary>マウスが外れたときの処理</summary>
        protected virtual void OnMouseLeave(object sender, EventArgs e)
        {
            this.parentForm.SetLabel(string.Format("マウスOFF: {0}", this.buttonName));
        }

        /// <summary>描画時の処理</summary>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
