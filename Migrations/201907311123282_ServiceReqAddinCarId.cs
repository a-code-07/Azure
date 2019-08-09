namespace GarageProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceReqAddinCarId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceRequests", "CarId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceRequests", "CarId");
        }
    }
}
