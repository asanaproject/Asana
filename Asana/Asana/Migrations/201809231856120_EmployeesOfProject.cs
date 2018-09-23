namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeesOfProject : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoles", "Project_Id", "dbo.Project");
            DropForeignKey("dbo.UserRoles", "Project_Id1", "dbo.Project");
            DropIndex("dbo.UserRoles", new[] { "ProjectId" });
            DropIndex("dbo.UserRoles", new[] { "Project_Id" });
            DropIndex("dbo.UserRoles", new[] { "Project_Id1" });
            DropColumn("dbo.UserRoles", "ProjectId");
            RenameColumn(table: "dbo.UserRoles", name: "Project_Id1", newName: "ProjectId");
            AlterColumn("dbo.UserRoles", "ProjectId", c => c.Guid(nullable: false));
            CreateIndex("dbo.UserRoles", "ProjectId");
            AddForeignKey("dbo.UserRoles", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
            DropColumn("dbo.UserRoles", "Project_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRoles", "Project_Id", c => c.Guid());
            DropForeignKey("dbo.UserRoles", "ProjectId", "dbo.Project");
            DropIndex("dbo.UserRoles", new[] { "ProjectId" });
            AlterColumn("dbo.UserRoles", "ProjectId", c => c.Guid());
            RenameColumn(table: "dbo.UserRoles", name: "ProjectId", newName: "Project_Id1");
            AddColumn("dbo.UserRoles", "ProjectId", c => c.Guid(nullable: false));
            CreateIndex("dbo.UserRoles", "Project_Id1");
            CreateIndex("dbo.UserRoles", "Project_Id");
            CreateIndex("dbo.UserRoles", "ProjectId");
            AddForeignKey("dbo.UserRoles", "Project_Id1", "dbo.Project", "Id");
            AddForeignKey("dbo.UserRoles", "Project_Id", "dbo.Project", "Id");
        }
    }
}
