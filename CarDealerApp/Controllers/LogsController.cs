using System.Web.Mvc;
using CarDealer.Models.ViewModels.Logs;
using CarDealer.Services;
using CarDealerApp.Security;

namespace CarDealerApp.Controllers
{
    [RoutePrefix("logs")]
    public class LogsController : Controller
    {
        private LogsService service;

        public LogsController()
        {
            this.service = new LogsService();
        }
     
        [HttpGet]
        [Route("all/{username?}")]
        public ActionResult All(string username, int? page)
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("ViewSuppliers", "Supplier");
            }

            AllLogsPageViewModel pageViewModel = this.service.GetAllLogsPage(username, page);

            return View(pageViewModel);
        }

        [HttpGet]
        [Route("deleteAll")]
        public ActionResult DeleteAll()
        {
            return this.View();
        }

        [HttpPost]
        [Route("deleteAll")]
        public ActionResult DeleteAlll()
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("ViewSuppliers", "Supplier");
            }

            this.service.DeleteAllLogs();
            return this.RedirectToAction("ViewSuppliers", "Supplier");
        }

    }
}