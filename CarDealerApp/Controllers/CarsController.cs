using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using CarDealer.Models.BindingModels;
using CarDealer.Services;
using CarDealerApp.Models;
using CarDealerApp.Security;

namespace CarDealerApp.Controllers
{
    [RoutePrefix("cars")]
    public class CarsController : Controller
    { 
         private ICarsService service;

        public CarsController(ICarsService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("{id}/parts")]
        public ActionResult About(int id)
        {
            CarsWithPartsViewModel cars = this.service.GetParts(id);
          

             return this.View(cars);
        }

        [HttpGet]
        [Route("{make?}")]
        public ActionResult All(string make)
        {

            IEnumerable<CarViewModel> carsByMake = this.service.GetCarsByMake(make);
         
            return this.View(carsByMake);
        }

    

        [HttpGet]
        [Route("addCar")]
        public ActionResult AddCar()
        {
            HttpCookie cookie = this.Request.Cookies.Get("sessionId");
            if (cookie != null && AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return this.View();
            }
            return RedirectToAction("Login", "Users");
        }

        [HttpPost]
        [Route("addCar")]
        public ActionResult AddCar(AddCarBm addCarBm)
        {
            HttpCookie cookie = this.Request.Cookies.Get("sessionId");

            if (cookie == null || !AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return RedirectToAction("Login", "Users");
            }

            if (this.ModelState.IsValid)
            {
                this.service.AddCar(addCarBm);
            
                return this.RedirectToAction("All");
            }

            return this.View();
        }


    }
}
