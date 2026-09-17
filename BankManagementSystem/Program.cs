using System;
using System.Collections.Generic;
using System.Linq;

namespace BankManagementSystem
{
    internal class Program
    {
        private static List<BankAccount> accounts = new List<BankAccount>();

        static void Main(string[] args)
        {
            Console.Title = "Mini Banking Management System";

            while (true)
            {
                ShowMenu();
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateSavingsAccount();
                        break;
                    case "2":
                        CreateCheckingAccount();
                        break;
                    case "3":
                        DepositMoney();
                        break;
                    case "4":
                        WithdrawMoney();
                        break;
                    case "5":
                        ViewAccountDetails();
                        break;
                    case "6":
                        DisplayAllAccounts();
                        break;
                    case "7":
                        CloseAccount();
                        break;
                    case "8":
                        BankLogger.BankStatistics();
                        break;
                    case "9":
                        BankLogger.DisplayTransactionLogs();
                        break;
                    case "0":
                        Console.WriteLine("\nThank you for using the Bank Management System. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("\nInvalid option. Please try again.\n");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("         BANK MANAGEMENT SYSTEM         ");
            Console.WriteLine("========================================");
            Console.WriteLine(" 1. Open Savings Account");
            Console.WriteLine(" 2. Open Checking Account");
            Console.WriteLine(" 3. Deposit Money");
            Console.WriteLine(" 4. Withdraw Money");
            Console.WriteLine(" 5. View Account Details");
            Console.WriteLine(" 6. Display All Accounts");
            Console.WriteLine(" 7. Close Account");
            Console.WriteLine(" 8. Show Bank Statistics");
            Console.WriteLine(" 9. Show Transaction Logs");
            Console.WriteLine(" 0. Exit");
            Console.WriteLine("========================================");
            Console.Write("Select an option: ");
        }

        static void CreateSavingsAccount()
        {
            Console.Clear();
            Console.WriteLine("\n===== OPEN SAVINGS ACCOUNT =====\n");

            string name = GetValidName();
            double balance = GetValidBalance();
            double interestRate = GetValidInterestRate();
            double minBalance = GetValidMinimumBalance();

            try
            {
                if (balance < minBalance)
                {
                    Console.WriteLine($"\nInitial balance must be at least minimum balance of ${minBalance}\n");
                    return;
                }

                var account = new SavingAccount(name, balance, interestRate, minBalance);
                accounts.Add(account);
                Console.WriteLine($"\n Savings Account #{account.AccountNumber} created successfully!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Error: {ex.Message}\n");
            }
            PressAnyKey();
        }

        static void CreateCheckingAccount()
        {
            Console.Clear();
            Console.WriteLine("\n===== OPEN CHECKING ACCOUNT =====\n");

            string name = GetValidName();
            double balance = GetValidBalance();
            double overdraftLimit = GetValidOverdraftLimit();

            try
            {
                var account = new CheckingAccount(name, balance, overdraftLimit);
                accounts.Add(account);
                Console.WriteLine($"\n Checking Account #{account.AccountNumber} created successfully!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Error: {ex.Message}\n");
            }
            PressAnyKey();
        }

        static void DepositMoney()
        {
            Console.Clear();
            Console.WriteLine("\n===== DEPOSIT MONEY =====\n");

            var account = FindAccount();
            if (account == null) return;

            Console.Write("Enter deposit amount: $");
            if (!double.TryParse(Console.ReadLine(), out double amount) || amount <= 0)
            {
                Console.WriteLine("\n Invalid amount. Must be greater than zero.\n");
                PressAnyKey();
                return;
            }

            account.Deposit(amount);
            PressAnyKey();
        }

        static void WithdrawMoney()
        {
            Console.Clear();
            Console.WriteLine("\n===== WITHDRAW MONEY =====\n");

            var account = FindAccount();
            if (account == null) return;

            Console.Write("Enter withdrawal amount: $");
            if (!double.TryParse(Console.ReadLine(), out double amount) || amount <= 0)
            {
                Console.WriteLine("\n Invalid amount. Must be greater than zero.\n");
                PressAnyKey();
                return;
            }

            account.Withdraw(amount);
            PressAnyKey();
        }

        static void ViewAccountDetails()
        {
            Console.Clear();
            Console.WriteLine("\n===== VIEW ACCOUNT DETAILS =====\n");

            var account = FindAccount();
            if (account == null) return;

            Console.WriteLine("\n===== ACCOUNT DETAILS =====\n");
            account.GetDetails();
            Console.WriteLine();
            PressAnyKey();
        }

        static void DisplayAllAccounts()
        {
            Console.Clear();
            Console.WriteLine("\n===== ALL ACCOUNTS =====\n");

            if (accounts.Count == 0)
            {
                Console.WriteLine("No accounts have been created yet.\n");
                PressAnyKey();
                return;
            }

            Console.WriteLine($"Total Accounts: {accounts.Count}\n");
            Console.WriteLine("========================================");

            foreach (var account in accounts)
            {
                account.GetDetails();
                Console.WriteLine($"Calculated Interest: ${account.CalculateInterest():F2}");
                Console.WriteLine("----------------------------------------");
            }

            PressAnyKey();
        }

        static void CloseAccount()
        {
            Console.Clear();
            Console.WriteLine("\n===== CLOSE ACCOUNT =====\n");

            var account = FindAccount();
            if (account == null) return;

            Console.Write($"Are you sure you want to close account #{account.AccountNumber}? (y/n): ");
            string confirm = Console.ReadLine()?.ToLower();

            if (confirm == "y" || confirm == "yes")
            {
                account.CloseAccount();
            }
            else
            {
                Console.WriteLine("\nAccount closure cancelled.\n");
            }
            PressAnyKey();
        }

        static BankAccount FindAccount()
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("No accounts exist. Please create an account first.\n");
                PressAnyKey();
                return null;
            }

            Console.Write("Enter Account Number: ");
            if (!int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                Console.WriteLine("\n Invalid account number format.\n");
                PressAnyKey();
                return null;
            }

            var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (account == null)
            {
                Console.WriteLine($"\n Account #{accountNumber} not found.\n");
                PressAnyKey();
                return null;
            }

            return account;
        }

