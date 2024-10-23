using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace atm_app.Entities
{
    internal class ATMservice
    {

        private User currentUser;
        private List<User> users;

        //constructor
        public ATMservice(User user, List<User> users)
        {

            this.currentUser = user;
            this.users = users;
        }


        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine($"Welcome, {currentUser.FirstName} {currentUser.LastName}");
                Console.WriteLine("Please choose an action:");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Transaction History");
                Console.WriteLine("5. Change PIN");
                Console.WriteLine("6. Exchange Currencies");
                Console.WriteLine("0. Exit");

                string menuItem = Console.ReadLine();

                if (menuItem == "1")
                {
                    CheckBalance();
                }
                else if (menuItem == "2")
                {
                    Console.WriteLine("Enter amount to withdraw:");
                    int amount = int.Parse(Console.ReadLine());
                    WithdrawAmount(amount);
                }
                else if (menuItem == "3")
                {
                    Console.WriteLine("Enter amount to deposit:");
                    int amount = int.Parse(Console.ReadLine());
                    //DepositAmount(amount);
                }
                else if (menuItem == "4")
                {
                    //ShowTransactionHistory();
                }
                else if (menuItem == "5")
                {
                    //ChangePin();
                }
                else if (menuItem == "6")
                {
                    //CurrencyExchange();
                }
                else if (menuItem == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please select a number corresponding to the menu item or 0 to exit. ");
                }
            }
        }

        public void CheckBalance()
        {
            Console.WriteLine("Current Balance:");
            Console.WriteLine($"{currentUser.BalanceGEL} GEL");
            Console.WriteLine($"{currentUser.BalanceUSD} USD");
            Console.WriteLine($"{currentUser.BalanceEUR} EUR");

            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow.ToString("o"),
                TransactionType = "Balance Inquiry",
                AmountGEL = 0,
                AmountUSD = 0,
                AmountEUR = 0
            };

            LogTransaction(currentUser, transaction, users);

        }

        public void ChangePin()
        {
            Console.WriteLine("Enter new PIN:");
            string newPin = Console.ReadLine();
            this.currentUser.Card.PinCode = newPin;
            Console.WriteLine("PIN updated successfully!");

            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow.ToString("o"),
                TransactionType = "PIN Change",
                AmountGEL = 0,
                AmountUSD = 0,
                AmountEUR = 0
            };

            LogTransaction(currentUser, transaction, users);
        }

        public void WithdrawAmount(int amount)

        //needs to be changed to make new balance and log transaction and save it to JSON.
        {
            Console.WriteLine("Choose currency:");
            Console.WriteLine("1. GEL");
            Console.WriteLine("2. USD");
            Console.WriteLine("3. EUR");

            var currency = Console.ReadLine();
            decimal newBalance = 0;

            if (currency == "1" && amount <= currentUser.BalanceGEL)
            {
                currentUser.BalanceGEL = Convert.ToDecimal(currentUser.BalanceGEL) - amount;
                newBalance = currentUser.BalanceGEL;
                Console.WriteLine($"Balance: {newBalance} GEL");
            }
            else if (currency == "2" && amount <= currentUser.BalanceUSD)
            {
                currentUser.BalanceUSD = Convert.ToDecimal(currentUser.BalanceUSD) - amount;
                newBalance = currentUser.BalanceUSD;
                Console.WriteLine($"Balance: {newBalance} USD");
            }
            else if (currency == "3" && amount <= currentUser.BalanceEUR)
            {
                currentUser.BalanceEUR = Convert.ToDecimal(currentUser.BalanceEUR) - amount;
                newBalance = currentUser.BalanceEUR;
                Console.WriteLine($"Balance: {newBalance} EUR");
            }
            else
            {
               
                Console.WriteLine("Please enter valid value.");
                return;
            }

            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow.ToString("o"),
                TransactionType = "Withdraw",
                AmountGEL = currency == "1" ? amount : 0,
                AmountUSD = currency == "2" ? amount : 0,
                AmountEUR = currency == "3" ? amount : 0
            };

            LogTransaction(currentUser, transaction, users);


        }

        public void DepositAmount (int amount)
        {
            Console.WriteLine("Choose currency:");
            Console.WriteLine("1. GEL");
            Console.WriteLine("2. USD");
            Console.WriteLine("3. EUR");

            string currency = Console.ReadLine();

            if (currency == "1")
            {
                currentUser.BalanceGEL += amount;
                Console.WriteLine($"New balance: {currentUser.BalanceGEL} GEL");
            }
            else if (currency == "2")
            {
                currentUser.BalanceUSD += amount;
                Console.WriteLine($"New balance: {currentUser.BalanceUSD} USD");
            }
            else if (currency == "3")
            {
                currentUser.BalanceEUR += amount;
                Console.WriteLine($"New balance: {currentUser.BalanceEUR} EUR");
            }
            else
            {
                Console.WriteLine("Please enter a valid value.");
                return;
            }

            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow.ToString("o"),
                TransactionType = "Deposit",
                AmountGEL = currency == "1" ? amount : 0,
                AmountUSD = currency == "2" ? amount : 0,
                AmountEUR = currency == "3" ? amount : 0
            };

            LogTransaction(currentUser, transaction, users);
        }


        public void ShowTransactionHistory()
        {
            Console.WriteLine("Last 5 transactions:");

            for (int i = 0; i < 5; i++)
            {

                Console.WriteLine($"{i + 1}. {currentUser.TransactionHistory[i]}");
                Console.WriteLine("----------");


            }
        }


            private void LogTransaction(User user, Transaction transaction, List<User> users)
        {
            user.TransactionHistory.Add(transaction);
            SaveChanges(users);
        }

        private void SaveChanges(List<User> users)
        {
            string filePath = "C:\\Users\\User\\Desktop\\comm-atm-assignment\\atm-app\\atm-app\\Files\\users.json";

            string updatedJson = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
        }
    


    }
}
