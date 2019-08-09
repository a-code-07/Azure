namespace GarageProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateReq : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarServices", "Requested", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarServices", "Requested");
        }
    }
}
