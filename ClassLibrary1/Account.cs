using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Account
    {
        public string AccountId { get; set; }
        public string AccountHolderName { get; set; }
        public decimal AccountBalance { get; set; }
        public Customer Customer;
        public List<Transaction> AccountTransactions = new List<Transaction>();


        public Account(string accountHolderName, string password, decimal initialDeposit, string BankName, string BankId)
        {
            AccountHolderName = accountHolderName;
            AccountId = AccountHolderName.Substring(0, 3) + "@" + BankName.Substring(0, 3) + DateTime.Now.ToString("mmss");
            Customer = new Customer(AccountHolderName, AccountId, password);
            AccountBalance += initialDeposit;
            AccountTransactions.Add(new Transaction("Account Created", AccountId, AccountBalance, AccountBalance));
        }


    }
}
