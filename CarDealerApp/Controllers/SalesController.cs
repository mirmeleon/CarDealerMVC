using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using CarDealer.Models.BindingModels.Sales;
using CarDealer.Models.ViewModels.Sales;
using CarDealer.Services;
using CarDealerApp.Models;
using AuthenticationManager = CarDealerApp.Security.AuthenticationManager;

namespace CarDealerApp.Controllers
{
    [RoutePrefix("sales")]
    public class SalesController : Controller
    {
        
        private SalesService service;

        public SalesController()
        {
            this.service = new SalesService();
        }

       
        [HttpGet]
        [Route("{id}")]
        public ActionResult SalesById(int id)
        {
            SalesViewModel sales = this.service.GetSalesById(id);
            
            return this.View(sales);
        }

       
        [HttpGet]
        [Route("discounted")]
        public ActionResult DiscountedSales(string discounted)
        {
            IEnumerable<SalesViewModel> sales = this.service.GetDiscountedSales(discounted);

            return this.View(sales);
        }

       
        [HttpGet]
        [Route("all")]
        public ActionResult All()
        {
            IEnumerable<SalesViewModel> sales = this.service.GetAllSales();

            return this.View(sales);
        }

        [HttpGet]
        [Route("AddSales")]
        public ActionResult AddSales()
        {
            HttpCookie cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }


            AddSalesViewModel addSalesVm = service.GenerateAddSalesForm();
            return this.View(addSalesVm);
        }

        [HttpPost]
        [Route("AddSales")] 
        public ActionResult AddSales([Bind(Include = "CustomerId, CarId, Discount")] AddSaleBm addSaleBm)
        {
            if (this.ModelState.IsValid)
            {
                AddSaleConfirmationViewModel salesConfirmVm = this.service.GetConfirmatinModel(addSaleBm);
                return this.RedirectToAction("AddConfirmation", salesConfirmVm);
            }
            AddSalesViewModel addSalesVm = service.GenerateAddSalesForm();
            return this.View(addSalesVm);
        }

        [HttpGet]
        [Route("AddConfirmation")]
        public ActionResult AddConfirmation(AddSaleConfirmationViewModel salesConfirmVm)
        {
            var cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            return this.View(salesConfirmVm);
        }

        [HttpPost]
        [Route("AddConfirmation")]
        public ActionResult AddConfirmation(AddSaleBm addSaleBm)
        {
            var cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }
            this.service.AddSale(addSaleBm);
            return this.RedirectToAction("All", "Sales");
        }
    }
}
