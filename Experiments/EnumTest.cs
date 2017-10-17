using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experiments
{
    using Base;

    public class DerivedEnum : BaseEnum
    {
        public static new List<BaseEnum> Items =  new List<BaseEnum>();

        public static DerivedEnum Area = new DerivedEnum(1, 1, "Area",
            new ValueItem(ValueItem.Usage.IsUsed, "F2", 1.23d, ValueType.Sum),
            new ValueItem(ValueItem.Usage.IsUsed, "F2", 2.34d, ValueType.Ave),
            new ValueItem(ValueItem.Usage.IsNotUsed, "F1", 3.45d, ValueType.SD));

        public static DerivedEnum Diameter = new DerivedEnum(2, 2, "Diameter",
            new ValueItem(ValueItem.Usage.IsUsed, "D", 123.4567d, ValueType.Sum),
            new ValueItem(ValueItem.Usage.IsUsed, "D2", 234.5678d, ValueType.Ave),
            new ValueItem(ValueItem.Usage.IsNotUsed, "N1", 12345.6789d, ValueType.SD));

        protected DerivedEnum(int id, int group, string name, ValueItem valueSum, ValueItem valueAve, ValueItem valueSD)
            : base(id, group, name, valueSum, valueAve, valueSD)
        {
            Items.Add(this);
        }
    }

    namespace Base
    {
        public abstract class BaseEnum
        {
[Obsolete("インターフェースを定義するために存在しているメンバです。派生クラスでnewしてください。", true)]
            public static List<BaseEnum> Items;

            public int ID { get; protected set; }
            public int Group { get; protected set; }
            public string Name { get; protected set; }

            protected List<ValueItem> values;
            public ValueItem Sum { get { return GetValue(ValueType.Sum); } }
            public ValueItem Average { get { return GetValue(ValueType.Ave); } }
            public ValueItem SD { get { return GetValue(ValueType.SD); } }

            protected BaseEnum(int id, int group, string name, ValueItem valueSum, ValueItem valueAve, ValueItem valueSD)
            {
                this.ID = id;
                this.Group = group;
                this.Name = name;

                this.values = new List<ValueItem>();
                this.values.Add(valueSum);
                this.values.Add(valueAve);
                this.values.Add(valueSD);
            }

            protected ValueItem GetValue(ValueType valType)
            {
                return this.values.Where(val => (val.Type == valType)).FirstOrDefault();
            }
        }

        public class ValueItem
        {
            public enum Usage
            {
                IsUsed,
                IsNotUsed
            }

            public bool IsUsed { get; private set; }
            public string Format { get; private set; }
            public double Value { get; set; }
            public ValueType Type { get; private set; }

            public ValueItem(Usage use, string format, double value, ValueType valType)
            {
                this.IsUsed = (use == Usage.IsUsed);
                this.Format = format;
                this.Value = value;
                this.Type = valType;
            }

            public override string ToString()
            {
                if (this.Format.Substring(0, 1) == "D")
                {
                    return ((int)this.Value).ToString(this.Format);
                }

                return this.Value.ToString(this.Format);
            }
        }

        public enum ValueType
        {
            Sum = 0,
            Ave,
            SD
        }
    }
}

namespace Sample
{
    /// <summary>
    /// ステータスオプション基底 : 全プラグインのステータスを定義しておく
    /// </summary>
    public abstract class BaseObjectStatusOption
    {
        protected ObjectStatusInfo m_Alive = null;  // 基底では null
        public ObjectStatusInfo Alive   // デフォルト用のステータス
        {
            get
            {
                if (this.m_Alive != null) { return this.m_Alive; }
                throw new Exception();
            }
        }

        protected ObjectStatusInfo m_CellBody = null;   // 基底では null
        public ObjectStatusInfo CellBody    // 神経突起プラグイン用のステータス
        {
            get
            {
                if (this.m_CellBody != null) { return this.m_CellBody; }
                throw new Exception();
            }
        }
    }

    /// <summary>
    /// デフォルトプラグイン用ステータスオプション
    /// </summary>
    public class SpheroidStatus : BaseObjectStatusOption
    {
        /// <remarks>
        /// 生スフェロイド: 有効
        /// セルボディー: null --> 無効、プロパティを取得すると例外
        /// </remarks>
        public SpheroidStatus()
        {
            this.m_Alive = new ObjectStatusInfo(1, "live spheroid");
        }
    }

    /// <summary>
    /// ステータス情報
    /// </summary>
    public class ObjectStatusInfo
    {
        public long Code { get; private set; }
        public string Message { get; private set; }
        public ObjectStatusInfo(long code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
    }
}
