using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experiments
{
    public class DelegateSupplier
    {
        private DelegateUser user;

        private int result;
        public int Result
        {
            get { return this.result; }
            set { this.result = value; }
        }

        public DelegateSupplier()
        {
            this.result = 0;
            this.user = new DelegateUser();
            SetAddition(AddMethod);
        }

        public void SetAddition(DelegateUser.AddDelegate addition)
        {
            this.user.DAdd += addition;
        }

        public void RemoveAddition(DelegateUser.AddDelegate addition)
        {
            this.user.DAdd -= addition;
        }

        public void ExecuteAddition(int left, int right)
        {
            this.user.DAdd(left, right, ref this.result);
        }

        private void AddMethod(int left, int right, ref int result)
        {
            result += left + right;
        }
    }

    public class DelegateUser
    {
        public delegate void AddDelegate(int left, int right, ref int result);

        public AddDelegate DAdd { get; set; }
    }
}
