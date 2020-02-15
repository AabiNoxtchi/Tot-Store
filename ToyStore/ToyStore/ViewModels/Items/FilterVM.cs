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

namespace ToyStore.ViewModels.Items
{
    public class FilterVM : BaseFilterVM<Item>
    {
        public string IdentificationKod { get; set; }

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

        [Skip]
        public int? AgeId { get; set; }

        [DisplayName("Age")]
        [DropDownFilter(TargetModelProperty = "AgeId", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList AgesList { get; set; }


        [Skip]
        public int? GenderInt { get; set; }

        [DisplayName("Gender")]
        [DropDownFilter(TargetModelProperty = "GenderInt", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList GendersList { get; set; }

       // [CheckBox]
       // [DisplayName("Count By Item Name")]
        //public string CountBy { get; set; }

        public override Expression<Func<Item, bool>> GenerateFilter()
        {
            return i => (string.IsNullOrEmpty(IdentificationKod) || i.IdentificationKod.Contains(IdentificationKod)) &&
                        (ItemId==null || i.Id==ItemId) &&
                        (CategoryId == null || i.CategoryId == CategoryId) &&
                        (AgeId==null||i.AgeId==AgeId)&&
                        (
                        GenderInt == null || 
                        (
                        GenderInt==(int)Gender.Boys? (i.Gender == Gender.Boys|| i.Gender==Gender.Both): 
                        GenderInt==(int)Gender.Girls? (i.Gender==Gender.Girls||i.Gender==Gender.Both):
                        i.Gender==Gender.Both
                        )
                        );
        }
    }
}