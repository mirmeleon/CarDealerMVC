using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealer.Models.BindingModels.Suppliers;
using CarDealer.Models.DbModels;
using CarDealer.Models.ViewModels;
using CarDealer.Models.ViewModels.Sales;
using CarDealer.Models.ViewModels.Suppliers;
using CarDealerApp.Models;

namespace CarDealerApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
      
        protected void Application_Start()
        {
            ConfigureAutoMapper();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void ConfigureAutoMapper()
        {
            
          Mapper.Initialize(expression =>
            {
             
                expression.CreateMap<Customer, CustomerByIdViewModel>()
                    .ForMember(destVm => destVm.TotalSpentMoney,
                        config => config.MapFrom(sale =>
                                sale.Sales.Sum(p => p.Car.Parts.Sum(part => part.Price))))
                 
                    .ForMember(desVm => desVm.BoughtCarsCount,
                        configuration => configuration.MapFrom(count => count.Sales.Count));

                expression.CreateMap<Car, CarViewModel>();
                expression.CreateMap<Part, PartsViewModel>();
                expression.CreateMap<Supplier, SupplierViewModel>();
       
                expression.CreateMap<Sale, SalesViewModel>()
                    .ForMember(destVM => destVM.Price,
                        configuration =>
                                configuration.MapFrom(p => p.Car.Parts.Sum(pr => pr.Price)));
               
                expression.CreateMap<Customer, CustomerViewModel>();
                expression.CreateMap<Customer, EditCustomerViewModel>();

                expression.CreateMap<Part, AllPartsViewModel>();
                expression.CreateMap<Supplier, AddPartSupplierViewModel>()
                    .ForMember(destVm => destVm.Id,
                        config =>
                                config.MapFrom(sup => sup.Id))
                    .ForMember(destVm => destVm.Name,
                        conf => conf.MapFrom(supl => supl.Name));
                expression.CreateMap<Part, DeletePartViewModel>();
                expression.CreateMap<Part, EditPartViewModel>();
             
                expression.CreateMap<Customer, AddSaleCustomerViewModel>();
                expression.CreateMap<Car, AddSaleCarViewModel>();
           
                expression.CreateMap<Supplier, EditSupplierViewModel>();
                expression.CreateMap<Supplier, DeleteSupplierViewModel>();

                //binding models 
                expression.CreateMap<AddCustomerBm, Customer>();
                expression.CreateMap<EditCustomerBm, Customer>();
                expression.CreateMap<AddPartBm, Part>();
                expression.CreateMap<DeletePartBm, Part>();
                expression.CreateMap<AddCarBm, Car>()
                .ForMember(car => car.Parts,
                configuration => 
                configuration.Ignore())
                .ForMember(car => car.Model,
                conf => conf.MapFrom(m=>m.CarModel));

                expression.CreateMap<RegisterUserBm, User>();
                expression.CreateMap<EditSupplierBm, Supplier>();
            
                expression.CreateMap<LoginUserBm, User>();
                expression.CreateMap<AddSupplierBm, Supplier>()
                .ForMember(im=>im.IsImporter,
                config => config.Ignore());

            });
        }
    }
}
