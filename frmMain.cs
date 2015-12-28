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
    public partial class frmMain : Form
    {
        private User session;
        public frmMain()
        {
            InitializeComponent();
        }
        public frmMain(User uLogin)
        {
            InitializeComponent();
            session = uLogin;
            timer1.Enabled = true;
            lblMainStatus.Text = "Logged in as " + uLogin.UserName;

        }

        public bool checkLogin()
        {
            return (DateTime.Now - session.LastLogin).Minutes <15;
        }
        private void showMdiForm(Form a)
        {
            a.MdiParent = this;
            Form b = Application.OpenForms[a.Name];
            if (b == null)
            {
                a.Show();
            }
            else
            {
                this.ActivateMdiChild(a);
            }
        }

        private void loadFrmCustomer()
        {
            frmCustomer a = new frmCustomer();
            showMdiForm(a);
        }
        private void loadFrmAccount()
        {
            frmAccount a = new frmAccount();
           showMdiForm(a);
           
        }
        private void loadFrmAccountType()
        {
            frmAccountType a = new frmAccountType();
           showMdiForm(a);
           
        }
        private void loadFrmCreditCard()
        {
            frmCreditCard a = new frmCreditCard();
           showMdiForm(a);
           
        }
        private void loadFrmCreditCardType()
        {
            frmCreditCardType a = new frmCreditCardType();
           showMdiForm(a);
           
        }
        private void loadFrmDebitCard()
        {
            frmDebitCard a = new frmDebitCard();
           showMdiForm(a);
           
        }
        private void loadFrmDebitCardType()
        {
            frmDebitCardType a = new frmDebitCardType();
           showMdiForm(a);
           
        }
        private void loadFrmTransaction()
        {
            frmTransaction a = new frmTransaction();
           showMdiForm(a);
           
        }
        private void loadFrmTransactionType()
        {
            frmTransactionType a = new frmTransactionType();
           showMdiForm(a);
           
        }
        private void loadFrmTransfer()
        {
            frmTransfer a = new frmTransfer();
           showMdiForm(a);
           
        }
        private void loadFrmTransferType()
        {
            frmTransferType a = new frmTransferType();
           showMdiForm(a);
           
        }
        private void loadFrmUser()
        {
            frmUser a = new frmUser();
            showMdiForm(a);
        }

        public string getUserRole()
        {
            return session.Role.Trim();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            //loadFrmCustomer();
            //loadFrmAccount();
            //loadFrmAccountType();
            //loadFrmCreditCard();
            //loadFrmCreditCardType();
            //loadFrmDebitCard();
            //loadFrmDebitCardType();
            //loadFrmTransaction();
            //loadFrmTransactionType();
            //loadFrmTransfer();
            //loadFrmTransferType();
            //loadFrmUser();
            
            //--- ROLES
            // Adminstrators
            // can Create, Read, Update and Delete
            // Autorisers
            // can Create, Read, and Update
            // Creater / Viewer
            // can Create and Read
            
            //toolStripStatusLabel1.Text = "Ready ... ";
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void tsbCustomer_Click(object sender, EventArgs e)
        {
            loadFrmCustomer();
        }
        private void tsbDebitCard_ButtonClick(object sender, EventArgs e)
        {
            loadFrmDebitCard();
        }

        private void transferTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.getUserRole() != "Adminstrator")
            {
                 MessageBox.Show("This is only available to Administrators ", "Permissions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            loadFrmTransferType();
        }

        private void tsbAccount_ButtonClick(object sender, EventArgs e)
        {
            loadFrmAccount();
        }

        private void accountTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.getUserRole() != "Adminstrator")
            {
                MessageBox.Show("This is only available to Administrators ", "Permissions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            loadFrmAccountType();
        }

        private void tsbCreditCard_ButtonClick(object sender, EventArgs e)
        {
            loadFrmCreditCard();
        }

        private void creditCardTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.getUserRole() != "Adminstrator")
            {
                MessageBox.Show("This is only available to Administrators ", "Permissions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            loadFrmCreditCardType();
        }

        private void debitCardTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.getUserRole() != "Adminstrator")
            {
                MessageBox.Show("This is only available to Administrators ", "Permissions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            loadFrmDebitCardType();
        }

        private void tsbTransaction_ButtonClick(object sender, EventArgs e)
        {
            loadFrmTransaction();
        }

        private void transactionTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.getUserRole() != "Adminstrator")
            {
                MessageBox.Show("This is only available to Administrators ", "Permissions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            loadFrmTransactionType();
        }

        private void tsbTransfer_ButtonClick(object sender, EventArgs e)
        {
            loadFrmTransfer();
        }
        private void tsbUsers_Click(object sender, EventArgs e)
        {
            if (this.getUserRole() != "Administrator")
            {
                MessageBox.Show("This is only available to Administrators ", "Permissions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            loadFrmUser();
        }

        private void toolbar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!checkLogin())
            {
                timer1.Enabled = false;
                lblMainStatus.Text = "Session Expired...";
                if (DialogResult.OK == MessageBox.Show(this, "The time on the session has elasped.", "Session expired!", MessageBoxButtons.OK, MessageBoxIcon.Information))
                {
                    Application.Exit();
                }
            }
            Console.WriteLine((DateTime.Now - session.LastLogin).Minutes);
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToShortTimeString();
        }
        
        

    }
}
