using System.Linq;
using AutoMapper;
using CarDealer.Models.BindingModels;
using CarDealer.Models.DbModels;

namespace CarDealer.Services
{
  public class UsersService : Service, IUsersService
  {
        public void RegisterUser(RegisterUserBm regUserBm)
        {
            var mapped = Mapper.Map<RegisterUserBm, User>(regUserBm);

            this.Context.Users.Add(mapped);
            this.Context.SaveChanges();

        }

        public void LoginUser(LoginUserBm loginUserBm, string sessionId)
        {
            if (!this.Context.Logins.Any(l => l.SessionId == sessionId))
            {
                Login login = new Login()
                {
                    SessionId = sessionId,

                };
                this.Context.Logins.Add(login);
               
                this.Context.SaveChanges();
            }
            Login logedUser = this.Context.Logins.FirstOrDefault(l => l.SessionId == sessionId);
            logedUser.IsActive = true;
            User user =
                this.Context.Users.FirstOrDefault(
                    u => u.Username == loginUserBm.Username && u.Password == loginUserBm.Password);
            logedUser.User = user;
            this.Context.SaveChanges();
        }

        public bool UserExist(LoginUserBm loginUserBm)
        {
            if (this.Context.Users.Any(u => u.Username == loginUserBm.Username  && u.Password == loginUserBm.Password))
            {
                return true;
            }

            return false;
        }
    }
}
