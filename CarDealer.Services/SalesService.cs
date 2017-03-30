using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDealer.Models;
using CarDealer.Models.BindingModels.Sales;
using CarDealer.Models.ViewModels.Sales;
using CarDealerApp.Models;

namespace CarDealer.Services
{
   public class SalesService : Service
    {
        public SalesViewModel GetSalesById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Sale id can't be 0");
            }
         
            Sale sale = this.Context.Sales.Find(id);


            SalesViewModel salesVm = Mapper.Map<Sale, SalesViewModel>(sale);
           
            return salesVm;
        }

        public IEnumerable<SalesViewModel> GetDiscountedSales(string discounted)
        {
            IEnumerable<Sale> sales = this.Context.Sales.Where(dis => dis.Discount != 0);
            IEnumerable<SalesViewModel> mappedSales = Mapper.Map<IEnumerable<Sale>, IEnumerable<SalesViewModel>>(sales);

            return mappedSales;
        }
        
        public IEnumerable<SalesViewModel> GetAllSales()
        {
            IEnumerable<Sale> sales = this.Context.Sales;
            IEnumerable<SalesViewModel> mappedSales = Mapper.Map<IEnumerable<Sale>, IEnumerable<SalesViewModel>>(sales);

            return mappedSales;
        }

        public AddSalesViewModel GenerateAddSalesForm()
        {
            IEnumerable<Customer> customers = this.Context.Customers;
            IEnumerable<AddSaleCustomerViewModel> mappedCustomers = Mapper.Map<IEnumerable<Customer>, IEnumerable<AddSaleCustomerViewModel>>(customers);

            IEnumerable<Car> cars = this.Context.Cars;
            IEnumerable<AddSaleCarViewModel> mappedCars =
                Mapper.Map<IEnumerable<Car>, IEnumerable<AddSaleCarViewModel>>(cars);
            
            AddSalesViewModel addSaleVm = new AddSalesViewModel();
            addSaleVm.Cars = mappedCars;
            addSaleVm.Customers = mappedCustomers;
            List<int> discounts = new List<int>();

            for (int i = 0; i <= 50; i+=5)
            {
                discounts.Add(i);
            }
            addSaleVm.Discounts = discounts;
            return addSaleVm;
        }

        public AddSaleConfirmationViewModel GetConfirmatinModel(AddSaleBm addSaleBm)
        {
            AddSaleConfirmationViewModel confVm = new AddSaleConfirmationViewModel();
            confVm.CarId = addSaleBm.CarId;
            confVm.CustomerId = addSaleBm.CustomerId;

            Customer customer = this.Context.Customers.Find(confVm.CustomerId);
            Car car = this.Context.Cars.Find(confVm.CarId);
            if (customer.IsYoungDriver)
            {
                confVm.TotalDiscount = 5 + addSaleBm.Discount;
            }

            confVm.CustomerName = customer.Name;
            confVm.TotalDiscount = addSaleBm.Discount;
            confVm.CarName = car.Make + " " + car.Model;
            confVm.CarPrice = (decimal) car.Parts.Sum(p => p.Price).Value;
            confVm.FinalCarPrice = confVm.CarPrice - confVm.CarPrice * confVm.TotalDiscount / 100;
            return confVm;
        }

        public void AddSale(AddSaleBm addSaleBm)
        {
           Sale sale = new Sale();
            Customer customer = Context.Customers.Find(addSaleBm.CustomerId);
            sale.Customer = customer;
            Car car = this.Context.Cars.Find(addSaleBm.CarId);
            sale.Car = car;
            sale.Discount = addSaleBm.Discount / 100.0;
            this.Context.Sales.Add(sale);
            this.Context.SaveChanges();
        }
    }
}
