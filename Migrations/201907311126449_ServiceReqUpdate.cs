namespace GarageProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceReqUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceRequests", "UserId", c => c.String());
            DropColumn("dbo.ServiceRequests", "User");
            DropColumn("dbo.ServiceRequests", "Car");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceRequests", "Car", c => c.String());
            AddColumn("dbo.ServiceRequests", "User", c => c.String());
            DropColumn("dbo.ServiceRequests", "UserId");
        }
    }
}
