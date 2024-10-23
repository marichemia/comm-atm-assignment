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
        public ATMservice(List<User> users)
        {
            this.users = users;
        }

        public void Startup()
        {
            // Load users from JSON
            users = User.LoadUsersFromJson("C:\\Users\\User\\Desktop\\comm-atm-assignment\\atm-app\\atm-app\\Files\\users.json");

            if (users == null || users.Count == 0)
            {
                Console.WriteLine("No users found.");
                return;
            }

            //card info
            currentUser = null;
            while (currentUser == null)
            {
                Console.WriteLine("Enter card number:");
                var cardNum = Console.ReadLine();

                Console.WriteLine("Enter exp. date:");
                var expDate = Console.ReadLine();

                Console.WriteLine("Enter CVC code:");
                var cvcCode = Console.ReadLine();

                currentUser = User.ValidateCardInfo(users, cardNum, expDate, cvcCode);

                if (currentUser == null)
                {
                    Console.WriteLine("Invalid card details. Please try again.");
                }
            }

            // PIN
            bool isPinValid = false;
            while (!isPinValid)
            {
                Console.WriteLine("Please enter your PIN:");
                var pin = Console.ReadLine();

                isPinValid = currentUser.ValidatePin(pin);

                if (!isPinValid)
                {
                    Console.WriteLine("Invalid PIN. Please try again.");
                }
            }

            Console.WriteLine($"Welcome {currentUser.FirstName} {currentUser.LastName}!");
        }


        public void ShowMenu()
        {
            while (true)
            {
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
                    string input = Console.ReadLine();
                    int amount;

                    if (int.TryParse(input, out amount))
                    {
               
                        WithdrawAmount(amount);
                    }
                    else
                    {
                        
                        Console.WriteLine("Invalid input. Please enter a valid integer amount.");
                    }
                }
                else if (menuItem == "3")
                {
                    Console.WriteLine("Enter amount to deposit:");
                    string input = Console.ReadLine();
                    int amount;

                    if (int.TryParse(input, out amount))
                    {

                        DepositAmount(amount);
                    }
                    else
                    {

                        Console.WriteLine("Invalid input. Please enter a valid integer amount.");
                    }
                }
                else if (menuItem == "4")
                {
                    ShowTransactionHistory();
                }
                else if (menuItem == "5")
                {
                    ChangePin();
                }
                else if (menuItem == "6")
                {
                    Console.WriteLine("Which currency do you want to Exchange? GEL/USD/EUR");
                    var fromCurrency = Console.ReadLine();
                    Console.WriteLine("Which currency are you exchanging into? GEL/USD/EUR");
                    var toCurrency = Console.ReadLine();
                    Console.WriteLine("Provide an amount to be exchanged:");
                    var input = Console.ReadLine();
                    decimal amount;


                    if (decimal.TryParse(input, out amount))
                    {

                        CurrencyExchange(fromCurrency, toCurrency, amount);
                    }
                    else
                    {

                        Console.WriteLine("Invalid input. Please enter a valid decimal amount.");
                    }

                    
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

        public void CurrencyExchange(string fromCurrency, string toCurrency, decimal amount) {

            decimal exchangeRate = 0;

            fromCurrency = fromCurrency.ToUpper();
            toCurrency = toCurrency.ToUpper();

            if (amount <= 0) Console.WriteLine("invalid amount.");

            if (fromCurrency == "GEL" && toCurrency == "USD")
            {

                exchangeRate = 0.37m;

                currentUser.BalanceGEL -= amount;
                currentUser.BalanceUSD += amount * exchangeRate;

            } else if (fromCurrency == "GEL" && toCurrency == "EUR")
            {
                exchangeRate = 0.34m;

                currentUser.BalanceGEL -= amount;
                currentUser.BalanceEUR += amount * exchangeRate;
            } else if (fromCurrency == "USD" && toCurrency == "GEL")
            {
                exchangeRate = 2.74m;

                currentUser.BalanceUSD -= amount;
                currentUser.BalanceGEL += amount * exchangeRate;
            } else if (fromCurrency == "USD" && toCurrency == "EUR")
            {
                exchangeRate = 0.93m;

                currentUser.BalanceUSD -= amount;
                currentUser.BalanceEUR += amount * exchangeRate;
            } else if (fromCurrency == "EUR" && toCurrency == "GEL")
            {
                exchangeRate = 2.95m;

                currentUser.BalanceEUR -= amount;
                currentUser.BalanceGEL += amount * exchangeRate;
            } else if (fromCurrency == "EUR" && toCurrency == "USD")
            {
                exchangeRate = 1.08m;

                currentUser.BalanceEUR -= amount;
                currentUser.BalanceUSD += amount * exchangeRate;
            }
                
            else
            {
                Console.WriteLine("invalid currencies.");
            }

            Console.WriteLine($"{amount} {fromCurrency} has been exchanged into {toCurrency}");

            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow.ToString("o"),
                TransactionType = "Currency Exchange",
                AmountGEL =  currentUser.BalanceGEL,
                AmountUSD =  currentUser.BalanceUSD,
                AmountEUR =  currentUser.BalanceEUR,
            };


            LogTransaction(currentUser, transaction, users);


        }


        public void ShowTransactionHistory()
        {
            Console.WriteLine("Last 5 transactions:");

            int historySize = currentUser.TransactionHistory.Count > 5 ? 5 : currentUser.TransactionHistory.Count;



            for (int i = 0; i < historySize; i++)
            {

                var transaction = currentUser.TransactionHistory[currentUser.TransactionHistory.Count - 1 - i];
                Console.WriteLine($"{i + 1}. Date: {transaction.TransactionDate}, Type: {transaction.TransactionType}, " +
                                  $"GEL: {transaction.AmountGEL}, USD: {transaction.AmountUSD}, EUR: {transaction.AmountEUR}");
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

            File.WriteAllText(filePath, updatedJson);
        }
    


    }
}
