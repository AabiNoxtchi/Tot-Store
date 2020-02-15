using DataAccess.Entity;
using ToyStore.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToyStore.ViewModels.Items
{
    public class OrderBy : BaseOrderBy<Item>
    {
        public string IdentificationKod { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Gender { get; set; }

        public string Price { get; set; }

        public string Quantity { get; set; }


        public override Func<IQueryable<Item>, IOrderedQueryable<Item>> GenerateOrderBy()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                return u => u.OrderBy(i => i.Name);
            }
            else if (!string.IsNullOrEmpty(IdentificationKod))
            {
                return u => u.OrderBy(i => i.IdentificationKod);
            }
            else if (!string.IsNullOrEmpty(Category))
            {
                return u => u.OrderBy(i => i.Category.Name);
            }
            else if (!string.IsNullOrEmpty(Gender))
            {
                return u => u.OrderBy(i => i.Gender);
            }
            else if(!string.IsNullOrEmpty(Price))
            {
                return u => u.OrderBy(i => i.AvailableItems.FirstOrDefault().Price);
            }
            else if (!string.IsNullOrEmpty(Quantity))
            {
                return u => u.OrderBy(i => i.AvailableItems.FirstOrDefault().AvailableQuantity);
            }

            return null;
        }
    }
}