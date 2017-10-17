#region using ディレクティブ

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;

#endregion // using ディレクティブ

namespace Experiments
{
    public class Program
    {
        #region デリゲート

        private delegate string TestDelegate(int num);

        #endregion // デリゲート

        #region 列挙体、定数

        /// <summary>
        /// ログ出力オプション
        /// </summary>
        private enum LogOptions
        {
            /// <summary>出力する</summary>
            Output,
            /// <summary>出力しない</summary>
            NoOutput
        }

        /// <summary>接続文字列フォーマット</summary>
        private const string ACCESS_CONN_STRING = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}";
        /// <summary></summary>
        private const string DB_TEST = @"Data\testdb.accdb";

        /// <summary>ログフォルダのパス</summary>
        private const string LOG_DIR_PATH = @"log";
        /// <summary>ログファイル名のフォーマット</summary>
        private const string LOG_FILE_FORMAT = "{0}_{1}.txt";

        /// <summary>実行する</summary>
        private const bool EXECUTE = true;
        /// <summary>実行をスキップ</summary>
        private const bool SKIP = false;


        #endregion // 列挙体、定数

        /// <summary>デザイナ表示エラー防止のためのデザインモード判定フラグ (true: デザインモード / false: 通常の実行)</summary>
        /// <remarks>
        /// VisualStudioの仕様でデザイナ表示時には基底クラスのデフォルトコンストラクタが自動的に実行され、これが原因で
        /// デザイナエラーが発生するケースが(多々)ある。
        /// 画面に配置したカスタムコントロール(プラグイン向けの派生クラス)でデザイナエラーが発生する場合に、
        /// 最初にコンストラクタが実行される親画面でデザインモードを判定し、この変数を更新することで
        /// デザインモード判定を共有することができるようになり、デザイナエラーが容易に回避できるようになる。
        /// </remarks>
        public static bool IsDesignMode = true;

        #region privateメンバ

        /// <summary>テスト用の数値メンバ</summary>
        private int number;

        #endregion // privateメンバ

        #region Main
        /// <summary>
        /// メイン
        /// </summary>
        /// <param name="args">コマンドライン引数</param>
        static void Main(string[] args)
        {
            IsDesignMode = false;

            MainForm.TestType test = null;
            using (var form = new MainForm())
            {
                form.ShowDialog();
                test = form.SelectedTest;
            }

            if (test == null)
            {
                return;
            }

            test.Start();
        }
        #endregion Main

        #region ExecuteMiscTest
        /// <summary>
        /// 各種テストを実施
        /// </summary>
        public void ExecuteMiscTest()
        {
            FreeTest(LogOptions.Output);  // 常時実行、ログ出力有無を指定

            XpathTest(EXECUTE);
            XmlSaveTest(EXECUTE);

            AccessDBTest(EXECUTE);
            ComputeTest(EXECUTE);

            FinallyTest(EXECUTE);
            DelegateTest(EXECUTE);
            DelegateSetTest(EXECUTE);
            InitializeComponentTest(EXECUTE);
            PolymorphismTest(EXECUTE);
            IsInstanceOfTypeTest(EXECUTE);
            ProtertyDefaultValueTest(EXECUTE);
            EnumTest(EXECUTE);
            EnumTest2(EXECUTE);
            CastTest(EXECUTE);
            TryParseTest(EXECUTE);
            LinqTest(EXECUTE);
            LinqTest2(EXECUTE);
            LinqTest3(EXECUTE);
            LinqTest4(EXECUTE);
            LinqTest5(EXECUTE);
            BoolTest(EXECUTE);
            CollectionTest(EXECUTE);
            AddRangeTest(EXECUTE);
            TypeSafeEnumTest(EXECUTE);
            RegExTest(EXECUTE);
            DateTimeTest(EXECUTE);
            TimeSpanTest(EXECUTE);
            FormatTest(EXECUTE);
            IndexOfTest(EXECUTE);
            SplitStringTest(EXECUTE);
            GetNumberOfDecimalPlacesTest(EXECUTE);
            ToStringTest(EXECUTE);
            ToStringFormattingTest(EXECUTE);
            AppendLineTest(EXECUTE);

            PathTest(EXECUTE);
            PathTest2(EXECUTE);
            FileAndDirNameTest(EXECUTE);
            IOTest(EXECUTE);
            IOTest2(EXECUTE);
            IOTest3(EXECUTE);
            FileCreateTest(EXECUTE);
            LargeFileCopyTest(EXECUTE);
            LockFileTest(EXECUTE);
            EnvironmentTest(EXECUTE);
            BitOperationTest(EXECUTE);
            RndTest(EXECUTE);
            IncrementTest(EXECUTE);
            MathRoundTest(EXECUTE);

            if (EXECUTE)
            {
                System.Diagnostics.Process.Start(LOG_DIR_PATH);
            }
        }
        #endregion // ExecuteMiscTest

