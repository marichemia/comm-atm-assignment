using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace atm_app.Entities
{
    internal class ATMservice
    {

        private User currentUser;

        public ATMservice(User user)
        {

            this.currentUser = user;

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
                    //CheckBalance();
                }
                else if (menuItem == "2")
                {
                    //WithdrawAmount();
                }
                else if (menuItem == "3")
                {
                    //DepositAmount();
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
                    Console.WriteLine("Invalid input. Please select a number corresponding to the menu item or 0 ");
                }
            }
        }

        public void CheckBalance()
        {
            Console.WriteLine("Current Balance:");
            Console.WriteLine($"{currentUser.BalanceGEL} GEL");
            Console.WriteLine($"{currentUser.BalanceUSD} USD");
            Console.WriteLine($"{currentUser.BalanceEUR} EUR");

        }

        public void ChangePin()
        {
            Console.WriteLine("Enter new PIN:");
            string newPin = Console.ReadLine();
            this.currentUser.Card.PinCode = newPin;
            Console.WriteLine("PIN updated successfully!");
        }

        public void WithdrawAmount(int amount)

            //needs to be changed to make new balance and log transaction and save it to JSON.
        {
            Console.WriteLine("Choose currency:");
            Console.WriteLine("1. GEL");
            Console.WriteLine("2. USD");
            Console.WriteLine("3. EUR");

            var currency = Console.ReadLine();
            int newBalance;

            if (currency == "1")
            {
                newBalance = Convert.ToInt32(this.currentUser.BalanceGEL) - amount;
            }
            else if (currency == "2")
            {
                newBalance = Convert.ToInt32(this.currentUser.BalanceUSD) - amount;
            }
            else if (currency == "3")
            {
                newBalance = Convert.ToInt32(this.currentUser.BalanceEUR) - amount;
            } else
            {
                // need to fix this
                Console.WriteLine("Please enter valid value.");
                newBalance = 0;
            }

            Console.WriteLine($"Balance: {newBalance}");
        }

        //add amount method needs to be implemented

        //show transaction history method needs to be implemented

        private void CreateTransaction()
        {
            //creates/logs transaction for any method
        }

        private void SaveChanges()
        {
            //needs to be implemented
        }


    }
}
