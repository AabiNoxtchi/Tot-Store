using DataAccess.Entity;
using DataAccess.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using ToyStore.ViewModels.ItemSales;
using ToyStore.ViewModels.Shared;

namespace ToyStore.Controllers
{
    public class ItemSalesController : BaseController<ItemSale, ItemSalesRepository, FilterVM, IndexVM, EditVM, OrderBy>
    {
        private List<SelectListItem> PopulateSalesSelectList(List<Sale> items)
        {
            SalesRepository repo = new SalesRepository();

            List<SelectListItem> listItems = new List<SelectListItem>();

            ListItemsAddFirstNull(listItems);
                foreach (Sale item in items)
                {
                    listItems.Add(new SelectListItem()
                    {
                        Text = item.Id.ToString(),
                        Value = item.Id.ToString()

                    });
                }

            return listItems;
        }

        private List<AvailableItem> PopulateAvailableItemsList()
        {
            AvailableItemsRepository repo = new AvailableItemsRepository();
            List<AvailableItem> items = new List<AvailableItem>();
                items=repo.GetAll(a => true, relatedEntity: "Item");
            return items;
        }

        public override void PopulateModel(IndexVM model)
        {

            AvailableItemsRepository repo = new AvailableItemsRepository();
            model.AvailableItems = repo.GetAll(a => true);

            SalesRepository sRepo = new SalesRepository();
            model.SalesItems = sRepo.GetAll(a => true, relatedEntity: "ItemSales.AvailableItem");

            model.Filter.ItemsList =
              new SelectList(base.PopulateSelectList(new ItemsRepository(), new Item()), "Value", "Text", model.Filter.ItemId);

            model.Filter.SaleIdsList =
               new SelectList(PopulateSalesSelectList(model.SalesItems), "value", "Text", model.Filter.SaleId);

        }

        public override void PopulateEditGetModel(EditVM model)
        {
            model.AvailableItemsList = PopulateAvailableItemsList();

        }

        public override void PopulateEditPostModel(EditVM model)
        {
            ItemSalesRepository irepo = new ItemSalesRepository();
            ItemSale itemSale = irepo.GetById(model.Id);
            if (itemSale.Id > 0)
            {
                AvailableItemsRepository airepo = new AvailableItemsRepository();
                AvailableItem availableitem;
                availableitem = airepo.GetById(model.AvailableItemId);
                if (availableitem != null)
                {

                    if (itemSale.AvailableItemId != model.AvailableItemId)
                    {

                        availableitem.AvailableQuantity += model.SoldQuantity;
                        airepo.Save(availableitem);
                    }
                    else if (itemSale.SoldQuantity != model.SoldQuantity)
                    {

                        int changedQuantity = 0;
                        if (itemSale.SoldQuantity > model.SoldQuantity)
                        {
                            changedQuantity = itemSale.SoldQuantity - model.SoldQuantity;
                            availableitem.AvailableQuantity += changedQuantity;
                        }
                        else
                        {
                            changedQuantity = model.SoldQuantity - itemSale.SoldQuantity;
                            availableitem.AvailableQuantity -= changedQuantity;
                        }

                        airepo.Save(availableitem);
                    }
                }
            }
        }

        protected override void UpdateRelatedEntity(ItemSale SoldItem)
        {
            
        }

        protected override void CheckRelatedEntitiesForDelete(ItemSale item)
        {
            AvailableItemsRepository airepo = new AvailableItemsRepository();
            AvailableItem availableitem;
            availableitem = airepo.GetById(item.AvailableItemId);

            if (item != null)
            {
                availableitem.AvailableQuantity += item.SoldQuantity;
                airepo.Save(availableitem);

            }
        }

        public ActionResult SaveSale(DateTime date, ItemSale[] itemSales)
        {
            string result = "Error! Order Is Not Complete!";
            if (date != null && itemSales != null)
            {

                Sale sale = new Sale();
                SalesRepository sRepo = new SalesRepository();

                sale.Date = date;
                SalesRepository srepo = new SalesRepository();


                sale.ItemSales = new List<ItemSale>();
                foreach (var model in itemSales)
                {

                    ItemSale item = new ItemSale();
                    item.AvailableItemId = model.AvailableItemId;
                    item.SoldQuantity = model.SoldQuantity;
                    item.SoldPrice = model.SoldPrice;
                    sale.ItemSales.Add(item);
                }
                try
                {
                    //further in repository serverside side logic
                    srepo.Save(sale);
                    result = "Success! Order Is Complete!";
                }
                catch (Exception e) { result = e.Data.ToString(); }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSale(int Id)
        {
            SalesRepository srepo = new SalesRepository();
            ItemSalesRepository isrepo = new ItemSalesRepository();
            Sale item = new Sale();
            item=srepo.GetById(Id);
            if (item.ItemSales == null)
            {
                item.ItemSales = isrepo.GetAll(i => i.SaleId == Id);
            }
            if(item.ItemSales!=null)
            {
                AvailableItemsRepository airepo = new AvailableItemsRepository();
                foreach (ItemSale itemSale in item.ItemSales)
                {
                  
                    AvailableItem availableitem = airepo.GetById(itemSale.AvailableItemId);
                    availableitem.AvailableQuantity += itemSale.SoldQuantity;
                    airepo.Save(availableitem);
                    isrepo.Delete(itemSale);
                }
            }
            srepo.Delete(item);
            return RedirectToAction("Index");
        }

      
    }
}
