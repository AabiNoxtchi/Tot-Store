using DataAccess.Entity;
using ToyStore.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ToyStore.Attributes.FilterVM;
using System.Web.Mvc;

namespace ToyStore.ViewModels.Items
{
   

    public class EditVM : BaseEditVM<Item>
    {
        [DisplayName("Identification Kod")]
        [MaxLength(100)]
        [Required(ErrorMessage = "* This field is Required!")]
        public string IdentificationKod { get; set; }

        [DisplayName("Product Name")]
        [MaxLength(150)]
        [Required(ErrorMessage = "* This field is Required!")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "* This field is Required!")]
        [DisplayName("Age")]
        public int AgeId { get; set; }

        [DropDownFilter(TargetModelProperty = "AgeId", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList AgesList { get; set; }

        [ReadOnly(true)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }

        [ReadOnly(true)]
        [DisplayName("Available Quantity")]
        public int AvailableQuantity { get; set; }

        [Required(ErrorMessage = "* This field is Required!")]
        [DisplayName("Category")]
        public int? CategoryId { get; set; }
        
        [DropDownFilter(TargetModelProperty = "CategoryId", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList CategoriesList { get; set; }

        [DisplayName("New Category : ")]
        public string NewCategory { get; set; }

        [Required(ErrorMessage = "* This field is Required!")]
        public Gender Gender { get; set; } = Gender.Both;


        public EditVM() { }

        public override void PopulateEntity(Item item)
        {

            item.Id = Id;
            item.IdentificationKod = IdentificationKod;
            item.Name = Name;
            item.Description = Description;
            item.AgeId = AgeId;
            item.CategoryId = (int)CategoryId;
            item.Gender = Gender;
        }

        public override void PopulateModel(Item item)
        {
            Id = item.Id;
            IdentificationKod = item.IdentificationKod;
            Name = item.Name;
            Description = item.Description;
            AgeId = item.AgeId;
            CategoryId = item.CategoryId;
            Gender = item.Gender;
        }
    }
}