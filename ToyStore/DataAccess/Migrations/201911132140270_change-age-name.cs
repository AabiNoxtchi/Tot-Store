namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeagename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Age", "Name", c => c.String(maxLength: 150));
            DropColumn("dbo.Age", "AgeGroup");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Age", "AgeGroup", c => c.String(maxLength: 150));
            DropColumn("dbo.Age", "Name");
        }
    }
}
