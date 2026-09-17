using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem
{
    internal class SavingAccount : BankAccount
    {
        public double InterestRate { get; private set; } = 0.10;
        public double MinimumBalance { get; private set; } = 5000;

        public SavingAccount(string holderName, double balance, double interestRate = 0.05, double minimumBalance=1000) : base(holderName, balance)
        {
            if (interestRate < 0)
                throw new ArgumentException("Interest rate cannot be negative");

            if (minimumBalance < 0)
                throw new ArgumentException("Minimum balance cannot be negative");

            if (balance < minimumBalance)
                throw new ArgumentException($"Initial balance must be at least minimum balance of ${minimumBalance}");

            InterestRate = interestRate;
            MinimumBalance = minimumBalance;

        }
        public override bool Deposit(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be greater than zero.");
                return false;
            }

            // Apply interest bonus
            double bonusInterest = amount * InterestRate;
            double totalDeposit = amount + bonusInterest;

            Console.WriteLine($"Deposit: ${amount} + Interest Bonus: ${bonusInterest:F2}");
            return base.Deposit(totalDeposit);

        }

        public override double CalculateInterest()
        {
            return Balance * InterestRate;
        }

        public override bool Withdraw(double amount)
        { 
            if(Balance - amount < MinimumBalance)
            {
                Console.WriteLine($"Cannot withdraw. Minimum balance of ${MinimumBalance} must be maintained.");
                BankLogger.FailedTransactions(AccountNumber, "Withdraw", amount, $"Would drop below minimum balance of ${MinimumBalance}");
                return false;

            }
            return base.Withdraw(amount);
        }
        public override void GetDetails()
        {
            base.GetDetails();
            Console.WriteLine($"Account Type : Savings");
            Console.WriteLine($"Minimum Balance : {MinimumBalance}");
            Console.WriteLine($"Interest Rate   : {InterestRate}");
        }
    }
}
