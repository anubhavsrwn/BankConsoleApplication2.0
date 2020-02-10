using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Customer
    {
        public string CustomerName;
        public string CustomerId;
        public string CustomerPassword;

        public Customer(string customerName, string customerId, string customerpassword)
        {
            this.CustomerName = customerName;
            this.CustomerId = customerId;
            this.CustomerPassword = customerpassword;
        }
    }
}
