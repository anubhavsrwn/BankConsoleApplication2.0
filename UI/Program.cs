using System;
using System.Collections.Generic;
using System.Text;
using Services;
namespace UserInterface
{
    
    class Program
    {
        static void CreateBank()
        {
            Console.Clear();
            Console.WriteLine("Enter Bank Name :");
            string bankName = Console.ReadLine();
            Console.WriteLine("Enter Bank Staff's Name : ");
            string bankStaffName = Console.ReadLine();
            Console.WriteLine("Enter Bank Staff's Password : ");
            string bankStaffPassword = Console.ReadLine();
            Services.CreateBank(bankName, bankStaffName, bankStaffPassword);
            Console.WriteLine("Bank Created Succesfully...");
            Console.WriteLine(Services.DisplayBankStaff());
            Console.ReadKey();
            Menu();
        }
        static void ViewBanks()
        {
            Console.Clear();
            if (Services.IsBank() == false)
            {
                Console.WriteLine("No Banks exist! \nCreate a bank first");
                Console.ReadKey();
                Menu();
            }

            Console.WriteLine(Services.DisplayBank());
            Console.ReadKey();
            Menu();
        }
        static void LoginBankStaff()
        {
            Console.Clear();
            if (Services.IsBank() == false)
            {
                Console.WriteLine("No Banks exist! \nCreate a bank first");
                Console.ReadKey();
                Menu();
            }


            Console.WriteLine("Select the bank in which you want to Login as a Staff :");
            Console.WriteLine(Services.bankList());
            int bankIndex = Convert.ToInt32(Console.ReadLine());
            bankIndex--;


            if (Services.ValidateBankChoice(bankIndex) == false)
            {
                Console.WriteLine("Invalid Selection!");
                Console.ReadKey();
                Menu();
            }

            Console.WriteLine("Enter BankStaff Id:");
            string bankStaffId = Console.ReadLine();
            Console.WriteLine("Enter BankStaff Password:");
            string bankStaffPassword = Console.ReadLine();
            if (Services.ValidateBankStaff(bankStaffId, bankStaffPassword, bankIndex) == false)
            {
                Console.WriteLine("Invalid Credentials! ");
                Console.ReadKey();
                Menu();
            }

        BankStaffMenu:

            Console.Clear();
            Console.WriteLine("Logged in.");
            Console.WriteLine("Select any option : ");
            Console.WriteLine("1. Create an account.");
            Console.WriteLine("2. Delete an account.");
            Console.WriteLine("3. Show Transactions.");
            Console.WriteLine("4. Revert Transactions.");
            Console.WriteLine("5. Exit this menu.");
            int choice2 = Convert.ToInt32(Console.ReadLine());
            switch (choice2)
            {
                case 1:
                    Console.WriteLine("Enter Account Holder's Name :");
                    string accountHolderName = Console.ReadLine();
                    Console.WriteLine("Create a Password");
                    string accountPassword = Console.ReadLine();
                    Console.WriteLine("Enter amount of initial deposit : ");
                    decimal initialDeposit = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine(Services.CreateAccount(bankIndex, accountHolderName, accountPassword, initialDeposit));
                    Console.WriteLine("Account Created Succesfully!");
                    Console.ReadKey();
                    goto BankStaffMenu;

                case 2:
                    Console.WriteLine("Enter Account ID :");
                    string accountId = Console.ReadLine();
                    Services.DeleteAccount(bankIndex, accountId);
                    Console.ReadKey();
                    goto BankStaffMenu;

                case 3:
                    Console.WriteLine(Services.BankTransactions(bankIndex));
                    Console.ReadKey();
                    goto BankStaffMenu;

                case 4:
                    Console.WriteLine("Enter Transaction ID : ");
                    string transactionId = Console.ReadLine();

                    if (Services.ValidateTransactionId(transactionId, bankIndex) == false)
                    {
                        Console.WriteLine("Transaction does not exist!!");
                        Console.ReadKey();
                        goto BankStaffMenu;
                    }

                    Services.RevertTransaction(transactionId);

                    goto BankStaffMenu;
                case 5:
                    Menu();
                    break;

                default:
                    Console.WriteLine("Invalid Selection!");
                    Console.ReadKey();
                    goto BankStaffMenu;
            }
        }
        static void LoginCustomer()
        {
            Console.Clear();
            if (Services.IsBank() == false)
            {
                Console.WriteLine("No Banks exist! \nCreate a bank first");
                Console.ReadKey();
                Menu();
            }


            Console.WriteLine("Select the bank in which you want to Login as a Customer :");
            Console.WriteLine(Services.bankList());
            int bankIndex = Convert.ToInt32(Console.ReadLine());
            bankIndex--;


            if (Services.ValidateBankChoice(bankIndex) == false)
            {
                Console.WriteLine("Invalid Selection!");
                Console.ReadKey();
                Menu();
            }

            Console.WriteLine("Enter Customer ID :");
            string customerId = Console.ReadLine();
            Console.WriteLine("Enter Your Password:");
            string customerPassword = Console.ReadLine();

            int accountIndex = Services.ValidateCustomer(customerId, customerPassword, bankIndex);
            if (accountIndex == -1)
            {
                Console.WriteLine("Invalid Credentials");
                Console.ReadKey();
                Menu();
            }

        CostumerLoginMenu:
            Console.Clear();
            Console.WriteLine("Logged in ");
            Console.WriteLine("Select any option : ");
            Console.WriteLine("1. Make a Deposit.");
            Console.WriteLine("2. Make a Withdrawal.");
            Console.WriteLine("3. Transfer funds.");
            Console.WriteLine("4. View Transactions.");
            Console.WriteLine("5. Exit this menu.");
            int choice3 = Convert.ToInt32(Console.ReadLine());
            switch (choice3)
            {
                case 1:
                    Console.WriteLine("Enter Amount to be deposited");
                    decimal depositAmount = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine(Services.Deposit(depositAmount, accountIndex, bankIndex));
                    Console.ReadKey();
                    goto CostumerLoginMenu;

                case 2:
                    Console.WriteLine("Enter Amount to be Withdrawn");
                    decimal withdrawalAmount = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine(Services.Withdrawal(withdrawalAmount, accountIndex, bankIndex));
                    Console.ReadKey();
                    goto CostumerLoginMenu;

                case 3:
                    Console.WriteLine("Enter Receiver's Bank ID : ");
                    string bankId = Console.ReadLine();
                    int receiverBankIndex = Services.ValidateBank(bankId);
                    if (receiverBankIndex == -1)
                    {
                        Console.WriteLine("Bank does not exist!!");
                        Console.ReadKey();
                        goto CostumerLoginMenu;
                    }

                    Console.WriteLine("Enter Receiver's Customer ID : ");
                    string receiverCustomerId = Console.ReadLine();

                    int receiverAccountIndex = Services.ValidateCustomer(receiverCustomerId, receiverBankIndex);
                    if (receiverAccountIndex == -1)
                    {
                        Console.WriteLine("Account does not exist!!");
                        Console.ReadKey();
                        goto CostumerLoginMenu;
                    }
                    string senderCustomerId = customerId;
                    Console.WriteLine("Enter Amount to be transferred : ");
                    decimal amount = Convert.ToDecimal(Console.ReadLine());
                    decimal serviceCharge;
                    if (bankIndex == receiverBankIndex)
                    {

                        Console.WriteLine("Select One :");
                        Console.WriteLine("1. IMPS (5%)");
                        Console.WriteLine("2. RTGS (0%)");
                        Console.WriteLine("RTGS will be default.");
                        int choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                serviceCharge = 0.05m;
                                break;

                            default:
                                serviceCharge = 0.00m;
                                break;
                        }
                    }
                    else
                    {

                        Console.WriteLine("Select One :");
                        Console.WriteLine("1. IMPS (6%)");
                        Console.WriteLine("2. RTGS (2%)");
                        Console.WriteLine("RTGS will be default.");
                        int choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                serviceCharge = 0.06m;
                                break;

                            default:
                                serviceCharge = 0.02m;
                                break;
                        }

                    }
                    Services.FundTransfer(amount, serviceCharge, accountIndex, bankIndex, receiverAccountIndex, receiverBankIndex);
                    goto CostumerLoginMenu;

                case 4:
                    Console.WriteLine(Services.AccountTransactions(accountIndex, bankIndex));
                    Console.ReadKey();
                    goto CostumerLoginMenu;

                case 5:
                    Menu();
                    break;

                default:
                    Console.WriteLine("Invalid Selection!");
                    Console.ReadKey();
                    goto CostumerLoginMenu;
            }
        }
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Main Menu :");
            Console.WriteLine("1. Create a New Bank.");
            Console.WriteLine("2. Show all Banks.");
            Console.WriteLine("3. Login as a Bank Staff");
            Console.WriteLine("4. Login as a Customer");

            int choice1 = Convert.ToInt32(Console.ReadLine());
            switch (choice1)
            {
                case 1:
                    CreateBank();
                    break;
                case 2:
                    ViewBanks();
                    break;
                case 3:
                    LoginBankStaff();
                    break;
                case 4:
                    LoginCustomer();
                    break;

                default:
                    Console.WriteLine("Invalid Selection! \nTry Again!");
                    Console.ReadKey();
                    Menu();
                    break;
            }
        

        
    }

        static Services.Services Services = new Services.Services();
        static void Main(string[] args)
        {
            Menu();



        }

    }
}