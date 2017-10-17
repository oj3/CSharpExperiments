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
    public partial class DesignerTestForm2 : DesignerTestForm1
    {
        protected override void AbstractInitializeComponent()
        {
            SetLog(typeof(DesignerTestForm2), "AbstractInitializeComponent");
            base.AbstractInitializeComponent();

            SetLog(typeof(DesignerTestForm2), "InitializeComponent");
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DesignerTestForm2()
        {
            SetLog(string.Format("[{0}] this.DesignMode = {1}", typeof(DesignerTestForm2).Name, this.DesignMode));
            SetLog(typeof(DesignerTestForm2), "ctor");

            if (Program.IsDesignMode)
            {
                SetLog(typeof(DesignerTestForm2), "return");
                ToFile();
                return;
            }

            SetLog(typeof(DesignerTestForm2), "normal end");
            ToFile();
            int num = Err.Number;
        }

        protected override void button1_Click(object sender, EventArgs e)
        {
            base.button1_Click(sender, e);
        }
    }
}
