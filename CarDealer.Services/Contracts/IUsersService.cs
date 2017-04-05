using CarDealer.Models.BindingModels;

namespace CarDealer.Services
{
    public interface IUsersService
    {
        void RegisterUser(RegisterUserBm regUserBm);
        void LoginUser(LoginUserBm loginUserBm, string sessionId);
        bool UserExist(LoginUserBm loginUserBm);
    }
}