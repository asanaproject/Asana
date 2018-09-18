namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Guid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChatRoomUser", "ChatRoomId", "dbo.ChatRoom");
            DropForeignKey("dbo.Message", "ChatRoomId", "dbo.ChatRoom");
            DropForeignKey("dbo.UsersProject", "UserId", "dbo.User");
            DropForeignKey("dbo.ChatRoomUser", "UserId", "dbo.User");
            DropForeignKey("dbo.Mail", "UserId", "dbo.User");
            DropForeignKey("dbo.Message", "UserId", "dbo.User");
            DropForeignKey("dbo.Column", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.UsersProject", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Task", "ColumnId", "dbo.Column");
            DropForeignKey("dbo.Task", "KanbanStateId", "dbo.KanbanState");
            DropForeignKey("dbo.Project", "DashboardId", "dbo.Dashboard");
            DropForeignKey("dbo.User", "UserRole_Id", "dbo.UserRole");
            DropPrimaryKey("dbo.ChatRoom");
            DropPrimaryKey("dbo.ChatRoomUser");
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.Project");
            DropPrimaryKey("dbo.Column");
            DropPrimaryKey("dbo.Task");
            DropPrimaryKey("dbo.KanbanState");
            DropPrimaryKey("dbo.Dashboard");
            DropPrimaryKey("dbo.Customer");
            DropPrimaryKey("dbo.Language");
            DropPrimaryKey("dbo.Mail");
            DropPrimaryKey("dbo.Message");
            DropPrimaryKey("dbo.UserRole");
            AlterColumn("dbo.ChatRoom", "ID", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.ChatRoomUser", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.User", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Project", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Column", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Task", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.KanbanState", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Dashboard", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Customer", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Language", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Mail", "ID", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Message", "ID", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.UserRole", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.ChatRoom", "ID");
            AddPrimaryKey("dbo.ChatRoomUser", "Id");
            AddPrimaryKey("dbo.User", "Id");
            AddPrimaryKey("dbo.Project", "Id");
            AddPrimaryKey("dbo.Column", "Id");
            AddPrimaryKey("dbo.Task", "Id");
            AddPrimaryKey("dbo.KanbanState", "Id");
            AddPrimaryKey("dbo.Dashboard", "Id");
            AddPrimaryKey("dbo.Customer", "Id");
            AddPrimaryKey("dbo.Language", "Id");
            AddPrimaryKey("dbo.Mail", "ID");
            AddPrimaryKey("dbo.Message", "ID");
            AddPrimaryKey("dbo.UserRole", "Id");
            AddForeignKey("dbo.ChatRoomUser", "ChatRoomId", "dbo.ChatRoom", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Message", "ChatRoomId", "dbo.ChatRoom", "ID", cascadeDelete: true);
            AddForeignKey("dbo.UsersProject", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ChatRoomUser", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Mail", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Message", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Column", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersProject", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Task", "ColumnId", "dbo.Column", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Task", "KanbanStateId", "dbo.KanbanState", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Project", "DashboardId", "dbo.Dashboard", "Id", cascadeDelete: true);
            AddForeignKey("dbo.User", "UserRole_Id", "dbo.UserRole", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "UserRole_Id", "dbo.UserRole");
            DropForeignKey("dbo.Project", "DashboardId", "dbo.Dashboard");
            DropForeignKey("dbo.Task", "KanbanStateId", "dbo.KanbanState");
            DropForeignKey("dbo.Task", "ColumnId", "dbo.Column");
            DropForeignKey("dbo.UsersProject", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Column", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Message", "UserId", "dbo.User");
            DropForeignKey("dbo.Mail", "UserId", "dbo.User");
            DropForeignKey("dbo.ChatRoomUser", "UserId", "dbo.User");
            DropForeignKey("dbo.UsersProject", "UserId", "dbo.User");
            DropForeignKey("dbo.Message", "ChatRoomId", "dbo.ChatRoom");
            DropForeignKey("dbo.ChatRoomUser", "ChatRoomId", "dbo.ChatRoom");
            DropPrimaryKey("dbo.UserRole");
            DropPrimaryKey("dbo.Message");
            DropPrimaryKey("dbo.Mail");
            DropPrimaryKey("dbo.Language");
            DropPrimaryKey("dbo.Customer");
            DropPrimaryKey("dbo.Dashboard");
            DropPrimaryKey("dbo.KanbanState");
            DropPrimaryKey("dbo.Task");
            DropPrimaryKey("dbo.Column");
            DropPrimaryKey("dbo.Project");
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.ChatRoomUser");
            DropPrimaryKey("dbo.ChatRoom");
            AlterColumn("dbo.UserRole", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Message", "ID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Mail", "ID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Language", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Customer", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Dashboard", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.KanbanState", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Task", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Column", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Project", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.User", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.ChatRoomUser", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.ChatRoom", "ID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.UserRole", "Id");
            AddPrimaryKey("dbo.Message", "ID");
            AddPrimaryKey("dbo.Mail", "ID");
            AddPrimaryKey("dbo.Language", "Id");
            AddPrimaryKey("dbo.Customer", "Id");
            AddPrimaryKey("dbo.Dashboard", "Id");
            AddPrimaryKey("dbo.KanbanState", "Id");
            AddPrimaryKey("dbo.Task", "Id");
            AddPrimaryKey("dbo.Column", "Id");
            AddPrimaryKey("dbo.Project", "Id");
            AddPrimaryKey("dbo.User", "Id");
            AddPrimaryKey("dbo.ChatRoomUser", "Id");
            AddPrimaryKey("dbo.ChatRoom", "ID");
            AddForeignKey("dbo.User", "UserRole_Id", "dbo.UserRole", "Id");
            AddForeignKey("dbo.Project", "DashboardId", "dbo.Dashboard", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Task", "KanbanStateId", "dbo.KanbanState", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Task", "ColumnId", "dbo.Column", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersProject", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Column", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Message", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Mail", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ChatRoomUser", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersProject", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Message", "ChatRoomId", "dbo.ChatRoom", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ChatRoomUser", "ChatRoomId", "dbo.ChatRoom", "ID", cascadeDelete: true);
        }
    }
}
