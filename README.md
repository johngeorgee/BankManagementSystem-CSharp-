# Mini Banking Management System

A simple console-based banking application written in C# that demonstrates
object-oriented design with abstract classes, inheritance, method overriding,
and a centralized transaction logger.

## Features

- **Account types**
  - **Savings Account** — enforces a minimum balance, applies an interest
    bonus on deposits, and calculates interest from a configurable rate.
  - **Checking Account** — supports overdraft up to a configurable limit.
- **Transactions**
  - Deposit and withdraw with validation and clear console feedback.
  - Failed transactions are logged with a reason.
- **Account management**
  - Create, view details, and close accounts.
  - Accounts can only be closed when the balance is zero.
- **Logging and statistics**
  - Transaction logs with timestamp, account, type, amount, status,
    and balance after.
  - Bank statistics: total accounts, deposits, withdraws, successful
    and failed operations.
- **Auto-incremented account numbers** starting at `1000`.

## Project Structure

| `BankAccount.cs` | Abstract base class with shared account logic and transaction flow. |
| `SavingAccount.cs` | Savings account with minimum balance and deposit interest bonus. |
| `CheckingAccount.cs` | Checking account with overdraft support. |
| `BankLogger.cs` | Static logger for transactions, counters, and statistics. |
| `Program.cs` | Console menu and user interaction. |

## Requirements

- .NET (compatible with the target framework used in your project)
- A terminal / console

## Getting Started

1. Clone the repository.
2. Open the solution in your IDE (Visual Studio, Rider, or VS Code).
3. Build and run the project:

   ```bash
   dotnet run
