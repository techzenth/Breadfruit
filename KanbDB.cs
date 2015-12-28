using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Kanb
{
    public static class KanbDB
    {
        public static SqlConnection GetConnection()
        {
            string connStr = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Mico_Ultrabook\Documents\Visual Studio 2013\Projects\C#\Kanb\Kanb\Kanb.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connStr);
            return conn;
        }
        public static void AddCustomer(string firstname, string lastname, string street, string address, string city, string state, string postalcode, string mobilephone, string workphone, string email) {
            int newId;
            string insStmt = "INSERT INTO dbo.Customers (FirstName, LastName, Street, Address, City, State, PostalCode, MobilePhone, WorkPhone, EmailAddress, RegisteredDate) VALUES (@FirstName, @LastName, @Street, @Address, @City, @State, @PostalCode, @MobilePhone, @WorkPhone, @EmailAddress, @RegisteredDate);"
            + "SELECT CAST(IDENT_CURRENT('Customers') As INT)";
            SqlConnection conn = GetConnection();
            SqlCommand insCmd = new SqlCommand(insStmt,conn);
            insCmd.Parameters.AddWithValue("@FirstName",firstname);
            insCmd.Parameters.AddWithValue("@LastName", lastname);
            insCmd.Parameters.AddWithValue("@Street", street);
            insCmd.Parameters.AddWithValue("@Address", address);
            insCmd.Parameters.AddWithValue("@City", city);
            insCmd.Parameters.AddWithValue("@State", state);
            insCmd.Parameters.AddWithValue("@PostalCode", postalcode);
            insCmd.Parameters.AddWithValue("@MobilePhone", mobilephone);
            insCmd.Parameters.AddWithValue("@WorkPhone", workphone);
            insCmd.Parameters.AddWithValue("@EmailAddress", email);
            insCmd.Parameters.AddWithValue("@RegisteredDate", DateTime.Now.ToShortDateString());
            try { conn.Open(); newId = (Int32) insCmd.ExecuteScalar(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            
        }
        public static void UpdateCustomer(string id, string firstname, string lastname, string street, string address, string city, string state, string postalcode, string mobilephone, string workphone, string email)
        {
            string updStmt = "UPDATE Customers SET FirstName=@FirstName, LastName=@LastName, Street=@Street, Address=@Address, City=@City, State=@State, PostalCode=@PostalCode, MobilePhone=@MobilePhone, WorkPhone=@WorkPhone, EmailAddress=@EmailAddress WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand updCmd = new SqlCommand(updStmt, conn);
            updCmd.Parameters.AddWithValue("@Id", id);
            updCmd.Parameters.AddWithValue("@FirstName", firstname);
            updCmd.Parameters.AddWithValue("@LastName", lastname);
            updCmd.Parameters.AddWithValue("@Street", street);
            updCmd.Parameters.AddWithValue("@Address", address);
            updCmd.Parameters.AddWithValue("@City", city);
            updCmd.Parameters.AddWithValue("@State", state);
            updCmd.Parameters.AddWithValue("@PostalCode", postalcode);
            updCmd.Parameters.AddWithValue("@MobilePhone", mobilephone);
            updCmd.Parameters.AddWithValue("@WorkPhone", workphone);
            updCmd.Parameters.AddWithValue("@EmailAddress", email);
            try { conn.Open(); updCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void DeleteCustomer(int id)
        {
            string delStmt = "DELETE FROM Customers WHERE Id = @Id";
            SqlConnection conn = GetConnection();
            SqlCommand delCmd = new SqlCommand(delStmt, conn);
            delCmd.Parameters.AddWithValue("@Id", id);
            try { conn.Open(); delCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static Customer GetCustomer(int id)
        {
            Customer customer = new Customer();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM Customers WHERE Id = @Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            selCmd.Parameters.AddWithValue("@Id", id);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                reader.Read();
                customer.ID = (int)reader["Id"];
                customer.FirstName = reader["FirstName"].ToString();
                customer.LastName = reader["LastName"].ToString();
                customer.BirthDay = (DateTime)reader["BirthDay"];
                customer.Street = reader["Street"].ToString();
                customer.Address = reader["Address"].ToString();
                customer.City = reader["City"].ToString();
                customer.State = reader["State"].ToString();
                customer.PostalCode = reader["PostalCode"].ToString();
                customer.MobilePhone = reader["MobilePhone"].ToString();
                customer.WorkPhone = reader["WorkPhone"].ToString();
                customer.EmailAddress = reader["EmailAddress"].ToString();
                customer.RegisteredDate = (DateTime)reader["RegisteredDate"];
                reader.Close();

            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }

            return customer;
        
        }
        public static List<Customer> GetCustomers()
        {
            List<Customer> customerList = new List<Customer>();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM Customers ORDER BY Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer();
                    customer.ID = (int)reader["Id"];
                    customer.FirstName = reader["FirstName"].ToString();
                    customer.LastName = reader["LastName"].ToString();
                    customer.Street = reader["Street"].ToString();
                    customer.BirthDay = (DateTime) reader["BirthDay"];
                    customer.Address = reader["Address"].ToString();
                    customer.City = reader["City"].ToString();
                    customer.State = reader["State"].ToString();
                    customer.PostalCode = reader["PostalCode"].ToString();
                    customer.MobilePhone = reader["MobilePhone"].ToString();
                    customer.WorkPhone = reader["WorkPhone"].ToString();
                    customer.EmailAddress = reader["EmailAddress"].ToString();
                    customer.RegisteredDate = (DateTime)reader["RegisteredDate"];
                    customerList.Add(customer);
                }
                reader.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return customerList;
        }
        public static void AddAccountType(string accountTypeName)
        {
            string insStmt = "INSERT INTO AccountTypes (AccountTypeName) VALUES (@AccountTypeName)";
            SqlConnection conn = GetConnection();
            SqlCommand insCmd = new SqlCommand(insStmt, conn);
            insCmd.Parameters.AddWithValue("@AccountTypeName", accountTypeName);
            try { conn.Open(); insCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void UpdateAccountType(int id, string accountTypeName) {
            string updStmt = "UPDATE AccountTypes SET AccountTypeName = @AccountTypeName WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand updCmd = new SqlCommand(updStmt, conn);
            updCmd.Parameters.AddWithValue("@Id", id);
            updCmd.Parameters.AddWithValue("@AccountTypeName", accountTypeName);
            try { conn.Open(); updCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void AddAccount(int customerId, int accountTypeId, double balance)
        {
            string insStmt = "INSERT INTO Accounts (CustomerID,AccountTypeID,Balance) VALUES(@CustomerID,@AccountTypeID,@Balance)";
            SqlConnection conn = GetConnection();
            SqlCommand insCmd = new SqlCommand(insStmt, conn);
            insCmd.Parameters.AddWithValue("@CustomerID", customerId);
            insCmd.Parameters.AddWithValue("@AccountTypeID", accountTypeId);
            insCmd.Parameters.AddWithValue("@Balance", balance);
            try { conn.Open(); insCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void UpdateAccount(int id, int customerId, int accountTypeId, double balance) 
        {
            string updStmt = "UPDATE Accounts SET CustomerID=@CustomerID, AccountTypeID=@AccountTypeID, Balance=@Balance WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand updCmd = new SqlCommand(updStmt, conn);
            updCmd.Parameters.AddWithValue("@Id", id);
            updCmd.Parameters.AddWithValue("@CustomerID", customerId);
            updCmd.Parameters.AddWithValue("@AccountTypeID", accountTypeId);
            updCmd.Parameters.AddWithValue("@Balance", balance);
            try { conn.Open(); updCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            
        }
        public static void DeleteAccount(int id)
        {
            string delStmt = "DELETE FROM Accounts WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand delCmd = new SqlCommand(delStmt,conn);
            delCmd.Parameters.AddWithValue("@Id", id);
            try { conn.Open(); delCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void AddCreditCardType(string creditCardTypeName) 
        {
            string insStmt = "INSERT INTO CreditCardTypes (CreditCardTypeName) VALUES (@CreditCardTypeName)";
            SqlConnection conn = GetConnection();
            SqlCommand insCmd = new SqlCommand(insStmt, conn);
            insCmd.Parameters.AddWithValue("@CreditCardTypeName", creditCardTypeName);
            try { conn.Open(); insCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void UpdateCreditCardType(int id, string creditCardTypeName)
        {
            string updStmt = "UPDATE CreditCardTypes SET CreditCardTypeName = @CreditCardTypeName WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand updCmd = new SqlCommand(updStmt, conn);
            updCmd.Parameters.AddWithValue("@Id", id);
            updCmd.Parameters.AddWithValue("@CreditCardTypeName", creditCardTypeName);
            try { conn.Open(); updCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); } 
        }
        public static void AddCreditCard(int accountId, int customerId, int pinCode, int creditCardType, string expiryDate)
        {
            string insStmt = "INSERT INTO CreditCards (AccountID,CustomerID,PinCode,CreditCardType,RegisteredDate,ExpiryDate) VALUES(@AccountID,@CustomerID,@PinCode,@CreditCardType,@RegisteredDate,@ExpiryDate)";
            SqlConnection conn = GetConnection();
            SqlCommand insCmd = new SqlCommand(insStmt, conn);
            insCmd.Parameters.AddWithValue("@AccountID",accountId);
            insCmd.Parameters.AddWithValue("@CustomerID", customerId);
            insCmd.Parameters.AddWithValue("@PinCode", pinCode);
            insCmd.Parameters.AddWithValue("@CreditCardType", creditCardType);
            insCmd.Parameters.AddWithValue("@RegisteredDate", DateTime.Today.ToShortDateString());
            insCmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
            try { conn.Open(); insCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void UpdateCreditCard(int id, int accountId, int customerId, int pinCode, int creditCardType, string expiryDate)
        {
            string updStmt = "UPDATE CreditCards SET CustomerID=@CustomerID, PinCode=@PinCode, CreditCardType=@CreditCardType, ExpiryDate=@ExpiryDate WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand updCmd = new SqlCommand(updStmt, conn);
            updCmd.Parameters.AddWithValue("@Id", id);
            updCmd.Parameters.AddWithValue("@AccountID",accountId);
            updCmd.Parameters.AddWithValue("@CustomerID", customerId);
            updCmd.Parameters.AddWithValue("@PinCode", pinCode);
            updCmd.Parameters.AddWithValue("@CreditCardType", creditCardType);
            updCmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
            try { conn.Open(); updCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            
        }
        public static void DeleteCreditCard(int id)
        {
            string delStmt = "DELETE FROM CreditCards WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand delCmd = new SqlCommand(delStmt, conn);
            delCmd.Parameters.AddWithValue("@Id", id);
            try { conn.Open(); delCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void AddDebitCardType(string debitCardTypeName)
        {
            string insStmt = "INSERT INTO DebitCardTypes (DebitCardTypeName) VALUES (@DebitCardTypeName)";
            SqlConnection conn = GetConnection();
            SqlCommand insCmd = new SqlCommand(insStmt, conn);
            insCmd.Parameters.AddWithValue("@DebitCardTypeName", debitCardTypeName);
            try { conn.Open(); insCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void UpdateDebitCardType(int id, string debitCardTypeName)
        {
            string updStmt = "UPDATE DebitCardTypes SET DebitCardTypeName = @DebitCardTypeName WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand updCmd = new SqlCommand(updStmt, conn);
            updCmd.Parameters.AddWithValue("@Id", id);
            updCmd.Parameters.AddWithValue("@DebitCardTypeName", debitCardTypeName);
            try { conn.Open(); updCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void AddDebitCard(int accountId, int customerId, int pinCode, int debitCardType)
        {
            string insStmt = "INSERT INTO DebitCards (AccountID,CustomerID,PinCode,DebitCardType,RegisteredDate) VALUES(@AccountID,@CustomerID,@PinCode,@DebitCardType,@RegisteredDate)";
            SqlConnection conn = GetConnection();
            SqlCommand insCmd = new SqlCommand(insStmt, conn);
            insCmd.Parameters.AddWithValue("@AccountID", accountId);
            insCmd.Parameters.AddWithValue("@CustomerID", customerId);
            insCmd.Parameters.AddWithValue("@PinCode", pinCode);
            insCmd.Parameters.AddWithValue("@DebitCardType", debitCardType);
            insCmd.Parameters.AddWithValue("@RegisteredDate", DateTime.Today.ToShortDateString());
            try { conn.Open(); insCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }

        }
        public static void UpdateDebitCard(int id, int accountId, int customerId, int pinCode, int debitCardType) 
        {
            string updStmt = "UPDATE DebitCards SET CustomerID=@CustomerID, PinCode=@PinCode, DebitCardType=@DebitCardType WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand updCmd = new SqlCommand(updStmt, conn);
            updCmd.Parameters.AddWithValue("@Id", id);
            updCmd.Parameters.AddWithValue("@AccountID", accountId);
            updCmd.Parameters.AddWithValue("@CustomerID", customerId);
            updCmd.Parameters.AddWithValue("@PinCode", pinCode);
            updCmd.Parameters.AddWithValue("@DebitCardType", debitCardType);
            try { conn.Open(); updCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            
        }
        public static void DeleteDebitCard(int id)
        {
            string delStmt = "DELETE FROM DebitCards WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand delCmd = new SqlCommand(delStmt, conn);
            delCmd.Parameters.AddWithValue("@Id", id);
            try { conn.Open(); delCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void AddTransaction(int accountId,int transactionTypeId, double transactionAmount, string transactionDescription, string transactionDate) 
        {
            string insStmt = "INSERT INTO Transactions (TransactionAmount, AccountID,TransactionTypeID,TransactionDate) VALUES(@TransactionAmount, @AccountID,@TransactionTypeID,@TransactionDate)";
            SqlConnection conn = GetConnection();
            SqlCommand insCmd = new SqlCommand(insStmt, conn);
            insCmd.Parameters.AddWithValue("@TransactionAmount", transactionAmount);
            insCmd.Parameters.AddWithValue("@AccountID", accountId);
            insCmd.Parameters.AddWithValue("@TransactionTypeID", transactionTypeId);
            insCmd.Parameters.AddWithValue("@TransactionDescription", transactionDescription);
            insCmd.Parameters.AddWithValue("@TransactionDate", transactionDate);
            try { conn.Open(); insCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void UpdateTransaction(int id, int accountId, int transactionTypeId, double transactionAmount, string transactionDescription, string transactionDate) 
        {
            string updStmt = "UPDATE Transactions SET TransactionAmount=@TransactionAmount, AccountID=@AccountID, TransactionTypeID=@TransactionTypeID, TransactionDate=@TransactionDate WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand updCmd = new SqlCommand(updStmt, conn);
            updCmd.Parameters.AddWithValue("@Id",id);
            updCmd.Parameters.AddWithValue("@TransactionAmount", transactionAmount);
            updCmd.Parameters.AddWithValue("@AccountID", accountId);
            updCmd.Parameters.AddWithValue("@TransactionTypeID", transactionTypeId);
            updCmd.Parameters.AddWithValue("@TransactionDescription", transactionDescription);
            updCmd.Parameters.AddWithValue("@TransactionDate", transactionDate);
            try { conn.Open(); updCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            
        }
        public static void DeleteTransaction(int id) 
        {
            string delStmt = "DELETE FROM Transactions WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand delCmd = new SqlCommand(delStmt, conn);
            delCmd.Parameters.AddWithValue("@Id", id);
            try { conn.Open(); delCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void AddTransactionType(string transactionType) 
        {
            string insStmt = "INSERT INTO TransactionTypes (TransactionTypeName) VALUES (@TransactionTypeName)";
            SqlConnection conn = GetConnection();
            SqlCommand insCmd = new SqlCommand(insStmt, conn);
            insCmd.Parameters.AddWithValue("@TransactionTypeName", transactionType);
            try { conn.Open(); insCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void UpdateTransactionType(int id,string transactionType) 
        {
            string updStmt = "UPDATE TransactionTypes SET TransactionTypeName = @TransactionTypeName WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand updCmd = new SqlCommand(updStmt, conn);
            updCmd.Parameters.AddWithValue("@Id", id);
            updCmd.Parameters.AddWithValue("@TransactionTypeName", transactionType);
            try { conn.Open(); updCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void AddTransfer(int account1Id, int account2Id, int transferTypeId, double transferAmount, string transferDate)
        {
            string insStmt = "INSERT INTO Transfers (TransferAmount, Account1ID,Account2ID,TransferTypeID,TransferDate) VALUES(@TransferAmount, @Account1ID,@Account2ID,@TransferTypeID,@TransferDate)";
            SqlConnection conn = GetConnection();
            SqlCommand insCmd = new SqlCommand(insStmt, conn);
            insCmd.Parameters.AddWithValue("@TransferAmount", transferAmount);
            insCmd.Parameters.AddWithValue("@Account1ID", account1Id);
            insCmd.Parameters.AddWithValue("@Account2ID", account2Id);
            insCmd.Parameters.AddWithValue("@TransferTypeID", transferTypeId);
            insCmd.Parameters.AddWithValue("@TransferDate", transferDate);
            try { conn.Open(); insCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void UpdateTransfer(int id, int account1Id, int account2Id, int transferTypeId, double transferAmount, string transferDate)
        {
            string updStmt = "UPDATE Transfers SET TransferAmount=@TransferAmount, Account1ID=@Account1ID,  Account2ID=@Account2ID, TransferTypeID=@TransferTypeID, TransferDate=@TransferDate WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand updCmd = new SqlCommand(updStmt, conn);
            updCmd.Parameters.AddWithValue("@Id",id);
            updCmd.Parameters.AddWithValue("@TransferAmount", transferAmount);
            updCmd.Parameters.AddWithValue("@Account1ID", account1Id);
            updCmd.Parameters.AddWithValue("@Account2ID", account2Id);
            updCmd.Parameters.AddWithValue("@TransferTypeID", transferTypeId);
            updCmd.Parameters.AddWithValue("@TransferDate", transferDate);
            try { conn.Open(); updCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }

        }
        public static void DeleteTransfer(int id)
        {
            string delStmt = "DELETE FROM Transfers WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand delCmd = new SqlCommand(delStmt, conn);
            delCmd.Parameters.AddWithValue("@Id", id);
            try { conn.Open(); delCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void AddTransferType(string transferType)
        {
            string insStmt = "INSERT INTO TransferTypes (TransferTypeName) VALUES (@TransferTypeName)";
            SqlConnection conn = GetConnection();
            SqlCommand insCmd = new SqlCommand(insStmt, conn);
            insCmd.Parameters.AddWithValue("@TransferTypeName", transferType);
            try { conn.Open(); insCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void UpdateTransferType(int id, string transactionType)
        {
            string updStmt = "UPDATE TransferTypes SET TransferTypeName = @TransferTypeName WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand updCmd = new SqlCommand(updStmt, conn);
            updCmd.Parameters.AddWithValue("@Id", id);
            updCmd.Parameters.AddWithValue("@TransferTypeName", transactionType);
            try { conn.Open(); updCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static List<string> GetAccountTypeNames()
        {
            List<string> accountTypeNameList = new List<string>();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM AccountTypes ORDER BY Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                while(reader.Read()){
                    string accountTypeName = reader["AccountTypeName"].ToString();
                    accountTypeNameList.Add(accountTypeName);
                }
                reader.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return accountTypeNameList;
        }
        public static string GetAccountTypeName(int id)
        {
            string accountTypeName;
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM AccountTypes WHERE Id=@Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            selCmd.Parameters.AddWithValue("@Id", id);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                reader.Read();
                accountTypeName = reader["AccountTypeName"].ToString();
                reader.Close();

            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return accountTypeName;
        }
        public static Account GetAccount(int id)
        {
            Account account = new Account();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM Accounts WHERE Id = @Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            selCmd.Parameters.AddWithValue("@Id", id);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                reader.Read();
                account.ID = (int)reader["Id"];
                account.CustomerID = (int)reader["CustomerID"];
                account.AccountTypeID = (int)reader["AccountTypeID"];
                account.Balance = Math.Round(Convert.ToDouble(reader["balance"]), 2); ;
                reader.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }

            return account;
        }
        public static List<Account> GetAccounts()
        {
            List<Account> accountList = new List<Account>();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM Accounts ORDER BY Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                while(reader.Read())
                {
                    Account account = new Account();
                    account.ID = (int)reader["Id"];
                    account.CustomerID = (int)reader["CustomerID"];
                    account.AccountTypeID = (int)reader["AccountTypeID"];
                    account.Balance = Math.Round(Convert.ToDouble(reader["balance"]),2);
                    accountList.Add(account);
                }
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return accountList;
        }
        public static CreditCard GetCreditCard(int id)
        {
            CreditCard creditCard = new CreditCard();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM CreditCards WHERE Id = @Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            selCmd.Parameters.AddWithValue("@Id", id);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                reader.Read();
                creditCard.ID = (int)reader["Id"];
                creditCard.AccountID = (int)reader["AccountID"];
                creditCard.CustomerID = (int)reader["CustomerID"];
                creditCard.PinCode = (int)reader["PinCode"];
                creditCard.CreditCardType = (int)reader["CreditCardType"];
                creditCard.RegisteredDate = Convert.ToDateTime(reader["RegisteredDate"]);
                creditCard.ExpiryDate = Convert.ToDateTime(reader["ExpiryDate"]); 
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return creditCard;
        }
        public static List<CreditCard> GetCreditCards()
        {
            List<CreditCard> creditCardList = new List<CreditCard>();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM CreditCards ORDER BY Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                while (reader.Read())
                {
                    CreditCard creditCard = new CreditCard();
                    creditCard.ID = (int)reader["Id"];
                    creditCard.AccountID = (int)reader["AccountID"];
                    creditCard.CustomerID = (int)reader["CustomerID"];
                    creditCard.PinCode = (int)reader["PinCode"];
                    creditCard.CreditCardType = (int)reader["CreditCardType"];
                    creditCard.RegisteredDate = Convert.ToDateTime(reader["RegisteredDate"]);
                    creditCard.ExpiryDate = Convert.ToDateTime(reader["ExpiryDate"]);
                    creditCardList.Add(creditCard);
                }
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return creditCardList;
        }
        public static string GetCreditCardTypeName(int id) 
        {
            string creditCardTypeName;
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM CreditCardTypes WHERE Id=@Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            selCmd.Parameters.AddWithValue("@Id", id);
            try
            {
                 conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                reader.Read();
                creditCardTypeName = reader["CreditCardTypeName"].ToString();
                reader.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }

            return creditCardTypeName;
        }
        public static List<string> GetCreditCardTypeNames() 
        {
            List<string> creditCardTypeNames = new List<string>();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM CreditCardTypes ORDER BY Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                while (reader.Read())
                {
                    string creditCardTypeName = reader["CreditCardTypeName"].ToString();
                    creditCardTypeNames.Add(creditCardTypeName);
                }
                reader.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return creditCardTypeNames;
        }
        public static DebitCard GetDebitCard(int id)
        {
            DebitCard debitCard = new DebitCard();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM DebitCards WHERE Id = @Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            selCmd.Parameters.AddWithValue("@Id", id);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                reader.Read();
                debitCard.ID = (int)reader["Id"];
                debitCard.AccountID = (int)reader["AccountID"];
                debitCard.CustomerID = (int)reader["CustomerID"];
                debitCard.PinCode = (int)reader["PinCode"];
                debitCard.DebitCardType = (int)reader["debitCardType"];
                debitCard.RegisteredDate = Convert.ToDateTime(reader["RegisteredDate"]);               
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return debitCard;
        }
        public static List<DebitCard> GetDebitCards() 
        {
            List<DebitCard> debitCardList = new List<DebitCard>();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM DebitCards ORDER BY Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                while (reader.Read())
                {
                    DebitCard debitCard = new DebitCard();
                    debitCard.ID = (int)reader["Id"];
                    debitCard.AccountID = (int)reader["AccountID"];
                    debitCard.CustomerID = (int)reader["CustomerID"];
                    debitCard.PinCode = (int)reader["PinCode"];
                    debitCard.DebitCardType = (int)reader["debitCardType"];
                    debitCard.RegisteredDate = Convert.ToDateTime(reader["RegisteredDate"]);
                    debitCardList.Add(debitCard);
                }
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return debitCardList;
        }
        public static string GetDebitCardTypeName(int id)
        {
            string debitCardTypeName;
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM DebitCardTypes WHERE Id=@Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            selCmd.Parameters.AddWithValue("@Id", id);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                reader.Read();
                debitCardTypeName = reader["DebitCardTypeName"].ToString();
                reader.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }

            return debitCardTypeName;
        }
        public static List<string> GetDebitCardTypeNames()
        {
            List<string> debitCardTypeNames = new List<string>();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM DebitCardTypes ORDER BY Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                while (reader.Read())
                {
                    string debitCardTypeName = reader["DebitCardTypeName"].ToString();
                    debitCardTypeNames.Add(debitCardTypeName);
                }
                reader.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return debitCardTypeNames;
        }
        public static Transaction GetTransaction(int id)
        {
            Transaction transaction = new Transaction();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM Transactions WHERE Id = @Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            selCmd.Parameters.AddWithValue("@Id", id);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                reader.Read();
                transaction.ID = (int)reader["Id"];
                transaction.AccountID = (int)reader["AccountID"];
                transaction.TransactionAmount = Convert.ToDouble(reader["TransactionAmount"]);
                transaction.TransactionTypeID = (int)reader["TransactionTypeID"];
                transaction.TransactionDescription = reader["TransactionDescription"].ToString();
                transaction.TransactionDate = Convert.ToDateTime(reader["TransactionDate"]);
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return transaction;
        }
        public static List<Transaction> GetTransactions()
        {
            List<Transaction> transactionList = new List<Transaction>();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM Transactions ORDER BY Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                while (reader.Read())
                {
                   Transaction transaction = new Transaction();
                   transaction.ID = (int)reader["Id"];
                   transaction.AccountID = (int)reader["AccountID"];
                   transaction.TransactionTypeID = (int)reader["TransactionTypeID"];
                   transaction.TransactionAmount = Convert.ToDouble(reader["TransactionAmount"]);
                   transaction.TransactionDescription = reader["TransactionDescription"].ToString();
                   transaction.TransactionDate = Convert.ToDateTime(reader["TransactionDate"]);
                   transactionList.Add(transaction);
                }
                reader.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return transactionList;
        }
        public static string GetTransactionTypeName(int id)
        {
            string transactionTypeName;
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM TransactionTypes WHERE Id=@Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            selCmd.Parameters.AddWithValue("@Id", id);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                reader.Read();
                transactionTypeName = reader["TransactionTypeName"].ToString();
                reader.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }

            return transactionTypeName;
        }
        public static List<string> GetTransactionTypeNames()
        {
            List<string> transactionNames = new List<string>();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM TransactionTypes ORDER BY Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                while (reader.Read())
                {
                    string transactionTypeName = reader["TransactionTypeName"].ToString();
                    transactionNames.Add(transactionTypeName);
                }
                reader.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return transactionNames;
        }
        public static Transfer GetTransfer(int id) {
            Transfer transfer = new Transfer();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM Transfers WHERE Id = @Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            selCmd.Parameters.AddWithValue("@Id", id);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                reader.Read();
                transfer.Account1ID = (int) reader["Account1ID"];
                transfer.Account2ID = (int)reader["Account2ID"];
                transfer.TransferTypeID = (int)reader["TransferTypeID"];
                transfer.TransferAmount = Convert.ToDouble(reader["TransferAmount"]);
                transfer.TransferDate = Convert.ToDateTime(reader["TransferDate"]);
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return transfer;
        }
        public static List<Transfer> GetTransfers() {
            List<Transfer> transferList = new List<Transfer>();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM Transfers ORDER BY Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                while (reader.Read())
                {
                    Transfer transfer = new Transfer();
                    transfer.ID = (int) reader["Id"];
                    transfer.Account1ID = (int)reader["Account1ID"];
                    transfer.Account2ID = (int)reader["Account2ID"];
                    transfer.TransferTypeID = (int)reader["TransferTypeID"];
                    transfer.TransferAmount = Convert.ToDouble(reader["TransferAmount"]);
                    transfer.TransferDate = Convert.ToDateTime(reader["TransferDate"]);
                    transferList.Add(transfer);
                }
                reader.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return transferList;
        }
        public static string GetTransferTypeName(int id)
        {
            string transferTypeName;
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM TransferTypes WHERE Id=@Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            selCmd.Parameters.AddWithValue("@Id", id);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                reader.Read();
                transferTypeName = reader["TransferTypeName"].ToString();
                reader.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }

            return transferTypeName;
        }
        public static List<string> GetTransferTypeNames()
        {
            List<string> transferNames = new List<string>();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM TransferTypes ORDER BY Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                while (reader.Read())
                {
                    string transferTypeName = reader["TransferTypeName"].ToString();
                    transferNames.Add(transferTypeName);
                }
                reader.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return transferNames;
        }
        public static void SetLastLogin(int id)
        {
            string updStmt = "UPDATE Login SET LastLogin = @LastLogin WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand updCmd = new SqlCommand(updStmt, conn);
            updCmd.Parameters.AddWithValue("@Id", id);
            updCmd.Parameters.AddWithValue("@LastLogin", DateTime.Now);
            try { conn.Open(); updCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        
        }
        public static User FindUser(string username)
        {
            User login = new User();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM Login WHERE UserName=@UserName";
            SqlCommand selCmd = new SqlCommand(selStmt,conn);
            selCmd.Parameters.AddWithValue("@UserName", username);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    login.ID = (int)reader["ID"];
                    login.UserName = reader["UserName"].ToString();
                    login.Email = reader["Email"].ToString();
                    login.Password = reader["Password"].ToString();
                    login.Question = reader["Question"].ToString();
                    login.Answer = reader["Answer"].ToString();
                    login.Role = reader["Role"].ToString();
                    login.LastLogin = reader["LastLogin"] != DBNull.Value ? Convert.ToDateTime(reader["LastLogin"]) : DateTime.Now;
                    login.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    reader.Close();
                }
            }
            catch (SqlException ex) { throw ex; }
            return login;
        }
        public static User GetUser(int id) {
            User user = new User();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM Login WHERE Id=@Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            selCmd.Parameters.AddWithValue("@Id", id);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                reader.Read();
                user.ID = (int)reader["Id"];
                user.UserName = reader["UserName"].ToString();
                user.Email = reader["Email"].ToString();
                user.Password = reader["Password"].ToString();
                user.Question = reader["Question"].ToString();
                user.Answer = reader["Answer"].ToString();
                user.Role = reader["Role"].ToString();
                user.LastLogin = (!Convert.IsDBNull(reader["LastLogin"]) ? Convert.ToDateTime(reader["LastLogin"]) : user.LastLogin);
                user.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                reader.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return user;
        }
        public static List<User> GetUsers() {
            List<User> userList = new List<User>();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM Login ORDER BY Id";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User();
                    user.ID = (int)reader["Id"];
                    user.UserName = reader["UserName"].ToString();
                    user.Email = reader["Email"].ToString();
                    user.Password = reader["Password"].ToString();
                    user.Question = reader["Question"].ToString();
                    user.Answer = reader["Answer"].ToString();
                    user.Role = reader["Role"].ToString();
                    user.LastLogin = (!Convert.IsDBNull(reader["LastLogin"]) ? Convert.ToDateTime(reader["LastLogin"]) : user.LastLogin);
                    user.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    userList.Add(user);
                }
                reader.Close();
            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return userList;
        }
        public static void AddUser(string username, string email, string password, string question, string answer, string role)
        {
            string insStmt = "INSERT INTO Login (UserName, Email, Password, Question, Answer, Role, CreatedDate) VALUES(@UserName, @Email, @Password, @Question, @Answer, @Role, @CreatedDate)";
            SqlConnection conn = GetConnection();
            SqlCommand insCmd = new SqlCommand(insStmt, conn);
            insCmd.Parameters.AddWithValue("@UserName", username);
            insCmd.Parameters.AddWithValue("@Email", email);
            insCmd.Parameters.AddWithValue("@Password", password);
            insCmd.Parameters.AddWithValue("@Question", question);
            insCmd.Parameters.AddWithValue("@Answer", answer);
            insCmd.Parameters.AddWithValue("@Role", role);
            insCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Today.ToShortDateString());
            try { conn.Open(); insCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void UpdateUser(int id, string username, string email, string password, string question, string answer, string role)
        {
            string insStmt = "UPDATE Login SET UserName=@UserName, Email=@Email, Password=@Password, Question=@Question, Answer=@Answer, Role=@Role WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand updCmd = new SqlCommand(insStmt, conn);
            updCmd.Parameters.AddWithValue("@Id", id);
            updCmd.Parameters.AddWithValue("@UserName", username);
            updCmd.Parameters.AddWithValue("@Email", email);
            updCmd.Parameters.AddWithValue("@Password", password);
            updCmd.Parameters.AddWithValue("@Question", question);
            updCmd.Parameters.AddWithValue("@Answer", answer);
            updCmd.Parameters.AddWithValue("@Role", role);
            try { conn.Open(); updCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
        public static void DeleteUser(int id)
        {
            string delStmt = "DELETE FROM Login WHERE Id=@Id";
            SqlConnection conn = GetConnection();
            SqlCommand delCmd = new SqlCommand(delStmt, conn);
            delCmd.Parameters.AddWithValue("@Id", id);
            try { conn.Open(); delCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }
    }
}
