using atm_app.Entities;
using Serilog;
using System.Net.Http.Headers;



List<User> users = User.LoadUsersFromJson("C:\\Users\\User\\Desktop\\comm-atm-assignment\\atm-app\\atm-app\\Files\\users.json");


var atmService = new ATMservice(users);
atmService.Startup();
atmService.ShowMenu();




