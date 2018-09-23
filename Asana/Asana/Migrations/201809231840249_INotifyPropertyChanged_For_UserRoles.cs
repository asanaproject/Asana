namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class INotifyPropertyChanged_For_UserRoles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsersProject", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRoles", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.UsersProject", "ProjectId", "dbo.Project");
            DropIndex("dbo.UsersProject", new[] { "UserId" });
            DropIndex("dbo.UsersProject", new[] { "ProjectId" });
            AddColumn("dbo.UserRoles", "Project_Id", c => c.Guid());
            AddColumn("dbo.UserRoles", "Project_Id1", c => c.Guid());
            CreateIndex("dbo.UserRoles", "Project_Id");
            CreateIndex("dbo.UserRoles", "Project_Id1");
            AddForeignKey("dbo.UserRoles", "Project_Id", "dbo.Project", "Id");
            AddForeignKey("dbo.UserRoles", "Project_Id1", "dbo.Project", "Id");
            DropTable("dbo.UsersProject");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsersProject",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.UserRoles", "Project_Id1", "dbo.Project");
            DropForeignKey("dbo.UserRoles", "Project_Id", "dbo.Project");
            DropIndex("dbo.UserRoles", new[] { "Project_Id1" });
            DropIndex("dbo.UserRoles", new[] { "Project_Id" });
            DropColumn("dbo.UserRoles", "Project_Id1");
            DropColumn("dbo.UserRoles", "Project_Id");
            CreateIndex("dbo.UsersProject", "ProjectId");
            CreateIndex("dbo.UsersProject", "UserId");
            AddForeignKey("dbo.UsersProject", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRoles", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersProject", "UserId", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
