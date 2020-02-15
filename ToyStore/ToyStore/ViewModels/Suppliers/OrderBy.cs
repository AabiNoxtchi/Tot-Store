using DataAccess.Entity;
using ToyStore.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToyStore.ViewModels.Suppliers
{
    public class OrderBy : BaseOrderBy<Supplier>
    {
        public string Name { get; set; }

        public string DDCNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }


        public override Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>> GenerateOrderBy()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                return u => u.OrderBy(i => i.Name);
            }
            else if (!string.IsNullOrEmpty(DDCNumber))
            {
                return u => u.OrderBy(i => i.DDCNumber);
            }
            else if (!string.IsNullOrEmpty(PhoneNumber))
            {
                return u => u.OrderBy(i => i.PhoneNumber);
            }
            if (!string.IsNullOrEmpty(Email))
            {
                return u => u.OrderBy(i => i.Email);
            }
            else if (!string.IsNullOrEmpty(Address))
            {
                return u => u.OrderBy(i => i.Address);
            }
            else if (!string.IsNullOrEmpty(City))
            {
                return u => u.OrderBy(i => i.City.Name);
            }


            return null;
        }
    }
}