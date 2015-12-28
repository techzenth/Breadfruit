using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanb
{
    public class CreditCard
    {
        private int creditCardId;
        private int accountId;
        private int customerId;
        private int pinCode;
        private int creditCardType;
        private DateTime registeredDate;
        private DateTime expiryDate;
        public CreditCard() { }
        public int ID
        {
            get { return creditCardId; }
            set { creditCardId = value; }
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
        public int CreditCardType
        {
            get { return creditCardType; }
            set { creditCardType = value; }
        }
        public DateTime RegisteredDate
        {
            get { return registeredDate; }
            set { registeredDate = value; }
        }
        public DateTime ExpiryDate
        {
            get { return expiryDate; }
            set { expiryDate = value; }
        }
    }
}
