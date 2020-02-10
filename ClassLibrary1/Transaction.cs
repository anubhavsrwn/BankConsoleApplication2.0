using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Transaction
    {
        public string TransactionType { get; set; } //Deposit, Withdrawal or Fund Transfer
        public string TransactionId { get; set; }
        public string AccountId { get; set; }
        public string SendersAccountId { get; set; }
        public string ReceiversAccountId { get; set; }
        public decimal UpdatedBalance { get; set; }
        public decimal Amount { get; set; }

        public void GenerateTransactionId()
        {
            TransactionId = "TXN" + DateTime.Now.ToString("mmss");
        }

        public Transaction(string transactionType, string accountId, decimal amount, decimal updatedbalance)
        {
            this.TransactionType = transactionType;
            this.Amount = amount;
            this.AccountId = accountId;
            this.UpdatedBalance = updatedbalance;
            GenerateTransactionId();
        }

        public Transaction(string transactionType,string otherAccountId, string accountId, decimal amount, decimal updatedbalance)
        {
            this.TransactionType = transactionType;
            if (transactionType == "Sent To")
            {
                this.ReceiversAccountId = otherAccountId;
            }
            else
            {
                this.SendersAccountId = otherAccountId;
            }
            this.Amount = amount;
            this.AccountId = accountId;
            this.UpdatedBalance = updatedbalance;
            GenerateTransactionId();
        }



    }
}
