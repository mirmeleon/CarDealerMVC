using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;

namespace CarDealer.Services
{
   public class PartsService : Service, IPartsService
   {
        public IEnumerable<AddPartSupplierViewModel> CreateViewModels()
        {
            
            var supps = this.Context.Suppliers;

            IEnumerable<AddPartSupplierViewModel> partsAndSups =
                Mapper.Map<IEnumerable<Supplier>, IEnumerable<AddPartSupplierViewModel>>(supps);

            return partsAndSups;
        }

        public void AddPart(AddPartBm addPartBm)
        {
            
           Part part = Mapper.Map<AddPartBm, Part>(addPartBm);
            Supplier supplier = Context.Suppliers.FirstOrDefault(sup => sup.Id == addPartBm.SupplierId);
            part.Supplier = supplier;
            if (part.Quantity == 0)
            {
                part.Quantity = 1;
            }
            this.Context.Parts.Add(part);
            this.Context.SaveChanges();
        }

        public IEnumerable<AllPartsViewModel> GetAllParts()
        {
            IEnumerable<Part> parts = this.Context.Parts;
            IEnumerable<AllPartsViewModel> allPartsMapped = Mapper.Map<IEnumerable<Part>, IEnumerable<AllPartsViewModel>>(parts);
            return allPartsMapped;
        }

        public DeletePartViewModel GetPartToDelete(int id)
        {
            Part partToDel = this.Context.Parts.Find(id);
            DeletePartViewModel delPart = Mapper.Map<Part, DeletePartViewModel>(partToDel);

            return delPart;
        }

        public void DeletePart(DeletePartBm delPartBm)
        {
          
           Part partToDel = this.Context.Parts.Find(delPartBm.Id);

            Context.Parts.Remove(partToDel);
            Context.SaveChanges();
            
        }

        public EditPartViewModel GetPartToEdit(int id)
        {
            Part part = this.Context.Parts.Find(id);
            EditPartViewModel editPartVm = Mapper.Map<Part, EditPartViewModel>(part);

            return editPartVm;
        }

        public void EditPart(EditPartBm editPartBm)
        {
            Part part = this.Context.Parts.Find(editPartBm.Id);
       
            part.Price = editPartBm.Price;
            part.Quantity = editPartBm.Quantity;
        
            this.Context.SaveChanges();
        }
    }
}
