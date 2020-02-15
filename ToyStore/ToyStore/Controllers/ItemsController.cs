using DataAccess.Entity;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToyStore.ViewModels.Items;

namespace ToyStore.Controllers
{
    public class ItemsController : BaseController<Item, ItemsRepository, FilterVM, IndexVM, EditVM, OrderBy>
    {

        private List<SelectListItem> PopulateGendersSelectList()
        {
            int i = 0;
            List<SelectListItem> listItems = new List<SelectListItem>();
            base.ListItemsAddFirstNull(listItems);
            foreach (string gender in Enum.GetNames(typeof(Gender)))
            {
                listItems.Add(new SelectListItem()
                {
                    Text = gender,
                    Value = i++.ToString()

                });
            }
            return listItems;
        }

       public override void PopulateModel(IndexVM model)
        {
            model.relatedEntity = "AvailableItems";

            model.Filter.ItemsList =
                new SelectList(base.PopulateSelectList(new ItemsRepository(), new Item()), "Value", "Text", model.Filter.ItemId);
            model.Filter.CategoriesList =
               new SelectList(base.PopulateSelectList(new CategoriesRepository(), new Category()),"Value", "Text", model.Filter.CategoryId);

            model.Filter.AgesList =
                new SelectList(base.PopulateSelectList(new AgesRepository(), new Age()), "Value", "Text", model.Filter.AgeId);

            model.Filter.GendersList =
                new SelectList(PopulateGendersSelectList(), "Value", "Text", model.Filter.GenderInt);

        }

        protected override void CheckModel(EditVM model, ref bool valid)
        {
            ItemsRepository irepo = new ItemsRepository();
            Item item = new Item();
            item=irepo.GetAll(i => i.Name == model.Name).FirstOrDefault();
            if(item!=null)
            {

                valid = false;
                ModelState.AddModelError("Name", " * An Item with this Name already exist in the Store !");
            }

            if ((model.CategoryId != null && model.NewCategory != null) || (model.CategoryId == null && model.NewCategory == null))
            {
                valid = false;
                ModelState.AddModelError("CategoryId", "You have to choose either an existing Category from the list or add a New one !");

            }
            else if (model.NewCategory != null)
            {

                foreach (var modelError in ModelState)
                {
                    string propertyName = modelError.Key;

                    if (propertyName.Contains("CategoryId"))
                    {
                        ModelState[propertyName].Errors.Clear();
                    }
                }
            }
        }

        public override void PopulateEditGetModel(EditVM model)
        {
            model.CategoriesList =
                new SelectList(base.PopulateSelectList(new CategoriesRepository(),new Category()), "Value", "Text");

            model.AgesList =
               new SelectList(base.PopulateSelectList(new AgesRepository(), new Age()), "Value", "Text", model.AgeId);

            if (model.Id > 0)
            {
                AvailableItemsRepository aiRepo = new AvailableItemsRepository();
                AvailableItem item = aiRepo.GetAll(ai => ai.ItemId == model.Id).FirstOrDefault();

                if (item != null)
                {
                    decimal price = item.Price;
                    model.AvailableQuantity = item.AvailableQuantity;
                }

            }
        }

        public override void PopulateEditPostModel(EditVM model)
        {

            if (!string.IsNullOrEmpty(model.NewCategory))
            {
                CategoriesRepository cRepo = new CategoriesRepository();
                Category category = cRepo.GetAll(c => c.Name == model.NewCategory).FirstOrDefault();
                if (category != null)
                {
                    model.CategoryId = category.Id;
                }
                else
                {
                    category = new Category();
                    category.Name = model.NewCategory;
                    cRepo.Save(category);
                    model.CategoryId = cRepo.GetAll(c => c.Name == model.NewCategory).FirstOrDefault().Id;
                }
            }
        }

    }
}