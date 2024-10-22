using atm_app.Entities;
using Serilog;



List<User> users = User.LoadUsersFromJson("C:\\Users\\User\\Desktop\\comm-atm-assignment\\atm-app\\atm-app\\Files\\users.json");


foreach (User user in users) {
    Console.WriteLine(user.FirstName);
}