        #region FreeTest
        /// <summary>
        /// 任意の動作確認用メソッド
        /// </summary>
        /// <param name="optLog">ログ出力オプション</param>
        private void FreeTest(LogOptions optLog)
        {
            var sb = new StringBuilder();

            Action<string> showLogSection = (sectionName =>
            {
                string dtStr = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                AppendLine(sb, "{0}", "".PadLeft(sectionName.Length * 3 + dtStr.Length, '*'));
                AppendLine(sb, "{0}{1} [{2}]", "".PadLeft(sectionName.Length, ' '), sectionName, dtStr);
                AppendLine(sb, "{0}", "".PadLeft(sectionName.Length * 3 + dtStr.Length, '*'));
                AppendLine(sb, "");
            });

            //--------------------------------------------------------------------------

            showLogSection("int.Parse");
            #region int.Parse

            int iConv;
            try { iConv = int.Parse("350"); }
            catch (Exception ex) { AppendLine(sb, "ERROR: {0}", ex.Message); }

            try { iConv = int.Parse("350.0"); }
            catch (Exception ex) { AppendLine(sb, "ERROR: {0}", ex.Message); }

            try { iConv = int.Parse("350.00001"); }
            catch (Exception ex) { AppendLine(sb, "ERROR: {0}", ex.Message); }

            AppendLine(sb, "");
            AppendLine(sb, "");

            #endregion // int.Parse

            showLogSection("XmlDocument.Load from URL");
            #region XmlDocument.Load from URL

            //try
            //{
            //    string[] url = new[]{@"http://xxx.xxx.xxx.xxx:#####/aaaaa.xml/",
            // @"http://xxx.xxx.xxx.xxx:#####/bbbbb.xml/"};
            //    var xd = new XmlDocument();
            //    AppendLine(sb, "xd.Load({0})", url[0]);
            //    xd.Load(url[0]);
            //    sb.AppendLine(xd.OuterXml);

            //    AppendLine(sb, "- - - - - - - - - - - - - - -", "");
            //    AppendLine(sb, "xd.Load({0})", url[1]);
            //    xd.Load(url[1]);
            //    sb.AppendLine(xd.OuterXml);
            //}
            //catch (Exception ex)
            //{
            //    AppendLine(sb, "ERROR[{0}]: {1}", ex.GetType(), ex.Message);
            //}

            //AppendLine(sb, "");
            //AppendLine(sb, "");

            #endregion // XmlDocument.Load from URL

            showLogSection("Directory.Exists for drive");
            #region Directory.Exists for drive

            try
            {
                string dir = @"C:\";
                bool hasDrive = false;
                hasDrive = Directory.Exists(@"c:");
                string dirName = Path.GetDirectoryName(dir);
            }
            catch (Exception)
            {
            }

            AppendLine(sb, "");
            AppendLine(sb, "");

            #endregion // Directory.Exists for drive

            showLogSection("Directory.GetLogicalDrives");
            #region Directory.GetLogicalDrives

            try
            {
                var drives = Directory.GetLogicalDrives();
                foreach (string dr in drives)
                {
                    AppendLine(sb, "{0}", dr);
                }
            }
            catch (Exception)
            {
            }

            AppendLine(sb, "");
            AppendLine(sb, "");

            #endregion // Directory.GetLogicalDrives

            showLogSection("Point.X, Point.Y");
            #region Point.X, Point.Y

            try
            {
                var p = new System.Drawing.Point(130000, 190000);
                int x = p.X;
                int y = p.Y;
            }
            catch (Exception)
            {
            }

            AppendLine(sb, "");
            AppendLine(sb, "");

            #endregion // Point.X, Point.Y

            showLogSection("string.IndexOf");
            #region string.IndexOf

            try
            {
                AppendLine(sb, "\"abcdabx\".IndexOf(\"abc\") = {0}", "abcdabx".IndexOf("abc"));
                AppendLine(sb, "\"abcdabx\".IndexOf(\"ab\") = {0}", "abcdabx".IndexOf("ab"));
                AppendLine(sb, "\"abcdabx\".IndexOf(\"bc\") = {0}", "abcdabx".IndexOf("bc"));
                AppendLine(sb, "\"abcdabx\".IndexOf(\"x\") = {0}", "abcdabx".IndexOf("x"));
                AppendLine(sb, "\"abcdabx\".IndexOf(\"abcde\") = {0}", "abcdabx".IndexOf("abcde"));
                AppendLine(sb, "\"abcdabx\".IndexOf(\"f\") = {0}", "abcdabx".IndexOf("f"));
            }
            catch (Exception)
            {
            }

            AppendLine(sb, "");
            AppendLine(sb, "");

            #endregion // string.IndexOf

            showLogSection("Registry");
            #region Registry

            try
            {
                var hklm = Registry.LocalMachine;
                using (var microsoftKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft"))
                {
                    foreach (string subKeyName in microsoftKey.GetSubKeyNames())
                    {
                        using (var subKey = microsoftKey.OpenSubKey(subKeyName))
                        {
                            foreach (string subSubKeyName in subKey.GetSubKeyNames())
                            {
                                using (var rKey = subKey.OpenSubKey(subSubKeyName))
                                {
                                    AppendLine(sb, "Key: {0}", rKey.ToString());
                                    foreach (string name in rKey.GetValueNames())
                                    {
                                        string value = rKey.GetValue(name).ToString();
                                        AppendLine(sb, " {0} = {1}", name, value);
                                    }

                                    AppendLine(sb, "");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppendLine(sb, "ERROR: {0}", ex.Message);
            }

            AppendLine(sb, "");
            AppendLine(sb, "");

            #endregion // Registry

            showLogSection("Factorization");
            #region Factorization
            try
            {
                int minNum = 1;
                int numCount = 200;
                for (int i = 0; i < numCount; i++)
                {
                    int num = minNum + i;
                    var factors = TestMath.Factorize(num);

                    string indent = (1 < factors.Length) ? "  " : "";
                    sb.AppendFormat("{0}{1} = ", indent, num);
                    for (int factId = 0; factId < factors.Length; factId++)
                    {
                        sb.Append((factId == 0) ? "" : " * ");
                        sb.Append(factors[factId]);
                    }
                    sb.AppendLine("");
                }
            }
            catch (Exception ex)
            {
            }

            AppendLine(sb, "");
            AppendLine(sb, "");

            #endregion // Factorization

            showLogSection("AutoCast");
            #region AutoCast

            AppendLine(sb, "[int]   (2 - 1) / 7 + 10 = {0}", (2 - 1) / 7 + 10);
            AppendLine(sb, "[int]   (3 - 1) / 7 + 10 = {0}", (3 - 1) / 7 + 10);
            AppendLine(sb, "[int]   (4 - 1) / 7 + 10 = {0}", (4 - 1) / 7 + 10);
            AppendLine(sb, "[int]   (5 - 1) / 7 + 10 = {0}", (5 - 1) / 7 + 10);
            AppendLine(sb, "[int]   (6 - 1) / 7 + 10 = {0}", (6 - 1) / 7 + 10);
            AppendLine(sb, "[int]   (7 - 1) / 7 + 10 = {0}", (7 - 1) / 7 + 10);
            AppendLine(sb, "[int]   (8 - 1) / 7 + 10 = {0}", (8 - 1) / 7 + 10);
            AppendLine(sb, "[int]   (9 - 1) / 7 + 10 = {0}", (9 - 1) / 7 + 10);
            AppendLine(sb, "");

            AppendLine(sb, "[float] (2 - 1) / 7.0f + 10 = {0}", (2 - 1) / 7.0f + 10);
            AppendLine(sb, "[float] (3 - 1) / 7.0f + 10 = {0}", (3 - 1) / 7.0f + 10);
            AppendLine(sb, "[float] (4 - 1) / 7.0f + 10 = {0}", (4 - 1) / 7.0f + 10);
            AppendLine(sb, "[float] (5 - 1) / 7.0f + 10 = {0}", (5 - 1) / 7.0f + 10);
            AppendLine(sb, "[float] (6 - 1) / 7.0f + 10 = {0}", (6 - 1) / 7.0f + 10);
            AppendLine(sb, "[float] (7 - 1) / 7.0f + 10 = {0}", (7 - 1) / 7.0f + 10);
            AppendLine(sb, "[float] (8 - 1) / 7.0f + 10 = {0}", (8 - 1) / 7.0f + 10);
            AppendLine(sb, "[float] (9 - 1) / 7.0f + 10 = {0}", (9 - 1) / 7.0f + 10);
            AppendLine(sb, "");

            AppendLine(sb, "[float -> int] (int)((2 - 1) / 7.0f) + 10 = {0}", (int)((2 - 1) / 7.0f) + 10);
            AppendLine(sb, "[float -> int] (int)((3 - 1) / 7.0f) + 10 = {0}", (int)((3 - 1) / 7.0f) + 10);
            AppendLine(sb, "[float -> int] (int)((4 - 1) / 7.0f) + 10 = {0}", (int)((4 - 1) / 7.0f) + 10);
            AppendLine(sb, "[float -> int] (int)((5 - 1) / 7.0f) + 10 = {0}", (int)((5 - 1) / 7.0f) + 10);
            AppendLine(sb, "[float -> int] (int)((6 - 1) / 7.0f) + 10 = {0}", (int)((6 - 1) / 7.0f) + 10);
            AppendLine(sb, "[float -> int] (int)((7 - 1) / 7.0f) + 10 = {0}", (int)((7 - 1) / 7.0f) + 10);
            AppendLine(sb, "[float -> int] (int)((8 - 1) / 7.0f) + 10 = {0}", (int)((8 - 1) / 7.0f) + 10);
            AppendLine(sb, "[float -> int] (int)((9 - 1) / 7.0f) + 10 = {0}", (int)((9 - 1) / 7.0f) + 10);
            AppendLine(sb, "");

            AppendLine(sb, "[float] 10 - 2.1f = {0}", 10 - 2.1f);
            AppendLine(sb, "[float] 10 - 3.2f = {0}", 10 - 3.2f);
            AppendLine(sb, "[float] 10 - 4.3f = {0}", 10 - 4.3f);
            AppendLine(sb, "[float] 10 - 5.4f = {0}", 10 - 5.4f);
            AppendLine(sb, "[float] 10 - 6.5f = {0}", 10 - 6.5f);
            AppendLine(sb, "[float] 10 - 7.6f = {0}", 10 - 7.6f);
            AppendLine(sb, "[float] 10 - 8.7f = {0}", 10 - 8.7f);
            AppendLine(sb, "[float] 10 - 9.8f = {0}", 10 - 9.8f);
            AppendLine(sb, "");

            AppendLine(sb, "[float] 2.1f + 10 = {0}", 2.1f + 10);
            AppendLine(sb, "[float] 3.2f + 10 = {0}", 3.2f + 10);
            AppendLine(sb, "[float] 4.3f + 10 = {0}", 4.3f + 10);
            AppendLine(sb, "[float] 5.4f + 10 = {0}", 5.4f + 10);
            AppendLine(sb, "[float] 6.5f + 10 = {0}", 6.5f + 10);
            AppendLine(sb, "[float] 7.6f + 10 = {0}", 7.6f + 10);
            AppendLine(sb, "[float] 8.7f + 10 = {0}", 8.7f + 10);
            AppendLine(sb, "[float] 9.8f + 10 = {0}", 9.8f + 10);

            AppendLine(sb, "");
            AppendLine(sb, "");

            #endregion // AutoCast

            showLogSection("Mod calculation");
            #region Mod calculation

            AppendResultLogLine(sb, "3.55 % 1.0", 3.55 % 1.0);
            AppendResultLogLine(sb, "3.55 % 1.2", 3.55 % 1.2);
            AppendResultLogLine(sb, "3.55f % 1.0f", 3.55f % 1.0f);
            AppendResultLogLine(sb, "3.55f % 1.2f", 3.55f % 1.2f);
            AppendResultLogLine(sb, "3.55f % 1.0d", 3.55f % 1.0d);
            AppendResultLogLine(sb, "3.55f % 1.2d", 3.55f % 1.2d);
            AppendLine(sb, "");

            AppendLine(sb, "");
            AppendLine(sb, "");

            #endregion // Mod calculation

            showLogSection("Designer test from .exe file");
            #region Designer test from .exe file

            DesignerTestForm1 designerTestForm;
            try
            {
                designerTestForm = new DesignerTestForm2();
                designerTestForm.ShowDialog();
            }
            catch (Exception ex)
            {
                AppendLine(sb, "[1] ERROR @ DesignerTestForm2 : {0}", ex.Message);
            }
            try
            {
                designerTestForm = new DesignerTestForm3();
                designerTestForm.ShowDialog();
            }
            catch (Exception ex)
            {
                AppendLine(sb, "[2] ERROR @ DesignerTestForm3 : {0}", ex.Message);
            }

            DesignerTestForm1.Err = new DesignerTestForm1.ErrorInvoker();

            try
            {
                designerTestForm = new DesignerTestForm2();
                designerTestForm.ShowDialog();
            }
            catch (Exception ex)
            {
                AppendLine(sb, "[3] ERROR @ DesignerTestForm2 : {0}", ex.Message);
            }
            try
            {
                designerTestForm = new DesignerTestForm3();
                designerTestForm.ShowDialog();
            }
            catch (Exception ex)
            {
                AppendLine(sb, "[4] ERROR @ DesignerTestForm3 : {0}", ex.Message);
            }

            #endregion // Designer test from .exe file

            if (optLog == LogOptions.Output) { ToFile(sb, "FreeTest", "yyyyMMdd_HHmmss"); }
            return;
        }
        #endregion // FreeTest

        #region XpathTest
        /// <summary>
        /// XPATHの動作確認
        /// </summary>
        private void XpathTest(bool execute)
        {
            if (!execute) { return; }

            string xml = @"
<?xml version=""1.0"" encoding=""UTF-8""?>
<root>
  <level1 id=""1"">
    <level2 id=""1"" value=""XXX"" />
    <level2 id=""2"" value="""" />
    <level2 id=""3"" />
  </level1>
  <level1 id=""2"">
    <level2 id=""1"" value=""YYY"" />
    <level2 id=""2"" value="""" />
    <level2 id=""3"" />
  </level1>
  <level1 id=""3"">
    <level2 id=""1"" value=""ZZZ""/>
    <level2 id=""2"" value="""" />
    <level2 id=""3"" value=""AAA""/>
    <level2 id=""3"" />
  </level1>
</root>
".Replace("\r\n", "");

            var xd = new XmlDocument();
            xd.LoadXml(xml);

            var sb = new StringBuilder();
            string xPath = "/root/level1[@id='{0}']/level2[@id='{1}']";
            int levelOne;
            int levelTwo;
            for (levelOne = 1; levelOne <= 3; levelOne++)
            {
                for (levelTwo = 1; levelTwo <= 3; levelTwo++)
                {
                    var xe = xd.SelectSingleNode(string.Format(xPath, levelOne, levelTwo)) as XmlElement;

                    sb.AppendLine(string.Format("level1/level2 = {0}/{1}", levelOne, levelTwo));
                    sb.AppendLine(string.Format("value = \"{0}\"", xe.GetAttribute("value")));
                    sb.AppendLine(string.Format("has value = \"{0}\"", xe.HasAttribute("value") ? "Y" : "N"));
                    sb.AppendLine("- - - - -");
                }
            }

            xPath = "/root/level1/level2[@id='{0}']";
            for (levelTwo = 1; levelTwo <= 3; levelTwo++)
            {
                sb.AppendLine(string.Format(xPath, levelTwo));
                var nodes = xd.SelectNodes(string.Format(xPath, levelTwo));
                foreach (XmlNode xn in nodes)
                {
                    var xe = xn as XmlElement;
                    sb.AppendLine(string.Format("{0} @ {1}", xe.OuterXml, xe.ParentNode.OuterXml));
                }
                sb.AppendLine("- - - - -");
            }


            xml = @"
<?xml version=""1.0"" encoding=""UTF-8""?>
<root>
  <level1 id=""1"" />
  <level1 id=""2"" />
  <level1 />
  <level1 id=""15"" />
  <level1 id="""" />
  <level1 id=""3"" />
</root>
".Replace("\r\n", "");
            xd.LoadXml(xml);

            var xElems = xd.SelectNodes("/root/level1").Cast<XmlElement>();
            sb.AppendLine("NOT SORTED");
            foreach (var xe in xElems)
            {
                sb.AppendLine(string.Format("level1[@Id='{0}']", xe.GetAttribute("id")));
            }
            sb.AppendLine("- - - - -");

            xElems = xd.SelectNodes("/root/level1").Cast<XmlElement>().OrderBy(xe => xe.GetAttribute("id")).Select(xn => xn);
            sb.AppendLine("SORTED AS STRING");
            foreach (var xe in xElems)
            {
                sb.AppendLine(string.Format("level1[@Id='{0}']", xe.GetAttribute("id")));
            }
            sb.AppendLine("- - - - -");

            //xElems = xd.SelectNodes("/root/level1").Cast<XmlElement>().OrderBy(xe => int.Parse(xe.GetAttribute("id"))).Select(xn => xn);
            //sb.AppendLine("SORTED AS INT");
            //foreach (var xe in xElems)
            //{
            // sb.AppendLine(string.Format("level1[@Id='{0}']", xe.GetAttribute("id")));
            //}

            try
            {
                xml = @"
<?xml version=""1.0"" encoding=""UTF-8""?>
<root>
</root>
<root2>
</root2>
".Replace("\r\n", "");
                xd.LoadXml(xml);
            }
            catch (Exception ex)
            {
                sb.AppendLine(ex.Message);
            }

            xml = @"
<?xml version=""1.0"" encoding=""UTF-8""?>
<root>
  <level1 id=""1"" />
  <level1 id=""2"" />
  <level1 />
  <level1 id=""15"" />
  <level1 id="""" />
  <level1 id=""3"" />
</root>
".Replace("\r\n", "");
            xd.LoadXml(xml);
            sb.AppendFormat("ROOT ELEMENT : {0}", xd.LastChild.Name);

            ToFile(sb, "XpathTest");
            return;
        }
        #endregion // XpathTest

        #region XmlSaveTest
        /// <summary>
        /// XPATHの動作確認
        /// </summary>
        private void XmlSaveTest(bool execute)
        {
            if (!execute) { return; }

            string xml = @"
<?xml version=""1.0"" encoding=""UTF-8""?>
<root>
  <level1 id=""1"">
    <level2 id=""1"" value=""XXX"" />
    <level2 id=""2"" value="""" />
    <level2 id=""3"" />
  </level1>
  <level1 id=""2"">
    <level2 id=""1"" value=""YYY"" />
    <level2 id=""2"" value="""" />
    <level2 id=""3"" />
  </level1>
  <level1 id=""3"">
    <level2 id=""1"" value=""ZZZ"" />
    <level2 id=""2"" value="""" />
    <level2 id=""3"" value=""AAA""/>
    <level2 id=""3"" />
  </level1>
</root>
".Replace("\r\n", "");

            var xd = new XmlDocument();
            xd.LoadXml(xml);

            string filePath = Path.Combine(LOG_DIR_PATH, "XmlSaveTest.xml");
            if (!Directory.Exists(LOG_DIR_PATH))
            {
                Directory.CreateDirectory(LOG_DIR_PATH);
            }
            xd.Save(filePath);
            return;
        }
        #endregion // XmlSaveTest

        #region AccessDBTest

        /// <summary>
        /// ACCESS DBの動作確認
        /// スキーマ情報取得
        /// </summary>
        private void AccessDBTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();
            string dbPath = DB_TEST;
            string connectionString = string.Format(ACCESS_CONN_STRING, dbPath);
            OleDbConnection conn = null;

            try
            {
                conn = new OleDbConnection(connectionString);
                conn.Open();

                // スキーマ情報取得
                var dtTable = conn.GetSchema("Tables", new string[] { null, null, null });

                foreach (DataRow dr in dtTable.Rows)
                {
                    if (dr["TABLE_TYPE"].ToString().ToUpper() != "TABLE")
                    {
                        continue;
                    }

                    sb.AppendLine(string.Format("[ {0} ]", dr["TABLE_NAME"]));

                    var dv = new DataView(conn.GetSchema("Columns", new string[] { null, null, dr["TABLE_NAME"].ToString(), null }));
                    dv.Sort = "ORDINAL_POSITION ASC";
                    var dt = dv.ToTable();

                    var sbRow = new StringBuilder();
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        sbRow.Append(c == 0 ? "" : "\t");
                        sbRow.Append(dt.Columns[c].ColumnName);
                    }
                    sb.AppendLine(sbRow.ToString());

                    foreach (DataRow row in dt.Rows)
                    {
                        sbRow.Clear();
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            sbRow.Append(c == 0 ? "" : "\t");
                            sbRow.Append(row[dt.Columns[c].ColumnName].ToString());
                        }
                        sb.AppendLine(sbRow.ToString());
                    }
                    sb.AppendLine("");
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
            }

            string schema = sb.ToString();
            ToFile(schema, "AccessDBTest");
            return;
        }
        #endregion // AccessDBTest

        #region ComputeTest
        /// <summary>
        /// Compute関数の動作確認
        /// </summary>
        private void ComputeTest(bool execute)
        {
            if (!execute) { return; }

            var dt = new DataTable();
            var results = new Dictionary<object, Type>();
            var sb = new StringBuilder();

            string expression = "(1 + 5) / 2";
            var val = dt.Compute(expression, "");
            results.Add(val, val.GetType());
            sb.AppendFormat("{0} = {1} [{2}]\r\n", expression, val.ToString(), val.GetType().Name);

            expression = "(1 + 5) / 7";
            val = dt.Compute(expression, "");
            results.Add(val, val.GetType());
            sb.AppendFormat("{0} = {1} [{2}]\r\n", expression, val.ToString(), val.GetType().Name);

            try
            {
                expression = "(1 + 5) / 0";
                val = dt.Compute(expression, "");
                results.Add(val, val.GetType());
                sb.AppendFormat("{0} = {1} [{2}]\r\n", expression, val.ToString(), val.GetType().Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var inf = double.PositiveInfinity;
            bool isInf = double.IsInfinity(inf);
            isInf = double.IsPositiveInfinity(inf);
            isInf = double.IsNegativeInfinity(inf);

            inf = double.NegativeInfinity;
            isInf = double.IsInfinity(inf);
            isInf = double.IsPositiveInfinity(inf);
            isInf = double.IsNegativeInfinity(inf);

            sb.AppendLine("");
            sb.AppendLine("- - - - - - - - - - - - - - - - - - - -");
            sb.AppendLine("");

            var expList = new List<string>();
            expList.Add("'abc' > 'def'");
            expList.Add("'abc' >= 'abc'");
            expList.Add("'abc' <> 'abc'");
            expList.Add("'abc' = 'abc'");
            expList.Add("'1234' > '234'");
            expList.Add("1234 > 234");
            expList.Add("Cdbl(1234) > Cdbl(234)");
            expList.Add("Cdbl('1234') > Cdbl('234')");
            foreach (string exp in expList)
            {
                try
                {
                    var eval = dt.Compute(exp, "");
                    string evalStr = (bool)eval ? "true" : "false";
                    sb.AppendFormat("{0} = {1} (eval: {2}[{3}])\r\n", exp, evalStr, eval, eval.GetType().Name);
                }
                catch (Exception ex)
                {
                    sb.AppendFormat("{0} = {1}\r\n", exp, ex.Message);
                }
            }

            ToFile(sb, "ComputeTest");
            return;
        }
        #endregion // ComputeTest

        #region RndTest
        /// <summary>
        /// 乱数生成の動作確認
        /// </summary>
        private void RndTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            var seeds = new[] { 1, 2 };
            int count = 5;
            int trial = 5;

            foreach (int seed in seeds)
            {
                AppendLine(sb, "[seed = {0}]", seed);
                for (int j = 0; j < trial; j++)
                {
                    AppendLine(sb, "[trial = {0}]", j + 1);

                    var rnd = new Random(seed);
                    for (int i = 0; i < count * 10; i++)
                    {
                        int newNum = rnd.Next(count);
                        sb.AppendFormat("{0}{1}{2}", (i % (count * 5) == 0) ? "" : ", ",
                                                    newNum,
                                                    ((i + 1) % (count * 5) == 0) ? "\r\n" : "");
                    }
                    AppendLine(sb, "");
                }
            }

            ToFile(sb, "RndTest");
            return;
        }
        #endregion // RndTest

        #region FinallyTest
        /// <summary>
        /// Finally句の動作確認
        /// </summary>
        private void FinallyTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            for (int i = 0; i < 5; i++)
            {
                if (0 < i)
                {
                    AppendLine(sb, "");
                }

                try
                {
                    AppendLine(sb, "try_{0} BEGIN", i);
                    if (i == 0)
                    {
                    }
                    else if (i == 1)
                    {
                        continue;
                    }
                    else if (i == 2 || i == 3)
                    {
                        throw new Exception("ERROR!");
                    }
                    else
                    {
                        return;
                    }
                    AppendLine(sb, "try_{0} END", i);
                }
                catch (Exception ex)
                {
                    if (i == 2)
                    {
                        continue;
                    }
                    AppendLine(sb, "try_{0} catch: {1}", i, ex.Message);
                }
                finally
                {
                    AppendLine(sb, "try_{0} finally", i);
                    if (3 < i)
                    {
                        AppendLine(sb, "try_{0} return --> finally", i);
                        ToFile(sb, "FinallyTest");
                    }
                }
            }

            AppendLine(sb, "END");
            ToFile(sb, "FinallyTest");
            return;
        }
        #endregion // FinallyTest

        #region DelegateTest
        /// <summary>
        /// デリゲートの動作確認
        /// </summary>
        private void DelegateTest(bool execute)
        {
            if (!execute) { return; }
            var sb = new StringBuilder();

            TestDelegate testDel = null;

            Action<int, Action> tryCatch = ((step, doSomething) =>
            {
                try
                {
                    doSomething();
                }
                catch (Exception ex)
                {
                    AppendLine(sb, "ERROR_{0}: {1}", step, ex.Message);
                }
            });
            Action append = () => AppendLine(sb, "this.number = {1} / testDel(1) = {0}", testDel(1), this.number);

            this.number = 0;

            AppendLine(sb, "[1] testDel = null");
            testDel += null;
            AppendLine(sb, "[2] testDel += null");
            tryCatch(2, () => append());


            testDel = IntToStr;
            AppendLine(sb, "[3] testDel = IntToStr");
            append();
            testDel = IntToStr2;
            AppendLine(sb, "[4] testDel = IntToStr2");
            append();

            tryCatch(5, () =>
            {
                testDel += IntToStr;
                AppendLine(sb, "[5a] testDel += IntToStr");
                testDel += IntToStr2;
                AppendLine(sb, "[5b] testDel += IntToStr2");
                append();
            });
            tryCatch(6, () =>
            {
                testDel -= IntToStr2;
                AppendLine(sb, "[6] testDel -= IntToStr2");
                append();
            });
            tryCatch(7, () =>
            {
                testDel -= IntToStr2;
                AppendLine(sb, "[7] testDel -= IntToStr2");
                append();
            });
            tryCatch(8, () =>
            {
                testDel -= IntToStr;
                AppendLine(sb, "[8] testDel -= IntToStr");
                append();
            });
            tryCatch(9, () =>
            {
                testDel += IntToStr2;
                AppendLine(sb, "[9a] testDel += IntToStr2");
                append();
                testDel = null;
                AppendLine(sb, "[9b] testDel = null");
                append();
            });

            ToFile(sb, "DelegateTest");
            return;
        }
        private string IntToStr(int num)
        {
            this.number++;
            return string.Format("IntTostr: {0:00000}", num);
        }

        private string IntToStr2(int num)
        {
            this.number++;
            return string.Format("IntToStr2: {0:#####}", num);
        }
        #endregion // DelegateTest

        #region DelegateSetTest
        /// <summary>
        /// デリゲートの動作確認
        /// </summary>
        private void DelegateSetTest(bool execute)
        {
            if (!execute) { return; }
            var sb = new StringBuilder();

            var sup = new DelegateSupplier();
            AppendLine(sb, "result = {0}", sup.Result);
            AppendLine(sb, "ExecuteAddition(1, 2)");
            sup.ExecuteAddition(1, 2);
            AppendLine(sb, "result = {0}", sup.Result);
            AppendLine(sb, "ExecuteAddition(1, 2)");
            sup.ExecuteAddition(1, 2);
            AppendLine(sb, "result = {0}", sup.Result);

            sup.SetAddition(Addition);

            AppendLine(sb, "ExecuteAddition(1, 2)");
            sup.ExecuteAddition(1, 2);
            AppendLine(sb, "result = {0}", sup.Result);

            sup.SetAddition(Addition);

            AppendLine(sb, "ExecuteAddition(1, 2)");
            sup.ExecuteAddition(1, 2);
            AppendLine(sb, "result = {0}", sup.Result);

            sup.RemoveAddition(Addition);

            AppendLine(sb, "ExecuteAddition(1, 2)");
            sup.ExecuteAddition(1, 2);
            AppendLine(sb, "result = {0}", sup.Result);

            sup.RemoveAddition(Addition);

            AppendLine(sb, "ExecuteAddition(1, 2)");
            sup.ExecuteAddition(1, 2);
            AppendLine(sb, "result = {0}", sup.Result);

            sup.RemoveAddition(Addition);

            AppendLine(sb, "ExecuteAddition(1, 2)");
            sup.ExecuteAddition(1, 2);
            AppendLine(sb, "result = {0}", sup.Result);


            ToFile(sb, "DelegateTest");
            return;
        }
        private void Addition(int left, int right, ref int result)
        {
            result += left * right;
        }
        #endregion // DelegateSetTest

        #region PolymorphismTest
        /// <summary>
        /// デリゲートの動作確認
        /// </summary>
        private void PolymorphismTest(bool execute)
        {
            if (!execute) { return; }
            var sb = new StringBuilder();

            var TypeA1 = new PolymorphismTestClassA1();
            AppendLine(sb, "PolymorphismTestClassA1 --> {0}", TypeA1.Message);
            var TypeA2 = new PolymorphismTestClassA2();
            AppendLine(sb, "PolymorphismTestClassA2 --> {0}", TypeA2.Message);

            var TypeBBase = new BasePolymorphismTestClassB();
            AppendLine(sb, "BasePolymorphismTestClassB --> {0}", TypeBBase.Message);
            var TypeB1 = new PolymorphismTestClassB1();
            AppendLine(sb, "PolymorphismTestClassB1 --> {0}", TypeB1.Message);
            var TypeB2 = new PolymorphismTestClassB2();
            AppendLine(sb, "PolymorphismTestClassB2 --> {0}", TypeB2.Message);

            ToFile(sb, "PolymorphismTest");
            return;
        }
        private abstract class BasePolymorphismTestClassA
        {
            public string Message { get; set; }
            protected abstract string GetTypeOfValue();
            public BasePolymorphismTestClassA()
            {
                this.Message = string.Format("GetType = {0}, GetTypeOfValue() = {1}", this.GetType().Name, GetTypeOfValue());
            }
        }
        private class PolymorphismTestClassA1 : BasePolymorphismTestClassA
        {
            public PolymorphismTestClassA1() : base() { }
            protected override string GetTypeOfValue() { return typeof(PolymorphismTestClassA1).Name; }
        }
        private class PolymorphismTestClassA2 : BasePolymorphismTestClassA
        {
            public PolymorphismTestClassA2() : base() { }
            protected override string GetTypeOfValue() { return typeof(PolymorphismTestClassA2).Name; }
        }
        private class BasePolymorphismTestClassB
        {
            public string Message { get; set; }
            //[Obsolete("You cannot use BasePolymorphismTestClassB.GetTypeOfValue", true)]
            protected virtual string GetTypeOfValue() { return typeof(BasePolymorphismTestClassB).Name; }
            public BasePolymorphismTestClassB()
            {
                this.Message = string.Format("GetType = {0}, GetTypeOfValue() = {1}", this.GetType().Name, GetTypeOfValue());
            }
        }
        private class PolymorphismTestClassB1 : BasePolymorphismTestClassB
        {
            public PolymorphismTestClassB1() : base() { }
            //[Obsolete("You cannot use PolymorphismTestClassB1.GetTypeOfValue", true)]
            protected override string GetTypeOfValue() { return typeof(PolymorphismTestClassB1).Name; }
        }
        private class PolymorphismTestClassB2 : BasePolymorphismTestClassB
        {
            public PolymorphismTestClassB2() : base() { }
            //[Obsolete("You cannot use PolymorphismTestClassB2.GetTypeOfValue", true)]
            protected override string GetTypeOfValue() { return typeof(PolymorphismTestClassB2).Name; }
        }
        #endregion // PolymorphismTest

        #region InitializeComponentTest
        /// <summary>
        /// デリゲートの動作確認
        /// </summary>
        private void InitializeComponentTest(bool execute)
        {
            if (!execute) { return; }
            var sb = new StringBuilder();

            InitializeComponentTestBase.mode = 1;
            AppendLine(sb, "[ new InitializeComponentTestBase ]");
            var baseObj = new InitializeComponentTestBase();
            AppendLine(sb, baseObj.sb.ToString());

            AppendLine(sb, "");
            AppendLine(sb, "");

            AppendLine(sb, "[ new InitializeComponentTestChild3 ]");
            var child1 = new InitializeComponentTestChild3();
            AppendLine(sb, child1.sb.ToString());

            AppendLine(sb, "");
            AppendLine(sb, "");

            InitializeComponentTestBase.mode = 0;
            AppendLine(sb, "[ new InitializeComponentTestBase ]");
            baseObj = new InitializeComponentTestBase();
            AppendLine(sb, baseObj.sb.ToString());

            AppendLine(sb, "");
            AppendLine(sb, "");

            AppendLine(sb, "[ new InitializeComponentTestChild3 ]");
            var child3 = new InitializeComponentTestChild3();
            AppendLine(sb, child3.sb.ToString());

            ToFile(sb, "InitializeComponentTest");
            return;
        }
        protected class InitializeComponentTestBase
        {
            public static int mode { get; set; }
            public StringBuilder sb { get; set; }
            public InitializeComponentTestBase()
            {
                this.sb = new StringBuilder();
                if (mode == 1)
                {
                    AbstractInitializeComponent();
                }
                else
                {
                    AbstractSubTest();
                }
            }
            private void InitializeComponent()
            {
                sb.AppendLine(" InitializeComponentTestBase.InitializeComponent");
            }
            protected virtual void AbstractInitializeComponent()
            {
                sb.AppendLine(" InitializeComponentTestBase.AbstractInitializeComponent");
                InitializeComponent();
            }
            protected virtual void AbstractSubTest()
            {
                sb.AppendLine(" InitializeComponentTestBase.AbstractSubTest");
                InitializeComponent();
            }
        }
        private class InitializeComponentTestChild1 : InitializeComponentTestBase
        {
            private void InitializeComponent()
            {
                sb.AppendLine(" InitializeComponentTestChild1.InitializeComponent");
            }
            protected override void AbstractInitializeComponent()
            {
                sb.AppendLine(" InitializeComponentTestChild1.AbstractInitializeComponent");
                base.AbstractInitializeComponent();
                InitializeComponent();
            }
            protected override void AbstractSubTest()
            {
                sb.AppendLine(" InitializeComponentTestChild1.AbstractSubTest");
                base.AbstractSubTest();
                InitializeComponent();
            }
        }
        private class InitializeComponentTestChild2 : InitializeComponentTestChild1
        {
            private void InitializeComponent()
            {
                sb.AppendLine(" InitializeComponentTestChild2.InitializeComponent");
            }
            protected override void AbstractInitializeComponent()
            {
                sb.AppendLine(" InitializeComponentTestChild2.AbstractInitializeComponent");
                base.AbstractInitializeComponent();
                InitializeComponent();
            }
        }
        private class InitializeComponentTestChild3 : InitializeComponentTestChild2
        {
            private void InitializeComponent()
            {
                sb.AppendLine(" InitializeComponentTestChild3.InitializeComponent");
            }
            protected override void AbstractInitializeComponent()
            {
                sb.AppendLine(" InitializeComponentTestChild3.AbstractInitializeComponent");
                base.AbstractInitializeComponent();
                InitializeComponent();
            }
        }
        #endregion // InitializeComponentTest

        #region IsInstanceOfTypeTest
        /// <summary>
        /// 派生クラス確認の動作確認
        /// </summary>
        private void IsInstanceOfTypeTest(bool execute)
        {
            if (!execute) { return; }
            var sb = new StringBuilder();

            var xd = new XmlDocument();
            xd.LoadXml(@"
<?xml version=""1.0"" encoding=""UTF-8""?>
<root>
  <level1 id=""1"">
    <level2 id=""1"" value=""XXX"" />
    <level2 id=""2"" value="""" />
    <level2 id=""3"" />
  </level1>
</root>
".Replace("\r\n", ""));

            AppendLine(sb, "typeof(XmlNode ).IsInstanceOfType(xd) = {0}", typeof(XmlNode).IsInstanceOfType(xd));

            var xn = xd.SelectSingleNode("root/level1");
            AppendLine(sb, "typeof(XmlDocument).IsInstanceOfType(xn) = {0}", typeof(XmlDocument).IsInstanceOfType(xn));

            ToFile(sb, "IsInstanceOfTypeTest");
            return;
        }
        #endregion // IsInstanceOfTypeTest

        #region ProtertyDefaultValueTest

        private class PropertyTestClass
        {
            public delegate void PropertyTestDelegate(int input);
            public enum PropertyTestEnum
            {
                A = 0,
                B,
                X = -999
            }
            public struct PropertyTestStruct
            {
                public int IntProperty { get; set; }
                public float FloatProperty { get; set; }
                public bool BooleanProperty { get; set; }
                public string StringProperty { get; set; }
                public DateTime DateTimeProperty { get; set; }
                public PropertyTestEnum EnumProperty { get; set; }

                public int? NullableIntProperty { get; set; }
                public DateTime? NullableDateTimeProperty { get; set; }
                public PropertyTestEnum? NullableEnumProperty { get; set; }

                public List<int> ListProperty { get; set; }
                public StringBuilder StringBuilderProperty { get; set; }
                public PropertyTestInnerClass ClassProperty { get; set; }
                public PropertyTestDelegate DelagateProperty { get; set; }
            }
            public class PropertyTestInnerClass
            {
                public int IntProperty { get; set; }
            }

            public int IntProperty { get; set; }
            public long LongProperty { get; set; }
            public float FloatProperty { get; set; }
            public double DoubleProperty { get; set; }
            public bool BooleanProperty { get; set; }
            public string StringProperty { get; set; }
            public DateTime DateTimeProperty { get; set; }
            public PropertyTestEnum EnumProperty { get; set; }
            public PropertyTestStruct StructProperty { get; set; }

            public int? NullableIntProperty { get; set; }
            public DateTime? NullableDateTimeProperty { get; set; }
            public PropertyTestEnum? NullableEnumProperty { get; set; }
            public PropertyTestStruct? NullableStructProperty { get; set; }

            public List<int> ListProperty { get; set; }
            public StringBuilder StringBuilderProperty { get; set; }
            public PropertyTestInnerClass ClassProperty { get; set; }
            public PropertyTestDelegate DelagateProperty { get; set; }
        }

        /// <summary>
        /// プロパティのデフォルト値確認
        /// </summary>
        private void ProtertyDefaultValueTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            var testClass = new PropertyTestClass();

            AppendLine(sb, "testClass.IntProperty = {0}", testClass.IntProperty);
            AppendLine(sb, "testClass.LongProperty = {0}", testClass.LongProperty);
            AppendLine(sb, "testClass.FloatProperty = {0}", testClass.FloatProperty);
            AppendLine(sb, "testClass.DoubleProperty = {0}", testClass.DoubleProperty);
            AppendLine(sb, "testClass.BooleanProperty = {0}", testClass.BooleanProperty);
            AppendLine(sb, "testClass.StringProperty = {0}", testClass.StringProperty);
            AppendLine(sb, "testClass.DateTimeProperty = {0}[Ticks: {1}]", testClass.DateTimeProperty.ToString("yyyy/MM/dd HH:mm:ss")
                                                                , testClass.DateTimeProperty.Ticks);
            AppendLine(sb, "testClass.EnumProperty = {0}", testClass.EnumProperty);
            AppendLine(sb, "");

            AppendLine(sb, "testClass.StructProperty = {0}", testClass.StructProperty);
            AppendLine(sb, "testClass.StructProperty.IntProperty = {0}", testClass.StructProperty.IntProperty);
            AppendLine(sb, "testClass.StructProperty.FloatProperty = {0}", testClass.StructProperty.FloatProperty);
            AppendLine(sb, "testClass.StructProperty.DateTimeProperty = {0}[Ticks: {1}]", testClass.StructProperty.DateTimeProperty.ToString("yyyy/MM/dd HH:mm:ss")
                                                                , testClass.StructProperty.DateTimeProperty.Ticks);
            AppendLine(sb, "testClass.StructProperty.EnumProperty = {0}", testClass.StructProperty.EnumProperty);
            AppendLine(sb, "testClass.StructProperty.NullableIntProperty = {0}", testClass.StructProperty.NullableIntProperty ?? -999);
            AppendLine(sb, "testClass.StructProperty.NullableDateTimeProperty = {0}", testClass.StructProperty.NullableDateTimeProperty == null ? "null" : testClass.StructProperty.NullableDateTimeProperty.ToString());
            AppendLine(sb, "testClass.StructProperty.NullableEnumProperty = {0}", testClass.StructProperty.NullableEnumProperty ?? PropertyTestClass.PropertyTestEnum.X);
            AppendLine(sb, "");

            AppendLine(sb, "testClass.StructProperty.ListProperty = {0}", testClass.StructProperty.ListProperty == null ? "null" : testClass.StructProperty.ListProperty.ToString());
            AppendLine(sb, "testClass.StructProperty.StringBuilderProperty = {0}", testClass.StructProperty.StringBuilderProperty == null ? "null" : testClass.StructProperty.StringBuilderProperty.ToString());
            AppendLine(sb, "testClass.StructProperty.ClassProperty = {0}", testClass.StructProperty.ClassProperty == null ? "null" : testClass.StructProperty.ClassProperty.ToString());
            AppendLine(sb, "testClass.StructProperty.DelagateProperty = {0}", testClass.StructProperty.DelagateProperty == null ? "null" : testClass.StructProperty.DelagateProperty.ToString());
            AppendLine(sb, "");

            AppendLine(sb, "testClass.NullableIntProperty = {0}", testClass.NullableIntProperty ?? -999);
            AppendLine(sb, "testClass.NullableDateTimeProperty = {0}", testClass.NullableDateTimeProperty == null ? "null" : testClass.NullableDateTimeProperty.ToString());
            AppendLine(sb, "testClass.NullableEnumProperty = {0}", testClass.NullableEnumProperty ?? PropertyTestClass.PropertyTestEnum.X);
            AppendLine(sb, "testClass.NullableStructProperty = {0}", testClass.NullableStructProperty == null ? "null" : testClass.NullableStructProperty.ToString());
            AppendLine(sb, "");

            AppendLine(sb, "testClass.ListProperty = {0}", testClass.ListProperty == null ? "null" : testClass.StructProperty.ListProperty.ToString());
            AppendLine(sb, "testClass.StringBuilderProperty = {0}", testClass.StringBuilderProperty == null ? "null" : testClass.StructProperty.StringBuilderProperty.ToString());
            AppendLine(sb, "testClass.ClassProperty = {0}", testClass.ClassProperty == null ? "null" : testClass.StructProperty.ClassProperty.ToString());
            AppendLine(sb, "testClass.DelagateProperty = {0}", testClass.DelagateProperty == null ? "null" : testClass.StructProperty.DelagateProperty.ToString());

            ToFile(sb, "ProtertyDefaultValueTest");
            return;
        }
        #endregion // ProtertyDefaultValueTest

        #region EnumTest

        private enum testEnum
        {
            AAA = 0,
            BBB,
            CCC = 3,
            DDD,
            EEE = 3,
            FFF
        }

        /// <summary>
        /// 列挙体の動作確認
        /// </summary>
        private void EnumTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();
            sb.AppendLine("AAA = " + testEnum.AAA);
            sb.AppendLine("(int)AAA = " + (int)testEnum.AAA);
            sb.AppendLine("AAA.ToString() = " + testEnum.AAA.ToString());
            sb.AppendLine("string.Format(\"{0}\", testEnum.AAA) = " + string.Format("{0}", testEnum.AAA));
            sb.AppendLine("BBB = " + testEnum.BBB);
            sb.AppendLine("(int)BBB = " + (int)testEnum.BBB);
            sb.AppendLine("BBB.ToString() = " + testEnum.BBB.ToString());
            sb.AppendLine("string.Format(\"{0}\", testEnum.BBB) = " + string.Format("{0}", testEnum.BBB));
            sb.AppendLine("CCC = " + testEnum.CCC);
            sb.AppendLine("(int)CCC = " + (int)testEnum.CCC);
            sb.AppendLine("CCC.ToString() = " + testEnum.CCC.ToString());
            sb.AppendLine("string.Format(\"{0}\", testEnum.CCC) = " + string.Format("{0}", testEnum.CCC));

            ToFile(sb, "EnumTest");
            return;
        }
        #endregion // EnumTest

        #region EnumTest2

        private int IntArgMethod(int intArg)
        {
            return intArg;
        }

        /// <summary>
        /// 列挙体の動作確認 2
        /// </summary>
        private void EnumTest2(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();
            sb.AppendFormat("IntArgMethod({0}) = {1} \r\n", "(int)testEnum.AAA", IntArgMethod((int)testEnum.AAA));
            sb.AppendFormat("IntArgMethod({0}) = {1} \r\n", "(int)testEnum.BBB", IntArgMethod((int)testEnum.BBB));
            sb.AppendFormat("IntArgMethod({0}) = {1} \r\n", "(int)testEnum.CCC", IntArgMethod((int)testEnum.CCC));
            sb.AppendFormat("IntArgMethod({0}) = {1} \r\n", "(int)testEnum.DDD", IntArgMethod((int)testEnum.DDD));
            sb.AppendFormat("IntArgMethod({0}) = {1} \r\n", "(int)testEnum.EEE", IntArgMethod((int)testEnum.EEE));
            sb.AppendFormat("IntArgMethod({0}) = {1} \r\n", "(int)testEnum.FFF", IntArgMethod((int)testEnum.FFF));

            ToFile(sb, "EnumTest2");
            return;
        }
        #endregion // EnumTest2

        #region CastTest
        /// <summary>
        /// キャストの動作確認
        /// </summary>
        private void CastTest(bool execute)
        {
            if (!execute) { return; }

            double numDbl = 19 / 7.0;

            int numInt = (int)numDbl;
            //numInt = numDbl;

            numDbl = (double)numInt;
            numDbl = numInt;

            numDbl = (long)numInt;

            numInt = (int)4.0;
            numInt = (int)2.5;

            Func<string, bool> isIntStr = (numStr =>
            {
                double dbl;
                if (!double.TryParse(numStr, out dbl))
                {
                    return false;
                }
                return dbl == (double)(int)dbl;
            });

            var sb = new StringBuilder();
            string str = "1.2";
            sb.AppendFormat("{0} : {1}\r\n", str, isIntStr(str) ? "int string" : "not int string");
            str = "1.0";
            sb.AppendFormat("{0} : {1}\r\n", str, isIntStr(str) ? "int string" : "not int string");
            str = "2";
            sb.AppendFormat("{0} : {1}\r\n", str, isIntStr(str) ? "int string" : "not int string");
            str = "2.5";
            sb.AppendFormat("{0} : {1}\r\n", str, isIntStr(str) ? "int string" : "not int string");
            str = "8.00001";
            sb.AppendFormat("{0} : {1}\r\n", str, isIntStr(str) ? "int string" : "not int string");
            str = "8.00000";
            sb.AppendFormat("{0} : {1}\r\n", str, isIntStr(str) ? "int string" : "not int string");

            str = "8.00000";
            double ceiling = Math.Ceiling(double.Parse(str));
            sb.AppendFormat("[str]{0} -> [dbl]{1} -> [dblUP]{2} -> [intUP]{3}\r\n",
                                str, double.Parse(str), ceiling, (int)ceiling);
            str = "8.00001";
            ceiling = Math.Ceiling(double.Parse(str));
            sb.AppendFormat("[str]{0} -> [dbl]{1} -> [dblUP]{2} -> [intUP]{3}\r\n",
                                str, double.Parse(str), ceiling, (int)ceiling);

            int intVal;
            bool isInt = int.TryParse("1.1", out intVal);

            ToFile(sb, "CastTest");
            return;
        }
        #endregion // CastTest

        #region TryParseTest
        /// <summary>
        /// キャストの動作確認
        /// </summary>
        private void TryParseTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            var targets = new List<string>();
            targets.Add(null);
            targets.Add("");
            targets.Add("-1");
            targets.Add("0");
            targets.Add("1");
            targets.Add("-1.0");
            targets.Add("0.0");
            targets.Add("1.0");
            targets.Add("-0.1");
            targets.Add("0.0");
            targets.Add("0.1");
            targets.Add("a");
            targets.Add("999999999999999999999999999999999999999999999999999999");
            targets.Add("-999999999999999999999999999999999999999999999999999999");

            int intValue;
            for (int i = 0; i < targets.Count; i++)
            {
                string target = targets[i] != null ? "\"" + targets[i] + "\"" : "null";
                try
                {
                    intValue = int.MinValue;
                    bool parseResult = int.TryParse(targets[i], out intValue);
                    AppendLine(sb, "int.TryParse({0}, \tout intValue) = {1} [intValue: {2}]", target, parseResult, intValue);
                }
                catch (Exception ex)
                {
                    AppendLine(sb, "ERROR: {0} [int.TryParse({1}, \tout intValue)]", ex.Message, target);
                }
            }

            AppendLine(sb, "");

            double dblValue;
            for (int i = 0; i < targets.Count; i++)
            {
                string target = targets[i] != null ? "\"" + targets[i] + "\"" : "null";
                try
                {
                    dblValue = double.MinValue;
                    bool parseResult = double.TryParse(targets[i], out dblValue);
                    AppendLine(sb, "double.TryParse({0}, \tout dblValue) = {1} [dblValue: {2}]", target, parseResult, dblValue);
                }
                catch (Exception ex)
                {
                    AppendLine(sb, "ERROR: {0} [double.TryParse({1}, \tout dblValue)]", ex.Message, target);
                }
            }

            ToFile(sb, "TryParseTest");
            return;
        }
        #endregion // TryParseTest
        
        #region LinqTest
        /// <summary>
        /// LINQの動作確認
        /// </summary>
        private void LinqTest(bool execute)
        {
            if (!execute) { return; }

            var xd = new XmlDocument();
            xd.LoadXml(@"
<?xml version=""1.0"" encoding=""UTF-8""?>
<root>
  <level1 id=""x"" />
  <level1 id=""6"" />
  <level1 id=""aa"" />
  <level1 id=""hrnr4"" />
  <level1 id=""3"" />
  <level1 id=""1.0"" />
  <level1 id=""1.5"" />
  <level1 id=""a0"" />
  <level1 id=""1"" />
  <level1 id=""hrnr4"" />
  <level1 id=""1"" />
  <level1 id=""0"" />
</root>
".Replace("\r\n", ""));

            var atList = xd.SelectNodes("root/level1")
                            .Cast<XmlElement>()
                            .Select<XmlElement, int>(xe =>
                            {
                                int id;
                                if (!int.TryParse(xe.GetAttribute("id"), out id))
                                {
                                    return -1;
                                }
                                return id;
                            })
                            .Where(id => !(id < 0))
                //.OrderBy(id => id)
                            .Distinct()
                            .ToList();

            int min = atList.Min();
            int max = atList.Max();

            return;
        }
        #endregion // LinqTest

        #region LinqTest2
        /// <summary>
        /// LINQの動作確認2
        /// </summary>
        private void LinqTest2(bool execute)
        {
            if (!execute) { return; }

            var xd = new XmlDocument();
            xd.LoadXml(@"
<?xml version=""1.0"" encoding=""UTF-8""?>
<root>
<!--
  <level1 id=""x"" />
-->
<!--
  <level1 id=""6"" />
  <level1 id=""aa"" />
  <level1 id=""hrnr4"" />
  <level1 id=""3"" />
  <level1 id=""1.0"" />
  <level1 id=""1.5"" />
  <level1 id=""a0"" />
  <level1 id=""1"" />
  <level1 id=""hrnr4"" />
  <level1 id=""1"" />
  <level1 id=""0"" />
-->
</root>
".Replace("\r\n", ""));

            var list1 = xd.SelectNodes("root/level1")
                            .Cast<XmlElement>()
                            .Select<XmlElement, int>(xe =>
                            {
                                int id;
                                if (!int.TryParse(xe.GetAttribute("id"), out id))
                                {
                                    return -1;
                                }
                                return id;
                            })
                            .Where(id => !(id < 0))
                //.OrderBy(id => id)
                            .Distinct()
                            .ToList();

            int min = int.MinValue;
            int max = int.MaxValue;
            if (0 < list1.Count)
            {
                min = list1.Min();
                max = list1.Max();
            }

            int count2 = 0;
            var list2 = xd.SelectNodes("root/level1")
                            .Cast<XmlElement>();
            foreach (var xe in list2)
            {
                count2++;
            }

            int count3 = 0;
            var list3 = xd.SelectNodes("root/level2")
                            .Cast<XmlElement>();
            foreach (var xe in list3)
            {
                count3++;
            }

            int count4 = 0;
            var list4 = xd.SelectNodes("root/level1");
            try
            {
                foreach (XmlElement xe in list4)
                {
                    count4++;
                }
            }
            catch (Exception)
            {
                count4 = -1;
            }

            try
            {
                var xeLevel1 = xd.SelectSingleNode("root/level1");
                var xeAs = xeLevel1 as XmlElement;
            }
            catch (Exception)
            {
            }

            return;
        }
        #endregion // LinqTest2

        #region LinqTest3
        /// <summary>
        /// LINQの動作確認3 - Whereでの抽出はクローンされるか
        /// </summary>
        private void LinqTest3(bool execute)
        {
            if (!execute) { return; }

            var rnd = new Random();
            var sb = new StringBuilder();

            var dtoList = new List<LinqTest3Dto>();
            dtoList.Add(new LinqTest3Dto(1, "name1"));
            dtoList.Add(new LinqTest3Dto(2, "name2"));
            dtoList.Add(new LinqTest3Dto(3, "name3"));
            dtoList.Add(new LinqTest3Dto(4, string.Format("name{0}", rnd.Next(4, 10))));
            dtoList.Add(new LinqTest3Dto(5, string.Format("name{0}", rnd.Next(4, 10))));
            dtoList.Add(new LinqTest3Dto(6, string.Format("name{0}", rnd.Next(4, 10))));

            AppendLine(sb, "dtoList:");
            foreach (var dto in dtoList)
            {
                AppendLine(sb, dto.ToString());
            }
            AppendLine(sb, "");

            var selectedDtos = dtoList.Where(dto => dto.ID < 7).OrderBy(dto => dto.Name).ToList();
            AppendLine(sb, "selectedDtos :");
            for (int i = 0; i < selectedDtos.Count; i++)
            {
                selectedDtos[i].Name += "_mod";
                AppendLine(sb, selectedDtos[i].ToString());
            }
            AppendLine(sb, "");

            AppendLine(sb, "dtoList:");
            foreach (var dto in dtoList)
            {
                AppendLine(sb, dto.ToString());
            }

            ToFile(sb, "LinqTest3");
            return;
        }
        private class LinqTest3Dto
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public LinqTest3Dto(int id, string name)
            {
                this.ID = id;
                this.Name = name;
            }
            public override string ToString()
            {
                return string.Format("[{0}] {1}", this.ID, this.Name);
            }
        }
        #endregion // LinqTest3

        #region LinqTest4
        /// <summary>
        /// LINQの動作確認4 - Distinctの挙動
        /// </summary>
        private void LinqTest4(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            var rnd = new Random();
            var nums = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                nums.Add(rnd.Next(10));
            }

            var distinctNumList = nums.Distinct().ToList();
            var distinctNumArray = nums.Distinct().ToArray();
            string numsJoined = string.Join(", ", nums);
            string distinctNumListJoined = (distinctNumList == null) ? "null" :
(distinctNumList.Count == 0) ? "Empty" : string.Join(", ", distinctNumList);
            string distinctNumArrayJoined = (distinctNumArray == null) ? "null" :
(distinctNumArray.Length == 0) ? "Empty" : string.Join(", ", distinctNumArray);
            AppendLine(sb, "nums = [{0}]", numsJoined);
            AppendLine(sb, "nums.Distinct.ToList = [{0}]", distinctNumListJoined);
            AppendLine(sb, "nums.Distinct.ToArray = [{0}]", distinctNumArrayJoined);

            nums.Clear();
            AppendLine(sb, "");

            distinctNumList = nums.Distinct().ToList();
            distinctNumArray = nums.Distinct().ToArray();
            numsJoined = string.Join(", ", nums);
            distinctNumListJoined = (distinctNumList == null) ? "null" :
                                (distinctNumList.Count == 0) ? "Empty" : string.Join(", ", distinctNumList);
            distinctNumArrayJoined = (distinctNumArray == null) ? "null" :
                                    (distinctNumArray.Length == 0) ? "Empty" : string.Join(", ", distinctNumArray);
            AppendLine(sb, "nums = [{0}]", numsJoined);
            AppendLine(sb, "nums.Distinct = [{0}]", distinctNumListJoined);
            AppendLine(sb, "nums.Distinct.ToArray = [{0}]", distinctNumArrayJoined);

            ToFile(sb, "LinqTest4");
            return;
        }
        #endregion // LinqTest4

        #region LinqTest5
        /// <summary>
        /// LINQの動作確認5 - Distinctの挙動
        /// </summary>
        private void LinqTest5(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            var chkBox1 = new CheckBox();
            var chkBox2 = new CheckBox();
            var chkBox3 = new CheckBox();
            chkBox1.Name = string.Format("CheckBox_{0:00}", 1);
            chkBox2.Name = string.Format("CheckBox_{0:00}", 2);
            chkBox3.Name = string.Format("CheckBox_{0:00}", 3);

            AppendLine(sb, "--- BEFORE ---");
            AppendLine(sb, "chkBox1.Name = {0}", chkBox1.Name);
            AppendLine(sb, "chkBox2.Name = {0}", chkBox2.Name);
            AppendLine(sb, "chkBox3.Name = {0}", chkBox3.Name);

            var chkBoxDic = new Dictionary<int, CheckBox>();
            chkBoxDic.Add(1, chkBox1);
            chkBoxDic.Add(2, chkBox2);
            chkBoxDic.Add(3, chkBox3);

            var copyList = chkBoxDic.Values.ToList();
            foreach (var chkBox in copyList)
            {
                chkBox.Name = string.Format("NEW_{0}", chkBox.Name);
                chkBox.Checked = true;
            }

            AppendLine(sb, "--- MODIFIED OBJECTS VIA LINQ ---");
            AppendLine(sb, "chkBox1.Name = {0}, Checked = {1}", chkBox1.Name, chkBox1.Checked);
            AppendLine(sb, "chkBox2.Name = {0}, Checked = {1}", chkBox2.Name, chkBox2.Checked);
            AppendLine(sb, "chkBox3.Name = {0}, Checked = {1}", chkBox3.Name, chkBox3.Checked);

            chkBox1.Name = string.Format("RE_{0}", chkBox1.Name);
            chkBox1.Checked = false;
            chkBox2.Name = string.Format("RE_{0}", chkBox2.Name);
            chkBox2.Checked = true;
            chkBox3.Name = string.Format("RE_{0}", chkBox3.Name);
            chkBox3.Checked = false;

            AppendLine(sb, "--- MODIFIED ORIGINAL OBJECTS ---");
            foreach (var chkBox in copyList)
            {
                AppendLine(sb, "chkBox.Name = {0}, Checked = {1}", chkBox.Name, chkBox.Checked);
            }

            ToFile(sb, "LinqTest5");
            return;
        }
        #endregion // LinqTest5

        #region BoolTest
        /// <summary>
        /// 列挙体の動作確認
        /// </summary>
        private void BoolTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            bool a = true;
            bool b = true;
            bool c = true;
            Action<bool, bool, bool> caseFTF = ((aValue, bValue, cValue) =>
            {
                a = aValue;
                b = bValue;
                c = cValue;
                AppendLine(sb, "[{0}, {1}, {2}] (a == false && b && c == false) = {3}", a, b, c, a == false && b && c == false);
            });

            caseFTF(true, true, true);
            caseFTF(true, true, false);
            caseFTF(true, false, true);
            caseFTF(true, false, false);
            caseFTF(false, true, true);
            caseFTF(false, true, false);
            caseFTF(false, false, true);
            caseFTF(false, false, false);

            AppendLine(sb, "");

            Action<bool, bool, bool> caseFTT = ((aValue, bValue, cValue) =>
            {
                a = aValue;
                b = bValue;
                c = cValue;
                AppendLine(sb, "[{0}, {1}, {2}] (a == false && b && c == true) = {3}", a, b, c, a == false && b && c == true);
            });

            caseFTT(true, true, true);
            caseFTT(true, true, false);
            caseFTT(true, false, true);
            caseFTT(true, false, false);
            caseFTT(false, true, true);
            caseFTT(false, true, false);
            caseFTT(false, false, true);
            caseFTT(false, false, false);

            ToFile(sb, "BoolTest");
            return;
        }
        #endregion // BoolTest

        #region CollectionTest
        /// <summary>
        /// コレクションの動作確認
        /// </summary>
        private void CollectionTest(bool execute)
        {
            if (!execute) { return; }

            var dic = new Dictionary<int[], string>();
            dic.Add(new int[] { 1, 2, 3 }, "aaa");
            dic.Add(new int[] { 1, 2, 3 }, "bbb");
            try
            {
                dic.Add(null, "ccc");
                dic.Add(null, "ddd");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var siDic = new Dictionary<string, List<string>>();
            var kv = new KeyValuePair<string, List<string>>("key", new List<string> { "value_1", "value_2" });

            siDic.Add(kv.Key, kv.Value);

            int count = 10;
            var rnd = new Random();
            var list = new List<string>();
            var rev = new List<string>();
            for (int i = 0; i < count; i++)
            {
                int value = rnd.Next(100);
                list.Add(value < 85 ? value.ToString() : null);
                rev.Add(value < 85 ? value.ToString() : null);
            }
            rev.Reverse();

            string listValues = "";
            string listValuesRev = "";
            for (int i = 0; i < list.Count; i++)
            {
                string separator = i == 0 ? "" : ", ";
                listValues += separator + (list[i] ?? "-").PadLeft(3, ' ');
                listValuesRev += separator + (rev[i] ?? "-").PadLeft(3, ' ');
            }
            var sb = new StringBuilder();
            sb.AppendFormat("list: {0} [Last = {1}]\r\n", listValues, list.Last());
            sb.AppendFormat("rev:  {0} [Last = {1}]\r\n", listValuesRev, rev.Last());

            //string[] strArr = new string[] { };
            //strArr[0] = "0";
            //strArr[1] = "1";

            ToFile(sb, "CollectionTest");
            return;
        }
        #endregion // CollectionTest

        #region AddRangeTest
        /// <summary>
        /// AddRangeメソッドの動作確認
        /// </summary>
        private void AddRangeTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            Func<List<int>, string> listToStr = (list =>
                    (list == null) ? "null" : (list.Count == 0) ? "empty" : string.Join(", ", list.Select(value => value.ToString()))
                );

            var listBase = new List<int>();
            AppendLine(sb, "listBase: {0}", listToStr(listBase));

            List<int> listNull = null;
            AppendLine(sb, "listNull: {0}", listToStr(listNull));
            var listEmpty = new List<int>();
            AppendLine(sb, "listEmpty: {0}", listToStr(listEmpty));

            var listAdd = new List<int>();
            listAdd.Add(1);
            listAdd.Add(4);
            listAdd.Add(10);
            listAdd.Add(12);
            AppendLine(sb, "listAdd: {0}", listToStr(listAdd));

            AppendLine(sb, "");

            try
            {
                AppendLine(sb, "listBase.AddRange(listNull)");
                listBase.AddRange(listNull);
                AppendLine(sb, "listBase: {0}", listToStr(listBase));
            }
            catch (Exception ex)
            {
                AppendLine(sb, "ERROR: {0}", ex.Message);
                AppendLine(sb, "listBase: {0}", listToStr(listBase));
            }

            AppendLine(sb, "");

            try
            {
                AppendLine(sb, "listBase.AddRange(listEmpty)");
                listBase.AddRange(listEmpty);
                AppendLine(sb, "listBase: {0}", listToStr(listBase));

                AppendLine(sb, "listBase.AddRange(listAdd)");
                listBase.AddRange(listAdd);
                AppendLine(sb, "listBase: {0}", listToStr(listBase));

                AppendLine(sb, "listBase.Add(XX) x2");
                listBase.Add(99);
                listBase.Add(98);
                AppendLine(sb, "listBase: {0}", listToStr(listBase));

                AppendLine(sb, "listBase.AddRange(listAdd)");
                listBase.AddRange(listAdd);
                AppendLine(sb, "listBase: {0}", listToStr(listBase));
            }
            catch (Exception ex)
            {
                AppendLine(sb, "ERROR: {0}", ex.Message);
                AppendLine(sb, "listBase: {0}", listToStr(listBase));
            }

            ToFile(sb, "AddRangeTest");

            return;
        }
        #endregion // AddRangeTest

        #region TypeSafeEnumTest
        /// <summary>
        /// TypeSafeEnumの動作確認
        /// </summary>
        private void TypeSafeEnumTest(bool execute)
        {
            if (!execute) { return; }
            var sb = new StringBuilder();

            foreach (var item in DerivedEnum.Items)
            {
                AppendLine(sb, "ID = {0}, Group = {1}, Name = {2}", item.ID, item.Group, item.Name);
                AppendLine(sb, "  Sum = {0}, Exists = {1}", item.Sum.ToString(), item.Sum.IsUsed);
                AppendLine(sb, "  Average = {0}, Exists = {1}", item.Average.ToString(), item.Average.IsUsed);
                AppendLine(sb, "  S.D = {0}, Exists = {1}", item.SD.ToString(), item.SD.IsUsed);
            }

            ToFile(sb, "TypeSafeEnumTest");
            return;
        }
        #endregion // TypeSafeEnumTest

        #region RegExTest
        /// <summary>
        /// 正規表現の動作確認
        /// </summary>
        private void RegExTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            // string.Format関数のフォーマットパラメータのプレースホルダを正規表現で調べる方法の検証
            string input = "{{{0}}}}_{{1}}  {{{{{10}_{{{{{12}}}";
            AppendLine(sb, "Input: {0}", input);

            string removed = input;
            string bk = removed;
            do
            {
                bk = removed;
                removed = removed.Replace("{{", "").Replace("}}", "");
            } while (removed != bk);

            AppendLine(sb, "Remove \"{{{{\" and \"}}}}\" --> {0}", removed);
            AppendLine(sb, "");

            const string PLACE_HOLDER = "Numbers";
            string patternBody = "{[0-9]+}";
            string pattern = string.Format(@"(?<{0}>{1})", PLACE_HOLDER, patternBody);

            AppendLine(sb, "Execute: Regex.Matches(\"{0}\", \"{1}\")", removed, pattern);
            var mc = Regex.Matches(removed, pattern);
            var nums = new List<double>();
            int count = 0;
            foreach (Match match in mc)
            {
                Append(sb, ">> Matched[{0}] @ {1} --> ", count, PLACE_HOLDER);
                if (match.Success)
                {
                    var grp = match.Groups[PLACE_HOLDER];
                    string results = "";
                    foreach (Capture capture in grp.Captures)
                    {
                        results += string.Format("{0}, ", capture.Value);
                    }
                    Append(sb, results.TrimEnd(',', ' '));
                    AppendLine(sb, "");
                }
                else
                {
                    AppendLine(sb, "no mathces.");
                }
                count++;
            }

            ToFile(sb, "RegExTest");
            return;
        }
        #endregion // RegExTest

        #region DateTimeTest
        /// <summary>
        /// DateTime型の動作確認
        /// </summary>
        private void DateTimeTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            var dt = new DateTime(2016, 01, 02, 12, 34, 56);

            AppendLine(sb, "DateTime.Date = {0} [{1}]", dt.Date, dt.Date.GetType());
            AppendLine(sb, "DateTime.Year = {0} [{1}]", dt.Year, dt.Year.GetType());
            AppendLine(sb, "DateTime.Month = {0} [{1}]", dt.Month, dt.Month.GetType());
            AppendLine(sb, "DateTime.Day = {0} [{1}]", dt.Day, dt.Day.GetType());
            AppendLine(sb, "DateTime.Hour = {0} [{1}]", dt.Hour, dt.Hour.GetType());
            AppendLine(sb, "DateTime.Minute = {0} [{1}]", dt.Minute, dt.Minute.GetType());
            AppendLine(sb, "DateTime.Second = {0} [{1}]", dt.Second, dt.Second.GetType());

            ToFile(sb, "DateTimeTest");
            return;
        }
        #endregion // DateTimeTest

        #region TimeSpanTest
        /// <summary>
        /// TimeSpan型の動作確認
        /// </summary>
        private void TimeSpanTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            var dtSmall = new DateTime(2016, 01, 01, 12, 0, 0);
            var dtLarge = new DateTime(2016, 01, 01, 12, 0, 1);

            sb.AppendFormat("{0} - {1} = {2}({3}ms)\r\n",
                            dtSmall.ToString("yyyy/MM/dd HH:mm:ss"),
                            dtLarge.ToString("yyyy/MM/dd HH:mm:ss"),
                            (dtSmall - dtLarge).ToString(),
                            (dtSmall - dtLarge).TotalMilliseconds);

            sb.AppendFormat("{0} - {1} = {2}({3}ms)\r\n",
                            dtLarge.ToString("yyyy/MM/dd HH:mm:ss"),
                            dtSmall.ToString("yyyy/MM/dd HH:mm:ss"),
                            (dtLarge - dtSmall).ToString(),
                            (dtLarge - dtSmall).TotalMilliseconds);

            ToFile(sb, "TimeSpanTest");
            return;
        }
        #endregion // TimeSpanTest

        #region FormatTest
        /// <summary>
        /// string.Formatの動作確認
        /// </summary>
        private void FormatTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();
            try
            {
                string[] forms = new string[] { "{{0}", "{0}_{1}_{2}{9}", "{0}_{1}_{2}{0}", "{", "}" };

                try { sb.AppendLine(string.Format(forms[0], "")); }
                catch (Exception ex) { sb.AppendLine(ex.Message); }

                try { sb.AppendLine(string.Format(forms[0], "AAA", "BBB")); }
                catch (Exception ex) { sb.AppendLine(ex.Message); }


                try { sb.AppendLine(string.Format(forms[1], "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10")); }
                catch (Exception ex) { sb.AppendLine(ex.Message); }

                try { sb.AppendLine(string.Format(forms[1], "0", "1", "2", "3")); }
                catch (Exception ex) { sb.AppendLine(ex.Message); }

                try { sb.AppendLine(string.Format(forms[1], "0", "1", "2")); }
                catch (Exception ex) { sb.AppendLine(ex.Message); }


                try { sb.AppendLine(string.Format(forms[2], "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10")); }
                catch (Exception ex) { sb.AppendLine(ex.Message); }

                try { sb.AppendLine(string.Format(forms[2], "0", "1", "2", "3")); }
                catch (Exception ex) { sb.AppendLine(ex.Message); }

                try { sb.AppendLine(string.Format(forms[2], "0", "1", "2")); }
                catch (Exception ex) { sb.AppendLine(ex.Message); }

                try { sb.AppendLine(string.Format(forms[3], "0", "1", "2")); }
                catch (Exception ex) { sb.AppendLine(ex.Message); }

                try { sb.AppendLine(string.Format(forms[4], "0", "1", "2")); }
                catch (Exception ex) { sb.AppendLine(ex.Message); }
            }
            catch (Exception ex)
            {
                sb.AppendLine(ex.Message);
            }

            ToFile(sb, "FormatTest");
            return;
        }
        #endregion // FormatTest

        #region IndexOfTest
        /// <summary>
        /// IndexOfメソッドの動作確認
        /// </summary>
        private void IndexOfTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();
            string src = "aaaa\r\nbbbb\r\ncccc\r\n";

            string separator = "\r\n";
            int gap = separator.Length;
            int pos = 0;

            int found = src.IndexOf("\r\n", pos, 1);
            sb.AppendFormat(@"{0}.IndexOf(""\r\n"", ""{1}"", ""1"") = {2}{3}", src, pos, found, "\r\n");
            pos = found + gap;

            found = src.IndexOf("\r\n", pos, 1);
            sb.AppendFormat(@"{0}.IndexOf(""\r\n"", ""{1}"", ""1"") = {2}{3}", src, pos, found, "\r\n");
            pos = found + gap;

            found = src.IndexOf("\r\n", pos, 1);
            sb.AppendFormat(@"{0}.IndexOf(""\r\n"", ""{1}"", ""1"") = {2}{3}", src, pos, found, "\r\n");
            pos = found + gap;

            found = src.IndexOf("\r\n", pos, 1);
            sb.AppendFormat(@"{0}.IndexOf(""\r\n"", ""{1}"", ""1"") = {2}{3}", src, pos, found, "\r\n");

            ToFile(sb, "IndexOfTest");
            return;
        }
        #endregion // IndexOfTest

        #region SplitStringTest
        /// <summary>
        /// SplitStringメソッドの動作確認
        /// </summary>
        private void SplitStringTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            string src = "aaaa\r\nbbbb\r\ncccc\r\n";
            string separator = "\r\n";
            var segments = SplitString(src, separator);

            sb.AppendFormat("SplitString(\"{0}\", \"{1}\")\r\n", src, separator);
            for (int i = 0; i < segments.Length; i++)
            {
                sb.AppendFormat("[{0}] \"{1}\"\r\n", i, segments[i]);
            }
            sb.AppendLine("");

            src = "aaaa\r\nbbbb\r\ncccc\r\ndddd\r\neeee";
            separator = "\r\n";
            segments = SplitString(src, separator);

            sb.AppendFormat("SplitString(\"{0}\", \"{1}\")\r\n", src, separator);
            for (int i = 0; i < segments.Length; i++)
            {
                sb.AppendFormat("[{0}] \"{1}\"\r\n", i, segments[i]);
            }
            sb.AppendLine("");

            src = "\r\nbbbb\r\ncccc\r\ndddd\r\n";
            separator = "\r\n";
            segments = SplitString(src, separator);

            sb.AppendFormat("SplitString(\"{0}\", \"{1}\")\r\n", src, separator);
            for (int i = 0; i < segments.Length; i++)
            {
                sb.AppendFormat("[{0}] \"{1}\"\r\n", i, segments[i]);
            }
            sb.AppendLine("");

            src = "aaaa+++bbbb+++cccc++++dddd+++eeee";
            separator = "+++";
            segments = SplitString(src, separator);

            sb.AppendFormat("SplitString(\"{0}\", \"{1}\")\r\n", src, separator);
            for (int i = 0; i < segments.Length; i++)
            {
                sb.AppendFormat("[{0}] \"{1}\"\r\n", i, segments[i]);
            }
            sb.AppendLine("");

            src = "++abc++";
            separator = "+++";
            segments = SplitString(src, separator);

            sb.AppendFormat("SplitString(\"{0}\", \"{1}\")\r\n", src, separator);
            for (int i = 0; i < segments.Length; i++)
            {
                sb.AppendFormat("[{0}] \"{1}\"\r\n", i, segments[i]);
            }
            sb.AppendLine("");

            src = "++";
            separator = "+++";
            segments = SplitString(src, separator);

            sb.AppendFormat("SplitString(\"{0}\", \"{1}\")\r\n", src, separator);
            for (int i = 0; i < segments.Length; i++)
            {
                sb.AppendFormat("[{0}] \"{1}\"\r\n", i, segments[i]);
            }
            sb.AppendLine("");

            src = "+++";
            separator = "+++";
            segments = SplitString(src, separator);

            sb.AppendFormat("SplitString(\"{0}\", \"{1}\")\r\n", src, separator);
            for (int i = 0; i < segments.Length; i++)
            {
                sb.AppendFormat("[{0}] \"{1}\"\r\n", i, segments[i]);
            }

            ToFile(sb, "SplitStringTest");
            return;
        }
        #endregion // SplitStringTest

        #region GetNumberOfDecimalPlacesTest
        /// <summary>
        /// GetNumberOfDecimalPlacesメソッドの動作確認
        /// </summary>
        private void GetNumberOfDecimalPlacesTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            var dt = DateTime.Now;
            int maxCount = 10000000;
            var rnd = new Random(dt.Millisecond);
            Func<double> getNext = (() => rnd.Next(1000000) / 1000d);

            AppendLine(sb, "GetNumberOfDecimalPlaces1 x {0} ---------------------------------", maxCount);
            dt = DateTime.Now;
            AppendLine(sb, "BEGIN - {0}", dt.ToString("yyyy/MM/dd HH:mm:ss.ffff"));
            for (int i = 0; i < maxCount; i++)
            {
                double value = getNext();
                if (i % (maxCount / 25) == 0)
                {
                    AppendResultLogLine(sb, string.Format("GetNumberOfDecimalPlaces1({0})", value), GetNumberOfDecimalPlaces1(value));
                }
            }
            AppendLine(sb, "END - {0} ({1}ms)", dt.ToString("yyyy/MM/dd HH:mm:ss.ffff"), (DateTime.Now - dt).TotalMilliseconds);

            AppendLine(sb, "");
            AppendLine(sb, "");

            AppendLine(sb, "GetNumberOfDecimalPlaces2 x {0} ---------------------------------", maxCount);
            dt = DateTime.Now;
            AppendLine(sb, "BEGIN - {0}", dt.ToString("yyyy/MM/dd HH:mm:ss.ffff"));
            for (int i = 0; i < maxCount; i++)
            {
                double value = getNext();
                if (i % (maxCount / 25) == 0)
                {
                    AppendResultLogLine(sb, string.Format("GetNumberOfDecimalPlaces2({0})", value), GetNumberOfDecimalPlaces2(value));
                }
            }
            AppendLine(sb, "END - {0} ({1}ms)", dt.ToString("yyyy/MM/dd HH:mm:ss.ffff"), (DateTime.Now - dt).TotalMilliseconds);

            AppendLine(sb, "");
            AppendLine(sb, "");

            AppendLine(sb, "GetNumberOfDecimalPlaces3 x {0} ---------------------------------", maxCount);
            dt = DateTime.Now;
            AppendLine(sb, "BEGIN - {0}", dt.ToString("yyyy/MM/dd HH:mm:ss.ffff"));
            for (int i = 0; i < maxCount; i++)
            {
                double value = getNext();
                if (i % (maxCount / 25) == 0)
                {
                    AppendResultLogLine(sb, string.Format("GetNumberOfDecimalPlaces3({0})", value), GetNumberOfDecimalPlaces3(value));
                }
            }
            AppendLine(sb, "END - {0} ({1}ms)", dt.ToString("yyyy/MM/dd HH:mm:ss.ffff"), (DateTime.Now - dt).TotalMilliseconds);

            AppendLine(sb, "");
            AppendLine(sb, "");

            AppendLine(sb, "GetNumberOfDecimalPlaces1, 2 and 3 ---------------------------------");
            for (int i = 0; i < maxCount; i++)
            {
                double value = (double)i / 1000d;
                if (i % 1000 < 2)
                {
                    AppendLine(sb, "GetNumberOfDecimalPlaces({0}) = {1} and {2}", value,
GetNumberOfDecimalPlaces1(value), GetNumberOfDecimalPlaces2(value), GetNumberOfDecimalPlaces3(value));
                }
            }

            ToFile(sb, "GetNumberOfDecimalPlacesTest");
            return;
        }
        /// <summary>
        /// 小数点以下桁数を取得する
        /// </summary>
        /// <param name="value">実数値</param>
        /// <returns>小数点以下桁数</returns>
        private int GetNumberOfDecimalPlaces1(double value)
        {
            // 整数の場合は0
            int result = 0;

            string text = value.ToString().TrimEnd('0');
            char sep = '.';

            int index = text.IndexOf(sep);
            if (index != -1)
            {
                // 小数点以降の桁数
                result = text.Substring(index + 1).Length;
            }
            return result;
        }
        /// <summary>
        /// 小数点以下桁数を取得する
        /// </summary>
        /// <param name="value">実数値</param>
        /// <returns>小数点以下桁数</returns>
        private int GetNumberOfDecimalPlaces2(double value)
        {
            string text = value.ToString().TrimEnd('0');
            char sep = '.';

            int index = text.IndexOf(sep);
            if (index < 0)
            {
                return 0;   // 整数: 0
            }

            return text.Length - index - 1;
        }
        /// <summary>
        /// 小数点以下桁数を取得する
        /// </summary>
        /// <param name="value">実数値</param>
        /// <returns>小数点以下桁数</returns>
        private int GetNumberOfDecimalPlaces3(double value)
        {
            char sep = '.';

            var parts = value.ToString().Split(sep);
            if (parts.Length < 2)
            {
                return 0;
            }
            return parts[1].TrimEnd('0').Length;
        }
        #endregion // GetNumberOfDecimalPlacesTest

        #region ToStringTest
        /// <summary>
        /// ToStringメソッドの動作確認
        /// </summary>
        private void ToStringTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();
            var rnd = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < 1000; i += rnd.Next(6))
            {
                AppendLine(sb, "{0}.ToString() = {1}", i / 100d, (i / 100d).ToString());
            }

            ToFile(sb, "ToStringTest");
            return;
        }
        #endregion // ToStringTest

        #region ToStringFormattingTest
        /// <summary>
        /// 書式設定したToStringTestメソッドの動作確認
        /// </summary>
        private void ToStringFormattingTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            var numbers = new[] { 0d, 2d, 10.0d, 10.1d, 0.12d, 987.6543d, 1234.5678d };
            var formats = new[] { "F", "F0", "F1", "F2", "F3", "N", "N0", "N1", "N2", "N3" };

            foreach (double num in numbers)
            {
                foreach (string format in formats)
                {
                    AppendLine(sb, "( {0} ).ToString({1}) = {2}", num, format, num.ToString(format));
                }
                AppendLine(sb, "- - - - - - - - - - - - - - - - - - -");
            }

            ToFile(sb, "ToStringFormattingTest");
            return;
        }
        #endregion // ToStringFormattingTest

        #region AppendLineTest
        /// <summary>
        /// AppendLineメソッドの動作確認
        /// </summary>
        private void AppendLineTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            int i = 0;
            sb.Append((++i).ToString() + ",");
            sb.Append((++i).ToString() + ",");
            sb.AppendLine((++i).ToString() + ",");

            sb.AppendLine((++i).ToString() + ",");

            sb.AppendLine((++i).ToString() + ",");

            sb.Append((++i).ToString() + ",");
            sb.Append((++i).ToString() + ",");
            sb.AppendLine((++i).ToString() + ",");

            sb.Append("");
            sb.Append("");
            sb.AppendLine("");

            sb.AppendLine("");

            sb.Append("");
            sb.Append((++i).ToString() + ",");
            sb.Append("");
            sb.Append((++i).ToString() + ",");
            sb.Append("");
            sb.AppendLine("");

            sb.Append((++i).ToString() + ",");
            sb.Append((++i).ToString() + ",");
            sb.AppendLine("");

            for (int itr = 0; itr < 3; itr++)
            {
                sb.Append("XXXX");
                sb.Append("YYYY");
                sb.Append("ZZZZ");
                sb.AppendLine("");

                sb.Append("XXXX");
                sb.Append("YYYY");
                sb.AppendLine("ZZZZ");
                sb.AppendLine("------");
            }

            ToFile(sb, "AppendLineTest");
            return;
        }
        #endregion // AppendLineTest

        #region PathTest
        /// <summary>
        /// パス操作の動作確認
        /// </summary>
        private void PathTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            var filePath = new[] { @"C:\aaa\bbb.xml", @"C:\aaaaaa\bbbxml", @"++:\??#%*\b*b.xml",
                                    @"\\192.168.255.255\XXXX\yyyy-mm-dd.xml",
                                    //Directory.GetFiles ( @"\\localhost\Shared","*.xml")[0]
                                };
            for (int i = 0; i < filePath.Length; i++)
            {
                int step = 0;
                string dir = Path.GetDirectoryName(filePath[i]);
                try
                {
                    step++;
                    AppendLine(sb, "Directory.Exists({0}) = {1}", dir, Directory.Exists(dir));
                    step++;
                    AppendLine(sb, "File.Exists({0}) = {1}", filePath[i], File.Exists(filePath[i]));
                }
                catch (Exception ex)
                {
                    AppendLine(sb, "[dir: {0}] Exception(step{1}): {2}", dir, step, ex.Message);
                }
                AppendLine(sb, "--");
            }


            AppendLine(sb, "");
            AppendLine(sb, "- - - - - ");
            AppendLine(sb, "");
            string desktopPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            AppendLine(sb, "Desktop entries:");
            foreach (string childDir in Directory.GetFileSystemEntries(desktopPath))
            {
                AppendLine(sb, childDir);
            }
            AppendLine(sb, "");
            AppendLine(sb, "- - - - - ");
            AppendLine(sb, "");

            AppendLine(sb, @"Path.Combine(@""C:\XXXX\"", ""dir_1"") = ""{0}""", Path.Combine(@"C:\XXXX\", "dir_1"));
            AppendLine(sb, @"Path.Combine(@""C:\XXXX"", ""dir_2"") = ""{0}""", Path.Combine(@"C:\XXXX", "dir_2"));
            AppendLine(sb, @"Path.Combine(@""C:\XXXX\"", """") = ""{0}""", Path.Combine(@"C:\XXXX\", ""));
            AppendLine(sb, @"Path.Combine(@""C:\XXXX"", ""\"") = ""{0}""", Path.Combine(@"C:\XXXX", "\\"));
            AppendLine(sb, @"Path.Combine(@""C:\XXXX"", """") = ""{0}""", Path.Combine(@"C:\XXXX", ""));

            ToFile(sb, "PathTest");
            return;
        }
        #endregion // PathTest

        #region PathTest2
        /// <summary>
        /// パス操作の動作確認
        /// </summary>
        private void PathTest2(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();
            var filesToBeDeleted = new List<string>();

            const string TIME_FORMAT = "yyyyMMdd-HHmmss";

            #region Func<string, bool> isTarget
            Func<string, bool> isTarget = (fileName =>
            {
                string okPattern = string.Format(@"^OK-[a-zA-Z0-9]* [0-9]{{8}}-[0-9]{{6}}\.log$");
                string ngPattern = string.Format(@"^NG-[a-zA-Z0-9]* [0-9]{{8}}-[0-9]{{6}}\.log$");
                return Regex.IsMatch(fileName, okPattern) || Regex.IsMatch(fileName, ngPattern);
            });
            #endregion

            #region Func<string, string> getDateTimePart
            Func<string, string> getDateTimePart = (fileName =>
            {
                string noExtName = Path.GetFileNameWithoutExtension(fileName);
                return fileName.Substring(noExtName.Length - TIME_FORMAT.Length);
            });
            #endregion

            #region Action<string, List<string>> outputSb
            Action<string, List<string>> outputSb = ((title, files) =>
            {
                AppendLine(sb, "[{0}]", title);

                if (files != null && 0 < files.Count)
                {
                    foreach (string file in files)
                    {
                        AppendLine(sb, "{0}", file);
                    }
                }
                else
                {
                    AppendLine(sb, "None");
                }
                AppendLine(sb, "\r\n- - - - - - - - - - - - - - - - - - - -\r\n");
            });
            #endregion

            try
            {
                string desktopDir = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var now = DateTime.Now;
                var fileNames = new string[]{
                string.Format("OK-file1 {0}.log", now.AddYears(1).ToString(TIME_FORMAT)),
                string.Format("OK-file2 {0}.log", now.AddYears(-2).ToString(TIME_FORMAT)),
                string.Format("NG-file1 {0}.log", now.AddYears(0).ToString(TIME_FORMAT)),
                string.Format("NG-file2 {0}.log", now.AddYears(-3).ToString(TIME_FORMAT))
            };
                foreach (string fileName in fileNames)
                {
                    string path = Path.Combine(desktopDir, fileName);
                    if (!File.Exists(path))
                    {
                        using (var sw = new StreamWriter(path))
                        {
                        }
                        filesToBeDeleted.Add(path);
                    }
                }

                var fileList = Directory.GetFiles(desktopDir)
                                    .Select(fullPath => Path.GetFileName(fullPath))
                                    .Where(fileName => isTarget(fileName))
                                    .OrderBy(fileName => getDateTimePart(fileName))
                                    .Select(fileName => Path.Combine(desktopDir, fileName))
                                    .ToList();
                outputSb("Do at once", fileList);

                var newList = new List<string>();
                newList.AddRange(fileList);

                var files2 = Directory.GetFiles(desktopDir);
                outputSb("Directory.GetFiles", files2.ToList());
                var files3 = files2.Select(fullPath => Path.GetFileName(fullPath)).ToList();
                outputSb("Select - Path.GetFileName", files3);
                var files4 = files3.Where(fileName => isTarget(fileName)).ToList();
                outputSb("Where - isTarget", files4);
                var files5 = files4.OrderBy(fileName => getDateTimePart(fileName)).ToList();
                outputSb("OrderBy - getDateTimePart", files5);
                var files6 = files5.Select(fileName => Path.Combine(desktopDir, fileName)).ToList();
                outputSb("Select - Path.Combine", files6);

                if (files4 != null && 0 < files4.Count)
                {
                    string cutName = files4[0].Substring(files4[0].Length - "yyyyMMdd-HHmmss.log".Length);
                }

                var noExtNames = files5.Select(fileName => Path.GetFileNameWithoutExtension(fileName)).ToList();
                newList.AddRange(noExtNames);
            }
            finally
            {
                foreach (string deletePath in filesToBeDeleted)
                {
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }
            }

            ToFile(sb, "PathTest2");
            return;
        }
        #endregion // PathTest2

        #region FileAndDirNameTest
        /// <summary>
        /// GetDirectoryNameメソッドの動作確認
        /// </summary>
        private void FileAndDirNameTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();
            string[] pathArray = { @"C:\temp\aaaa\vsfbaa.xml", @"C:\temp\aaaa\", @"C:\temp\aaaa", @"C:\temp\aaaa\\" };

            foreach (string path in pathArray)
            {
                try
                {
                    string dirName = Path.GetDirectoryName(path);
                    sb.AppendFormat("GetDirectoryName({0}) = {1}\r\n", path, dirName);
                }
                catch (Exception ex)
                {
                    sb.AppendFormat("[Error] GetDirectoryName({0}) : {1}\r\n", path, ex.Message);
                }
            }
            foreach (string path in pathArray)
            {
                try
                {
                    string fileName = Path.GetFileName(path);
                    sb.AppendFormat("GetFileName({0}) = {1}\r\n", path, fileName);
                }
                catch (Exception ex)
                {
                    sb.AppendFormat("[Error] GetFileName({0}) : {1}\r\n", path, ex.Message);
                }
            }
            foreach (string path in pathArray)
            {
                sb.AppendFormat("{0} -> ", path);
                var segments = path.Split('\\');
                for (int i = 0; i < segments.Length; i++)
                {
                    sb.AppendFormat("{0}[{1}] \"{2}\"", (i == 0) ? "" : ", ", i, segments[i]);
                }
                sb.Append("\r\n");
            }

            ToFile(sb, "FileAndDirNameTest");
            return;
        }
        #endregion // FileAndDirNameTest

        #region IOTest
        /// <summary>
        /// ファイルIOの動作確認
        /// </summary>
        private void IOTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();
            string path = Path.Combine(LOG_DIR_PATH, @"temp\aaaavsfbaa.xml");
            string path_2 = Path.Combine(LOG_DIR_PATH, @"temp\xxxx\aaaavsfbaa.xml");
            string dirPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            var xd = new XmlDocument();

            sb.AppendFormat("Open file to write: {0}\r\n", path);
            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                sb.AppendFormat("--> {0}\r\n", "success");

                try
                {
                    System.Threading.Thread.Sleep(500);

                    sb.AppendFormat("Open file to read: {0}\r\n", path);
                    using (var fsSecond = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        sb.AppendFormat("--> {0}\r\n", "success");
                    }
                }
                catch (Exception ex)
                {
                    sb.AppendFormat("--> {0} [{1}]\r\n", ex.GetType().Name, ex.Message);
                }

                try
                {
                    sb.AppendFormat("Open file as XML: {0}\r\n", path);
                    xd.Load(path);

                    sb.AppendFormat("--> {0}\r\n", "success");
                }
                catch (Exception ex)
                {
                    sb.AppendFormat("--> {0} [{1}]\r\n", ex.GetType().Name, ex.Message);
                }
            }

            try
            {
                sb.AppendFormat("Open file as XML: {0}\r\n", path);
                xd.Load(path);

                sb.AppendFormat("--> {0}\r\n", "success");
            }
            catch (Exception ex)
            {
                sb.AppendFormat("--> {0} [{1}]\r\n", ex.GetType().Name, ex.Message);
            }

            try
            {
                string dirOfPath_2 = Path.GetDirectoryName(path_2);
                sb.AppendFormat("Directory.Exists({0}) = {1}\r\n", dirOfPath_2, Directory.Exists(dirOfPath_2));
                sb.AppendFormat("Open file as XML: {0}\r\n", path_2);
                xd.Load(path);

                sb.AppendFormat("--> {0}\r\n", "success");
            }
            catch (Exception ex)
            {
                sb.AppendFormat("--> {0} [{1}]\r\n", ex.GetType().Name, ex.Message);
            }

            File.Delete(path);
            sb.AppendFormat("Delete file: {0}\r\n", (File.Exists(path) ? "failed" : "success"));

            try
            {
                System.Threading.Thread.Sleep(100);

                sb.AppendFormat("Open deleted file: {0}\r\n", path);
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    sb.AppendFormat("--> {0}\r\n", "success");
                }
            }
            catch (Exception ex)
            {
                sb.AppendFormat("--> {0} [{1}]\r\n", ex.GetType().Name, ex.Message);
            }
            try
            {
                sb.AppendFormat("Open deleted XML: {0}\r\n", path);
                xd.Load(path);

                sb.AppendFormat("--> {0}\r\n", "success");
            }
            catch (Exception ex)
            {
                sb.AppendFormat("--> {0} [{1}]\r\n", ex.GetType().Name, ex.Message);
            }

            ToFile(sb, "IOTest");
            return;
        }
        #endregion // IOTest

        #region IOTest2
        /// <summary>
        /// ファイルIOの動作確認 - フォルダ作成
        /// </summary>
        private void IOTest2(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();
            string dirpath = string.Format(@"{0}\IOTest2\{1}",LOG_DIR_PATH,  DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            string parentPath = dirpath.Substring(0, dirpath.LastIndexOf(@"\"));

            sb.AppendFormat("Directory.Exists({0}) = {1}\r\n", parentPath, Directory.Exists(parentPath));
            sb.AppendFormat("Directory.Exists({0}) = {1}\r\n", dirpath, Directory.Exists(dirpath));
            sb.AppendLine("- - - - - -");

            try
            {
                sb.AppendFormat("try Directory.CreateDirectory({0})\r\n", dirpath);
                Directory.CreateDirectory(dirpath);
                sb.AppendFormat("OK\r\n");
            }
            catch (Exception ex)
            {
                sb.AppendFormat("Error: {0}\r\n", ex.Message);
            }

            try
            {
                sb.AppendFormat("try Directory.CreateDirectory({0})\r\n", parentPath);
                Directory.CreateDirectory(parentPath);
                sb.AppendFormat("OK\r\n");
                sb.AppendFormat("try Directory.CreateDirectory({0})\r\n", dirpath);
                Directory.CreateDirectory(dirpath);
                sb.AppendFormat("OK\r\n");
            }
            catch (Exception ex)
            {
                sb.AppendFormat("Error: {0}\r\n", ex.Message);
            }

            try
            {
                sb.AppendFormat("try Directory.CreateDirectory({0})\r\n", dirpath);
                Directory.CreateDirectory(dirpath);
                sb.AppendFormat("OK\r\n");
            }
            catch (Exception ex)
            {
                sb.AppendFormat("Error: {0}\r\n", ex.Message);
            }
            sb.AppendLine("- - - - - -");

            try
            {
                sb.AppendFormat("try Directory.Delete({0})\r\n", parentPath);
                Directory.Delete(parentPath);
                sb.AppendFormat("OK\r\n");
            }
            catch (Exception ex)
            {
                sb.AppendFormat("Error: {0}\r\n", ex.Message);

                sb.AppendFormat("Directory.Delete({0})\r\n", dirpath);
                Directory.Delete(dirpath);
                sb.AppendFormat("Directory.Delete({0})\r\n", parentPath);
                Directory.Delete(parentPath);
            }

            ToFile(sb, "IOTest2");
            return;
        }
        #endregion // IOTest2

        #region IOTest3
        /// <summary>
        /// ファイルIOの動作確認 - フォルダ作成
        /// </summary>
        private void IOTest3(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();
            string filePath = string.Format(@"{0}\IOTest3_{1}.txt", LOG_DIR_PATH, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            string bkPath = string.Format(@"{0}\@bk", LOG_DIR_PATH);

            if (File.Exists(filePath))
            {
                AppendLine(sb, "File found: {0}", filePath);
            }
            else
            {
                try
                {
                    AppendLine(sb, "File not found, try to open: {0}", filePath);
                    using (var fs = new FileStream(filePath, FileMode.Open)) { }
                }
                catch (FileNotFoundException ex)
                {
                    AppendLine(sb, "Catch FileNotFoundException: {1}", ex.GetType(), ex.Message);
                }
                catch (Exception ex)
                {
                    AppendLine(sb, "[{0}]: {1}", ex.GetType(), ex.Message);
                }
            }

            AppendLine(sb, "");
            AppendLine(sb, "- - - - - - - - - - - - - - - -");
            AppendLine(sb, "");

            if (File.Exists(filePath))
            {
                AppendLine(sb, "File found: {0}", filePath);
            }
            else
            {
                try
                {
                    AppendLine(sb, "File not found, try to move: {0}", filePath);
                    File.Move(filePath, Path.Combine(bkPath, Path.GetFileName(filePath)));
                }
                //catch (IOException ex)
                //{
                //    AppendLine(sb, "Catch IOException: {1}", ex.GetType(), ex.Message);
                //}
                catch (FileNotFoundException ex)
                {
                    AppendLine(sb, "Catch FileNotFoundException: {1}", ex.GetType(), ex.Message);
                }
                catch (Exception ex)
                {
                    AppendLine(sb, "[{0}]: {1}", ex.GetType(), ex.Message);
                }
            }

            AppendLine(sb, "");
            AppendLine(sb, "- - - - - - - - - - - - - - - -");
            AppendLine(sb, "");

            try
            {
                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                { }
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    AppendLine(sb, "Open file normally, try to move: {0}", filePath);
                    File.Move(filePath, Path.Combine(bkPath, Path.GetFileName(filePath)));
                    AppendLine(sb, "File moved.");
                }
            }
            catch (FileNotFoundException ex)
            {
                AppendLine(sb, "Catch FileNotFoundException: {1}", ex.GetType(), ex.Message);
            }
            catch (Exception ex)
            {
                AppendLine(sb, "[{0}]: {1}", ex.GetType(), ex.Message);
            }

            AppendLine(sb, "");
            AppendLine(sb, "- - - - - - - - - - - - - - - -");
            AppendLine(sb, "");

            try
            {
                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    AppendLine(sb, "Created file exclusively, try to move: {0}", filePath);
                    File.Move(filePath, Path.Combine(bkPath, Path.GetFileName(filePath)));
                    AppendLine(sb, "File moved.");
                }
            }
            catch (FileNotFoundException ex)
            {
                AppendLine(sb, "Catch FileNotFoundException: {1}", ex.GetType(), ex.Message);
            }
            catch (Exception ex)
            {
                AppendLine(sb, "[{0}]: {1}", ex.GetType(), ex.Message);
            }

            ToFile(sb, "IOTest3");
            return;
        }
        #endregion // IOTest3

        #region FileCreateTest
        /// <summary>
        /// ファイル生成の動作確認
        /// </summary>
        private void FileCreateTest(bool execute)
        {
            if (!execute) { return; }

            string path = Path.Combine(LOG_DIR_PATH, @"temp\aaaavsfbaa.xml");
            string dirPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            using (var sw = new StreamWriter(fs))
            {
                ;
            }

            File.Delete(path);
        }
        #endregion // FileCreateTest

        #region LargeFileCopyTest
        /// <summary>
        /// 巨大ファイルコピー中の動作確認
        /// </summary>
        private void LargeFileCopyTest(bool execute)
        {
            if (!execute) { return; }

            var fileNames = new[] { "copy_test.zip", "copy_test2.zip" };
            string filePath = Path.Combine(new[] { LOG_DIR_PATH, "LargeFileCopyTest", fileNames[0] });
            string dirPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string format = "[{0}] step{1} {2}";
            int step = 0;

            var sb = new StringBuilder();
            try
            {
                if (File.Exists(filePath))
                {
                    step++;
                    sb.AppendLine(string.Format(format, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), step, "Found."));
                    step++;
                    for (int i = 0; i < 4; i++)
                    {
                        var fi = new FileInfo(filePath);
                        sb.AppendLine(string.Format(format, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), step, "File Size: " + fi.Length));
                        System.Threading.Thread.Sleep(500);
                    }
                    step++;
                    string toFileName = "copy_test_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".zip";
                    string toPath = Path.Combine(dirPath, toFileName);
                    sb.AppendLine(string.Format(format, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), step, "Copy to: " + toPath));
                    File.Copy(filePath, toPath);
                    step++;
                    sb.AppendLine(string.Format(format, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), step, "End Copy."));
                }
                else
                {
                    step++;
                    sb.AppendLine(string.Format(format, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), step, "Not Found."));
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine(string.Format(format, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), step, "ERROR!!"));
                sb.AppendLine(string.Format(format, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), step, ex.Message));
            }

            step++;
            sb.AppendLine(string.Format(format, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), step, "End MacTest."));

