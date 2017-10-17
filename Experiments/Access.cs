using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;

namespace Experiments
{
    /// <summary>
    /// Accessクラス
    /// </summary>
    /// <remarks>
    /// Acccess DataBaseに関する処理をまとめたクラスです。
    /// </remarks>
    public class Access
    {
        /// <summary>
        /// DBコネクション
        /// </summary>
        private OleDbConnection Conn = null;

        /// <summary>
        /// 実行SQL情報
        /// </summary>
        /// <remarks>
        /// 実行SQL文とパラメータ情報をセットで保持するデータクラスです。
        /// 実行SQL文にパラメータが設定されている場合はパラメータ値を
        /// 設定順番に格納したListを設定してください。
        /// </remarks>
        public class SQLItem
        {
            /// <summary>
            /// 実行SQL文
            /// </summary>
            public string execSql;

            /// <summary>
            /// パラメータ情報リスト
            /// </summary>
            public List<OleDbParameter> paramList;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="sql">実行するSQL</param>
            public SQLItem(string sql)
            {
                execSql = sql;
                paramList = null;
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="sql">実行するSQL</param>
            /// <param name="list">パラメータ情報リスト</param>
            public SQLItem(string sql, List<OleDbParameter> list)
            {
                execSql = sql;
                paramList = list;
            }
        }

        /// <summary>
        /// DB接続処理
        /// </summary>
        /// <param name="accdbPath">AccessDBファイルパス</param>
        /// <param name="pwd">パスワード</param>
        /// <returns>true:成功、false:失敗</returns>
        public Boolean Connect(string accdbPath, string pwd = "")
        {
            Boolean result = true;

            try
            {
                if (Conn == null)
                {
                    Conn = new OleDbConnection();
                }

                // 接続情報の作成
                StringBuilder connStr = new StringBuilder();
connStr.Append("Provider=Microsoft.ACE.OLEDB.12.0;");
                connStr.Append("Data Source=" + accdbPath + ";");
                // パスワードが指定されている場合
                if (pwd.Length > 0)
                {
                    connStr.Append("Jet OLEDB:Database Password=" + pwd + ";");
                }

                // 接続情報の設定
                Conn.ConnectionString = connStr.ToString();
                // Database接続
                Conn.Open();
                //Trn = Conn.BeginTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine  ( "[AccessDB Connect] "+ ex.Message);
                result = false;
                Conn = null;
            }

            return result;
        }

        public void Commit()
        {
            //Trn.Commit();
        }

        /// <summary>
        /// DB切断処理
        /// </summary>
        public void Disconnect()
        {
            if (Conn != null)
            {
                Conn.Close();
                Conn = null;
            }
        }

        /// <summary>
        /// SELECTの実行処理
        /// </summary>
        /// <param name="item">実行SQL情報</param>
        /// <returns>検索結果、取得に失敗した場合はnull</returns>
        /// <remarks>
        /// SELECTのSQLを実行したい場合に使用します。
        /// 取得されたDataTableから値を取得したい場合は、
        /// datatable.Rows[対象行(0～)][対象カラム名 or 対象カラム位置(0～)]で取得が可能です。
        /// </remarks>
        public DataTable ExecuteReader(SQLItem item)
        {
            // DBコネクションが存在しない場合は終了
            if (Conn == null)
            {
                Console.WriteLine("[AccessDB ExecuteReader] " + "Counld not connect.");
                return null;
            }

            DataTable dt = new DataTable();
            using (OleDbCommand cmd = new OleDbCommand())
            {
                try
                {
                    cmd.Connection = Conn;
                    cmd.CommandText = item.execSql;

                    // パラメータが設定されている場合はパラメータを追加する
                    if (item.paramList != null)
                    {
                        foreach (OleDbParameter param in item.paramList)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[AccessDB ExecuteReader] " + ex.Message);
                    dt = null;
                }
            }
            return dt;
        }

        /// <summary>
        /// SELECTの実行処理
        /// </summary>
        /// <param name="item">実行SQL情報</param>
        /// <returns>検索結果、取得に失敗した場合はnull</returns>
        /// <remarks>
        /// SELECTのSQLを実行したい場合に使用します。
        /// 取得されたDataTableから値を取得したい場合は、
        /// datatable.Rows[対象行(0～)][対象カラム名 or 対象カラム位置(0～)]で取得が可能です。
        /// </remarks>
        public int ExecuteNonQuery(SQLItem item)
        {
            // DBコネクションが存在しない場合は終了
            if (Conn == null)
            {
                Console.WriteLine("[AccessDB ExecuteReader] " + "Counld not connect.");
                return -1;
            }
            int result = 0;
            using (OleDbCommand cmd = new OleDbCommand())
            {
                try
                {
                    cmd.Connection = Conn;
                    cmd.CommandText = item.execSql;

                    // パラメータが設定されている場合はパラメータを追加する
                    if (item.paramList != null)
                    {
                        foreach (OleDbParameter param in item.paramList)
                        {
                            cmd.Parameters.Add(param);
                        }
                    }

                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[AccessDB ExecuteReader] " + ex.Message);
                    return -1;
                }
            }

            return result;
        }
    }
}
