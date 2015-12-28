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
    public partial class frmUser : Form
    {
        private string strSave;
        private string userId;
        private string questionText;
        private string role;

        public frmUser()
        {
            InitializeComponent();
        }
        private void showQuestionList(bool a)
        {
            cboQuestion.Visible = a;
            txtQuestion.Visible = !a;
        }
        private void enableUserFrm(bool a)
        {
            txtUsername.Enabled = a;
            txtEmail.Enabled = a;
            txtPassword.Enabled = a;
            txtQuestion.Enabled = a;
            cboQuestion.Enabled = a;
            txtAnswer.Enabled = a;
            cboRole.Enabled = a;

            btnAdd.Enabled = !a;
            btnEdit.Enabled = !a;
            btnDelete.Enabled = !a;
            btnSave.Enabled = a;
            btnCancel.Enabled = a;
        }
        private void loadUser(int id)
        {
            User user = new User();
            try
            {
                user = KanbDB.GetUser(id);
                userId = user.ID.ToString();
                txtUsername.Text = user.UserName.Trim();
                txtEmail.Text = user.Email.Trim();
                showQuestionList(false);
                questionText = txtQuestion.Text = user.Question.Trim();
                txtAnswer.Text = user.Answer.Trim();
                role = cboRole.Text = user.Role.Trim();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void loadUsers(object sender, EventArgs e)
        {
            List<User> userList = new List<User>();
            lstUsers.Items.Clear();
            try
            {
                userList = KanbDB.GetUsers();
                if (userList.Count > 0)
                {
                    User user = new User();
                    for (int i = 0; i < userList.Count; i++)
                    {
                        user = userList[i];
                        lstUsers.Items.Add(user.ID.ToString());
                        lstUsers.Items[i].SubItems.Add(user.UserName.Trim());
                        lstUsers.Items[i].SubItems.Add(user.Email.Trim());
                        lstUsers.Items[i].SubItems.Add(user.Role.Trim());
                        //if (user.LastLogin)
                        //{
                            lstUsers.Items[i].SubItems.Add(user.LastLogin.ToShortDateString() + " " + user.LastLogin.ToShortTimeString());
                        //}
                        //else
                        //{
                          //  lstUsers.Items[i].SubItems.Add("no data");
                        //}
                        lstUsers.Items[i].SubItems.Add(user.CreatedDate.ToShortDateString());
                    }
                }
                else
                {
                    btnAdd_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Console.WriteLine(ex.GetType().ToString() + " - " + ex.Message);
            }
        }
        private void loadQuestions()
        {

        }
        private void loadRoles()
        {

        }
        private void clearUserInfo()
        {
            txtUsername.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtQuestion.Clear();
            txtAnswer.Clear();
            cboQuestion.Text = "";
            cboRole.Text = "";
        }
        private bool isFrmValid()
        {
            bool valid = true;
            int i = 0;
            string[] arrError = new string[6];
            if (!Common.textboxIsValid(txtUsername))
            {
                arrError[i] = "Username is invalid!";
                valid = false;
                i++;
            }
            if (!Common.textboxIsValid(txtEmail))
            {
                arrError[i] = "Email is invalid!";
                valid = false;
                i++;
            }
            if (!Common.textboxIsValid(txtPassword))
            {
                arrError[i] = "Password is invalid!";
                valid = false;
                i++;
            }
            if (!Common.listboxIsValid(cboQuestion) && String.IsNullOrEmpty(questionText))
            {
                arrError[i] = "Select Question!";
                valid = false;
                i++;
            }
            if (!Common.textboxIsValid(txtAnswer))
            {
                arrError[i] = "Answer is invalid!";
                valid = false;
                i++;
            }
            if (!Common.listboxIsValid(cboRole) && String.IsNullOrEmpty(role))
            {
                arrError[i] = "Select Role";
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

        private void frmUser_Load(object sender, EventArgs e)
        {
            showQuestionList(false);
            enableUserFrm(false);
            loadUsers(sender, e);
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var si = lstUsers.SelectedItems;
            if (si.Count > 0)
            {
                userId = si[0].Text;
                loadUser(Convert.ToInt32(userId));
            }
        }

        private void cboQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            questionText = cboQuestion.Text;
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            role = cboRole.Text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            strSave = btnAdd.Text;
            showQuestionList(true);
            enableUserFrm(true);
            clearUserInfo();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(userId))
            {
                strSave = btnEdit.Text;
                showQuestionList(false);
                enableUserFrm(true);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(userId))
            {
                DialogResult m = MessageBox.Show("Are you sure you want to delete", "Delete", MessageBoxButtons.YesNo);
                if (m == DialogResult.Yes)
                {
                    KanbDB.DeleteUser(Convert.ToInt32(userId));
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
                KanbDB.AddUser(txtUsername.Text.Trim(), txtEmail.Text.Trim(), txtPassword.Text.Trim(), txtQuestion.Text.Trim(), txtAnswer.Text.Trim(), cboRole.Text.Trim());
                clearUserInfo();
                enableUserFrm(false);
                strSave = "";
            }
            else if (strSave == "Edit")
            {
                if (!String.IsNullOrEmpty(userId))
                {
                    KanbDB.UpdateUser(Convert.ToInt32(userId), txtUsername.Text.Trim(), txtEmail.Text.Trim(), txtPassword.Text.Trim(), txtQuestion.Text.Trim(), txtAnswer.Text.Trim(), cboRole.Text.Trim());
                }
            }
            loadUsers(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            enableUserFrm(false);
            clearUserInfo();
            strSave = "";
        }
    }
}
