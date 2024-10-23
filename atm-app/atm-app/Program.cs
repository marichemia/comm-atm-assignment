using atm_app.Entities;
using Serilog;
using System.Net.Http.Headers;



List<User> users = User.LoadUsersFromJson("C:\\Users\\User\\Desktop\\comm-atm-assignment\\atm-app\\atm-app\\Files\\users.json");


foreach (User user in users) {
    Console.WriteLine(user.FirstName);
}

Console.WriteLine("enter card number:");
var cardNum = Console.ReadLine();
Console.WriteLine("enter exp. date:");
var expDate = Console.ReadLine();
Console.WriteLine("enter CVC code");
var cvcCode  = Console.ReadLine();

User user1 = User.ValidateCardInfo(users, cardNum, expDate, cvcCode);

Console.WriteLine(user1.FirstName);

var lala = new ATMservice(user1);

lala.ShowMenu();