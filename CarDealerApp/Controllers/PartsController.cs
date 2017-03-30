using System.Collections.Generic;
using System.Web.Mvc;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;
using CarDealer.Services;

namespace CarDealerApp.Controllers
{
    [RoutePrefix("parts")]
    public class PartsController : Controller
    {
        private PartsService service;

        public PartsController()
        {
            this.service = new PartsService();
        }


        [HttpGet]
        [Route("allParts")]
        public ActionResult AllParts()
        {
          IEnumerable<AllPartsViewModel>  parts = this.service.GetAllParts();
            return this.View(parts);
        }

        [HttpGet]
        [Route("addparts")]
        public ActionResult AddParts()
        {
            IEnumerable<AddPartSupplierViewModel> parts = this.service.CreateViewModels();
            return this.View(parts);
        }

        [HttpPost]
        [Route("addparts")]
        public ActionResult AddPart([Bind(Include = "Name, Price, Quantity, supplierId")] AddPartBm addPartBm)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddPart(addPartBm);
               return this.RedirectToAction("allParts", "Parts");
            }
            return this.View();
        }

        [HttpGet]
        [Route("deleteParts/{id}")]
        public ActionResult DeletePart(int id)
        {
            DeletePartViewModel delPartVm = this.service.GetPartToDelete(id);
            return this.View(delPartVm);
        }

        [HttpPost]
        [Route("deleteParts/{id}")]
        public ActionResult DeletePart([Bind(Include = "Id")] DeletePartBm delPartBm)
        {
            if (this.ModelState.IsValid)
            {
               this.service.DeletePart(delPartBm);
                return this.RedirectToAction("AllParts");
            }

            DeletePartViewModel delPartVm = this.service.GetPartToDelete(delPartBm.Id);
            
            return this.View(delPartVm);
        }


        [HttpGet]
        [Route("editPart/{id}")]
        public ActionResult EditPart(int id)
        {
           
            EditPartViewModel editPartVm = this.service.GetPartToEdit(id);
            return this.View(editPartVm);
        }

        [HttpPost]
        [Route("editPart/{id}")]
        public ActionResult EditPart([Bind(Include = "Id, Price, Quantity")] EditPartBm editPartBm)
        {
            if (this.ModelState.IsValid)
            {
                this.service.EditPart(editPartBm);
                return this.RedirectToAction("AllParts");
            }

            EditPartViewModel editPartVm = this.service.GetPartToEdit(editPartBm.Id);
            return this.View(editPartVm);
        }
    }
}
