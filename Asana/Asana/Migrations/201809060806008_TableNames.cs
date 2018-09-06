namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ChatRooms", newName: "ChatRoom");
            RenameTable(name: "dbo.ChatRoomUsers", newName: "ChatRoomUser");
            RenameTable(name: "dbo.Users", newName: "User");
            RenameTable(name: "dbo.Projects", newName: "Project");
            RenameTable(name: "dbo.Columns", newName: "Column");
            RenameTable(name: "dbo.Tasks", newName: "Task");
            RenameTable(name: "dbo.KanbanStates", newName: "KanbanState");
            RenameTable(name: "dbo.Dashboards", newName: "Dashboard");
            RenameTable(name: "dbo.ExtraInfoes", newName: "Customer");
            RenameTable(name: "dbo.Languages", newName: "Language");
            RenameTable(name: "dbo.Mails", newName: "Mail");
            RenameTable(name: "dbo.Messages", newName: "Message");
            RenameTable(name: "dbo.UserRoles", newName: "UserRole");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserRole", newName: "UserRoles");
            RenameTable(name: "dbo.Message", newName: "Messages");
            RenameTable(name: "dbo.Mail", newName: "Mails");
            RenameTable(name: "dbo.Language", newName: "Languages");
            RenameTable(name: "dbo.Customer", newName: "ExtraInfoes");
            RenameTable(name: "dbo.Dashboard", newName: "Dashboards");
            RenameTable(name: "dbo.KanbanState", newName: "KanbanStates");
            RenameTable(name: "dbo.Task", newName: "Tasks");
            RenameTable(name: "dbo.Column", newName: "Columns");
            RenameTable(name: "dbo.Project", newName: "Projects");
            RenameTable(name: "dbo.User", newName: "Users");
            RenameTable(name: "dbo.ChatRoomUser", newName: "ChatRoomUsers");
            RenameTable(name: "dbo.ChatRoom", newName: "ChatRooms");
        }
    }
}
