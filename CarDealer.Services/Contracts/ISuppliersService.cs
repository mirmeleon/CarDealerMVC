using System.Collections.Generic;
using CarDealer.Models.BindingModels.Suppliers;
using CarDealer.Models.DbModels;
using CarDealer.Models.ViewModels.Suppliers;
using CarDealerApp.Models;

namespace CarDealer.Services
{
    public interface ISuppliersService
    {
        IEnumerable<SupplierViewModel> GetSuppliers(string local);
        IEnumerable<EditSupplierViewModel> GenerateViewSuppliers();
        EditSupplierViewModel GetSupplierToEdit(int id);
        void EditSupplier(EditSupplierBm editSupplierBm, int userId);
        void AddLog(int userId, OperationLog edit, string suppliers);
        DeleteSupplierViewModel GetSupplierToDelete(int id);
        void DeleteSupplier(DeleteSupplierBm deleteSupplierBm, int userId);
        void AddSupplier(AddSupplierBm addSupplierBm, int userId);
    }
}