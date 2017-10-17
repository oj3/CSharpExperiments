using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Experiments
{
    public partial class MainForm : Form
    {
        /// <summary>テスト種別</summary>
        public sealed class TestType
        {
            /// <summary></summary>
            private static List<TestType> dic;

            public int ID { get; private set; }

            /// <summary>画像</summary>
            public static TestType Image = new TestType(0);
            /// <summary>イベント</summary>
            public static TestType Event = new TestType(1);
            /// <summary>プラグイン</summary>
            public static TestType Plugin = new TestType(2);
            /// <summary>フォーム継承</summary>
            public static TestType Child = new TestType(3);
            /// <summary>各種テスト</summary>
            public static TestType Misc = new TestType(4);

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="id"></param>
            /// <param name="button"></param>
            private TestType(int id)
            {
                ID = id;

                if (dic == null)
                {
                    dic = new List<TestType>();
                }
                dic.Add(this);
            }

            public static void ForEach(Action<TestType> action)
            {
                foreach (var testType in dic)
                {
                    action(testType);
                }
            }

            public void Start()
            {
                Form form = null;
                if (ID == Image.ID)
                {
                    form = new ImageTestForm();
                }
                else if (ID == Event.ID)
                {
                    form = new EventTestForm();
                }
                else if (ID == Plugin.ID)
                {
                    form = new PluginTest.PluginTestForm();
                }
                else if (ID == Child.ID)
                {
                    form = new DerivedTestForm();
                }
                else if (ID == Misc.ID)
                {
                    var prg = new Program();
                    prg.ExecuteMiscTest();
                    return;
                }

                if (form == null)
                {
                    return;
                }

                form.ShowDialog();
                form.Dispose();
            }
        }

        /// <summary>選択されたテスト</summary>
        public TestType SelectedTest { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.SelectedTest = null;
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.btnMiscTest;
        }

        /// <summary>
        /// ボタンクリック
        /// </summary>
        private void Button_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if (btn == this.btnImageTest)
            {
                this.SelectedTest = TestType.Image;
            }
            else if (btn == this.btnEventTest)
            {
                this.SelectedTest = TestType.Event;
            }
            else if (btn == this.btnPluginTest)
            {
                this.SelectedTest = TestType.Plugin;
            }
            else if (btn == this.btnChildTest)
            {
                this.SelectedTest = TestType.Child;
            }
            else if (btn == this.btnMiscTest)
            {
                this.SelectedTest = TestType.Misc;
            }
            else
            {
                this.SelectedTest = null;
            }
            Close();
        }
    }
}