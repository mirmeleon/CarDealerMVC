using System.Collections.Generic;
using CarDealer.Models.BindingModels;
using CarDealerApp.Models;

namespace CarDealer.Services
{
    public interface ICarsService
    {
        IEnumerable<CarViewModel> GetCarsByMake(string make);
        CarsWithPartsViewModel GetParts(int id);
        void AddCar(AddCarBm addCarBm);
    }
}