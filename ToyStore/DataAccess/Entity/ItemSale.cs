using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    public class ItemSale:BaseEntity
    {
        public int AvailableItemId { get; set; }

        public virtual AvailableItem AvailableItem { get; set; }

        public int SaleId { get; set; }

        public virtual Sale Sale { get; set; }

        [Range(0.0, int.MaxValue)]
        public int SoldQuantity { get; set; }

        [Range(0.0, double.MaxValue)]
        public decimal SoldPrice { get; set; }

    }
}