            ToFile(sb, "LargeFileCopyTest");
            return;
        }
        #endregion // LargeFileCopyTest

        #region LockFileTest
        /// <summary>
        /// ファイルロックの動作確認
        /// </summary>
        private void LockFileTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            string fileName = string.Format("LockFileTest_{0}.txt", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            string srcPath = Path.Combine(LOG_DIR_PATH, "LockFileTest", fileName);
            string destPath = Path.Combine(LOG_DIR_PATH, @"LockFileTest\@bk", fileName);

            var pathList = new[] { srcPath, destPath };
            foreach (string path in pathList)
            {
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }

            using (var fs = new FileStream(srcPath, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var sw = new StreamWriter(fs))
            {
                sw.WriteLine("WriteLine.");
                try
                {
                    File.Copy(srcPath, destPath);
                    AppendLine(sb, "Copy Completed: {0} -> {1}", srcPath, destPath);
                }
                catch (Exception ex)
                {
                    AppendLine(sb, "Copy Failed[{3}]: {0} -> {1} / {2}", srcPath, destPath, ex.Message, ex.GetType());
                }
            }

            foreach (string path in pathList)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    AppendLine(sb, "Delete Completed: {0}", srcPath);
                }
            }

            ToFile(sb, "LockFileTest");
            return;
        }
        #endregion // LockFileTest

        #region EnvironmentTest
        /// <summary>
        /// 実行環境取得の動作確認
        /// </summary>
        private void EnvironmentTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            sb.AppendLine(System.Environment.MachineName);

            ToFile(sb, "EnvironmentTest");
            return;
        }
        #endregion // EnvironmentTest

        #region BitOperationTest
        /// <summary>
        /// ビット演算の動作確認
        /// </summary>
        private void BitOperationTest(bool execute)
        {
            if (!execute) { return; }

            var sb = new StringBuilder();

            AppendLine(sb, "{0} >> {1} & {2} = {3}", 87, 0, 1, ((87 >> 0) & 1));
            AppendLine(sb, "{0} >> {1} & {2} = {3}", 87, 1, 1, ((87 >> 1) & 1));
            AppendLine(sb, "{0} >> {1} & {2} = {3}", 87, 2, 1, ((87 >> 2) & 1));
            AppendLine(sb, "{0} >> {1} & {2} = {3}", 87, 3, 1, ((87 >> 3) & 1));
            AppendLine(sb, "{0} >> {1} & {2} = {3}", 87, 4, 1, ((87 >> 4) & 1));
            AppendLine(sb, "{0} >> {1} & {2} = {3}", 87, 5, 1, ((87 >> 5) & 1));
            AppendLine(sb, "{0} >> {1} & {2} = {3}", 87, 6, 1, ((87 >> 6) & 1));

            AppendLine(sb, "{0} >> {1} & {2} = {3}", 27, 0, 1, ((27 >> 0) & 1));
            AppendLine(sb, "{0} >> {1} & {2} = {3}", 27, 1, 1, ((27 >> 1) & 1));
            AppendLine(sb, "{0} >> {1} & {2} = {3}", 27, 2, 1, ((27 >> 2) & 1));
            AppendLine(sb, "{0} >> {1} & {2} = {3}", 27, 3, 1, ((27 >> 3) & 1));
            AppendLine(sb, "{0} >> {1} & {2} = {3}", 27, 4, 1, ((27 >> 4) & 1));

            AppendLine(sb, "{0} >> {1} & {2} = {3}", 87, 0, 27, ((87 >> 0) & 27));
            AppendLine(sb, "{0} >> {1} & {2} = {3}", 87, 1, 27, ((87 >> 1) & 27));
            AppendLine(sb, "{0} >> {1} & {2} = {3}", 87, 2, 27, ((87 >> 2) & 27));
            AppendLine(sb, "{0} >> {1} & {2} = {3}", 87, 3, 27, ((87 >> 3) & 27));

            var positions = new[] { 0, 1, 2, 3, 4, 5 };
            var bitObj = new BitTestClass();

            foreach (int pos in positions)
            {
                bitObj.BitPosition = pos;
                bitObj.Flag = true;
                AppendLine(sb, "(flag ? 1 : 0) << {0} = {1} << {0} = {2}", bitObj.BitPosition, bitObj.Flag ? 1 : 0, bitObj.Bit);
                bitObj.Flag = false;
                AppendLine(sb, "(flag ? 1 : 0) << {0} = {1} << {0} = {2}", bitObj.BitPosition, bitObj.Flag ? 1 : 0, bitObj.Bit);
            }

            ToFile(sb, "BitOperationTest");
            return;
        }
        private class BitTestClass
        {
            public int BitPosition { get; set; }
            public bool Flag { get; set; }
            public int Bit { get { return (this.Flag ? 1 : 0) << this.BitPosition; } }
        }
        #endregion // EnvironmentTest

        #region IncrementTest
        /// <summary>
        /// インクリメントの動作確認
        /// </summary>
        private void IncrementTest(bool execute)
        {
            if (!execute) { return; }
            var sb = new StringBuilder();

            int count = 0;
            AppendLine(sb, "count = {0}", count);
            AppendLine(sb, "count++ = {0}", count++);
            AppendLine(sb, "count = {0}", count);
            AppendLine(sb, "count++ = {0}", count++);
            AppendLine(sb, "count = {0}", count);
            AppendLine(sb, "++count = {0}", ++count);
            AppendLine(sb, "count = {0}", count);
            AppendLine(sb, "++count = {0}", ++count);
            AppendLine(sb, "count = {0}", count);
            AppendLine(sb, "count++ = {0}", count++);
            AppendLine(sb, "count = {0}", count);
            AppendLine(sb, "++count = {0}", ++count);
            AppendLine(sb, "count = {0}", count);
            int n = 1;
            AppendLine(sb, "(count += {0}) = {1}", n, count += n);
            AppendLine(sb, "count = {0}", count);
            n = 3;
            AppendLine(sb, "(count += {0}) = {1}", n, count += n);
            AppendLine(sb, "count = {0}", count);

            ToFile(sb, "IncrementTest");
            return;
        }
        #endregion // IncrementTest

        #region MathRoundTest
        /// <summary>
        /// インクリメントの動作確認
        /// </summary>
        private void MathRoundTest(bool execute)
        {
            if (!execute) { return; }
            var sb = new StringBuilder();

            AppendLine(sb, " Math.Round(1.4) = {0}", Math.Round(1.4));
            AppendLine(sb, " Math.Round(1.5) = {0}", Math.Round(1.5));
            AppendLine(sb, " Math.Round(1.6) = {0}", Math.Round(1.6));
            AppendLine(sb, " Math.Round(1.24) = {0}", Math.Round(1.24));
            AppendLine(sb, " Math.Round(1.25) = {0}", Math.Round(1.25));
            AppendLine(sb, " Math.Round(1.26) = {0}", Math.Round(1.26));
            AppendLine(sb, " Math.Round(1.34) = {0}", Math.Round(1.34));
            AppendLine(sb, " Math.Round(1.35) = {0}", Math.Round(1.35));
            AppendLine(sb, " Math.Round(1.36) = {0}", Math.Round(1.36));
            AppendLine(sb, " Math.Round(1.24, 1) = {0}", Math.Round(1.24, 1));
            AppendLine(sb, " Math.Round(1.25, 1) = {0}", Math.Round(1.25, 1));
            AppendLine(sb, " Math.Round(1.26, 1) = {0}", Math.Round(1.26, 1));
            AppendLine(sb, " Math.Round(1.34, 1) = {0}", Math.Round(1.34, 1));
            AppendLine(sb, " Math.Round(1.35, 1) = {0}", Math.Round(1.35, 1));
            AppendLine(sb, " Math.Round(1.36, 1) = {0}", Math.Round(1.36, 1));
            AppendLine(sb, " Math.Round(1.24, 1, MidpointRounding.AwayFromZero) = {0}", Math.Round(1.24, 1, MidpointRounding.AwayFromZero));
            AppendLine(sb, " Math.Round(1.25, 1, MidpointRounding.AwayFromZero) = {0}", Math.Round(1.25, 1, MidpointRounding.AwayFromZero));
            AppendLine(sb, " Math.Round(1.26, 1, MidpointRounding.AwayFromZero) = {0}", Math.Round(1.26, 1, MidpointRounding.AwayFromZero));
            AppendLine(sb, " Math.Round(1.34, 1, MidpointRounding.AwayFromZero) = {0}", Math.Round(1.34, 1, MidpointRounding.AwayFromZero));
            AppendLine(sb, " Math.Round(1.35, 1, MidpointRounding.AwayFromZero) = {0}", Math.Round(1.35, 1, MidpointRounding.AwayFromZero));
            AppendLine(sb, " Math.Round(1.36, 1, MidpointRounding.AwayFromZero) = {0}", Math.Round(1.36, 1, MidpointRounding.AwayFromZero));
            AppendLine(sb, " Math.Round(1.24, 1, MidpointRounding.ToEven) = {0}", Math.Round(1.24, 1, MidpointRounding.ToEven));
            AppendLine(sb, " Math.Round(1.25, 1, MidpointRounding.ToEven) = {0}", Math.Round(1.25, 1, MidpointRounding.ToEven));
            AppendLine(sb, " Math.Round(1.26, 1, MidpointRounding.ToEven) = {0}", Math.Round(1.26, 1, MidpointRounding.ToEven));
            AppendLine(sb, " Math.Round(1.34, 1, MidpointRounding.ToEven) = {0}", Math.Round(1.34, 1, MidpointRounding.ToEven));
            AppendLine(sb, " Math.Round(1.35, 1, MidpointRounding.ToEven) = {0}", Math.Round(1.35, 1, MidpointRounding.ToEven));
            AppendLine(sb, " Math.Round(1.36, 1, MidpointRounding.ToEven) = {0}", Math.Round(1.36, 1, MidpointRounding.ToEven));

            AppendLine(sb, " Math.Round(-1.4) = {0}", Math.Round(-1.4));
            AppendLine(sb, " Math.Round(-1.5) = {0}", Math.Round(-1.5));
            AppendLine(sb, " Math.Round(-1.6) = {0}", Math.Round(-1.6));
            AppendLine(sb, " Math.Round(-1.24) = {0}", Math.Round(-1.24));
            AppendLine(sb, " Math.Round(-1.25) = {0}", Math.Round(-1.25));
            AppendLine(sb, " Math.Round(-1.26) = {0}", Math.Round(-1.26));
            AppendLine(sb, " Math.Round(-1.34) = {0}", Math.Round(-1.34));
            AppendLine(sb, " Math.Round(-1.35) = {0}", Math.Round(-1.35));
            AppendLine(sb, " Math.Round(-1.36) = {0}", Math.Round(-1.36));
            AppendLine(sb, " Math.Round(-1.24, 1) = {0}", Math.Round(-1.24, 1));
            AppendLine(sb, " Math.Round(-1.25, 1) = {0}", Math.Round(-1.25, 1));
            AppendLine(sb, " Math.Round(-1.26, 1) = {0}", Math.Round(-1.26, 1));
            AppendLine(sb, " Math.Round(-1.34, 1) = {0}", Math.Round(-1.34, 1));
            AppendLine(sb, " Math.Round(-1.35, 1) = {0}", Math.Round(-1.35, 1));
            AppendLine(sb, " Math.Round(-1.36, 1) = {0}", Math.Round(-1.36, 1));
            AppendLine(sb, " Math.Round(-1.24, 1, MidpointRounding.AwayFromZero) = {0}", Math.Round(-1.24, 1, MidpointRounding.AwayFromZero));
            AppendLine(sb, " Math.Round(-1.25, 1, MidpointRounding.AwayFromZero) = {0}", Math.Round(-1.25, 1, MidpointRounding.AwayFromZero));
            AppendLine(sb, " Math.Round(-1.26, 1, MidpointRounding.AwayFromZero) = {0}", Math.Round(-1.26, 1, MidpointRounding.AwayFromZero));
            AppendLine(sb, " Math.Round(-1.34, 1, MidpointRounding.AwayFromZero) = {0}", Math.Round(-1.34, 1, MidpointRounding.AwayFromZero));
            AppendLine(sb, " Math.Round(-1.35, 1, MidpointRounding.AwayFromZero) = {0}", Math.Round(-1.35, 1, MidpointRounding.AwayFromZero));
            AppendLine(sb, " Math.Round(-1.36, 1, MidpointRounding.AwayFromZero) = {0}", Math.Round(-1.36, 1, MidpointRounding.AwayFromZero));
            AppendLine(sb, " Math.Round(-1.24, 1, MidpointRounding.ToEven) = {0}", Math.Round(-1.24, 1, MidpointRounding.ToEven));
            AppendLine(sb, " Math.Round(-1.25, 1, MidpointRounding.ToEven) = {0}", Math.Round(-1.25, 1, MidpointRounding.ToEven));
            AppendLine(sb, " Math.Round(-1.26, 1, MidpointRounding.ToEven) = {0}", Math.Round(-1.26, 1, MidpointRounding.ToEven));
            AppendLine(sb, " Math.Round(-1.34, 1, MidpointRounding.ToEven) = {0}", Math.Round(-1.34, 1, MidpointRounding.ToEven));
            AppendLine(sb, " Math.Round(-1.35, 1, MidpointRounding.ToEven) = {0}", Math.Round(-1.35, 1, MidpointRounding.ToEven));
            AppendLine(sb, " Math.Round(-1.36, 1, MidpointRounding.ToEven) = {0}", Math.Round(-1.36, 1, MidpointRounding.ToEven));

            ToFile(sb, "MathRoundTest");
            return;
        }
        #endregion // MathRoundTest

        // ヘルパ

        #region ToFile
        /// <summary>
        /// テスト結果をファイルに出力します。
        /// </summary>
        /// <param name="result">テスト結果文字列</param>
        /// <param name="testName">テスト名</param>
        public static void ToFile(string result, string testName)
        {
            if (string.IsNullOrEmpty(result))
            {
                return;
            }

            if (!Directory.Exists(LOG_DIR_PATH))
            {
                Directory.CreateDirectory(LOG_DIR_PATH);
            }

            string format = Path.Combine(LOG_DIR_PATH, LOG_FILE_FORMAT);
            string path = string.Format(format, DateTime.Now.ToString("yyyyMMdd_HHmmss"), testName);
            using (var sw = new StreamWriter(path))
            {
                sw.Write(result);
            }
        }
        /// <summary>
        /// テスト結果をファイルに出力します。
        /// </summary>
        /// <param name="sb">テスト結果文字列</param>
        /// <param name="testName">テスト名</param>
        private void ToFile(StringBuilder sb, string testName)
        {
            if (sb == null || sb.Length == 0)
            {
                return;
            }
            ToFile(sb.ToString(), testName);
        }
        /// <summary>
        /// テスト結果をファイルに出力します。
        /// </summary>
        /// <param name="sb">テスト結果文字列</param>
        /// <param name="testName">テスト名</param>
        /// <param name="yyyymmdd">日時フォーマット</param>
        private void ToFile(StringBuilder sb, string testName, string yyyymmdd)
        {
            if (sb == null || sb.Length == 0)
            {
                return;
            }
            string nameWithDate = string.Format("{0}_{1}", testName, DateTime.Now.ToString(yyyymmdd));
            ToFile(sb.ToString(), nameWithDate);
        }
        #endregion // ToFile

        #region SplitString
        /// <summary>
        /// <para>文字列を指定した分割用文字列で分割し、配列を取得します。</para>
        /// <para>(string.Split(char) を拡張してstring型での分割を受け付けるようにしたメソッド)</para>
        /// </summary>
        /// <param name="src">分割対象の文字列</param>
        /// <param name="separator">分割用文字列(セパレータ)</param>
        /// <returns>分割用文字列ごとに文字列を分割した配列</returns>
        protected string[] SplitString(string src, string separator)
        {
            if (string.IsNullOrEmpty(separator))
            {
                return new string[] { };
            }
            if (string.IsNullOrEmpty(separator))
            {
                return new[] { src };
            }

            var segments = new List<string>();
            string segment = "";

            int pos = 0;
            int gap = separator.Length;
            while (pos < src.Length)
            {
                int searchEnd = pos + gap - 1;
                if (src.Length <= searchEnd)
                {
                    segment += src[pos];
                    pos++;
                }
                else if (src.Substring(pos, gap) == separator)
                {
                    segments.Add(segment);
                    segment = "";
                    pos += gap;
                }
                else
                {
                    segment += src[pos];
                    pos++;
                }
            }
            segments.Add(segment);

            return segments.ToArray();
        }
        #endregion // SplitString

        #region Append
        /// <summary>
        /// <para>[StringBuilder.AppendFormat のラッパ]</para>
        /// <para>指定のフォーマットでStringBuilderオブジェクトに文字列を追記します。</para>
        /// </summary>
        /// <param name="sb">対象のStringBuilderオブジェクト</param>
        /// <param name="format">文字列のフォーマット</param>
        /// <param name="args">置換用文字列の配列</param>
        private void Append(StringBuilder sb, string format, params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                sb.Append(format);
                return;
            }
            sb.AppendFormat(format, args);
        }
        #endregion // Append

        #region AppendLine
        /// <summary>
        /// 指定のフォーマットでStringBuilderオブジェクトに文字列を1行分追記します。
        /// </summary>
        /// <param name="sb">対象のStringBuilderオブジェクト</param>
        /// <param name="format">文字列のフォーマット</param>
        /// <param name="args">置換用文字列の配列</param>
        private void AppendLine(StringBuilder sb, string format, params object[] args)
        {
            sb.AppendLine(string.Format(format, args));
        }
        #endregion // AppendLine

        #region AppendResultLogLine
        /// <summary>
        /// StringBuilderオブジェクトに計算結果ログの行を追記します。
        /// </summary>
        /// <param name="sb">対象のStringBuilderオブジェクト</param>
        /// <param name="expression">計算式文字列</param>
        /// <param name="value">計算値</param>
        private void AppendResultLogLine(StringBuilder sb, string expression, double value)
        {
            AppendLine(sb, "{0} = {1}", expression, value);
        }
        #endregion // AppendResultLogLine
    }
}