using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using AutoMapper;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;
using CarDealerApp.Models;

namespace CarDealer.Services
{
  public  class CustomersService : Service
    {
        public IEnumerable<CustomerViewModel> GetAll(string order)
        {

            IEnumerable<Customer> customers;
            if (order == "ascending")
            {
                customers = this.Context.Customers.OrderBy(c => c.BirthDate).ThenBy(c => c.IsYoungDriver);
            }
            else if (order == "descending")
            {
                customers = this.Context.Customers.OrderByDescending(c => c.BirthDate).ThenBy(c => c.IsYoungDriver);
               
            }
            else
            {
                customers = this.Context.Customers;
            }

         
            IEnumerable<CustomerViewModel> mapper =
                Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(customers);

            return mapper;
        }

        public CustomerByIdViewModel FindById(int? id)
        {
            
            if (id == null)
            {
                id = 1;
              
            }

            Customer customer = this.Context.Customers.FirstOrDefault(c => c.Id == id);

            CustomerByIdViewModel customerVm = Mapper.Map<Customer, CustomerByIdViewModel>(customer);
            
            return customerVm;
        }

        public void AddCustomer(AddCustomerBm customerBm)
        {
            Customer customer = Mapper.Map<AddCustomerBm, Customer>(customerBm);
            if (DateTime.Now.Year - customerBm.BirthDate.Year < 21)
            {
                customer.IsYoungDriver = true;
            }
            this.Context.Customers.Add(customer);
            this.Context.SaveChanges();
        }

        public void EditCustomer( EditCustomerBm editCustomerBm)
        {
            Customer customer = this.Context.Customers.Find(editCustomerBm.Id);
            if (customer == null)
            {
                throw new ArgumentException("Cannot find customer with such id!");
            }
            customer = Mapper.Map<EditCustomerBm, Customer>(editCustomerBm);
            this.Context.Customers.AddOrUpdate(customer);
            this.Context.SaveChanges();
        }

        public EditCustomerViewModel GetCustomerForEdit(int id)
        {
            Customer customer = this.Context.Customers.Find(id);
            EditCustomerViewModel mappedcustomer = Mapper.Map<Customer, EditCustomerViewModel>(customer);
            return mappedcustomer;
        }
    }
}
