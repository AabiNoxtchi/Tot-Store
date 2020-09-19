
using DataAccess.Entity;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ToyStore.ViewModels.Shared;

namespace ToyStore.Controllers
{
    public abstract class BaseController<E, R, FilterVM, IndexVM, EditVM,OrderBy> : Controller
        where E : BaseEntity, new()
        where R : BaseRepository<E>, new()
        where IndexVM : BaseIndexVM<E, FilterVM,OrderBy>, new()
        where FilterVM : BaseFilterVM<E>, new()
        where EditVM : BaseEditVM<E>, new()
        where OrderBy : BaseOrderBy<E>, new()
    {

        protected void ListItemsAddFirstNull(List<SelectListItem> listItems)
        {
            listItems.Add(new SelectListItem()
            {
                Text = "",
                Value = null

            });
        }
        public List<SelectListItem> PopulateSelectList<T,I>(T repo,I entity) 
            where T:BaseRepository<I>,new() 
            where I:BaseEntity,new()
        {
            
            List<I> items = repo.GetAll(a => true);

            List<SelectListItem> listItems = new List<SelectListItem>();

            ListItemsAddFirstNull(listItems);

            try
            {
                foreach (I item in items)
                {
                    listItems.Add(new SelectListItem()
                    {
                        Text = item.GetType().GetProperty("Name").GetValue(item).ToString(),
                        Value = item.Id.ToString()

                    });
                }

            }
            catch (Exception) { }
            return listItems;
        }

        public virtual void PopulateModel(IndexVM model) { }
        public virtual void PopulateEditGetModel(EditVM model) { }
        public virtual void PopulateEditPostModel(EditVM model) { }
        protected virtual void CheckModel(EditVM model, ref bool valid) { }
        protected virtual void UpdateRelatedEntity(E item) { }
        protected virtual ActionResult GetView(EditVM model)
        {
            return View(model);
        }
        protected virtual void CheckRelatedEntitiesForDelete(E item) { }

        [HttpGet]
        public virtual ActionResult Index(IndexVM model)
        {
            model.Pager = model.Pager ?? new PagerVM();
            model.Pager.Prefix = "Pager";
            model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            model.Filter = model.Filter ?? new FilterVM();
            model.Filter.Prefix = "Filter";
            PopulateModel(model);
            Expression<Func<E, bool>> filter = model.Filter.GenerateFilter();

            model.OrderBy = model.OrderBy ?? new OrderBy();
            Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = model.OrderBy.GenerateOrderBy();

            R repo = new R();
            model.Items = repo.GetAll(filter, model.Pager.Page, model.Pager.ItemsPerPage, orderBy, model.relatedEntity);

            model.Pager.PagesCount = (int)Math.Ceiling(repo.Count(filter) / (double)(model.Pager.ItemsPerPage));

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml)
        {
            return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "Grid.xls");
        }

        [HttpGet]
        public ActionResult Edit(EditVM model, FormCollection form)
        {
            ModelState.Clear();

            model = model ?? new EditVM();

            if (model.Id > 0)
            {
                E item = null;
                R repo = new R();

                item = repo.GetById(model.Id);
                model.PopulateModel(item);
            }
           
            PopulateEditGetModel(model);

           /* if (Request.IsAjaxRequest())
            {
                return PartialView("_EditPartial", model);
            }*/
            return GetView(model);
        }

       

        [HttpPost]
        public virtual ActionResult Edit(EditVM model)
        {
            bool valid = true;
            CheckModel(model, ref valid);

            if (!ModelState.IsValid || !valid)
            {
                PopulateEditGetModel(model);
                return View(model);
            }

            R repo = new R();
            E item = new E();
            PopulateEditPostModel(model);
            model.PopulateEntity(item);

            repo.Save(item);
            UpdateRelatedEntity(item);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual ActionResult Delete(int id)
        {
            R repo = new R();
            E item = repo.GetById(id);

            CheckRelatedEntitiesForDelete(item);
            repo.Delete(item);

            return RedirectToAction("Index"); 
        }

       
    }
}