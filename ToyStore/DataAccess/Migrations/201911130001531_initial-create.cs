namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AvailableItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 6, scale: 2),
                        AvailableQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdentificationKod = c.String(maxLength: 100),
                        Name = c.String(maxLength: 150),
                        Description = c.String(),
                        CategoryId = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemDelivery",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AvailableItemId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        DeliveredQuantity = c.Int(nullable: false),
                        DeliveryPrice = c.Decimal(nullable: false, precision: 6, scale: 2),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AvailableItem", t => t.AvailableItemId, cascadeDelete: true)
                .ForeignKey("dbo.Supplier", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.AvailableItemId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        DDCNumber = c.String(maxLength: 11),
                        PhoneNumber = c.String(maxLength: 10),
                        Email = c.String(maxLength: 200),
                        Address = c.String(maxLength: 150),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemSale",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AvailableItemId = c.Int(nullable: false),
                        SaleId = c.Int(nullable: false),
                        SoldQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AvailableItem", t => t.AvailableItemId, cascadeDelete: true)
                .ForeignKey("dbo.Sale", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.AvailableItemId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.Sale",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemSale", "SaleId", "dbo.Sale");
            DropForeignKey("dbo.ItemSale", "AvailableItemId", "dbo.AvailableItem");
            DropForeignKey("dbo.ItemDelivery", "SupplierId", "dbo.Supplier");
            DropForeignKey("dbo.Supplier", "CityId", "dbo.City");
            DropForeignKey("dbo.ItemDelivery", "AvailableItemId", "dbo.AvailableItem");
            DropForeignKey("dbo.Item", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.AvailableItem", "ItemId", "dbo.Item");
            DropIndex("dbo.ItemSale", new[] { "SaleId" });
            DropIndex("dbo.ItemSale", new[] { "AvailableItemId" });
            DropIndex("dbo.Supplier", new[] { "CityId" });
            DropIndex("dbo.ItemDelivery", new[] { "SupplierId" });
            DropIndex("dbo.ItemDelivery", new[] { "AvailableItemId" });
            DropIndex("dbo.Item", new[] { "CategoryId" });
            DropIndex("dbo.AvailableItem", new[] { "ItemId" });
            DropTable("dbo.Sale");
            DropTable("dbo.ItemSale");
            DropTable("dbo.City");
            DropTable("dbo.Supplier");
            DropTable("dbo.ItemDelivery");
            DropTable("dbo.Category");
            DropTable("dbo.Item");
            DropTable("dbo.AvailableItem");
        }
    }
}
