using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanb
{
    public class User
    {
        private int id;
        private string username;
        private string email;
        private string password;
        private string question;
        private string answer;
        private string role;
        private DateTime lastlogin;
        private DateTime createddate;
        public User() { }
        public int ID 
        {
            get { return id; }
            set { id = value; }
        }
        public string UserName 
        {
            get { return username; }
            set { username = value; }
        }
        public string Email 
        {
            get { return email; }
            set { email = value; }
        }
        public string Password 
        {
            get { return password; }
            set { password = value; }
        }
        public string Question 
        {
            get { return question; }
            set { question = value; }
        }
        public string Answer 
        {
            get { return answer; }
            set { answer = value; }
        }
        public string Role 
        {
            get { return role; }
            set { role = value; }
        }
        public DateTime LastLogin 
        {
            get { return lastlogin; }
            set { lastlogin = value; }
        }
        public DateTime CreatedDate 
        {
            get { return createddate; }
            set { createddate = value; }
        }
    }
}
