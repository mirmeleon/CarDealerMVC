using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDealer.Models;
using CarDealer.Models.BindingModels.Suppliers;
using CarDealer.Models.DbModels;
using CarDealer.Models.ViewModels.Suppliers;
using CarDealerApp.Models;

namespace CarDealer.Services
{
   public class SuppliersService : Service
    {
        public IEnumerable<SupplierViewModel> GetSuppliers(string local)
        {
            IEnumerable<Supplier> suppliers;
            if (local.ToLower() != "local")
            {
               
                    suppliers = this.Context.Suppliers.Where(s => s.IsImporter == true);
               
            }
            else
            {
                suppliers = this.Context.Suppliers.Where(s => s.IsImporter == false);
            }
           

            IEnumerable<SupplierViewModel> mapped = Mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierViewModel>>(suppliers);

            return mapped;
        }

        public IEnumerable<EditSupplierViewModel> GenerateViewSuppliers()
        {
            IEnumerable<Supplier> suppliers = this.Context.Suppliers;
            IEnumerable<EditSupplierViewModel> mappedSuppliers = Mapper.Map<IEnumerable<Supplier>,IEnumerable<EditSupplierViewModel>>(suppliers);
            return mappedSuppliers;
        }

        public EditSupplierViewModel GetSupplierToEdit(int id)
        {
            Supplier supplier = this.Context.Suppliers.Find(id);
            EditSupplierViewModel mappedSupplier = Mapper.Map<Supplier, EditSupplierViewModel>(supplier);

            return mappedSupplier;
        }

        public void EditSupplier(EditSupplierBm editSupplierBm, int userId)
        {
            Supplier supplier = this.Context.Suppliers.Find(editSupplierBm.Id);

            supplier.IsImporter = editSupplierBm.IsImporter == "on";
            supplier.Name = editSupplierBm.Name;
       
            this.Context.SaveChanges();
            this.AddLog(userId, OperationLog.Edit, "suppliers");
        }

        private void AddLog(int userId, OperationLog edit, string suppliers)
        {
            User user = this.Context.Users.Find(userId);
            Log log = new Log();
            log.User = user;
            log.ModifiedAt = DateTime.Now;
            log.ModifiedTableName = suppliers;
            log.Operation = edit;

            this.Context.Logs.Add(log);
            Context.SaveChanges();
        }

        public DeleteSupplierViewModel GetSupplierToDelete(int id)
        {
            Supplier supplier = this.Context.Suppliers.Find(id);
            DeleteSupplierViewModel mappedSuplier = Mapper.Map<Supplier, DeleteSupplierViewModel>(supplier);
            return mappedSuplier;
        }

        public void DeleteSupplier(DeleteSupplierBm deleteSupplierBm, int userId)
        {
            Supplier supplier = this.Context.Suppliers.Find(deleteSupplierBm.Id);
            this.Context.Suppliers.Remove(supplier);
            Context.SaveChanges();
            this.AddLog(userId, OperationLog.Delete, "suppliers");
        }

        public void AddSupplier(AddSupplierBm addSupplierBm, int userId)
        {
          Supplier supplier = Mapper.Map<AddSupplierBm, Supplier>(addSupplierBm);

            if (addSupplierBm.IsImporter == "on")
            {
                supplier.IsImporter = true;
            }
            else
            {
                supplier.IsImporter = false;
            }
            this.Context.Suppliers.Add(supplier);
            Context.SaveChanges();

            this.AddLog(userId, OperationLog.Add, "suppliers");
        }
    }
}
