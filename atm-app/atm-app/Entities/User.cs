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

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required Card Card { get; set; }
        public decimal BalanceGEL { get; set; }
        public decimal BalanceUSD { get; set; }
        public decimal BalanceEUR { get; set; }
        public required List<Transaction> TransactionHistory { get; set; }



        public static List<User> LoadUsersFromJson(string filePath)
        {
            
            if (File.Exists(filePath))
            {

                try
                {
                    
                    string jsonString = File.ReadAllText(filePath);

                    
                    List<User> users = JsonSerializer.Deserialize<List<User>>(jsonString) ?? new List<User>();

                    
                    if (users.Count == 0)
                    {
                        Console.WriteLine("No users found in the list.");
                    }

                    return users;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading users: {ex.Message}");
                    return new List<User>();  
                }


            }
            else
            {
                Console.WriteLine("File not found.");
            }

            return new List<User>(); 
        }

        public static User? ValidateCardInfo (List<User> users, string cardNumber, string expDate, string cvcCode) 
        {

            User user = users.FirstOrDefault( user =>
                user.Card.CardNumber == cardNumber && user.Card.CvcCode == cvcCode && user.Card.ExpDate == expDate)!;
            

            return user;

        }

        public bool ValidatePin (string pin)
        {
            return this.Card.PinCode == pin;
        }

       
    }
}
