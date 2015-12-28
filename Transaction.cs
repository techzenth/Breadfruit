using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanb
{
    public class Transaction
    {
        private int Id;
        private int accountId;
        private int transactionTypeId;
        private double transactionAmount;
        private string transactionDescription;
        private DateTime transactionDate;
        public Transaction() { }
        public int ID 
        {
            get { return Id; }
            set { Id = value; }
        }
        public int AccountID 
        {
            get { return accountId; }
            set { accountId = value; }
        }
        public int TransactionTypeID
        {
            get { return transactionTypeId; }
            set { transactionTypeId = value; }
        }
        public double TransactionAmount
        {
            get { return transactionAmount; }
            set { transactionAmount = value; }
        }
        public string TransactionDescription
        {
            get { return transactionDescription; }
            set { transactionDescription = value; }
        }
        public DateTime TransactionDate
        {
            get { return transactionDate; }
            set { transactionDate = value; }
        }
    }
}
