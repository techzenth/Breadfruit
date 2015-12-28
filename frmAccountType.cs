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
    public partial class frmAccountType : Form
    {
        private string strSave;
        private string accountTypeId;
        public frmAccountType()
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
                    lstAccountTypes.Items.Clear();
                    for (int i = 0; i < accountTypeNameList.Count; i++)
                    {
                        accountTypeName = accountTypeNameList[i];
                        lstAccountTypes.Items.Add(accountTypeName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void enableAccTypeInfo(bool a){
            
            txtAccountType.Enabled=a;

            btnSave.Enabled = a;
            btnCancel.Enabled = a;

            btnAdd.Enabled = !a;
            btnEdit.Enabled = !a;
        }
        private void clearAccTypeFrm()
        {
            txtAccountType.Clear();
        }

        private bool isFrmValid()
        {
            bool valid = true;
            string[] arrError = new string[1];
            if (!Common.textboxIsValid(txtAccountType))
            {
                arrError[0] = "Account Type Name InValid";
                valid = false;
            }
            if (!valid)
            {
                string strError = "";
                for (int i = 0; i < arrError.Length; i++)
                    strError += arrError[i] + "\n";

                MessageBox.Show(strError, "Form Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return valid;
        }
        private void frmAccountType_Load(object sender, EventArgs e)
        {
            loadAccountTypes();
            enableAccTypeInfo(false);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            strSave = btnAdd.Text;
            enableAccTypeInfo(true);
            clearAccTypeFrm();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            strSave = btnEdit.Text;
            enableAccTypeInfo(true);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isFrmValid())
            {
                return;
            }

            if (strSave == "Add")
            {
                KanbDB.AddAccountType(txtAccountType.Text);
                clearAccTypeFrm();
                enableAccTypeInfo(false);
                strSave = "";
            }
            else if (strSave == "Edit")
            {
                if (!String.IsNullOrEmpty(accountTypeId))
                {
                    KanbDB.UpdateAccountType(Convert.ToInt32(accountTypeId),txtAccountType.Text);
                }
            }
            loadAccountTypes();

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            accountTypeId = "";
            enableAccTypeInfo(false);
            clearAccTypeFrm();
        }
        private void lstAccountTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            accountTypeId = Convert.ToString(lstAccountTypes.SelectedIndex + 1);
            txtAccountType.Text = lstAccountTypes.SelectedItem.ToString();
        }
    }
}
