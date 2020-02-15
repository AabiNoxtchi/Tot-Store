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

namespace ToyStore.ViewModels.ItemDeliveries
{
    public class EditVM : BaseEditVM<ItemDelivery>
    {
        [Required(ErrorMessage = "* This field is Required!")]
        [DisplayName("Supplier")]
        public int SupplierId { get; set; }

        [DropDownFilter(TargetModelProperty = "SupplierId", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList SuppliersList { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        public int? AvailableItemId { get; set; }

        [DisplayName("Item")]
        [Required(ErrorMessage = "* This field is Required!")]
        public int? ItemId { get; set; }
       
        [DropDownFilter(TargetModelProperty = "ItemId", DataProperty = "Value", DisplayProperty = "Text")]
        public List<Item> ItemsList { get; set; }

        [DisplayName("Delivered Quantity")]
        [Required(ErrorMessage = "* This field is Required!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int? DeliveredQuantity { get; set; }

        [Required(ErrorMessage = "* This field is Required!")]
        [DisplayName("Delivery Item Price")]
        [DisplayFormat(DataFormatString ="{0:C}")]
        [Range( 1, double.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public decimal? DeliveryPrice { get; set; }

        public Items.EditVM ItemsEditVM { get; set; } 


        public EditVM() { }

        public override void PopulateEntity(ItemDelivery item)
        {
            item.Id = Id;
            item.AvailableItemId = (int)AvailableItemId;
            item.Date = Date;
            item.DeliveredQuantity = (int)DeliveredQuantity;
            item.DeliveryPrice = (decimal)DeliveryPrice;
            item.SupplierId = SupplierId;
        }

        public override void PopulateModel(ItemDelivery item)
        {
            Id = item.Id;
            AvailableItemId = item.AvailableItemId;
            Date = item.Date;
            DeliveredQuantity = item.DeliveredQuantity;
            DeliveryPrice = item.DeliveryPrice;
            SupplierId = item.SupplierId;
        }
    }
}