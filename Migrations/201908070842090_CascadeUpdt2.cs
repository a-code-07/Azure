namespace GarageProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeUpdt2 : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Cars", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
        }
    }
}
