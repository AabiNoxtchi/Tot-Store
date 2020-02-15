using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class ItemDelivery:BaseEntity
    {
        
        public int AvailableItemId { get; set; }

        public virtual AvailableItem AvailableItem { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Range(0.0, int.MaxValue)]
        public int DeliveredQuantity { get; set; }

        [Range(0.0, double.MaxValue)]
        public decimal DeliveryPrice { get; set; }

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

    }
}
