namespace CarDealerApp.Models
{
    public class SalesViewModel
    {
        public CarViewModel Car { get; set; }

        public CustomerViewModel Customer { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }
    }
}