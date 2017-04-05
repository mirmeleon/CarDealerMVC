using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using CarDealer.Models.BindingModels.Suppliers;
using CarDealer.Models.DbModels;
using CarDealer.Models.ViewModels.Suppliers;
using CarDealer.Services;
using CarDealerApp.Models;
using CarDealerApp.Security;

namespace CarDealerApp.Controllers
{
   [RoutePrefix("supplier")]
    public class SupplierController : Controller
    {
        private ISuppliersService service;
      

        public SupplierController(ISuppliersService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("{local}")]
        public ActionResult Local(string local)
        {
            IEnumerable<SupplierViewModel> supplier = this.service.GetSuppliers(local);
          
            return View(supplier);
        }

        [HttpGet]
        [Route("viewSuppliers")]
        public ActionResult ViewSuppliers()
        {
            HttpCookie cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            IEnumerable<EditSupplierViewModel> supplierView = this.service.GenerateViewSuppliers();
            return View(supplierView);
        }

        [HttpGet]
        [Route("editSupplier/{id:int}")]
        public ActionResult EditSupplier(int id)
        {
            HttpCookie cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            EditSupplierViewModel supplier = this.service.GetSupplierToEdit(id);
            return View(supplier);
        }

        [HttpPost]
        [Route("editSupplier/{id:int}")]
        public ActionResult EditSupplier([Bind(Include = "Id, Name, IsImporter")]EditSupplierBm editSupplierBm)
        {
            HttpCookie cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            if (!this.ModelState.IsValid)
            {
                EditSupplierViewModel supplier = this.service.GetSupplierToEdit(editSupplierBm.Id);
                return View(supplier);
            }

            User user = AuthenticationManager.GetAuthenticatedUser(cookie.Value);

            this.service.EditSupplier(editSupplierBm, user.Id);
            return this.RedirectToAction("ViewSuppliers", "Supplier");
         
        }

        [HttpGet]
        [Route("deleteSupplier/{id:int}")]
        public ActionResult DeleteSupplier(int id)
        {
            HttpCookie cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }
            DeleteSupplierViewModel supplierBind = this.service.GetSupplierToDelete(id);
            return this.View(supplierBind);
        }
      
        [HttpPost]
        [Route("deleteSupplier/{id:int}")]
        public ActionResult DeleteSupplier([Bind(Include = "Id")]DeleteSupplierBm deleteSupplierBm)
        {
            HttpCookie cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            if (!this.ModelState.IsValid)
            {
                DeleteSupplierViewModel supplierBind = this.service.GetSupplierToDelete(deleteSupplierBm.Id);
                return this.View(supplierBind);
            }
            User user = AuthenticationManager.GetAuthenticatedUser(cookie.Value);

            service.DeleteSupplier(deleteSupplierBm, user.Id);
            return this.RedirectToAction("viewSuppliers", "Supplier");
        }
        
        [HttpGet]
        [Route("addSupplier")]
        public ActionResult AddSupplier()
        {
            HttpCookie cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }
            
            return this.View();
        }

        [HttpPost]
        [Route("addSupplier")]
        public ActionResult AddSupplier([Bind(Include = "Name, IsImporter")]AddSupplierBm addSupplierBm)
        {
            HttpCookie cookie = this.Request.Cookies.Get("sessionId");
            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            User user = AuthenticationManager.GetAuthenticatedUser(cookie.Value);

            this.service.AddSupplier(addSupplierBm, user.Id);
            return this.RedirectToAction("ViewSuppliers", "Supplier");
        }
    }
}