using System;

namespace CarDealer.Models.DbModels
{
   public class Log
    {
        public int Id { get; set; }

        public virtual User User { get; set; }
        public OperationLog Operation { get; set; }

        public string ModifiedTableName { get; set; }

        public DateTime ModifiedAt { get; set; }


    }
}
