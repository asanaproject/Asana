namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newGuid : DbMigration
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
            DropIndex("dbo.ChatRoomUser", new[] { "UserId" });
            DropIndex("dbo.ChatRoomUser", new[] { "ChatRoomId" });
            DropIndex("dbo.User", new[] { "UserRole_Id" });
            DropIndex("dbo.UsersProject", new[] { "UserId" });
            DropIndex("dbo.UsersProject", new[] { "ProjectId" });
            DropIndex("dbo.Project", new[] { "DashboardId" });
            DropIndex("dbo.Column", new[] { "ProjectId" });
            DropIndex("dbo.Task", new[] { "ColumnId" });
            DropIndex("dbo.Task", new[] { "KanbanStateId" });
            DropIndex("dbo.Mail", new[] { "UserId" });
            DropIndex("dbo.Message", new[] { "UserId" });
            DropIndex("dbo.Message", new[] { "ChatRoomId" });
            DropPrimaryKey("dbo.ChatRoom");
            DropPrimaryKey("dbo.ChatRoomUser");
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.UsersProject");
            DropPrimaryKey("dbo.Project");
            DropPrimaryKey("dbo.Column");
            DropPrimaryKey("dbo.Task");
            DropPrimaryKey("dbo.KanbanState");
            DropPrimaryKey("dbo.Dashboard");
            DropPrimaryKey("dbo.Customer");
            DropPrimaryKey("dbo.Language");
            DropPrimaryKey("dbo.Mail");
            DropPrimaryKey("dbo.UserRole");
            AddColumn("dbo.Column", "IsColumnAdded", c => c.Boolean(nullable: false));
            AddColumn("dbo.Task", "IsTaskAdded", c => c.Boolean(nullable: false));
            AddColumn("dbo.Task", "IsStarred", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ChatRoom", "ID", c => c.Guid(nullable: false));
            AlterColumn("dbo.ChatRoomUser", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.ChatRoomUser", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.ChatRoomUser", "ChatRoomId", c => c.Guid(nullable: false));
            AlterColumn("dbo.User", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.User", "UserRole_Id", c => c.Guid());
            AlterColumn("dbo.UsersProject", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.UsersProject", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.UsersProject", "ProjectId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Project", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Project", "DashboardId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Column", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Column", "ProjectId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Task", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Task", "ColumnId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Task", "KanbanStateId", c => c.Guid(nullable: false));
            AlterColumn("dbo.KanbanState", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Dashboard", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Customer", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Language", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Mail", "ID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Mail", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Message", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Message", "ChatRoomId", c => c.Guid(nullable: false));
            AlterColumn("dbo.UserRole", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.ChatRoom", "ID");
            AddPrimaryKey("dbo.ChatRoomUser", "Id");
            AddPrimaryKey("dbo.User", "Id");
            AddPrimaryKey("dbo.UsersProject", "Id");
            AddPrimaryKey("dbo.Project", "Id");
            AddPrimaryKey("dbo.Column", "Id");
            AddPrimaryKey("dbo.Task", "Id");
            AddPrimaryKey("dbo.KanbanState", "Id");
            AddPrimaryKey("dbo.Dashboard", "Id");
            AddPrimaryKey("dbo.Customer", "Id");
            AddPrimaryKey("dbo.Language", "Id");
            AddPrimaryKey("dbo.Mail", "ID");
            AddPrimaryKey("dbo.UserRole", "Id");
            CreateIndex("dbo.ChatRoomUser", "UserId");
            CreateIndex("dbo.ChatRoomUser", "ChatRoomId");
            CreateIndex("dbo.User", "UserRole_Id");
            CreateIndex("dbo.UsersProject", "UserId");
            CreateIndex("dbo.UsersProject", "ProjectId");
            CreateIndex("dbo.Project", "DashboardId");
            CreateIndex("dbo.Column", "ProjectId");
            CreateIndex("dbo.Task", "ColumnId");
            CreateIndex("dbo.Task", "KanbanStateId");
            CreateIndex("dbo.Mail", "UserId");
            CreateIndex("dbo.Message", "UserId");
            CreateIndex("dbo.Message", "ChatRoomId");
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
            DropIndex("dbo.Message", new[] { "ChatRoomId" });
            DropIndex("dbo.Message", new[] { "UserId" });
            DropIndex("dbo.Mail", new[] { "UserId" });
            DropIndex("dbo.Task", new[] { "KanbanStateId" });
            DropIndex("dbo.Task", new[] { "ColumnId" });
            DropIndex("dbo.Column", new[] { "ProjectId" });
            DropIndex("dbo.Project", new[] { "DashboardId" });
            DropIndex("dbo.UsersProject", new[] { "ProjectId" });
            DropIndex("dbo.UsersProject", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "UserRole_Id" });
            DropIndex("dbo.ChatRoomUser", new[] { "ChatRoomId" });
            DropIndex("dbo.ChatRoomUser", new[] { "UserId" });
            DropPrimaryKey("dbo.UserRole");
            DropPrimaryKey("dbo.Mail");
            DropPrimaryKey("dbo.Language");
            DropPrimaryKey("dbo.Customer");
            DropPrimaryKey("dbo.Dashboard");
            DropPrimaryKey("dbo.KanbanState");
            DropPrimaryKey("dbo.Task");
            DropPrimaryKey("dbo.Column");
            DropPrimaryKey("dbo.Project");
            DropPrimaryKey("dbo.UsersProject");
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.ChatRoomUser");
            DropPrimaryKey("dbo.ChatRoom");
            AlterColumn("dbo.UserRole", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Message", "ChatRoomId", c => c.Int(nullable: false));
            AlterColumn("dbo.Message", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Mail", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Mail", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Language", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Customer", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Dashboard", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.KanbanState", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Task", "KanbanStateId", c => c.Int(nullable: false));
            AlterColumn("dbo.Task", "ColumnId", c => c.Int(nullable: false));
            AlterColumn("dbo.Task", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Column", "ProjectId", c => c.Int(nullable: false));
            AlterColumn("dbo.Column", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Project", "DashboardId", c => c.Int(nullable: false));
            AlterColumn("dbo.Project", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UsersProject", "ProjectId", c => c.Int(nullable: false));
            AlterColumn("dbo.UsersProject", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UsersProject", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.User", "UserRole_Id", c => c.Int());
            AlterColumn("dbo.User", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ChatRoomUser", "ChatRoomId", c => c.Int(nullable: false));
            AlterColumn("dbo.ChatRoomUser", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.ChatRoomUser", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ChatRoom", "ID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Task", "IsStarred");
            DropColumn("dbo.Task", "IsTaskAdded");
            DropColumn("dbo.Column", "IsColumnAdded");
            AddPrimaryKey("dbo.UserRole", "Id");
            AddPrimaryKey("dbo.Mail", "ID");
            AddPrimaryKey("dbo.Language", "Id");
            AddPrimaryKey("dbo.Customer", "Id");
            AddPrimaryKey("dbo.Dashboard", "Id");
            AddPrimaryKey("dbo.KanbanState", "Id");
            AddPrimaryKey("dbo.Task", "Id");
            AddPrimaryKey("dbo.Column", "Id");
            AddPrimaryKey("dbo.Project", "Id");
            AddPrimaryKey("dbo.UsersProject", "Id");
            AddPrimaryKey("dbo.User", "Id");
            AddPrimaryKey("dbo.ChatRoomUser", "Id");
            AddPrimaryKey("dbo.ChatRoom", "ID");
            CreateIndex("dbo.Message", "ChatRoomId");
            CreateIndex("dbo.Message", "UserId");
            CreateIndex("dbo.Mail", "UserId");
            CreateIndex("dbo.Task", "KanbanStateId");
            CreateIndex("dbo.Task", "ColumnId");
            CreateIndex("dbo.Column", "ProjectId");
            CreateIndex("dbo.Project", "DashboardId");
            CreateIndex("dbo.UsersProject", "ProjectId");
            CreateIndex("dbo.UsersProject", "UserId");
            CreateIndex("dbo.User", "UserRole_Id");
            CreateIndex("dbo.ChatRoomUser", "ChatRoomId");
            CreateIndex("dbo.ChatRoomUser", "UserId");
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
