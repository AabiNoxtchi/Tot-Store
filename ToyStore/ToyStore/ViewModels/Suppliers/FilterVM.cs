using DataAccess.Entity;
using ToyStore.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using ToyStore.Attributes.FilterVM;

namespace ToyStore.ViewModels.Suppliers
{
    public class FilterVM : BaseFilterVM<Supplier>
    {

        public string Name { get; set; }
        
        public string DDCNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        [Skip]
        public int? CityId { get; set; }

        [DisplayName("City")]
        [DropDownFilter(TargetModelProperty = "CityId", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList CitiesList { get; set; }

        public override Expression<Func<Supplier, bool>> GenerateFilter()
        {
            return i => (string.IsNullOrEmpty(Name) || i.Name.Contains(Name)) &&
                        (string.IsNullOrEmpty(DDCNumber) || i.Name.Contains(DDCNumber)) &&
                        (string.IsNullOrEmpty(PhoneNumber) || i.PhoneNumber.Contains(PhoneNumber)) &&
                        (string.IsNullOrEmpty(Email) || i.Email.Contains(Email)) &&
                        (string.IsNullOrEmpty(Address) || i.Address.Contains(Address)) &&
                        (CityId==null || i.CityId==CityId);
        }
    }
}