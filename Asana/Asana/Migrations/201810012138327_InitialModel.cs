namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRoles", "Task_Id", c => c.Guid());
            CreateIndex("dbo.UserRoles", "Task_Id");
            AddForeignKey("dbo.UserRoles", "Task_Id", "dbo.Task", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Task_Id", "dbo.Task");
            DropIndex("dbo.UserRoles", new[] { "Task_Id" });
            DropColumn("dbo.UserRoles", "Task_Id");
        }
    }
}
