using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem
{
    internal static class BankLogger
    {
        private static List<TransactionLog> _transactionLogs = new List<TransactionLog>();

        private static int _totalActiveAccounts = 0;
        private static int _totalDeposits = 0;
        private static int _totalWithdraw =0;
        private static int _successfulTransactions = 0;
        private static int _failedTransactions = 0;
  
        public static void SuccessfulTransactions(int accNo, string transType, double amount, double newBalance)
        {
            _successfulTransactions++;
            var log = new TransactionLog
            {
                Timestamp = DateTime.Now,
                AccountNumber = accNo,
                Type = transType,
                Amount = amount,
                BalanceAfter = newBalance,
                Status = "SUCCESS"
            };
            _transactionLogs.Add(log);

        }
        public static void FailedTransactions(int accNo, string transType, double amount, string reason)
        {
            _failedTransactions++;
            var log = new TransactionLog
            {
                Timestamp = DateTime.Now,
                AccountNumber = accNo,
                Type = transType,
                Amount = amount,
                BalanceAfter = null,
                Status = "FAILED",
                Reason = reason
            };
            _transactionLogs.Add(log);
        }
        public static void TotalAccountsOpened(bool status, int accNo)
        {
            if(status == true)
            {
                _totalActiveAccounts++;
                Console.WriteLine($"Account #{accNo} created Successfully");
                Console.WriteLine($"Total Accounts Opened : {_totalActiveAccounts}");
            }
        }
        
        public static void BankStatistics()
        {
            Console.WriteLine("======= Bank Statistics ========");
            Console.WriteLine($"Total Accounts : {_totalActiveAccounts}");
            Console.WriteLine($"Total Deposits : {_totalDeposits}");
            Console.WriteLine($"Total Withdraws : {_totalWithdraw}");
            Console.WriteLine($"Successful Operations : {_successfulTransactions}");
            Console.WriteLine($"Failed Operations : {_failedTransactions}");

        }
        public static void DisplayTransactionLogs()
        {
            if (_transactionLogs.Count == 0)
            {
                Console.WriteLine("\nNo transactions have been performed yet.\n");
                return;
            }

            Console.WriteLine("\n========== TRANSACTION LOGS ==========");
            foreach (var log in _transactionLogs)
            {
                Console.WriteLine($"[{log.Timestamp:yyyy-MM-dd HH:mm}]");
                Console.WriteLine($"Account: #{log.AccountNumber}");
                Console.WriteLine($"Type: {log.Type}");
                Console.WriteLine($"Amount: ${log.Amount:F2}");
                Console.WriteLine($"Status: {log.Status}");

                if (log.Status == "SUCCESS")
                {
                    Console.WriteLine($"Balance After: ${log.BalanceAfter:F2}");
                }
                else
                {
                    Console.WriteLine($"Reason: {log.Reason}");
                }
                Console.WriteLine("-------------------------------------");
            }
            Console.WriteLine("=====================================\n");
        }
        public static void DisplayWithdraws()
        {
            _totalWithdraw++;
            Console.WriteLine($"Total Withdraws : {_totalWithdraw}");
        }
        public static void DisplayDeposits()
        {
            _totalDeposits++;
            Console.WriteLine($"Total Deposits : {_totalDeposits}");
        }

        public class TransactionLog
        {
            public DateTime Timestamp { get; set; }
            public int AccountNumber { get; set; }
            public string Type { get; set; }
            public double Amount { get; set; }
            public double? BalanceAfter { get; set; }
            public string Status { get; set; }
            public string Reason { get; set; }
        }
    }
   
}
