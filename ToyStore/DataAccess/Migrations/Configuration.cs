namespace DataAccess.Migrations
{
    using DataAccess.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.Repository.ToyStoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccess.Repository.ToyStoreDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Cities.Any())
            {
                List<City> cities = new List<City>()
                {
                   new City(){ Name ="Varna" ,Suppliers=new List<Supplier>()
                      {
                       new Supplier(){ Name="'Ivan Ivanov'OOD",DDCNumber="12345678901",PhoneNumber="0897011223",Address="Khan Kubrat 5a"},
                       new Supplier(){ Name="'Ilian Ilianov'OOD",DDCNumber="12345678876",PhoneNumber="0897987654",Address="Kniaz Boris 2a"},
                       new Supplier(){ Name="'Sirma Group'OOD",DDCNumber="16205678876",PhoneNumber="0897944444",Address="Angel Kunchev 12"}
                      }
                   },
                   new City(){Name="Sofia"},
                   new City(){Name="Burgas"},
                   new City(){Name="Shumen"},
                   new City(){Name="Ruse"},
                   new City(){Name="Plovdiv"}

                };

                context.Cities.AddRange(cities);

            }

            if (!context.Categories.Any())
            {

                List<Category> categories = new List<Category>()
                {
                    new Category(){Name="Baby Toys"},
                    new Category(){Name="Interactive toys"},
                    new Category(){Name="Puzzles"},
                    new Category(){Name="Dolls"},
                    new Category(){Name="LEGO"},
                    new Category(){Name="Plushy toys"},

                };

                context.Categories.AddRange(categories);

            }

            if (!context.Ages.Any())
            {

                List<Age> Ages = new List<Age>()
                {
                    new Age(){Name="0 to 12 Months"},
                    new Age(){Name="12 to 24 Months"},
                    new Age(){ Name="2 to 4 Years"},
                    new Age(){ Name="5 to 7 Years"},
                    new Age(){ Name="8 to 11 Years"},
                    new Age(){ Name="12 Years & Up "},
                };
                context.Ages.AddRange(Ages);
            }

        }
    }
}
