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
    public partial class frmTransferType : Form
    {
        private string strSave;
        private string transferTypeId;
        public frmTransferType()
        {
            InitializeComponent();
        }
        private void loadTransferTypes()
        {
            lstTransferTypes.Items.Clear();
            List<string> transferTypes;

            try
            {
                transferTypes = KanbDB.GetTransferTypeNames();
                if (transferTypes.Count > 0)
                {
                    for (int i = 0; i < transferTypes.Count; i++)
                    {
                        string transferType = transferTypes[i];
                        lstTransferTypes.Items.Add(transferType);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void enableTransInfo(bool a)
        {
            txtTransferType.Enabled = a;

            btnAdd.Enabled = !a;
            btnEdit.Enabled = !a;

            btnSave.Enabled = a;
            btnCancel.Enabled = a;
        }
        private void clearTransFrm()
        {
            txtTransferType.Clear();
        }
        private bool isFrmValid()
        {
            bool valid = true;
            string[] arrError = new string[1];
            if (!Common.textboxIsValid(txtTransferType))
            {
                arrError[0] = "Transfer Type Name InValid";
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

        private void frmTransferTypes_Load(object sender, EventArgs e)
        {
            enableTransInfo(false);
            loadTransferTypes();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            strSave = btnAdd.Text;
            enableTransInfo(true);
            clearTransFrm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(transferTypeId))
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
                KanbDB.AddTransferType(txtTransferType.Text);
                clearTransFrm();
                enableTransInfo(false);
                strSave = "";
            }
            else if (strSave == "Edit")
            {
                if (!String.IsNullOrEmpty(transferTypeId))
                {
                    KanbDB.UpdateTransferType(Convert.ToInt32(transferTypeId), txtTransferType.Text);
                }
            }
            loadTransferTypes();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            enableTransInfo(false);
            clearTransFrm();
        }

        private void lstTransferTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            transferTypeId = Convert.ToString(lstTransferTypes.SelectedIndex + 1);
            txtTransferType.Text = lstTransferTypes.Text.Trim();
        }
    }
}
