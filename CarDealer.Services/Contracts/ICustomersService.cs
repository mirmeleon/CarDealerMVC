using System.Collections.Generic;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;
using CarDealerApp.Models;

namespace CarDealer.Services
{
    public interface ICustomersService
    {
        IEnumerable<CustomerViewModel> GetAll(string order);
        CustomerByIdViewModel FindById(int? id);
        void AddCustomer(AddCustomerBm customerBm);
        void EditCustomer( EditCustomerBm editCustomerBm);
        EditCustomerViewModel GetCustomerForEdit(int id);
    }
}