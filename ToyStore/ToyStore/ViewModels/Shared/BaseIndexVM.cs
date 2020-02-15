using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToyStore.ViewModels.Shared
{
    public class BaseIndexVM<E, F,O>
        where E : BaseEntity
        where F : BaseFilterVM<E>
        where O:BaseOrderBy<E>
    {
        public string relatedEntity { get; set; } 
        public PagerVM Pager { get; set; }
        public F Filter { get; set; }
        public O OrderBy { get; set; }

        public List<E> Items { get; set; }
        
    }
}