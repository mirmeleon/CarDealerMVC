using System;

namespace CarDealerApp.Models
{
    public class CustomerViewModel
    { 
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }

        public bool IsYoungDriver { get; set; }
    }
}