using CarDealer.Data;

namespace CarDealer.Services
{
   public class Service
   {
       private CarDealerContext context;

       public Service()
       {
           this.context = new CarDealerContext();
       }
       protected CarDealerContext Context => this.context;

   }
}
