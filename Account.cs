using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanb
{
    public class Account
    {
        private int id;
        private int customerId;
        private int accountTypeId;
        private double balance;

        public Account() { }

        public int ID{
            get { return id; }
            set { id = value; }
        }
        public int CustomerID
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public int AccountTypeID
        {
            get { return accountTypeId; }
            set { accountTypeId = value; }
        }
        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }

    }
}
