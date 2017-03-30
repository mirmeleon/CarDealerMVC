using System.Collections.Generic;

namespace CarDealer.Models.ViewModels.Logs
{
   public class AllLogsPageViewModel
    {
        public string WantedUserName { get; set; }
        public IEnumerable<AllLogsViewModel> Logs { get; set; }
        public int CurrentPage { get; set; }
        public int TotalNumberOfPages { get; set; }
    }
}
