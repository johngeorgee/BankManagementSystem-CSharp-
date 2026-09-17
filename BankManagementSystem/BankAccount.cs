using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem
{
    public abstract class BankAccount
    {

        private static int _accountNumber;
        public int AccountNumber { get; set; }
        private string _holderName;
        public string HolderName
        {
            get
            {
                return _holderName; 
            } set
            {
                if (!string.IsNullOrEmpty(value) && value.Length >= 3 && !value.Contains(" "))
                {
                    _holderName = value;
                }
            }
        }
        private double _balance;
        public double Balance
        {
            get
            {
                return _balance;
            } private set
            {
                if(value >= 0)
                {
                    _balance = value;
                    IsActive = true;
                }
                else
                {
                    IsActive = false;
                    Console.WriteLine("Cannot Assign Balance of Negative Value");
                }
            }
        }
        public bool IsActive { get; set; } = true;
        static BankAccount()
        {
            _accountNumber = 1000;
        }
        public BankAccount(string holderName, double balance)
        {
            AccountNumber = _accountNumber++;
            IsActive = true;
            HolderName = holderName;
            Balance = balance;
            BankLogger.TotalAccountsOpened(true, AccountNumber);
        }

        public abstract double CalculateInterest();
        public virtual bool Deposit(double amount)
        {
            if(IsActive == false)
            {
                Console.WriteLine("Account Cannot Perform Transactions");
                BankLogger.FailedTransactions(AccountNumber, "Deposit", amount, "Account is closed");
                return false;
            }
            else
            {
                if(amount > 0)
                {
                    Balance += amount;
                    BankLogger.SuccessfulTransactions(AccountNumber, "Deposit", amount, Balance);
                    
                    Console.WriteLine($"Adding amount of : {amount} || Your Current Balance is {Balance}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Mount Cannot be Zero or Negative");
                    BankLogger.FailedTransactions(AccountNumber, "Deposit", amount, "Amount must be > 0");
                    return false;
                }
            }
            
        }
        public  virtual bool Withdraw(double amount)
        {
            if (IsActive == false)
            {
                Console.WriteLine("Account Cannot Perform Transactions");
                BankLogger.FailedTransactions(AccountNumber, "Withdraw", amount, "Account is closed");
                return false;

            }
            else
            {
                if (amount <= Balance && amount > 0)
                {
                    Balance -= amount;
                    if (Balance < 0)
                    {
                        Console.WriteLine("WARNING: Account has entered overdraft!");
                    }

                    BankLogger.SuccessfulTransactions(AccountNumber, "Withdraw", amount, Balance);
                    Console.WriteLine($"Withdrawing amount of : {amount} || Your Current Balance is {Balance}");
                    return true;
                } else
                {
                    BankLogger.FailedTransactions(AccountNumber, "Withdraw", amount, "Amount must be > 0");
                    Console.WriteLine($"Invalid Amount to withdraw: Maybe Not Have Enough Balance or Entered A Negative Number");
                    return false;
                }
            }
            
        }

        public virtual void GetDetails()
        {
            Console.WriteLine($"Account #{AccountNumber}");
            Console.WriteLine($"Holder Name: {HolderName}");
            Console.WriteLine($"Balance : {Balance}");
            Console.WriteLine($"Interest: ${CalculateInterest():F2}"); 
            Console.WriteLine($"Status: {(IsActive ? "Active" : "Closed")}");
        }
        public virtual bool CloseAccount()
        {
            if (!IsActive)
            {
                Console.WriteLine("Account is already closed.");
                return false;
            }

            if (Balance != 0)
            {
                Console.WriteLine($"Cannot close account. Balance must be zero. Current balance: ${Balance}");
                return false;
            }

            IsActive = false;
            Console.WriteLine($"Account #{AccountNumber} has been closed successfully.");
            return true;
        }
       
    }

}

