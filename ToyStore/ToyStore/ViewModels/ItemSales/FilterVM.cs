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

namespace ToyStore.ViewModels.ItemSales
{
    public class FilterVM : BaseFilterVM<ItemSale>
    {
        [Skip]
        public int? SaleId { get; set; }

        [DisplayName("Sale Number")]
        [DropDownFilter(TargetModelProperty = "SaleId", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList SaleIdsList { get; set; }

        [Skip]
        public int? ItemId { get; set; }

        [DisplayName("Item")]
        [DropDownFilter(TargetModelProperty = "ItemId", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList ItemsList { get; set; }

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


        public override Expression<Func<ItemSale, bool>> GenerateFilter()
        {
            return i => (SaleId == null || i.SaleId == SaleId) &&
                       (ItemId == null || i.AvailableItem.ItemId == ItemId) &&
                        (FromDate == null || i.Sale.Date >= FromDate) &&
                        (ToDate == null || i.Sale.Date <= ToDate) &&
                        (FromPrice == null || i.SoldPrice >= FromPrice) &&
                        (ToPrice == null || i.SoldQuantity <= ToPrice) &&
                        (FromQuantity == null || i.SoldQuantity >= FromQuantity) &&
                        (ToQuantity == null || i.SoldQuantity <= ToQuantity) ;

        }
    }
}