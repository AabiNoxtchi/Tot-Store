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

namespace ToyStore.ViewModels.Suppliers
{
   
    public class EditVM : BaseEditVM<Supplier>
    {
        [Required(ErrorMessage = "* This field is Required!")]
        [MaxLength(200,ErrorMessage ="* Name cannot be Longer Than 200 characters")]
        [MinLength(5, ErrorMessage = "* Name cannot be Less than 5")]
        public string Name { get; set; }

        [DisplayName("DDC Number")]
        [Required(ErrorMessage = "* This field is Required!")]
        [MaxLength(11, ErrorMessage = "* DDC Number cannot be Longer Than 11 characters")]
        [MinLength(5, ErrorMessage = "* DDC Number cannot be Less than 8")]
        public string DDCNumber { get; set; }

        [DisplayName("Phone Number")]
        [Phone]
        [MaxLength(12,ErrorMessage ="* Phone Number cannot be Longer than 12 numbers")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Email ")]
        [MaxLength(200, ErrorMessage = "* Email Address cannot be Longer than 200 charracters")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [MaxLength(150, ErrorMessage = "* Address cannot be Longer than 150 characters")]
        public string Address { get; set; }

        [DisplayName("City")]
        public int? CityId { get; set; }
        
        
        [DropDownFilter(TargetModelProperty = "CityId", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList CitiesList { get; set; }

        [DisplayName("Add new City : ")]
        public string NewCity { get; set; }


        public EditVM() { }

        public override void PopulateEntity(Supplier item)
        {

            item.Id = Id;
            item.Name = Name;
            item.DDCNumber = DDCNumber;
            item.PhoneNumber = PhoneNumber;
            item.Email = Email;
            item.Address = Address;
            item.CityId = (int)CityId;
        }

        public override void PopulateModel(Supplier item)
        {
            Id = item.Id;
            Name = item.Name;
            DDCNumber = item.DDCNumber;
            PhoneNumber = item.PhoneNumber;
            Email = item.Email;
            Address = item.Address;
            CityId = item.CityId;

           
        }
    }
}