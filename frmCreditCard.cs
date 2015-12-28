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
    public partial class frmCreditCard : Form
    {
        private string creditCardId;
        private string accountId;
        private string customerId;
        private string creditCardType;
        private string strSave;

        private frmMain main;

        public frmCreditCard()
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
                        cboCustomerName.Items.Add(account.ID + " " + customer.ID + " " + customer.FirstName.Trim() + " " + customer.LastName.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void loadCreditCardTypes()
        {
            cboCreditCardType.Items.Clear();
            List<string> creditCardTypes;

            try
            {
                creditCardTypes = KanbDB.GetCreditCardTypeNames();
                if (creditCardTypes.Count > 0)
                {
                    for (int i = 0; i < creditCardTypes.Count; i++)
                    {
                        string creditCardType = creditCardTypes[i];
                        cboCreditCardType.Items.Add(creditCardType);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void loadCreditCard(int id)
        {
            CreditCard credItCard = new CreditCard();
            try
            {
                credItCard = KanbDB.GetCreditCard(id);
                accountId = credItCard.AccountID.ToString();
                customerId = credItCard.CustomerID.ToString();
                creditCardType = credItCard.CreditCardType.ToString();
                Customer customer = new Customer();
                try
                {
                    customer = KanbDB.GetCustomer(credItCard.CustomerID);
                    txtCustomerName.Text = customer.FirstName.Trim() + " " + customer.LastName.Trim();
                    cboCustomerName.SelectedText = customer.ID.ToString() + " " + customer.FirstName.Trim() + " " + customer.LastName.Trim();
                    cboCustomerName.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
                }
                loadCreditCardTypes();
                cboCreditCardType.SelectedText = KanbDB.GetCreditCardTypeName(credItCard.CreditCardType);
                dtpExpiryDate.Text = credItCard.ExpiryDate.ToLongDateString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);

            }
        }
        private void loadCreditCards(object sender, EventArgs e)
        {
            lstCreditCards.Items.Clear();
            List<CreditCard> creditCardList = new List<CreditCard>();
            try
            {
                creditCardList = KanbDB.GetCreditCards();
                if (creditCardList.Count > 0)
                {
                    CreditCard creditCard = new CreditCard();
                    for (int i = 0; i < creditCardList.Count; i++)
                    {
                        creditCard = creditCardList[i];
                        lstCreditCards.Items.Add(creditCard.ID.ToString());
                        lstCreditCards.Items[i].SubItems.Add(creditCard.AccountID.ToString());
                        Customer customer;
                        try
                        {
                            customer = KanbDB.GetCustomer(creditCard.CustomerID);
                            lstCreditCards.Items[i].SubItems.Add(customer.FirstName.Trim() + " " + customer.LastName.Trim());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                            Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
                        }
                        string creditCardTypeName;
                        try
                        {
                            creditCardTypeName = KanbDB.GetCreditCardTypeName(creditCard.CreditCardType);
                            lstCreditCards.Items[i].SubItems.Add(creditCardTypeName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                            Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
                        }
                        lstCreditCards.Items[i].SubItems.Add(creditCard.ExpiryDate.ToLongDateString());
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
        private void enableCardInfo(bool a)
        {
            cboCustomerName.Enabled = a;
            txtCustomerName.Enabled = a;
            txtPinCode.Enabled = a;
            cboCreditCardType.Enabled = a;
            dtpExpiryDate.Enabled = a;

            btnAdd.Enabled = !a;
            btnEdit.Enabled = !a;
            btnDelete.Enabled = !a;

            btnSave.Enabled = a;
            btnCancel.Enabled = a;
        }
        private void clearCardFrm()
        {
            txtCustomerName.Clear();
            txtPinCode.Clear();
            cboCreditCardType.Items.Clear();

        }
        private bool isFrmValid()
        {
            bool valid = true;
            int i=0;
            string[] arrError = new string[2];
            if(!Common.listboxIsValid(cboCustomerName) && string.IsNullOrEmpty(customerId)){
                arrError[i] = "Select Customer!";
                valid = false;
                i++;
            }
            if (!Common.listboxIsValid(cboCreditCardType) && string.IsNullOrEmpty(creditCardType))
            {
                arrError[i] = "Select Credit Card type";
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

        private void frmCreditCard_Load(object sender, EventArgs e)
        {
            showCustomerList(false);
            enableCardInfo(false);
            loadCreditCards(sender,e);
            main = (frmMain)Application.OpenForms["frmMain"];
        }
        private void cboCreditCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            creditCardType = Convert.ToString(cboCreditCardType.SelectedIndex + 1);
        }
        private void dtpExpiryDate_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine(Convert.ToDateTime(dtpExpiryDate.Text).ToLongDateString());
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            strSave = btnAdd.Text;
            enableCardInfo(true);
            clearCardFrm();
            loadCreditCardTypes();
            showCustomerList(true);
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (main.getUserRole() == "Creator/Viewer")
            {
                MessageBox.Show("Update Permissions are required to \'Edit\' ", "Permissions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!String.IsNullOrEmpty(creditCardId))
            {
                strSave = btnEdit.Text;
                enableCardInfo(true);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (main.getUserRole() == "Authoriser" || main.getUserRole() == "Creator/Viewer")
            {
                MessageBox.Show("Delete Permissions are required to \'Delete\' ", "Permissions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!String.IsNullOrEmpty(creditCardId))
            {
                DialogResult m = MessageBox.Show("Are you sure you want to delete", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (m == DialogResult.Yes)
                {
                    KanbDB.DeleteCreditCard(Convert.ToInt32(creditCardId));
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!isFrmValid()){
                return;
            }

            if (strSave == "Add")
            {
                KanbDB.AddCreditCard(Convert.ToInt32(accountId), Convert.ToInt32(customerId), Convert.ToInt32(txtPinCode.Text), Convert.ToInt32(creditCardType), Convert.ToDateTime(dtpExpiryDate.Text).ToShortDateString());
                clearCardFrm();
                enableCardInfo(false);
                strSave = "";
            }
            else if (strSave == "Edit")
            {
                if (!String.IsNullOrEmpty(creditCardId))
                {
                    KanbDB.UpdateCreditCard(Convert.ToInt32(creditCardId), Convert.ToInt32(accountId), Convert.ToInt32(customerId), Convert.ToInt32(txtPinCode.Text), Convert.ToInt32(creditCardType), Convert.ToDateTime(dtpExpiryDate.Text).ToShortDateString());
                }
            }
            loadCreditCards(sender,e);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            enableCardInfo(false);
            clearCardFrm();
        }
        private void cboCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string customer = cboCustomerName.Text;
            string[] a = customer.Split(' ');
            accountId = a[0]; Console.Write(a[0]);

            Account account = new Account();
            try
            {
                account = KanbDB.GetAccount(Convert.ToInt32(accountId));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
            customerId = account.CustomerID.ToString();
            Console.Write(customerId.ToString() + " " + accountId.ToString());
        }
        private void lstCreditCards_SelectedIndexChanged(object sender, EventArgs e)
        {
            var si = lstCreditCards.SelectedItems;
            if (si.Count > 0)
            {
                creditCardId = si[0].Text;
                loadCreditCard(Convert.ToInt32(creditCardId));
            }
        }
    }
}