        // Helper Methods
        static string GetValidName()
        {
            while (true)
            {
                Console.Write("Enter holder name (min 3 characters): ");
                string name = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(name) && name.Length >= 3)
                    return name;

                Console.WriteLine(" Invalid name. Must be at least 3 characters.");
            }
        }

        static double GetValidBalance()
        {
            while (true)
            {
                Console.Write("Enter initial balance: $");
                if (double.TryParse(Console.ReadLine(), out double balance) && balance >= 0)
                    return balance;

                Console.WriteLine(" Invalid balance. Must be zero or positive.");
            }
        }

        static double GetValidInterestRate()
        {
            while (true)
            {
                Console.Write("Enter interest rate (as decimal, e.g., 0.02 for 2%): ");
                if (double.TryParse(Console.ReadLine(), out double rate) && rate >= 0)
                    return rate;

                Console.WriteLine(" Invalid interest rate. Must be zero or positive.");
            }
        }

        static double GetValidMinimumBalance()
        {
            while (true)
            {
                Console.Write("Enter minimum balance requirement: $");
                if (double.TryParse(Console.ReadLine(), out double minBalance) && minBalance >= 0)
                    return minBalance;

                Console.WriteLine(" Invalid minimum balance. Must be zero or positive.");
            }
        }

        static double GetValidOverdraftLimit()
        {
            while (true)
            {
                Console.Write("Enter overdraft limit: $");
                if (double.TryParse(Console.ReadLine(), out double limit) && limit >= 0)
                    return limit;

                Console.WriteLine(" Invalid overdraft limit. Must be zero or positive.");
            }
        }

        static void PressAnyKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}