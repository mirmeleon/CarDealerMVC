using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealerApp.Models;

namespace CarDealer.Services
{
   public class CarsService : Service, ICarsService
   {
        public IEnumerable<CarViewModel> GetCarsByMake(string make)
        {
            IEnumerable<Car> carsFromDb;
            if (make == null)
            {
                make = "all";
            }

            if (make.ToLower() == "all")
            {
                carsFromDb = this.Context.Cars.OrderBy(c => c.Model).ThenByDescending(c => c.TravelledDistance);
              
            }
            else
            {
                carsFromDb = this.Context.Cars.Where(c => c.Make == make).OrderBy(c => c.Model).ThenByDescending(c => c.TravelledDistance);
            }
           
    
            IEnumerable<CarViewModel> mappedCars = Mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(carsFromDb);

            return mappedCars;
        }

        public CarsWithPartsViewModel GetParts(int id)
        {
           Car car = this.Context.Cars.Find(id);
           
            IEnumerable<Part> parts = car.Parts;

            CarViewModel mappedCar = Mapper.Map<Car, CarViewModel>(car);
            var mapperPart = Mapper.Map<IEnumerable<Part>, IEnumerable<PartsViewModel>>(parts);

            return new CarsWithPartsViewModel()
            {
                Cars = mappedCar,
                Parts = mapperPart
            };
            
        }

        public void AddCar(AddCarBm addCarBm)
        {
            if (addCarBm.CarModel != null && addCarBm.Make != null && addCarBm.TravelledDistance != 0)
            {
                Car car = Mapper.Map<AddCarBm, Car>(addCarBm);
                int[] partIds = addCarBm.Parts.Split(' ').Select(int.Parse).ToArray();

                foreach (var partId in partIds)
                {
                    Part part = this.Context.Parts.Find(partId);
                    if (part != null)
                    {
                        car.Parts.Add(part);
                    }
                }
                this.Context.Cars.Add(car);
                this.Context.SaveChanges();
            }
           
        }
    }
}
