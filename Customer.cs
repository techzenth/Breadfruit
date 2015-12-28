using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanb
{
    public class Customer
    {
        private int customerId;
        private string firstname;
        private string lastname;
        private DateTime birthDay;
        private string street;
        private string address;
        private string city;
        private string state;
        private string postalcode;
        private string mobilephone;
        private string workphone;
        private string emailaddress;
        private DateTime registeredate;
        public Customer() { }
        public int ID
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }
        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }
        public DateTime BirthDay
        {
            get { return birthDay; }
            set { birthDay = value; }
        }
        public string Street
        {
            get { return street; }
            set { street = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string State
        {
            get { return state; }
            set { state = value; }
        }
        public string PostalCode
        {
            get { return postalcode; }
            set { postalcode = value; }
        }
        public string MobilePhone
        {
            get { return mobilephone; }
            set { mobilephone = value; }
        }
        public string WorkPhone
        {
            get { return workphone; }
            set { workphone = value; }
        }
        public string EmailAddress
        {
            get { return emailaddress; }
            set { emailaddress = value; }
        }
        public DateTime RegisteredDate
        {
            get { return registeredate; }
            set { registeredate = value; }
        }
    }
}
