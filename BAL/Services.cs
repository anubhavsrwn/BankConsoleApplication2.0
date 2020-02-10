using System;
using System.Collections.Generic;
using System.Text;
using Model;
namespace Services
{
    
    public class Services
    {
        public static List<Bank> BankList = new List<Bank>();

        public void CreateBank(string bankName, string bankStaffName, string bankStaffPassword)
        {
            BankList.Add(new Bank(bankName, bankStaffName, bankStaffPassword));
 
        }

        public string DisplayBank()
        {
            string BankInfo = "";
            foreach (var Bank in BankList)
            {
                BankInfo += "\n\nBank Name : " + Bank.BankName + "\nBank ID : " + Bank.BankId + "\nBank Currency : " + Bank.Currency;
            }

            return BankInfo;
        }

        public string DisplayBankStaff()
        {
            return "\nBank ID : " + BankList[BankList.Count - 1].BankId  + "\nBank Staff ID : " + BankList[BankList.Count-1].Bankstaff.BankStaffId;
        }

        public bool IsBank()
        {
            if (BankList.Count == 0)
                return false;
            else
                return true;
        }

        public bool ValidateBankStaff(string bankStaffId, string bankStaffPassword, int bankIndex)
        {
            if(BankList[bankIndex].Bankstaff.BankStaffId==bankStaffId && BankList[bankIndex].Bankstaff.BankStaffPassword==bankStaffPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }

        public string bankList()
        {
            string bankList = "";
            for (int i = 0; i < BankList.Count; i++)
            {
                Bank Bank = BankList[i];
                bankList += (i+1)+ ". " + Bank.BankName+"\n";
            }

            return bankList;
        }

        public bool ValidateBankChoice(int bankIndex)
        {
            if (bankIndex < BankList.Count)
                return true;
            else
                return false;
        }

        public string CreateAccount(int bankIndex, string accountHolderName, string accountPassword, decimal initialDeposit)
        {
            BankList[bankIndex].Accounts.Add(new Account(accountHolderName, accountPassword, initialDeposit, BankList[bankIndex].BankName, BankList[bankIndex].BankId));

            return "\nCustomer ID : " + BankList[bankIndex].Accounts[BankList[bankIndex].Accounts.Count - 1].AccountId + "\nBank ID : " + BankList[bankIndex].BankId;
        }

        public void DeleteAccount(int bankIndex, string accountId)
        {
            BankList[bankIndex].Accounts.RemoveAll(x => x.AccountId == accountId);
        }

        public string TransactionHistory(Transaction transaction)
        {
            return "\n" + transaction.AccountId + " | " + transaction.TransactionId + " | " + transaction.TransactionType + " | " + transaction.SendersAccountId + transaction.ReceiversAccountId + " | " + transaction.Amount + " | BAL : " + transaction.UpdatedBalance;
        }

        public string BankTransactions(int bankIndex)
        {
            string bankTransaction = "";

            foreach (var  Account in BankList[bankIndex].Accounts)
            {
                foreach (var Transaction in Account.AccountTransactions)
                {
                    bankTransaction += TransactionHistory(Transaction);
                }
            }

            return bankTransaction;
        }
        
        public int ValidateCustomer(string customerId, string customerPassword, int bankIndex)
        {
            int index = BankList[bankIndex].Accounts.FindIndex(item => item.Customer.CustomerId == customerId && item.Customer.CustomerPassword == customerPassword);
            return index;
        }

        public int ValidateBank(string bankId)
        {
            int index = BankList.FindIndex(item => item.BankId == bankId);

            return index;
        }

        public int ValidateCustomer(string customerId, int bankIndex)
        {
            int index = BankList[bankIndex].Accounts.FindIndex(item => item.Customer.CustomerId == customerId );
           
            return index;
        }

        public string Deposit(decimal depositAmount, int accountIndex, int bankIndex)
        {
            BankList[bankIndex].Accounts[accountIndex].AccountBalance += depositAmount;
            BankList[bankIndex].Accounts[accountIndex].AccountTransactions.Add(new Transaction("Deposit", BankList[bankIndex].Accounts[accountIndex].AccountId, depositAmount, BankList[bankIndex].Accounts[accountIndex].AccountBalance));
            return "Updated balance : " + BankList[bankIndex].Accounts[accountIndex].AccountBalance;
        }

        public string Deposit(decimal depositAmount, string sendersAccountId, int accountIndex, int bankIndex)
        {
            BankList[bankIndex].Accounts[accountIndex].AccountBalance += depositAmount;
            BankList[bankIndex].Accounts[accountIndex].AccountTransactions.Add(new Transaction("Received From", sendersAccountId, BankList[bankIndex].Accounts[accountIndex].AccountId, depositAmount, BankList[bankIndex].Accounts[accountIndex].AccountBalance));
            return "Updated balance : " + BankList[bankIndex].Accounts[accountIndex].AccountBalance;
        }

        public string Withdrawal(decimal withdrawnAmount, int accountIndex, int bankIndex)
        {
            BankList[bankIndex].Accounts[accountIndex].AccountBalance -= withdrawnAmount;
            BankList[bankIndex].Accounts[accountIndex].AccountTransactions.Add(new Transaction("Withdrawal", BankList[bankIndex].Accounts[accountIndex].AccountId, withdrawnAmount, BankList[bankIndex].Accounts[accountIndex].AccountBalance));
            return "Updated balance : " + BankList[bankIndex].Accounts[accountIndex].AccountBalance;
        }

        public string Withdrawal(decimal withdrawnAmount, string receiverAccountId, int accountIndex, int bankIndex)
        {
            BankList[bankIndex].Accounts[accountIndex].AccountBalance -= withdrawnAmount;
            BankList[bankIndex].Accounts[accountIndex].AccountTransactions.Add(new Transaction("Sent To", receiverAccountId, BankList[bankIndex].Accounts[accountIndex].AccountId, withdrawnAmount, BankList[bankIndex].Accounts[accountIndex].AccountBalance));
            return "Updated balance : " + BankList[bankIndex].Accounts[accountIndex].AccountBalance;
        }

        public string AccountTransactions(int accountIndex, int bankIndex)
        {
            string accountTransaction = "";

            foreach (var Transaction in BankList[bankIndex].Accounts[accountIndex].AccountTransactions)
            {
                accountTransaction += TransactionHistory(Transaction);
            }
            
            return accountTransaction;
        }

        public void FundTransfer(decimal amount, decimal serviceCharge, int sAccountIndex, int sBankIndex, int rAccountIndex, int rBankIndex)
        {
            string sendersAccountId = BankList[sBankIndex].Accounts[sAccountIndex].AccountId;
            string receiversAccountId = BankList[rBankIndex].Accounts[rAccountIndex].AccountId;
            decimal ServiceCharge = serviceCharge;

            Withdrawal(amount, receiversAccountId, sAccountIndex, sBankIndex);
            Deposit((amount- amount*ServiceCharge), sendersAccountId, rAccountIndex, rBankIndex);
            

        }

        public bool ValidateTransactionId(string transactionId, int bankIndex)
        {
            bool flag = false;
            foreach (var Account in BankList[bankIndex].Accounts)
            {
                foreach (var Transaction in Account.AccountTransactions)
                {
                    if (Transaction.TransactionId == transactionId)
                        flag = true;
                }
            }

            return flag;
        }

        public void RevertTransaction(string transactionId)
        {
            foreach (var Bank in BankList)
            {
                foreach (var Account in Bank.Accounts)
                {
                    for (int i = 0; i < Account.AccountTransactions.Count; i++)
                    {
                        Transaction Transaction = Account.AccountTransactions[i];
                        if (Transaction.TransactionId == transactionId)
                        {
                            if (Transaction.TransactionType == "Sent To")
                            {
                                Account.AccountBalance += Transaction.Amount;
                                Account.AccountTransactions.Remove(Account.AccountTransactions[i]);

                            }

                            else if (Transaction.TransactionType == "Received From")
                            {
                                Account.AccountBalance -= Transaction.Amount;
                                Account.AccountTransactions.Remove(Account.AccountTransactions[i]);
                            }
                        }
                    }
                }

            }
        }
    }

}
