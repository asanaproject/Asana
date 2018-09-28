namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_model : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Project", "DashboardId", "dbo.Dashboard");
            DropForeignKey("dbo.Project", "User_Id", "dbo.User");
            DropIndex("dbo.Project", new[] { "DashboardId" });
            DropIndex("dbo.Project", new[] { "User_Id" });
            RenameColumn(table: "dbo.Project", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Project", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Project", "UserId");
            AddForeignKey("dbo.Project", "UserId", "dbo.User", "Id", cascadeDelete: true);
            DropColumn("dbo.Project", "DashboardId");
            DropTable("dbo.Dashboard");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Dashboard",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Project", "DashboardId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Project", "UserId", "dbo.User");
            DropIndex("dbo.Project", new[] { "UserId" });
            AlterColumn("dbo.Project", "UserId", c => c.Guid());
            RenameColumn(table: "dbo.Project", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.Project", "User_Id");
            CreateIndex("dbo.Project", "DashboardId");
            AddForeignKey("dbo.Project", "User_Id", "dbo.User", "Id");
            AddForeignKey("dbo.Project", "DashboardId", "dbo.Dashboard", "Id", cascadeDelete: true);
        }
    }
}
