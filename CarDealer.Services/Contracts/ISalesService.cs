using System.Collections.Generic;
using CarDealer.Models.BindingModels.Sales;
using CarDealer.Models.ViewModels.Sales;
using CarDealerApp.Models;

namespace CarDealer.Services
{
    public interface ISalesService
    {
        SalesViewModel GetSalesById(int id);
        IEnumerable<SalesViewModel> GetDiscountedSales(string discounted);
        IEnumerable<SalesViewModel> GetAllSales();
        AddSalesViewModel GenerateAddSalesForm();
        AddSaleConfirmationViewModel GetConfirmatinModel(AddSaleBm addSaleBm);
        void AddSale(AddSaleBm addSaleBm);
    }
}