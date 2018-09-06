namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_All_Tables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ChatRooms", newName: "ChatRoom");
            RenameTable(name: "dbo.ChatRoomUsers", newName: "ChatRoomUser");
            RenameTable(name: "dbo.Users", newName: "User");
            RenameTable(name: "dbo.Columns", newName: "Column");
            RenameTable(name: "dbo.Projects", newName: "Project");
            RenameTable(name: "dbo.Dashboards", newName: "Dashboard");
            RenameTable(name: "dbo.Tasks", newName: "Task");
            RenameTable(name: "dbo.KanbanStates", newName: "KanbanState");
            RenameTable(name: "dbo.ExtraInfoes", newName: "Customer");
            RenameTable(name: "dbo.Languages", newName: "Language");
            RenameTable(name: "dbo.Mails", newName: "Mail");
            RenameTable(name: "dbo.Messages", newName: "Message");
            RenameTable(name: "dbo.UserRoles", newName: "UserRole");
            CreateTable(
                "dbo.UsersProject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersProject", "UserId", "dbo.User");
            DropForeignKey("dbo.UsersProject", "ProjectId", "dbo.Project");
            DropIndex("dbo.UsersProject", new[] { "ProjectId" });
            DropIndex("dbo.UsersProject", new[] { "UserId" });
            DropTable("dbo.UsersProject");
            RenameTable(name: "dbo.UserRole", newName: "UserRoles");
            RenameTable(name: "dbo.Message", newName: "Messages");
            RenameTable(name: "dbo.Mail", newName: "Mails");
            RenameTable(name: "dbo.Language", newName: "Languages");
            RenameTable(name: "dbo.Customer", newName: "ExtraInfoes");
            RenameTable(name: "dbo.KanbanState", newName: "KanbanStates");
            RenameTable(name: "dbo.Task", newName: "Tasks");
            RenameTable(name: "dbo.Dashboard", newName: "Dashboards");
            RenameTable(name: "dbo.Project", newName: "Projects");
            RenameTable(name: "dbo.Column", newName: "Columns");
            RenameTable(name: "dbo.User", newName: "Users");
            RenameTable(name: "dbo.ChatRoomUser", newName: "ChatRoomUsers");
            RenameTable(name: "dbo.ChatRoom", newName: "ChatRooms");
        }
    }
}
