namespace GarageProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeUpdt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "ApplicationUserId", "dbo.AspNetUsers");
        }
        
        public override void Down()
        {
        }
    }
}
