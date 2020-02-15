namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class decimalprecision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ItemSale", "SoldPrice", c => c.Decimal(nullable: false, precision: 6, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ItemSale", "SoldPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
