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
    public partial class frmDebitCardType : Form
    {
        private string strSave;
        private string debitCardTypeId;
        public frmDebitCardType()
        {
            InitializeComponent();
        }
        private void loadDebitCardTypes()
        {
            List<string> debitCardTypeNameList;
            try
            {
                debitCardTypeNameList = KanbDB.GetDebitCardTypeNames();
                if (debitCardTypeNameList.Count > 0)
                {
                    string accountTypeName;
                    lstDebitCardTypes.Items.Clear();
                    for (int i = 0; i < debitCardTypeNameList.Count; i++)
                    {
                        accountTypeName = debitCardTypeNameList[i];
                        lstDebitCardTypes.Items.Add(accountTypeName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }

        }
        private void enableDebitTypeInfo(bool a)
        {
            txtDebitCardType.Enabled = a;

            btnSave.Enabled = a;
            btnCancel.Enabled = a;

            btnAdd.Enabled = !a;
            btnEdit.Enabled = !a;

        }
        private void clearDebitTypeFrm() 
        {
            txtDebitCardType.Clear();
        }

        private bool isFrmValid()
        {
            bool valid = true;
            string[] arrError = new string[1];
            if (!Common.textboxIsValid(txtDebitCardType))
            {
                arrError[0] = "Debit Card Type Name InValid";
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
        private void frmDebitCardType_Load(object sender, EventArgs e)
        {
            enableDebitTypeInfo(false);
            loadDebitCardTypes();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            strSave = btnAdd.Text;
            enableDebitTypeInfo(true);
            clearDebitTypeFrm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(debitCardTypeId))
            {
                strSave = btnEdit.Text;
                enableDebitTypeInfo(true);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
         if(!isFrmValid()){
             return;
         }   
            if (strSave == "Add")
            {
                KanbDB.AddDebitCardType(txtDebitCardType.Text);
                clearDebitTypeFrm();
                enableDebitTypeInfo(false);
                strSave = "";
            }
            else if (strSave == "Edit")
            {
                if (!String.IsNullOrEmpty(debitCardTypeId))
                {
                    KanbDB.UpdateDebitCardType(Convert.ToInt32(debitCardTypeId), txtDebitCardType.Text);
                }
            }
            loadDebitCardTypes();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            enableDebitTypeInfo(false);
            clearDebitTypeFrm();
        }

        private void lstDebitCardTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            debitCardTypeId = Convert.ToString(lstDebitCardTypes.SelectedIndex + 1);
            txtDebitCardType.Text = lstDebitCardTypes.SelectedItem.ToString();
        }
    }
}
