using System.Drawing;
using System.Windows.Forms;

namespace Experiments.PluginTest
{
    /// <summary>
    /// 青ボタンの基底クラス
    /// </summary>
    public abstract partial class BaseButtonBlue : BaseButton
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">親フォーム</param>
        /// <param name="src">対応するボタン</param>
        public BaseButtonBlue(PluginTestForm parent, Button src)
            : base(parent, src)
        {
            InitializeComponent();

            this.buttonName = "青ボタン";
            this.defaultColor = Color.Blue;
            this.ForeColor = defaultColor;
        }
    }
}
