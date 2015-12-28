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
    public partial class frmAccount : Form
    {
        private string accountId;
        private string customerId;
        private string accountTypeId;
        private string strSave;

        private frmMain main;
        public frmAccount()
        {
            InitializeComponent();
        }

        private void loadAccountTypes()
        {

            List<string> accountTypeNameList;
            try
            {
                accountTypeNameList = KanbDB.GetAccountTypeNames();
                if (accountTypeNameList.Count > 0)
                {
                    string accountTypeName;
                    cboAccountType.Items.Clear();
                    for (int i = 0; i < accountTypeNameList.Count; i++)
                    {
                        accountTypeName = accountTypeNameList[i];
                        cboAccountType.Items.Add(accountTypeName.Trim());
                    }
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
            List<Customer> customerList = new List<Customer>();
            try
            {
                customerList = KanbDB.GetCustomers();
                if (customerList.Count > 0)
                {
                    Customer customer = new Customer();
                    cboCustomerName.Items.Clear();
                    for (int i = 0; i < customerList.Count; i++)
                    {
                        customer = customerList[i];
                        cboCustomerName.Items.Add(customer.ID.ToString() + " " + customer.FirstName.Trim() + " " + customer.LastName.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void loadAccount(int accountId)
        {
            Account account;
            try
            {
                account = KanbDB.GetAccount(accountId);
                Customer customer;
                try
                {
                    customerId = account.CustomerID.ToString();
                    customer = KanbDB.GetCustomer(account.CustomerID);
                    cboCustomerName.SelectedText = customer.FirstName.Trim() + " " + customer.LastName.Trim();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
                }
                loadAccountTypes();
                accountTypeId = account.AccountTypeID.ToString();
                cboAccountType.SelectedText = KanbDB.GetAccountTypeName(account.AccountTypeID);
                txtBalance.Text = account.Balance.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void loadAccounts(object sender, EventArgs e)
        {
            lstAccount.Items.Clear();
            List<Account> accountList;
            try
            {
                accountList = KanbDB.GetAccounts();
                if (accountList.Count > 0)
                {
                    Account account;
                    for (int i = 0; i < accountList.Count; i++)
                    {
                        account = accountList[i];
                        lstAccount.Items.Add(account.ID.ToString());
                        Customer customer;
                        try
                        {
                            customer = KanbDB.GetCustomer(account.CustomerID);
                            lstAccount.Items[i].SubItems.Add(customer.FirstName.Trim() + " " + customer.LastName.Trim());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                            Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
                        }

                        lstAccount.Items[i].SubItems.Add(KanbDB.GetAccountTypeName(account.AccountTypeID).Trim());
                        lstAccount.Items[i].SubItems.Add(account.Balance.ToString());
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
        private void enableAccInfo(bool a)
        {
            cboCustomerName.Enabled = a;
            cboAccountType.Enabled = a;
            txtBalance.Enabled = a;

            btnAdd.Enabled = !a;
            btnEdit.Enabled = !a;
            btnDelete.Enabled = !a;
            btnSave.Enabled = a;
            btnCancel.Enabled = a;
        }
        private void clearAccFrm()
        {
            txtBalance.Clear();
        }

        private bool isFrmValid()
        {
            bool valid =true;
            int i = 0;
            string[] arrError = new string[3];
            if (!Common.listboxIsValid(cboCustomerName) && String.IsNullOrEmpty(customerId))
            {
                arrError[i]="Select a Customer";
                valid = false;
            }
            if (!Common.listboxIsValid(cboAccountType) && String.IsNullOrEmpty(accountTypeId))
            {
                arrError[i] = "Select an Account Type";
                valid = false;
            }
            if (!Common.textboxIsValid(txtBalance))
            {
                arrError[i] = "Balance is InValid";
                valid = false;
            }

            if (!valid)
            {
                string strError = "";
                for (int x = 0; x < arrError.Length; x++)
                    strError += arrError[x] + "\n";

                MessageBox.Show(strError, "Form Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return valid;
        }
        private void frmAccounts_Load(object sender, EventArgs e)
        {
            enableAccInfo(false);
            loadAccounts(sender, e);
            main = (frmMain) Application.OpenForms["frmMain"];
            
        }
        private void lstAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            var si = lstAccount.SelectedItems;
            if (si.Count > 0)
            {
                // Display text of first item selected.
                accountId = si[0].Text;
                loadAccount(Convert.ToInt32(accountId));
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            strSave = "Add";
            enableAccInfo(true);
            clearAccFrm();
            loadCustomerName();
            loadAccountTypes();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (main.getUserRole()=="Creator/Viewer")
            {
                MessageBox.Show("Update Permissions are required to \'Edit\' ","Permissions",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            if (!String.IsNullOrEmpty(accountId))
            {
                enableAccInfo(true);
            }

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (main.getUserRole() == "Authoriser" || main.getUserRole() == "Creator/Viewer")
            {
                MessageBox.Show("Delete Permissions are required to \'Delete\' ", "Permissions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!String.IsNullOrEmpty(accountId))
            {
                DialogResult m = MessageBox.Show("Are you sure you want to delete", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (m == DialogResult.Yes)
                {
                    KanbDB.DeleteAccount(Convert.ToInt32(accountId));
                    loadAccounts(sender, e);
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
                KanbDB.AddAccount(Convert.ToInt32(customerId), Convert.ToInt32(accountTypeId), Convert.ToDouble(txtBalance.Text));
                enableAccInfo(false);
                clearAccFrm();
            }
            else if (strSave == "Edit")
            {
                if (!String.IsNullOrEmpty(accountId))
                {
                    KanbDB.UpdateAccount(Convert.ToInt32(accountId), Convert.ToInt32(customerId), Convert.ToInt32(accountTypeId), Convert.ToDouble(txtBalance.Text));
                }
            }
            loadAccounts(sender, e);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            enableAccInfo(false);
            clearAccFrm();
        }
        private void cboAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            accountTypeId = cboAccountType.SelectedIndex.ToString();
        }
        private void cboCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string customer = cboCustomerName.Text;
            string[] a = customer.Split(' ');
            customerId = a[0]; Console.Write(a[0]);
        }

    }
}
