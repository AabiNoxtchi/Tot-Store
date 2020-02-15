using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using ToyStore.Attributes.FilterVM;
using ToyStore.ViewModels.Shared;

namespace ToyStore.ViewModels.ItemDeliveries
{
    public class FilterVM : BaseFilterVM<ItemDelivery>
    {
        [Skip]
        public int? ItemId { get; set; }

        [DisplayName("Item")]
        [DropDownFilter(TargetModelProperty = "ItemId", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList ItemsList { get; set; }

        [Skip]
        public int? CategoryId { get; set; }

        [DisplayName("Category")]
        [DropDownFilter(TargetModelProperty = "CategoryId", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList CategoriesList { get; set; }

        [DateTime]
        [DisplayName("Date From")]
        public DateTime? FromDate { get; set; }

            [DateTime]
        [DisplayName("Date To")]
        public DateTime? ToDate { get; set; } 

        [DisplayName("Quantity From")]
        public int? FromQuantity { get; set; }

        [DisplayName("Quantity To")]
        public int? ToQuantity { get; set; }

        [DisplayName("Min Price")]
        public decimal? FromPrice { get; set; }

        [DisplayName("Max Price")]
        public decimal? ToPrice { get; set; }

        [Skip]
        public int? SupplierId { get; set; }

        [DisplayName("Supplier")]
        [DropDownFilter(TargetModelProperty = "SupplierId", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList SuppliersList { get; set; }



        public override Expression<Func<ItemDelivery, bool>> GenerateFilter()
        {
            return i => (ItemId == null || i.AvailableItem.ItemId == ItemId) &&
                        (FromDate == null || i.Date >= FromDate) &&
                        (ToDate == null || i.Date <= ToDate) &&
                        (FromQuantity == null || i.DeliveredQuantity >= FromQuantity) &&
                        (ToQuantity == null || i.DeliveredQuantity <= ToQuantity) &&
                        (FromPrice == null || i.DeliveryPrice >= FromPrice) &&
                        (ToPrice == null || i.DeliveryPrice <= ToPrice) &&
                        (SupplierId == null || i.SupplierId == SupplierId) &&
                        (CategoryId == null || i.AvailableItem.Item.CategoryId == CategoryId);
            
        }
    }
}