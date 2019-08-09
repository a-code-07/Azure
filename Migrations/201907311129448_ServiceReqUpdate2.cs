namespace GarageProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceReqUpdate2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ServiceRequests", "CarService");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceRequests", "CarService", c => c.String());
        }
    }
}
