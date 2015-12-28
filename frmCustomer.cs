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
    public partial class frmCustomer : Form
    {
        private string strSave;
        private string customerId;
        public frmCustomer()
        {
            InitializeComponent();
        }
        private void loadCustomer(int customerId)
        {
            Customer customer;
            try
            {
                customer = KanbDB.GetCustomer(customerId);
                txtFirstName.Text = customer.FirstName;
                txtLastName.Text = customer.LastName;
                dtpBirthDay.Text = customer.BirthDay.ToLongDateString();
                txtStreet.Text = customer.Street;
                txtAddress.Text = customer.Address;
                txtCity.Text = customer.City;
                txtState.Text = customer.State;
                txtPostalCode.Text = customer.PostalCode;
                txtMobilePhone.Text = customer.MobilePhone;
                txtWorkPhone.Text = customer.WorkPhone;
                txtEmailAddress.Text = customer.EmailAddress;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void loadCustomers(object sender, EventArgs e)
        {
            lstCustomer.Items.Clear();
            List<Customer> customerList;
            try
            {
                customerList = KanbDB.GetCustomers();
                if (customerList.Count > 0)
                {
                    Customer customer;
                    for (int i = 0; i < customerList.Count; i++)
                    {
                        customer = customerList[i];
                        lstCustomer.Items.Add(customer.ID.ToString());
                        lstCustomer.Items[i].SubItems.Add(customer.FirstName.Trim());
                        lstCustomer.Items[i].SubItems.Add(customer.LastName.Trim());
                        lstCustomer.Items[i].SubItems.Add(customer.MobilePhone.Trim());
                        lstCustomer.Items[i].SubItems.Add(customer.EmailAddress.Trim());
                        lstCustomer.Items[i].SubItems.Add(customer.RegisteredDate.ToShortDateString());
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
        void enableCustInfo(bool a)
        {
            txtFirstName.Enabled = a;
            txtLastName.Enabled = a;
            dtpBirthDay.Enabled = a;
            txtStreet.Enabled = a;
            txtAddress.Enabled = a;
            txtCity.Enabled = a;
            txtState.Enabled = a;
            txtPostalCode.Enabled = a;
            txtMobilePhone.Enabled = a;
            txtWorkPhone.Enabled = a;
            txtEmailAddress.Enabled = a;

            btnSave.Enabled = a;
            btnCancel.Enabled = a;

            btnAdd.Enabled = !a;
            btnEdit.Enabled = !a;
            btnDelete.Enabled = !a;

        }
        void clearCustFrm()
        {
            customerId = "";
            txtFirstName.Clear();
            txtLastName.Clear();
            txtStreet.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtState.Clear();
            txtPostalCode.Clear();
            txtMobilePhone.Clear();
            txtWorkPhone.Clear();
            txtEmailAddress.Clear();
        }
        private bool isFrmValid()
        {
            bool valid = true;
            int i = 0;
            string[] arrError = new string[10];
            if (!Common.textboxIsValid(txtFirstName))
            {
                arrError[i] = "Firstname is Invalid";
                valid = false;
                i++;
            }
            if (!Common.textboxIsValid(txtLastName))
            {
                arrError[i] = "Lastname is Invalid";
                valid = false;
                i++;
            }
            if (!Common.textboxIsValid(txtFirstName))
            {
                arrError[i] = "Street is Invalid";
                valid = false;
                i++;
            }
            if (!Common.textboxIsValid(txtAddress))
            {
                arrError[i] = "Address is Invalid";
                valid = false;
                i++;
            }
            if (!Common.textboxIsValid(txtCity))
            {
                arrError[i] = "City is Invalid";
                valid = false;
                i++;
            }
            if (!Common.textboxIsValid(txtState))
            {
                arrError[i] = "State is Invalid";
                valid = false;
                i++;
            }
            if (!Common.textboxIsValid(txtPostalCode))
            {
                arrError[i] = "Postal Code is Invalid";
                valid = false;
                i++;
            }
            if (!Common.textboxIsValid(txtMobilePhone))
            {
                arrError[i] = "Mobile Phone is Invalid";
                valid = false;
                i++;
            }
            if (!Common.textboxIsValid(txtWorkPhone))
            {
                arrError[i] = "Work Phone is Invalid";
                valid = false;
                i++;
            }
            if (!Common.textboxIsValid(txtEmailAddress))
            {
                arrError[i] = "Email Address is Invalid";
                valid = false;
                i++;
            }

            if (!valid)
            {
                string strError="";
                for (int x = 0; x < arrError.Length; x++)
                    strError += arrError[x] + "\n";

                    MessageBox.Show(strError, "Form Error");
            }

            return valid;
        }
        
        private void frmCustomer_Load(object sender, EventArgs e)
        {
            enableCustInfo(false);
            loadCustomers(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isFrmValid())
            {
                return;
            }

            if (strSave == "Add")
            {
                KanbDB.AddCustomer(txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtStreet.Text.Trim(), txtAddress.Text.Trim(), txtCity.Text.Trim(), txtState.Text.Trim(), txtPostalCode.Text.Trim(), txtMobilePhone.Text.Trim(), txtWorkPhone.Text.Trim(), txtEmailAddress.Text.Trim());
                clearCustFrm();
                enableCustInfo(false);
                strSave = "";
            }
            else if (strSave == "Edit")
            {
                if (!String.IsNullOrEmpty(customerId))
                {
                    KanbDB.UpdateCustomer(customerId, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtStreet.Text.Trim(), txtAddress.Text.Trim(), txtCity.Text.Trim(), txtState.Text.Trim(), txtPostalCode.Text.Trim(), txtMobilePhone.Text.Trim(), txtWorkPhone.Text.Trim(), txtEmailAddress.Text.Trim());
                }
            }
            loadCustomers(sender, e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            strSave = btnAdd.Text;
            enableCustInfo(true);
            clearCustFrm();

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            clearCustFrm();
            enableCustInfo(false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(customerId))
            {
                strSave = btnEdit.Text;
                enableCustInfo(true);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(customerId))
            {
                DialogResult m = MessageBox.Show("Are you sure you want to delete", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (m == DialogResult.Yes)
                {
                    KanbDB.DeleteCustomer(Convert.ToInt32(customerId));
                    clearCustFrm();
                    enableCustInfo(false);
                }
            }
        }
        private void lstCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var si = lstCustomer.SelectedItems;
            if (si.Count > 0)
            {
                // Display text of first item selected.
                customerId = si[0].Text;
                loadCustomer(Convert.ToInt32(customerId));
            }

        }



    }
}
