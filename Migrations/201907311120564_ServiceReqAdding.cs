namespace GarageProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceReqAdding : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User = c.String(),
                        CarService = c.String(),
                        Miles = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Details = c.String(),
                        ServiceType = c.String(),
                        Car = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ServiceRequests");
        }
    }
}
