using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace atm_app.Entities
{
    internal class User
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Card Card { get; set; }
        public decimal BalanceGEL { get; set; }
        public decimal BalanceUSD { get; set; }
        public decimal BalanceEUR { get; set; }
        public List<Transaction> TransactionHistory { get; set; }



        public static List<User> LoadUsersFromJson(string filePath)
        {
            
            if (File.Exists(filePath))
            {
                
                string jsonString = File.ReadAllText(filePath);
                
                List<User> users = JsonSerializer.Deserialize<List<User>>(jsonString);

 
                return users;
            }
            return new List<User>(); 
        }

        public static User ValidateCardInfo (List<User> users, string cardNumber, string expDate, string cvcCode) 
        {

            User user = users.FirstOrDefault( user =>
                user.Card.CardNumber == cardNumber && user.Card.CvcCode == cvcCode && user.Card.ExpDate == expDate)!;

            
                if (user == null)
            {
                Console.WriteLine("Invalid credentials. No user found.");
            } else
            {
                Console.WriteLine("Please enter PIN.");
                var pin = Console.ReadLine();

                user.ValidatePin(pin);
            }

            return user;

        }

        public bool ValidatePin (string pin)
        {
            return this.Card.PinCode == pin;
        }

       
    }
}
