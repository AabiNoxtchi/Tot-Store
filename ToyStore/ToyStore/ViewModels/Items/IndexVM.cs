using DataAccess.Entity;
using ToyStore.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToyStore.ViewModels.Items
{
    public class IndexVM : BaseIndexVM<Item, FilterVM, OrderBy>
    {
    }
}