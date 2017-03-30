using System.Linq;
using CarDealer.Data;
using CarDealer.Models.DbModels;

namespace CarDealerApp.Security
{
    public class AuthenticationManager
    {
        private static CarDealerContext context = new CarDealerContext();

        public static bool IsAuthenticated(string sessionId)
        {
            if (context.Logins.Any(login => login.SessionId == sessionId && login.IsActive))
            {
                return true;
            }
            return false;
        }

        public static User GetAuthenticatedUser(string sessionId)
        {
            Login logged = context.Logins.FirstOrDefault(login => login.SessionId == sessionId && login.IsActive);

            if (logged != null)
            {
                User authenticated = logged.User;
                return authenticated;
            }
            return null;
        }

        public static void Logout(string sessionId)
        {
            Login login = context.Logins.FirstOrDefault(l => l.SessionId == sessionId && l.IsActive);
            login.IsActive = false;
            context.SaveChanges();
        }
    }
}