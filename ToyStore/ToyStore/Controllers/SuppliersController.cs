using DataAccess.Entity;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToyStore.ViewModels.Suppliers;

namespace ToyStore.Controllers
{
    public class SuppliersController : BaseController<Supplier,SuppliersRepository, FilterVM, IndexVM, EditVM, OrderBy>
    {
       

       public override void PopulateModel(IndexVM model)
        {
            model.Filter.CitiesList =
                new SelectList(base.PopulateSelectList(new CitiesRepository(),new City()), "Value", "Text",model.Filter.CityId);

        }

        protected override void CheckModel(EditVM model, ref bool valid)
        {
            if (model.PhoneNumber != null || model.Email != null || model.Name!=null)
            {
                SuppliersRepository srepo = new SuppliersRepository();
                List<Supplier> suppliers = new List<Supplier>();
                suppliers = srepo.GetAll(s => (string.IsNullOrEmpty(model.Email)||s.Email == model.Email) &&
                (string.IsNullOrEmpty(model.PhoneNumber)||s.PhoneNumber == model.PhoneNumber)&&
                (string.IsNullOrEmpty(model.Name) || s.Name == model.Name));
                if (suppliers != null)
                {
                    if (suppliers.FirstOrDefault(s => s.Email == model.Email) != null)
                    {
                        valid = false;
                        ModelState.AddModelError("Email", " * A Supplier with this Email Address already exists ! !");

                    }
                    else if (suppliers.FirstOrDefault(s => s.PhoneNumber == model.PhoneNumber) != null)
                    {
                        valid = false;
                        ModelState.AddModelError("PhoneNumber", " * A Supplier with this Phone Number Address already exists ! !");

                    }
                    else if (suppliers.FirstOrDefault(s => s.Name == model.Name) != null)
                    {
                        valid = false;
                        ModelState.AddModelError("Name", " * A Supplier with this Name already exists ! !");

                    }
                }
            }

            if ((model.CityId != null && model.NewCity != null) || (model.CityId == null && model.NewCity == null))
            {
                valid = false;
                ModelState.AddModelError("CityId", "You have to choose either an existing City from the list or add a New one !");

            }
        }

        public override void PopulateEditGetModel(EditVM model)
        {
            model.CitiesList =
                new SelectList(base.PopulateSelectList(new CitiesRepository(), new City()), "Value", "Text");
        }

        public override void PopulateEditPostModel(EditVM model)
        {
            if (!string.IsNullOrEmpty(model.NewCity))
            {
                CitiesRepository cRepo = new CitiesRepository();
                City city = cRepo.GetAll(c => c.Name == model.NewCity).FirstOrDefault();
                if (city != null)
                {
                    model.CityId = city.Id;
                }
                else
                {
                    city = new City();
                    city.Name = model.NewCity;
                    cRepo.Save(city);
                    model.CityId = cRepo.GetAll(c => c.Name == model.NewCity).FirstOrDefault().Id;
                }
            }
        }
    }
}