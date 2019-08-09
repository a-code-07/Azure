namespace GarageProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abcdkj : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ServiceRequests", "DateAdded");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceRequests", "DateAdded", c => c.DateTime(nullable: false));
        }
    }
}
