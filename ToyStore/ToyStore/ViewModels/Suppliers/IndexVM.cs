using DataAccess.Entity;
using ToyStore.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToyStore.ViewModels.Suppliers
{
    public class IndexVM : BaseIndexVM<Supplier, FilterVM, OrderBy>
    {
    }
}