using DataAccess.Entity;
using ToyStore.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToyStore.ViewModels.ItemSales
{
    public class OrderBy : BaseOrderBy<ItemSale>
    {
        public string Date { get; set; }

        public string SaleId { get; set; }

        public string Item { get; set; }

        public string SoldQuantity { get; set; }

        public string SoldPrice { get; set; }


        public override Func<IQueryable<ItemSale>, IOrderedQueryable<ItemSale>> GenerateOrderBy()
        {
            if (!string.IsNullOrEmpty(Date))
            {
                return u => u.OrderBy(i => i.Sale.Date);
            }
            else if (!string.IsNullOrEmpty(SaleId))
            {
                return u => u.OrderBy(i => i.Sale.Id);
            }
            else if (!string.IsNullOrEmpty(Item))
            {
                return u => u.OrderBy(i => i.AvailableItem.Item.Name);
            }
            else if (!string.IsNullOrEmpty(SoldQuantity))
            {
                return u => u.OrderBy(i => i.SoldQuantity);
            }
            else if (!string.IsNullOrEmpty(SoldPrice))
            {
                return u => u.OrderBy(i => i.SoldPrice);
            }

            return null;
        }
    }
}