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
                
               foreach (User user in users)
                {
                    Console.WriteLine(user.FirstName);
                }
            }
            return new List<User>(); 
        }
    }
}
