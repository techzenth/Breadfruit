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
    public partial class frmDebitCard : Form
    {
        private string strSave;
        private string debitCardId;
        private string accountId;
        private string customerId;
        private string debitCardType;

        public frmDebitCard()
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
        private void loadDebitCardTypes()
        {
            cboDebitCardType.Items.Clear();
            List<string> debitCardTypes;

            try
            {
                debitCardTypes = KanbDB.GetDebitCardTypeNames();
                if (debitCardTypes.Count > 0)
                {
                    for (int i = 0; i < debitCardTypes.Count; i++)
                    {
                        string debitCardType = debitCardTypes[i];
                        cboDebitCardType.Items.Add(debitCardType);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void loadDebitCard(int id)
        {
            DebitCard debitCard = new DebitCard();
            try
            {
                debitCard = KanbDB.GetDebitCard(id);
                accountId = debitCard.AccountID.ToString();
                customerId = debitCard.CustomerID.ToString();
                debitCardType = debitCard.DebitCardType.ToString();
                Customer customer = new Customer();
                try
                {
                    customer = KanbDB.GetCustomer(debitCard.CustomerID);
                    txtCustomerName.Text = customer.FirstName.Trim() + " " + customer.LastName.Trim();
                    cboCustomerName.SelectedText = customer.ID.ToString() + " " + customer.FirstName.Trim() + " " + customer.LastName.Trim();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);

                }
                loadDebitCardTypes();
                cboDebitCardType.SelectedText = KanbDB.GetDebitCardTypeName(debitCard.DebitCardType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);

            }
        }
        private void loadDebitCards(object sender, EventArgs e)
        {
            lstDebitCards.Items.Clear();
            List<DebitCard> debitCardList = new List<DebitCard>();
            try
            {
                debitCardList = KanbDB.GetDebitCards();
                if (debitCardList.Count > 0)
                {
                    DebitCard debitCard = new DebitCard();
                    for (int i = 0; i < debitCardList.Count; i++)
                    {
                        debitCard = debitCardList[i];
                        lstDebitCards.Items.Add(debitCard.ID.ToString());
                        lstDebitCards.Items[i].SubItems.Add(debitCard.AccountID.ToString());
                        Customer customer = new Customer();
                        try
                        {
                            customer = KanbDB.GetCustomer(debitCard.CustomerID);
                            lstDebitCards.Items[i].SubItems.Add(customer.FirstName.Trim() + " " + customer.LastName.Trim());
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                        string debitCardTypeName;
                        try
                        {
                            debitCardTypeName = KanbDB.GetDebitCardTypeName(debitCard.DebitCardType);
                            lstDebitCards.Items[i].SubItems.Add(debitCardTypeName);
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                        lstDebitCards.Items[i].SubItems.Add(debitCard.RegisteredDate.ToShortDateString());

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
        private void enableCardInfo(bool a)
        {
            cboCustomerName.Enabled = a;
            txtCustomerName.Enabled = a;
            txtPinCode.Enabled = a;
            cboDebitCardType.Enabled = a;

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
            cboDebitCardType.Items.Clear();
        }
        private bool isFrmValid()
        {
            bool valid = true;
            int i = 0;
            string[] arrError = new string[2];
            if (!Common.listboxIsValid(cboCustomerName) && String.IsNullOrEmpty(customerId))
            {
                arrError[i] = "Select Customer!";
                valid = false;
                i++;
            }
            if (!Common.listboxIsValid(cboDebitCardType) && String.IsNullOrEmpty(debitCardType))
            {
                arrError[i] = "Select Debit Card type";
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

        private void frmDebitCard_Load(object sender, EventArgs e)
        {
            showCustomerList(false);
            enableCardInfo(false);
            loadDebitCards(sender, e);
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
        }
        private void cboDebitCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            debitCardType = Convert.ToString(cboDebitCardType.SelectedIndex + 1);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            strSave = btnAdd.Text;
            enableCardInfo(true);
            clearCardFrm();
            loadDebitCardTypes();
            showCustomerList(true);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(debitCardId))
            {
                strSave = btnEdit.Text;
                enableCardInfo(true);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(debitCardId))
            {
                DialogResult m = MessageBox.Show("Are you sure you want to delete", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (m == DialogResult.Yes)
                {
                    KanbDB.DeleteDebitCard(Convert.ToInt32(debitCardId));
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
                KanbDB.AddDebitCard(Convert.ToInt32(accountId), Convert.ToInt32(customerId), Convert.ToInt32(txtPinCode.Text), Convert.ToInt32(debitCardType));
                clearCardFrm();
                enableCardInfo(false);
                strSave = "";
            }
            else if (strSave == "Edit")
            {
                if (!String.IsNullOrEmpty(debitCardId))
                {
                    KanbDB.UpdateDebitCard(Convert.ToInt32(debitCardId), Convert.ToInt32(accountId), Convert.ToInt32(customerId), Convert.ToInt32(txtPinCode.Text), Convert.ToInt32(debitCardType));
                }
            }
            loadDebitCards(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            enableCardInfo(false);
            clearCardFrm();
        }

        private void lstDebitCards_SelectedIndexChanged(object sender, EventArgs e)
        {
            var si = lstDebitCards.SelectedItems;
            if (si.Count > 0)
            {
                debitCardId = si[0].Text;
                loadDebitCard(Convert.ToInt32(debitCardId));
            }
        }
    }
}
