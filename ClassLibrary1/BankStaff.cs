using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class BankStaff
    {
        public string BankStaffName { get; set; }
        public string BankStaffId { get; set; }
        public string BankStaffPassword { get; set; }

        public BankStaff(string bankStaffName, string bankName, string bankStaffPassword)
        { 
            BankStaffName = bankStaffName;
            BankStaffId = BankStaffName + "@" + bankName.Substring(0, 3);
            BankStaffPassword = bankStaffPassword;
        }



    }
}
