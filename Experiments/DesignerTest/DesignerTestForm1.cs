using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Experiments
{
    public partial class DesignerTestForm1 : Form
    {
        private readonly bool exportLog = false;

        private const string LOG_DIR_PATH = @"log\DesignerTest";
        private const string LOG_FILE_FORMAT = "{0}_{1}.txt";
        /// <summary>ログ出力用文字列</summary>
        protected StringBuilder sbLog;

        /// <summary>デザイナエラー確認用のオブジェクト</summary>
        public static ErrorInvoker Err = null;

        protected virtual void AbstractInitializeComponent()
        {
            SetLog(typeof(DesignerTestForm1), "AbstractInitializeComponent");
            InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected DesignerTestForm1()
        {
            SetLog(string.Format("[{0}] this.DesignMode = {1}", typeof(DesignerTestForm1).Name, this.DesignMode));

            SetLog(typeof(DesignerTestForm1), "ctor_1");
            AbstractInitializeComponent();
            SetLog(typeof(DesignerTestForm1), "ctor_2");

            if (Program.IsDesignMode)
            {
                SetLog(typeof(DesignerTestForm1), "return");
                ToFile();
                return;
            }

            SetLog(typeof(DesignerTestForm1), "normal end");
            ToFile();
            int num = Err.Number;
        }

        protected virtual void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// ログを出力用文字列に追記します。
        /// </summary>
        /// <param name="t"></param>
        /// <param name="method"></param>
        protected void SetLog(Type t, string method)
        {
            var args = new object[] { Program.IsDesignMode ? "**" : "",
                                    t.Name, method, this.GetType().Name };
            SetLog(string.Format("{0}[{1}.{2}] this.GetType().Name = {3}", args));
        }
        /// <summary>
        /// ログを出力用文字列に追記します。
        /// </summary>
        /// <param name="t"></param>
        /// <param name="method"></param>
        protected void SetLog(string log)
        {
            if (this.sbLog == null)
            {
                this.sbLog = new StringBuilder();
            }
            this.sbLog.AppendLine(log);
        }

        /// <summary>
        /// テスト結果をファイルに出力します。
        /// </summary>
        /// <param name="result">テスト結果文字列</param>
        protected void ToFile()
        {
            if (!this.exportLog)
            {
                return;
            }

            if (this.sbLog == null || this.sbLog.Length == 0)
            {
                return;
            }

            string format = Path.Combine(LOG_DIR_PATH, LOG_FILE_FORMAT);
            string path = string.Format(format, DateTime.Now.ToString("yyyyMMdd_HHmmss"), "DesignerTest");
            using (var sw = new StreamWriter(path))
            {
                sw.Write(this.sbLog.ToString());
            }
        }

        /// <summary>
        /// デザイナエラー確認用のクラス
        /// </summary>
        public class ErrorInvoker
        {
            public int Number { get; set; }
        }
    }
}
