using System.Collections.Generic;

namespace CarDealer.Models.DbModels
{
   public class User
    {
        public User()
        {
            this.Logins = new HashSet<Login>();
        }
        public int Id { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
    }
}
