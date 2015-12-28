using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanb
{
    public class DebitCard
    {
        private int debitCardId;
        private int accountId;
        private int customerId;
        private int pinCode;
        private int debitCardType;
        private DateTime registeredDate;
        public DebitCard() { }
        public int ID 
        {
            get { return debitCardId; }
            set { debitCardId = value; }
        }
        public int AccountID
        {
            get { return accountId; }
            set { accountId = value; }
        }
        public int CustomerID
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public int PinCode
        {
            get { return pinCode; }
            set { pinCode = value; }
        }
        public int DebitCardType
        {
            get { return debitCardType; }
            set { debitCardType = value; }
        }
        public DateTime RegisteredDate
        {
            get { return registeredDate; }
            set { registeredDate = value; }
        }
    }
}
