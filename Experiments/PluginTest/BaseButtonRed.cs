using System.Drawing;
using System.Windows.Forms;

namespace Experiments.PluginTest
{
    /// <summary>
    /// 赤ボタンの基底クラス
    /// </summary>
    public abstract partial class BaseButtonRed : BaseButton
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">親フォーム</param>
        /// <param name="src">対応するボタン</param>
        public BaseButtonRed(PluginTestForm parent, Button src)
            : base(parent, src)
        {
            InitializeComponent();

            this.buttonName = "赤ボタン";
            this.defaultColor = Color.Red;
            this.ForeColor = defaultColor;
        }
    }
}
