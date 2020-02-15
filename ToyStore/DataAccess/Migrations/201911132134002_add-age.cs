namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Age",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgeGroup = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Item", "AgeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Item", "AgeId");
            AddForeignKey("dbo.Item", "AgeId", "dbo.Age", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Item", "AgeId", "dbo.Age");
            DropIndex("dbo.Item", new[] { "AgeId" });
            DropColumn("dbo.Item", "AgeId");
            DropTable("dbo.Age");
        }
    }
}
