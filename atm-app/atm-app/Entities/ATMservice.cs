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
    }
}
