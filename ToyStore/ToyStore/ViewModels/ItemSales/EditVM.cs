using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToyStore.Attributes.FilterVM;
using ToyStore.ViewModels.Shared;

namespace ToyStore.ViewModels.ItemSales
{
    public class EditVM : BaseEditVM<ItemSale>
    {
        [DisplayName("Sale Number")]
        public int SaleId { get; set; }

        [Required(ErrorMessage = "* This field is Required!")]
        [DisplayName("Item")]
        public int AvailableItemId { get; set; }

        
        public List<AvailableItem> AvailableItemsList { get; set; }

        public int availablequantity { get; set; }

        [DisplayName("Quantity")]
        [Required(ErrorMessage = "* This field is Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int SoldQuantity { get; set; } = 1;

        
        [DisplayName("Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public decimal SoldPrice { get; set; }

        

        public EditVM() { }

        public override void PopulateEntity(ItemSale item)
        {
            item.Id = Id;
            item.AvailableItemId = AvailableItemId;
            item.SaleId = (int)SaleId;
            item.SoldQuantity = SoldQuantity;
            item.SoldPrice = SoldPrice;
            
        }

        public override void PopulateModel(ItemSale item)
        {
            Id = item.Id;
            SaleId = item.SaleId;
            AvailableItemId = item.AvailableItemId;
            SoldQuantity = item.SoldQuantity;
            SoldPrice = item.SoldPrice;
        }
    }
}