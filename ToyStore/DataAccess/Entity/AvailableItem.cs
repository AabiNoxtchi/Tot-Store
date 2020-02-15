using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class AvailableItem : BaseEntity
    {
        public int ItemId { get; set; }

        public Item Item { get; set; }

        [Range(0.0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0.0, double.MaxValue)]
        public int AvailableQuantity { get; set; }

        public virtual ICollection<ItemDelivery> ItemDeliveries { get; set; }

        public virtual ICollection<ItemSale> ItemSales { get; set; }
    }
}
