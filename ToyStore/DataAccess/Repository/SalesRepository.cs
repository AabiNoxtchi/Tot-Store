using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SalesRepository:BaseRepository<Sale>
    {
       public override void Save(Sale item)
        {
            if (item.Id <= 0)
                Insert(item);
            else
                Update(item);
        }

        private void Insert(Sale item)
        {
            Items.Add(item);
            AvailableItemsRepository airepo = new AvailableItemsRepository();

            if(item.ItemSales!=null)
            {
                foreach (ItemSale itemSale in item.ItemSales)
                {
                    AvailableItem availableitem = airepo.GetById(itemSale.AvailableItemId);
                    availableitem.AvailableQuantity -= itemSale.SoldQuantity;
                    airepo.Save(availableitem);
                }
            }

            Context.SaveChanges();
        }

        private void Update(Sale item)
        {
            Context.Entry(item).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public override void Delete(Sale item)
        {

            base.Delete(item);
        }
    }
}
