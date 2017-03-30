using System.Collections.Generic;

namespace CarDealerApp.Models
{
    public class CarsWithPartsViewModel
    {
        public CarsWithPartsViewModel()
        {
           this.Parts  = new HashSet<PartsViewModel>();
        }
          public CarViewModel Cars { get; set; }

        public IEnumerable<PartsViewModel> Parts { get; set; }
     
    }
}