using System.Collections.Generic;
using System.Web.Mvc;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;
using CarDealer.Services;
using CarDealerApp.Models;

namespace CarDealerApp.Controllers
{
    [RoutePrefix("customers")]
    public class CustomersController : Controller
    {
        private ICustomersService service;

        public CustomersController(ICustomersService service)
        {
            this.service = service;
        }
  
        [HttpGet]
        [Route("all/{order:regex(ascending|descending)?}")]
        public ActionResult All(string order)
        {
           
           IEnumerable<CustomerViewModel> customers = service.GetAll(order);
          
            return this.View(customers);
        }

      
        [HttpGet]
        [Route("findById/{id:int}")]
        public ActionResult FindById(int? id)
        {
            
            CustomerByIdViewModel customer = this.service.FindById(id);

            return this.View(customer);

        }

        [HttpGet]
        [Route("addCustomer")]
        public ActionResult AddCustomer()
        {
          return this.View();
        }

        [HttpPost]
        [Route("addCustomer")]
        public ActionResult AddCustomer([Bind(Include = "Name, BirthDate")]AddCustomerBm customerBm)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddCustomer(customerBm);
                return this.RedirectToAction("All", new {order = "ascending" });

            }

            return this.View();
        }

        
        [HttpGet]
        [Route("editCustomer/{id:int}")]
        public ActionResult EditCustomer(int id)
        {
            EditCustomerViewModel editCustVm = service.GetCustomerForEdit(id);
            return this.View(editCustVm);
        }

        [HttpPost]
        [Route("editCustomer/{id:int}")]
        public ActionResult EditCustomer([Bind(Include = "Id, Name, BirthDate")]EditCustomerBm editCustomerBm)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditCustomer(editCustomerBm);
                return this.RedirectToAction("All", new { order = "ascending" });
            }
            return this.View();
        }

    }
}
