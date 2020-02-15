using System;
using System.Linq.Expressions;
using ToyStore.Attributes.FilterVM;

namespace ToyStore.ViewModels.Shared
{
    public interface IFilterVM
    {
        string Prefix { get; set; }
    }

    public abstract class BaseFilterVM<E> : IFilterVM
    {
        [Skip]
        public string Prefix { get; set; }

        public abstract Expression<Func<E, bool>> GenerateFilter();
    }
}