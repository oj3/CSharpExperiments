using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Experiments
{
    /// <summary>
    /// 派生フォーム
    /// </summary>
    public partial class DerivedTestForm : BaseTestForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DerivedTestForm()
        {
            InitializeComponent();
            this.count = 100;
            AppendMsg("child.ctor", false);
        }

        protected override void ButtonClicked()
        {
            AppendMsg("child.ButtonClicked_1");

            base.ButtonClicked();

            AppendMsg("child.ButtonClicked_2");
        }

        protected override void AbstractButtonClicked()
        {
            AppendMsg("child.AbstractButtonClicked");
        }
    }
}
