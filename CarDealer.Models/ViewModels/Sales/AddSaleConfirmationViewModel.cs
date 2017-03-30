namespace CarDealer.Models.ViewModels.Sales
{
   public class AddSaleConfirmationViewModel
    {

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int CarId { get; set; }
        public string CarName { get; set; }
        public int TotalDiscount { get; set; }

        public decimal CarPrice { get; set; }

        public decimal FinalCarPrice { get; set; }
    }
}
