namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phonenumberlengthchange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Supplier", "PhoneNumber", c => c.String(maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Supplier", "PhoneNumber", c => c.String(maxLength: 10));
        }
    }
}
