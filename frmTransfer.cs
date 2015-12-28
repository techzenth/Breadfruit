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
    public partial class frmTransfer : Form
    {
        private string strSave;
        private string transferId;
        private string account1Id;
        private string account2Id;
        private string transferTypeId;

        public frmTransfer()
        {
            InitializeComponent();
        }
        private void loadTransferTypes()
        {
            cboTransferType.Items.Clear();
            List<string> transferTypes;

            try
            {
                transferTypes = KanbDB.GetTransferTypeNames();
                if (transferTypes.Count > 0)
                {
                    for (int i = 0; i < transferTypes.Count; i++)
                    {
                        string transactionType = transferTypes[i];
                        cboTransferType.Items.Add(transactionType);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void loadTransfer(int id)
        {
            Transfer transfer = new Transfer();
            try
            {
                transfer = KanbDB.GetTransfer(Convert.ToInt32(transferId));
                account1Id = transfer.Account1ID.ToString();
                account2Id = transfer.Account2ID.ToString();
                transferTypeId = transfer.TransferTypeID.ToString();
                loadCustomerName();
                cboAccount1.Text = transfer.Account1ID.ToString();
                cboAccount2.Text = transfer.Account2ID.ToString();
                loadTransferTypes();
                cboTransferType.SelectedIndex = transfer.TransferTypeID - 1;
                txtAmount.Text = transfer.TransferAmount.ToString();
                dtpTransferDate.Text = transfer.TransferDate.ToLongDateString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void loadTransfers(object sender, EventArgs e)
        {
            List<Transfer> transferList = new List<Transfer>();
            lstTransfers.Items.Clear();
            try
            {
                transferList = KanbDB.GetTransfers();
                if (transferList.Count > 0)
                {
                    for (int i = 0; i < transferList.Count; i++)
                    {
                        Transfer transfer = new Transfer();
                        transfer = transferList[i];
                        lstTransfers.Items.Add(transfer.ID.ToString());
                        Account account = new Account();
                        try
                        {
                            account = KanbDB.GetAccount(transfer.Account1ID);
                            Customer customer = new Customer();
                            try
                            {
                                customer = KanbDB.GetCustomer(account.CustomerID);
                                lstTransfers.Items[i].SubItems.Add(customer.FirstName.Trim() + " " + customer.LastName.Trim());
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

                        Account account2 = new Account();
                        try
                        {
                            account2 = KanbDB.GetAccount(transfer.Account2ID);
                            Customer customer = new Customer();
                            try
                            {
                                customer = KanbDB.GetCustomer(account2.CustomerID);
                                lstTransfers.Items[i].SubItems.Add(customer.FirstName.Trim() + " " + customer.LastName.Trim());
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
                        
                        string transferType;
                        try
                        {
                            transferType = KanbDB.GetTransferTypeName(transfer.TransferTypeID);
                            lstTransfers.Items[i].SubItems.Add(transferType);
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }

                        lstTransfers.Items[i].SubItems.Add(transfer.TransferAmount.ToString());
                        lstTransfers.Items[i].SubItems.Add(transfer.TransferDate.ToShortDateString());
                    }
                }
                else
                {
                    btnAdd_Click(sender, e);
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
                    cboAccount1.Items.Clear();
                    cboAccount2.Items.Clear();
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
                        cboAccount1.Items.Add(account.ID + " " + customer.FirstName.Trim() + " " + customer.LastName.Trim());
                        cboAccount2.Items.Add(account.ID + " " + customer.FirstName.Trim() + " " + customer.LastName.Trim());
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void enableTransferInfo(bool a)
        {
            cboAccount1.Enabled = a;
            cboAccount2.Enabled = a;
            cboTransferType.Enabled = a;
            txtAmount.Enabled = a;
            dtpTransferDate.Enabled = a;

            btnAdd.Enabled = !a;
            btnEdit.Enabled = !a;
            btnDelete.Enabled = !a;

            btnSave.Enabled = a;
            btnCancel.Enabled = a;

        }
        private void clearTransferFrm()
        {
            txtAmount.Clear();
        }
        private bool isFrmValid()
        {
            bool valid = true;
            int i = 0;
            string[] arrError = new string[4];
            if (!Common.listboxIsValid(cboAccount1) && string.IsNullOrEmpty(account1Id))
            {
                arrError[i] = "Select First Account!";
                valid = false;
                i++;
            }

            if (!Common.listboxIsValid(cboAccount2) && string.IsNullOrEmpty(account2Id))
            {
                arrError[i] = "Select Second Account!";
                valid = false;
                i++;
            }
            if (!Common.listboxIsValid(cboTransferType) && string.IsNullOrEmpty(transferTypeId))
            {
                arrError[i] = "Select Transfer type";
                valid = false;
                i++;
            }
            if (!Common.textboxIsValid(txtAmount))
            {
                arrError[i] = "Transfer amount invalid!";
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
        
        private void frmTransfer_Load(object sender, EventArgs e)
        {
            enableTransferInfo(false);
            loadTransfers(sender,e);
        }

        private void cboAccount1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string customer = cboAccount1.Text;
            string[] a = customer.Split(' ');
            account1Id = a[0]; Console.Write(a[0]);
        }

        private void cboAccount2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string customer = cboAccount2.Text;
            string[] a = customer.Split(' ');
            account2Id = a[0]; Console.Write(a[0]);
        }

        private void cboTransferType_SelectedIndexChanged(object sender, EventArgs e)
        {
            transferTypeId = Convert.ToString(cboTransferType.SelectedIndex + 1);
        }

        private void dtpTransferDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            strSave = btnAdd.Text;
            loadCustomerName();
            loadTransferTypes();
            enableTransferInfo(true);
            clearTransferFrm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(transferId))
            {
                strSave = btnEdit.Text;
                loadCustomerName();
                enableTransferInfo(true);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(transferId))
            {
                DialogResult m = MessageBox.Show("Are you sure you want to delete", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (m == DialogResult.Yes)
                {
                    KanbDB.DeleteTransfer(Convert.ToInt32(transferId));
                    loadTransfers(sender,e);
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
                KanbDB.AddTransfer(Convert.ToInt32(account1Id), Convert.ToInt32(account2Id), Convert.ToInt32(transferTypeId), Convert.ToDouble(txtAmount.Text), Convert.ToDateTime(dtpTransferDate.Text).ToShortDateString());
                clearTransferFrm();
                enableTransferInfo(false);
                strSave = "";
            }
            else if (strSave == "Edit")
            {
                if (!String.IsNullOrEmpty(transferId))
                {
                    KanbDB.UpdateTransfer(Convert.ToInt32(transferId), Convert.ToInt32(account1Id), Convert.ToInt32(account2Id), Convert.ToInt32(transferTypeId), Convert.ToDouble(txtAmount.Text), Convert.ToDateTime(dtpTransferDate.Text).ToShortDateString());
                }
            }
            loadTransfers(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            enableTransferInfo(false);
            clearTransferFrm();
        }

        private void lstTransfers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var si = lstTransfers.SelectedItems;
            if (si.Count > 0)
            {
                transferId = si[0].Text;
                loadTransfer(Convert.ToInt32(transferId));
            }
        }
    }
}
