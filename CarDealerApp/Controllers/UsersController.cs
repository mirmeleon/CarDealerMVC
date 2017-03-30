using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealer.Services;
using CarDealerApp.Security;


namespace CarDealerApp.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : Controller
    {
        private UsersService service;


        public UsersController()
        {
            this.service = new UsersService();
        }

        [HttpGet]
        [Route("register")]
        public ActionResult Register()
        {
            HttpCookie cookie = Request.Cookies.Get("sessionId");
            if (cookie != null && AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }
            return this.View();
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register(
            [Bind(Include = "Email, Username, Password, ConfirmPassword")] RegisterUserBm regUserBm)
        {
            HttpCookie cookie = Request.Cookies.Get("sessionId");
            if (cookie != null && AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            if (this.ModelState.IsValid && regUserBm.Password == regUserBm.ConfirmPassword)
            {
                this.service.RegisterUser(regUserBm);
                return this.RedirectToAction("Login");
            }
            return this.RedirectToAction("Register");
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            HttpCookie httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie != null && AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }
            return this.View();
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login([Bind(Include = "Username, Password")] LoginUserBm loginUserBm)
        {
            HttpCookie cookie = this.Request.Cookies.Get("sessionId");
            if (cookie != null && AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("All", "Cars");
            }

            if (this.ModelState.IsValid && this.service.UserExist(loginUserBm))
            {
                this.service.LoginUser(loginUserBm, Session.SessionID);

                this.Response.SetCookie(new HttpCookie("sessionId", Session.SessionID));
                return this.RedirectToAction("All", "Cars");
            }
            return this.RedirectToAction("Login", "Users");
        }

        [HttpPost]
        [Route("logout")]
        public ActionResult Logout()
        {
            HttpCookie cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            AuthenticationManager.Logout(Request.Cookies.Get("sessionId").Value);

            return this.RedirectToAction("All", "Cars");
        }

    }
}