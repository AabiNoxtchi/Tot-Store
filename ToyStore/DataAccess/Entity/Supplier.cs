using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    
    public class Supplier:BaseEntity
    {
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(11)]
        public string DDCNumber { get; set; }

        [MaxLength(12)]
        public string PhoneNumber { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(150)]
        public string Address { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public ICollection<ItemDelivery> ItemDeliveries { get; set; }

    }
}
