using System;
using CarDealer.Models.DbModels;

namespace CarDealer.Models.ViewModels.Logs
{
  public class AllLogsViewModel
    {
        public string UserName { get; set; }

        public OperationLog Operation { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string ModifiedTable { get; set; }
    }
}
