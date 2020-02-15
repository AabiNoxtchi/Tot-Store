using DataAccess.Entity;
using DataAccess.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToyStore.ViewModels.ItemDeliveries;

namespace ToyStore.Controllers
{
    public class ItemDeliveriesController : BaseController<ItemDelivery,ItemDeliveriesRepository,FilterVM,IndexVM,EditVM,OrderBy>
    {
        /* private List<SelectListItem> PopulateDatesList()
         {
             List<SelectListItem> listItems = new List<SelectListItem>();
             ItemDeliveriesRepository repo = new ItemDeliveriesRepository();
             var FirstDate = repo.GetAll(i=>true,itemsPerPage:1, OrderByDescending: u => u.OrderBy(i => i.Date));//oldest date
             var LastDate = repo.GetAll(i => true,itemsPerPage:1, OrderByDescending: u => u.OrderByDescending(i => i.Date));//latest date
             return listItems;
         }*/

        private void PopulateNewItemEditPostModel(EditVM model)
        {
            ItemsController itemsController = new ItemsController();
            itemsController.PopulateEditPostModel(model.ItemsEditVM);
            Item item = new Item();
            model.ItemsEditVM.PopulateEntity(item);
            ItemsRepository irepo = new ItemsRepository();
            irepo.Save(item);
            model.ItemId = irepo.GetAll(i => i.Name == item.Name).FirstOrDefault().Id;
        }

        private List<Item> PopulateItemsList()
        {
            ItemsRepository arepo = new ItemsRepository();
            List<Item> items = new List<Item>();
            items = arepo.GetAll(a => true,relatedEntity:"AvailableItems");
            return items;
        }

        protected override void CheckModel(EditVM model, ref bool valid)
        {
            if(model.ItemId!=null)
            {
                foreach (var modelError in ModelState)
                {
                    string propertyName = modelError.Key;

                    if (propertyName.Contains("ItemsEditVM"))
                    {
                        ModelState[propertyName].Errors.Clear();
                    }
                }
            }
            else if (model.ItemId == null && model.ItemsEditVM.Name == null)
            {
                valid = false;
                ModelState.AddModelError("", "You have to choose either an existing Item from the list or add a New one !");
            }
            else if(model.ItemId==null && model.ItemsEditVM.Name!=null)
            {
                if ((model.ItemsEditVM.CategoryId != null && model.ItemsEditVM.NewCategory != null) ||
                       (model.ItemsEditVM.CategoryId == null && model.ItemsEditVM.NewCategory == null))
                {
                    valid = false;
                    ModelState.AddModelError("", "You have to choose either an existing Category from the list or add a New one !");

                }
            }


        }

        public override void PopulateModel(IndexVM model)
        {
            model.relatedEntity = "AvailableItem";
            model.Filter.CategoriesList =
                new SelectList(base.PopulateSelectList(new CategoriesRepository(),new Category()), "Value", "Text", model.Filter.CategoryId);

            model.Filter.ItemsList =
               new SelectList(base.PopulateSelectList(new ItemsRepository(),new Item()), "Value", "Text",model.Filter.ItemId);

            model.Filter.SuppliersList =
                 new SelectList(base.PopulateSelectList(new SuppliersRepository(),new Supplier()), "Value", "Text",model.Filter.SupplierId);

           // model.Filter.FromDatesList = new SelectList(PopulateDatesList(), "Value", "Text", model.Filter.FromDate);

        }

        public override void PopulateEditGetModel(EditVM model)
        {
            model.ItemsList = PopulateItemsList();

           model.SuppliersList=
                new SelectList(base.PopulateSelectList(new SuppliersRepository(),new Supplier()), "Value", "Text");

            if(model.Id>0)
            {
                AvailableItemsRepository repo = new AvailableItemsRepository();
                model.ItemId = repo.GetAll(i => i.Id == model.AvailableItemId).FirstOrDefault().ItemId;

            }

            model.ItemsEditVM = new ViewModels.Items.EditVM();
            ItemsController itemsController = new ItemsController();
            itemsController.PopulateEditGetModel(model.ItemsEditVM);
        }

        public override void PopulateEditPostModel(EditVM model)
        {
            if (model.ItemId == null)
            {
                PopulateNewItemEditPostModel(model);
            }
            
            AvailableItemsRepository aiRepo = new AvailableItemsRepository();
            AvailableItem availableItem = aiRepo.GetAll(ai => ai.ItemId == model.ItemId).FirstOrDefault();

            if (availableItem == null)
            {
                availableItem = new AvailableItem();
                availableItem.ItemId = (int)model.ItemId;
                aiRepo.Save(availableItem);
            }

            model.AvailableItemId = aiRepo.GetAll(ai => ai.ItemId == model.ItemId).FirstOrDefault().Id;

        }

        protected override void UpdateRelatedEntity(ItemDelivery deliveredItem)
        {
            AvailableItemsRepository aiRepo = new AvailableItemsRepository();
            AvailableItem availableItem = aiRepo.GetAll(ai => ai.Id == deliveredItem.AvailableItemId).FirstOrDefault();

            decimal availableTotal = availableItem.Price * availableItem.AvailableQuantity;
            
            decimal deliveredTotal = deliveredItem.DeliveryPrice * deliveredItem.DeliveredQuantity;
            
            int totalQuantity = availableItem.AvailableQuantity + deliveredItem.DeliveredQuantity;

            decimal newPrice=0;
            if (availableTotal!=0)
            {
                newPrice= (decimal)((availableTotal + deliveredTotal) / totalQuantity);
            }
            else
            {
                newPrice = deliveredItem.DeliveryPrice;
            }

            availableItem.Price = newPrice;
            availableItem.AvailableQuantity += deliveredItem.DeliveredQuantity;
            aiRepo.Save(availableItem);            
        }

       /* [ChildActionOnly]
        public ActionResult EditPartial(EditVM model, FormCollection form)
        {
            ModelState.Clear();

            model = model ?? new EditVM();

            if (model.Id > 0)
            {
                ItemDelivery item = null;
                ItemDeliveriesRepository repo = new ItemDeliveriesRepository();

                item = repo.GetById(model.Id);
                model.PopulateModel(item);
            }

            PopulateEditGetModel(model);

            return PartialView("EditPartial",model);
        }*/
    }
}