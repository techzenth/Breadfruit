using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kanb
{
    public partial class frmTransaction : Form
    {
        private string strSave;
        private string transactionId;
        private string accountId;
        private string transactionTypeId;
        public frmTransaction()
        {
            InitializeComponent();
        }
        private void showCustomerList(bool a)
        {
            txtCustomerName.Visible = !a;
            cboCustomerName.Visible = a;
            if (a)
            {
                loadCustomerName();
            }
        }
        private void loadTransactionTypes()
        {
            cboTransactionType.Items.Clear();
            List<string> transactionTypes;

            try
            {
                transactionTypes = KanbDB.GetTransactionTypeNames();
                if (transactionTypes.Count > 0)
                {
                    for (int i = 0; i < transactionTypes.Count; i++)
                    {
                        string transactionType = transactionTypes[i];
                        cboTransactionType.Items.Add(transactionType);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void loadTransaction(int id) 
        {
            Transaction transaction = new Transaction();
            try
            {
                transaction = KanbDB.GetTransaction(id);
                Account account = new Account();
                try
                {
                    account = KanbDB.GetAccount(transaction.AccountID);
                    accountId = account.ID.ToString();
                    Customer customer = new Customer();
                    try
                    {
                        customer = KanbDB.GetCustomer(account.CustomerID);
                        txtCustomerName.Text = customer.FirstName.Trim() + " " + customer.LastName.Trim();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
                loadTransactionTypes();
                cboTransactionType.SelectedIndex = transaction.TransactionTypeID - 1;
                txtAmount.Text = Convert.ToString(transaction.TransactionAmount);
                dtpTransactionDate.Text = transaction.TransactionDate.ToLongDateString();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void loadTransactions(object sender, EventArgs e)
        {
            lstTransactions.Items.Clear();
            List<Transaction> transactionList = new List<Transaction>();
            try
            {
                transactionList = KanbDB.GetTransactions();
                if (transactionList.Count>0)
                {
                
                    for (int i = 0; i < transactionList.Count; i++)
                    {
                        Transaction transaction = new Transaction();
                        transaction = transactionList[i];
                        lstTransactions.Items.Add(transaction.ID.ToString());
                        Account account = new Account();
                        try
                        {
                            account = KanbDB.GetAccount(transaction.AccountID);
                            Customer customer = new Customer();
                            try
                            {
                                customer = KanbDB.GetCustomer(account.CustomerID);
                                lstTransactions.Items[i].SubItems.Add(customer.FirstName.Trim() + " " + customer.LastName.Trim());
                            }
                            catch (Exception ex)
                            {
                                
                                throw ex;
                            }
                        }
                        catch (Exception ex)
                        {
                            
                            throw ex;
                        }
                        

                        
                        string transactionType;
                        try
                        {
                            transactionType = KanbDB.GetTransactionTypeName(transaction.TransactionTypeID);
                            lstTransactions.Items[i].SubItems.Add(transactionType.Trim());
                        }
                        catch (Exception ex)
                        {
                            
                            throw ex;
                        }
                        
                        lstTransactions.Items[i].SubItems.Add(transaction.TransactionAmount.ToString());
                        lstTransactions.Items[i].SubItems.Add(transaction.TransactionDate.ToShortDateString());
                    }
                }
                else
                {
                    btnAdd_Click(sender,e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void loadCustomerName()
        {
            List<Account> accountList = new List<Account>();
            try
            {
                accountList = KanbDB.GetAccounts();
                if (accountList.Count > 0)
                {
                    Account account = new Account();
                    cboCustomerName.Items.Clear();
                    for (int i = 0; i < accountList.Count; i++)
                    {
                        account = accountList[i];
                        Customer customer = new Customer();
                        try
                        {
                            customer = KanbDB.GetCustomer(account.CustomerID);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                            Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
                        }
                        cboCustomerName.Items.Add(account.ID + " " + customer.FirstName.Trim() + " " + customer.LastName.Trim());
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void enableTransInfo(bool a)
        {
            cboCustomerName.Enabled = a;
            cboTransactionType.Enabled = a;
            txtCustomerName.Enabled = a;
            txtAmount.Enabled = a;
            dtpTransactionDate.Enabled = a;

            btnAdd.Enabled = !a;
            btnEdit.Enabled = !a;
            btnDelete.Enabled = !a;
            btnSave.Enabled = a;
            btnCancel.Enabled = a;

        }
        private void clearTransFrm()
        {
            txtCustomerName.Clear();
            txtAmount.Clear();
        }
        private bool isFrmValid()
        {
            bool valid = true;
            int i = 0;
            string[] arrError = new string[3];
            if (!Common.listboxIsValid(cboCustomerName) && string.IsNullOrEmpty(accountId))
            {
                arrError[i] = "Select Customer!";
                valid = false;
                i++;
            }
            if (!Common.listboxIsValid(cboTransactionType) && string.IsNullOrEmpty(transactionTypeId))
            {
                arrError[i] = "Select Transaction type";
                valid = false;
                i++;
            }
            if(!Common.textboxIsValid(txtAmount)){
                arrError[i] = "Transaction amount invalid!";
                valid = false;
                i++;
            }
            if (!valid)
            {
                string strError = "";
                for (int x = 0; x < arrError.Length; x++)
                {
                    strError += arrError[x] + "\n";
                }
                MessageBox.Show(strError, "Form Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return valid;

        }

        private void frmTransaction_Load(object sender, EventArgs e)
        {
            showCustomerList(false);
            enableTransInfo(false);
            loadTransactions(sender,e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            strSave = btnAdd.Text;
            showCustomerList(true);
            loadTransactionTypes();
            enableTransInfo(true);
            clearTransFrm();
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(transactionId))
            {
                strSave = btnEdit.Text;
                showCustomerList(false);
                enableTransInfo(true);

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(transactionId))
            {
                DialogResult m = MessageBox.Show("Are you sure you want to delete", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (m == DialogResult.Yes)
                {
                    KanbDB.DeleteTransaction(Convert.ToInt32(transactionId));
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isFrmValid())
            {
                return;
            }
            if (strSave == "Add")
            {
                KanbDB.AddTransaction(Convert.ToInt32(accountId), Convert.ToInt32(transactionTypeId), Convert.ToDouble(txtAmount.Text), Convert.ToString(txtTransactionDescription.Text),Convert.ToDateTime(dtpTransactionDate.Text).ToShortDateString());
                clearTransFrm();
                enableTransInfo(false);
                strSave = "";

            }
            else if (strSave == "Edit")
            {
                if (!String.IsNullOrEmpty(transactionId))
                {
                    KanbDB.UpdateTransaction(Convert.ToInt32(transactionId), Convert.ToInt32(accountId), Convert.ToInt32(transactionTypeId), Convert.ToDouble(txtAmount.Text), Convert.ToString(txtTransactionDescription.Text), Convert.ToDateTime(dtpTransactionDate.Text).ToShortDateString());
                }
            }
            loadTransactions(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            enableTransInfo(false);
            clearTransFrm();
        }

        private void dtpTransactionDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cboCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string customer = cboCustomerName.Text;
            string[] a = customer.Split(' ');
            accountId = a[0]; Console.Write(a[0]);
        }

        private void cboTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            transactionTypeId = Convert.ToString(cboTransactionType.SelectedIndex + 1);
        }

        private void lstTransanctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            var si = lstTransactions.SelectedItems;
            if (si.Count > 0)
            {
                transactionId = si[0].Text;
                loadTransaction(Convert.ToInt32(transactionId));
            }
        }
    }
}
