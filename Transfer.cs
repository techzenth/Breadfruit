using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanb
{
    public class Transfer
    {
        private int transferId;
        private int account1Id;
        private int account2Id;
        private int transferTypeId;
        private double transferAmount;
        private DateTime transferDate;

        public int ID
        {
            get { return transferId; }
            set { transferId = value; }
        }
        public int Account1ID
        {
            get { return account1Id; }
            set { account1Id = value; }
        }
        public int Account2ID
        {
            get { return account2Id; }
            set { account2Id = value; }
        }
        public int TransferTypeID
        {
            get { return transferTypeId; }
            set { transferTypeId = value; }
        }
        public double TransferAmount
        {
            get { return transferAmount; }
            set { transferAmount = value; }
        }
        public DateTime TransferDate
        {
            get { return transferDate; }
            set { transferDate = value; }
        }
    }
}
