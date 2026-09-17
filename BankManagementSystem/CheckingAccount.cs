using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem
{
    internal class CheckingAccount : BankAccount
    {
        public static double OverdraftLimit { get; private set; }
        
        public CheckingAccount(string holderName, double balance, double overdraftLimit = 500) : base(holderName, balance)
        {
            if (overdraftLimit < 0)
                throw new ArgumentException("Overdraft limit cannot be negative");

            OverdraftLimit = overdraftLimit;
        }

        public override double CalculateInterest()
        {
            return Balance * 0.02;
        }

        public override bool Withdraw(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine($"Cannot withdraw. Overdraft limit of ${OverdraftLimit} exceeded.");
                Console.WriteLine($"Available funds (including overdraft): ${Balance + OverdraftLimit}");
                BankLogger.FailedTransactions(AccountNumber, "Withdraw", amount, $"Exceeds overdraft limit of ${OverdraftLimit}");
                return false;
            }
            double previousBalance = Balance;
            bool result = base.Withdraw(amount);

            if (result && Balance < 0 && previousBalance >= 0)
            {
                Console.WriteLine("WARNING: Account has entered overdraft!");
                Console.WriteLine($"Overdraft amount: ${Math.Abs(Balance):F2}");
                Console.WriteLine($"Remaining overdraft available: ${OverdraftLimit - Math.Abs(Balance):F2}");
            }

            return result;

        }
        public override void GetDetails()
        {
            base.GetDetails();
            Console.WriteLine($"Account Type : Checking");
            Console.WriteLine($"Overdraft Limit : {OverdraftLimit}");
            if (Balance < 0)
            {
                Console.WriteLine($"Overdraft Used: ${Math.Abs(Balance):F2}");
                Console.WriteLine($"Remaining Overdraft: ${OverdraftLimit - Math.Abs(Balance):F2}");
            }

        }
    }
}
