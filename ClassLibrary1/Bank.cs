using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    public class Bank
    {

        public string BankName { get; set; }
        public string BankId { get; set; }
        public decimal SIMPS = 0.05m;
        public decimal OIMPS = 0.06m;
        public decimal SRTGS = 0m;
        public decimal ORTGS = 0.02m;
        public string Currency { get; set; }
        public BankStaff Bankstaff { get; set; }
        public List<Account> Accounts  = new List<Account>();

        public Bank(string bankName, string bankStaffName, string bankStaffPassword)
        {
            BankName = bankName;
            BankId = BankName.Substring(0, 3) + DateTime.Now.ToString("mmss");
            Currency = "INR";
            Bankstaff = new BankStaff(bankStaffName,bankName,bankStaffPassword);
        }


            
    }
}
