using System.Collections.Generic;

namespace CarDealer.Models.ViewModels.Sales
{
   public class AddSalesViewModel
    {
        public IEnumerable<AddSaleCustomerViewModel> Customers { get; set; }

        public IEnumerable<AddSaleCarViewModel> Cars { get; set; }
        public IEnumerable<int> Discounts { get; set; }

    }
}
