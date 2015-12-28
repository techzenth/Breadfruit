using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kanb
{
    public partial class frmLogin : Form
    {
        string username;
        string role;
        User uLogin;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void autoLogin()
        {
            // this allows the automatic login for test purposes
            frmMain a = new frmMain(uLogin);
            a.Show();

            this.Hide();
        }
        private void doLogin()
        {
            // does username and password match with what is in database
            User login = new User();
            try
            {
                login = KanbDB.FindUser(txtLoginId.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }

            if (login.UserName != null)
            {
                if (Common.hashPassword(txtPassword.Text) == login.Password.Trim())
                {
                    
                    // what is the role
                    username = login.UserName;
                    role = login.Role;
                    uLogin = login;
                    try
                    {
                        // set last login
                        KanbDB.SetLastLogin(login.ID);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                        Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
                    } 
                    autoLogin();
                }
                else
                {
                    MessageBox.Show("The username and password does not matches", Application.ProductName + " " + this.Text,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("The username doesnt exist", Application.ProductName + " " + this.Text,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }
        

        private void frmLogin_Load(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Common.hashPassword(txtPassword.Text).ToString());
            doLogin();
        }

        private void txtLoginId_TextChanged(object sender, EventArgs e)
        {
            if (!Common.textboxIsValid(sender) || btnLogin.Enabled != true)
            {
                //  btnLogin.Enabled = true;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (!Common.textboxIsValid(sender) || txtLoginId.Text != "")
            {
                btnLogin.Enabled = true;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            ActiveForm.AcceptButton = btnLogin;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            ActiveForm.AcceptButton = null;
        }

        // --- Unnessary Code
        private Bitmap default_image;
        private string strImage;
        private void picLogin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // set to B/W
            Bitmap cmi;
            // reset to Color
            if (default_image == null)
            {
                cmi = default_image = (Bitmap)picLogin.Image.Clone();
                strImage = picLogin.ImageLocation;
                picLogin.Image = Common.Grayscale(cmi);
            }
            else
            {
                picLogin.ImageLocation = strImage;
                //picLogin.Image = default_image;
                default_image = null;
            }

        }

        private void picLogin_Click(object sender, EventArgs e)
        {

        }

 


    }
}
