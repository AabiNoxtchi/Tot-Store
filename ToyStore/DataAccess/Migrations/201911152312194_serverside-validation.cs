namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serversidevalidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Supplier", "Name", c => c.String(maxLength: 150));
            AlterColumn("dbo.Supplier", "Email", c => c.String(maxLength: 150));
            AlterColumn("dbo.City", "Name", c => c.String(maxLength: 150));
            AlterColumn("dbo.Category", "Name", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Category", "Name", c => c.String());
            AlterColumn("dbo.City", "Name", c => c.String());
            AlterColumn("dbo.Supplier", "Email", c => c.String(maxLength: 200));
            AlterColumn("dbo.Supplier", "Name", c => c.String(maxLength: 200));
        }
    }
}
