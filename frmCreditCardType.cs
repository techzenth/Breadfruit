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
    public partial class frmCreditCardType : Form
    {
        private string strSave;
        private string creditCardTypeId;
        public frmCreditCardType()
        {
            InitializeComponent();
        }
        private void loadCreditCardTypes()
        {
            List<string> creditCardTypeNameList;
            try
            {
                creditCardTypeNameList = KanbDB.GetCreditCardTypeNames();
                if (creditCardTypeNameList.Count > 0)
                {
                    string accountTypeName;
                    lstCreditCardTypes.Items.Clear();
                    for (int i = 0; i < creditCardTypeNameList.Count; i++)
                    {
                        accountTypeName = creditCardTypeNameList[i];
                        lstCreditCardTypes.Items.Add(accountTypeName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void enableCreditType(bool a)
        {

            txtCreditCardType.Enabled = a;

            btnSave.Enabled = a;
            btnCancel.Enabled = a;

            btnAdd.Enabled = !a;
            btnEdit.Enabled = !a;
        }
        private void clearCreditType()
        {
            txtCreditCardType.Clear();
        }
        
        private bool isFrmValid()
        {
            bool valid = true;
            string[] arrError = new string[1];
            if (!Common.textboxIsValid(txtCreditCardType))
            {
                arrError[0] = "Credit Card Type Name InValid";
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
        private void frmCreditCardType_Load(object sender, EventArgs e)
        {
            enableCreditType(false);
            loadCreditCardTypes();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            strSave = btnAdd.Text;
            enableCreditType(true);
            clearCreditType();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            strSave = btnEdit.Text;
            enableCreditType(true);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isFrmValid())
            {
                return;
            }
            if (strSave == "Add")
            {
                KanbDB.AddCreditCardType(txtCreditCardType.Text);
                clearCreditType();
                enableCreditType(false);
                strSave = "";
            }
            else if (strSave == "Edit")
            {
                if (!String.IsNullOrEmpty(creditCardTypeId))
                {
                    KanbDB.UpdateCreditCardType(Convert.ToInt32(creditCardTypeId), txtCreditCardType.Text);
                }
            }
            loadCreditCardTypes();

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            creditCardTypeId = "";
            enableCreditType(false);
            clearCreditType();
        }
        private void lstCreditCardTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            creditCardTypeId = Convert.ToString(lstCreditCardTypes.SelectedIndex+1);
            txtCreditCardType.Text = lstCreditCardTypes.SelectedItem.ToString();
        }

       

    }
}
