using System.Collections.Generic;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;

namespace CarDealer.Services
{
    public interface IPartsService
    {
        IEnumerable<AddPartSupplierViewModel> CreateViewModels();
        void AddPart(AddPartBm addPartBm);
        IEnumerable<AllPartsViewModel> GetAllParts();
        DeletePartViewModel GetPartToDelete(int id);
        void DeletePart(DeletePartBm delPartBm);
        EditPartViewModel GetPartToEdit(int id);
        void EditPart(EditPartBm editPartBm);
    }
}