using DataAccess.Entity;
using ToyStore.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToyStore.Attributes.FilterVM;
using System.Web.Mvc;

namespace ToyStore.ViewModels.ItemSales
{
    public class IndexVM : BaseIndexVM<ItemSale, FilterVM, OrderBy>
    {
        public List<Sale> SalesItems { get; set; }
        public List<AvailableItem> AvailableItems { get; set; }
        public string SaleView { get; set; } = "SaleView";

    }
}