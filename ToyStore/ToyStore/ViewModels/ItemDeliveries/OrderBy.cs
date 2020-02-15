using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToyStore.ViewModels.Shared;

namespace ToyStore.ViewModels.ItemDeliveries
{
    public class OrderBy:BaseOrderBy<ItemDelivery>
    {
        public string DeliveredItem { get; set; }

        public string Date { get; set; }

        public string DeliveredQuantity { get; set; }

        public string DeliveryPrice { get; set; }

        public string Supplier { get; set; }


        public override Func<IQueryable<ItemDelivery>, IOrderedQueryable<ItemDelivery>> GenerateOrderBy()
        {
            if (!string.IsNullOrEmpty(DeliveredItem))
            {
                return u => u.OrderBy(i => i.AvailableItem.Item.Name);
            }
            else if (!string.IsNullOrEmpty(Date))
            {
                return u => u.OrderBy(i => i.Date);
            }
            else if (!string.IsNullOrEmpty(DeliveredQuantity))
            {
                return u => u.OrderBy(i => i.DeliveredQuantity);
            }
            else if (!string.IsNullOrEmpty(DeliveryPrice))
            {
                return u => u.OrderBy(i => i.DeliveryPrice);
            }
            else if (!string.IsNullOrEmpty(Supplier))
            {
                return u => u.OrderBy(i => i.Supplier.Name);
            }

            return null;
        }

    }
}