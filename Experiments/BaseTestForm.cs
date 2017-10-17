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
    /// 親フォーム
    /// </summary>
    public partial class BaseTestForm : Form
    {
        /// <summary>メッセージ</summary>
        protected StringBuilder sbMsg;

        /// <summary>処理カウント</summary>
        protected int count;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseTestForm()
        {
            InitializeComponent();

            this.count = 0;
            AppendMsg("base.ctor", false);
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            ButtonClicked();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.sbMsg = new StringBuilder();
            this.count = 0;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            this.txtMsg.Text = this.sbMsg.ToString();
        }

        protected virtual void ButtonClicked()
        {
            AppendMsg("base.ButtonClicked");

            AbstractButtonClicked();
        }

        protected virtual void AbstractButtonClicked()
        {
            AppendMsg("base.AbstractButtonClicked");
        }

        #region AppendMsg
        /// <summary>
        /// メッセージを1行出力します。
        /// </summary>
        /// <param name="msg">出力するメッセージ</param>
        protected void AppendMsg(string msg)
        {
            AppendMsg(msg, true);
        }
        /// <summary>
        /// メッセージを1行出力します。
        /// </summary>
        /// <param name="msg">出力するメッセージ</param>
        /// <param name="incrementCount">true: カウントする / false: カウントしない</param>
        protected void AppendMsg(string msg, bool incrementCount)
        {
            try
            {
                if (incrementCount)
                {
                    this.count++;
                }
                this.sbMsg.AppendLine(string.Format("[{0}] {1}", this.count, msg));
            }
            catch
            {
                this.sbMsg = new StringBuilder();
                this.sbMsg.AppendLine("Create sbMsg.");
                this.sbMsg.AppendLine("");

                AppendMsg(msg, false);
            }
        }
        #endregion // AppendMsg
    }
}
