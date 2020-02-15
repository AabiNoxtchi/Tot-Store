using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToyStore.ViewModels.Shared
{
    public class BaseOrderBy<E>
    {
        // public string orderBY { get; set; }

        public virtual Func<IQueryable<E>, IOrderedQueryable<E>> GenerateOrderBy() { return null; }
    }
}