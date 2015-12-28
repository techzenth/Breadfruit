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
    public partial class frmTransactionType : Form
    {
        private string strSave;
        private string transactionTypeId;
        public frmTransactionType()
        {
            InitializeComponent();
        }
        private void loadTransactionTypes()
        {
            lstTransactionTypes.Items.Clear();
            List<string> transactionTypes;

            try
            {
                transactionTypes = KanbDB.GetTransactionTypeNames();
                if (transactionTypes.Count > 0)
                {
                    for (int i = 0; i < transactionTypes.Count; i++)
                    {
                        string transactionType = transactionTypes[i];
                        lstTransactionTypes.Items.Add(transactionType);
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
            txtTransactionType.Enabled = a;

            btnAdd.Enabled = !a;
            btnEdit.Enabled = !a;

            btnSave.Enabled = a;
            btnCancel.Enabled = a;
        }
        private void clearTransFrm()
        {
            txtTransactionType.Clear();
        }
       
        private bool isFrmValid()
        {
            bool valid = true;
            string[] arrError = new string[1];
            if (!Common.textboxIsValid(txtTransactionType))
            {
                arrError[0] = "Transaction Type Name InValid";
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
        
        private void frmTransactionType_Load(object sender, EventArgs e)
        {
            enableTransInfo(false);
            loadTransactionTypes();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            strSave = btnAdd.Text;
            enableTransInfo(true);
            clearTransFrm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(transactionTypeId))
            {
                strSave = btnEdit.Text;
                enableTransInfo(true);
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
                KanbDB.AddTransactionType(txtTransactionType.Text);
                clearTransFrm();
                enableTransInfo(false);
                strSave = "";
            }
            else if (strSave == "Edit")
            {
                if (!String.IsNullOrEmpty(transactionTypeId))
                {
                    KanbDB.UpdateTransactionType(Convert.ToInt32(transactionTypeId), txtTransactionType.Text);
                }
            }
            loadTransactionTypes();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            enableTransInfo(false);
            clearTransFrm();
        }

        private void lstTransactionTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            transactionTypeId = Convert.ToString(lstTransactionTypes.SelectedIndex + 1);
            txtTransactionType.Text = lstTransactionTypes.Text.Trim();
        }
    }
}
