namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Model : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Column", newName: "ColumnOfTask");
            DropForeignKey("dbo.UsersProject", "UserId", "dbo.User");
            DropForeignKey("dbo.UsersProject", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.User", "UserRole_Id", "dbo.UserRole");
            DropForeignKey("dbo.ChatRoomUser", "ChatRoomId", "dbo.ChatRoom");
            DropForeignKey("dbo.Message", "ChatRoomId", "dbo.ChatRoom");
            DropForeignKey("dbo.ChatRoomUser", "UserId", "dbo.User");
            DropForeignKey("dbo.Mail", "UserId", "dbo.User");
            DropForeignKey("dbo.Message", "UserId", "dbo.User");
            DropForeignKey("dbo.Column", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Task", "ColumnId", "dbo.Column");
            DropForeignKey("dbo.Task", "KanbanStateId", "dbo.KanbanState");
            DropForeignKey("dbo.Project", "DashboardId", "dbo.Dashboard");
            DropIndex("dbo.User", new[] { "UserRole_Id" });
            DropIndex("dbo.UsersProject", new[] { "UserId" });
            DropIndex("dbo.UsersProject", new[] { "ProjectId" });
            RenameColumn(table: "dbo.User", name: "ProjectId", newName: "Project_Id");
            RenameColumn(table: "dbo.User", name: "UserRole_Id", newName: "UserRoleId");
            DropPrimaryKey("dbo.ChatRoom");
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.Project");
            DropPrimaryKey("dbo.ColumnOfTask");
            DropPrimaryKey("dbo.Task");
            DropPrimaryKey("dbo.KanbanState");
            DropPrimaryKey("dbo.Dashboard");
            DropPrimaryKey("dbo.Customer");
            DropPrimaryKey("dbo.Language");
            DropPrimaryKey("dbo.Mail");
            DropPrimaryKey("dbo.Message");
            DropPrimaryKey("dbo.UserRole");
            AddColumn("dbo.Task", "Description", c => c.String());
            AddColumn("dbo.Task", "ExtraInfoId", c => c.Guid());
            AddColumn("dbo.Task", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.ChatRoom", "ID", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.User", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.User", "UserRoleId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Project", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.ColumnOfTask", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Task", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.KanbanState", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Dashboard", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Customer", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Language", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Mail", "ID", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Message", "ID", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.UserRole", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.ChatRoom", "ID");
            AddPrimaryKey("dbo.User", "Id");
            AddPrimaryKey("dbo.Project", "Id");
            AddPrimaryKey("dbo.ColumnOfTask", "Id");
            AddPrimaryKey("dbo.Task", "Id");
            AddPrimaryKey("dbo.KanbanState", "Id");
            AddPrimaryKey("dbo.Dashboard", "Id");
            AddPrimaryKey("dbo.Customer", "Id");
            AddPrimaryKey("dbo.Language", "Id");
            AddPrimaryKey("dbo.Mail", "ID");
            AddPrimaryKey("dbo.Message", "ID");
            AddPrimaryKey("dbo.UserRole", "Id");
            CreateIndex("dbo.User", "UserRoleId");
            CreateIndex("dbo.User", "Project_Id");
            CreateIndex("dbo.Task", "ExtraInfoId");
            AddForeignKey("dbo.Task", "ExtraInfoId", "dbo.Customer", "Id");
            AddForeignKey("dbo.User", "Project_Id", "dbo.Project", "Id");
            AddForeignKey("dbo.User", "UserRoleId", "dbo.UserRole", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ChatRoomUser", "ChatRoomId", "dbo.ChatRoom", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Message", "ChatRoomId", "dbo.ChatRoom", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ChatRoomUser", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Mail", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Message", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ColumnOfTask", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Task", "ColumnId", "dbo.ColumnOfTask", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Task", "KanbanStateId", "dbo.KanbanState", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Project", "DashboardId", "dbo.Dashboard", "Id", cascadeDelete: true);
            DropColumn("dbo.Customer", "Password");
            DropTable("dbo.UsersProject");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsersProject",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customer", "Password", c => c.String(nullable: false, maxLength: 20));
            DropForeignKey("dbo.Project", "DashboardId", "dbo.Dashboard");
            DropForeignKey("dbo.Task", "KanbanStateId", "dbo.KanbanState");
            DropForeignKey("dbo.Task", "ColumnId", "dbo.ColumnOfTask");
            DropForeignKey("dbo.ColumnOfTask", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Message", "UserId", "dbo.User");
            DropForeignKey("dbo.Mail", "UserId", "dbo.User");
            DropForeignKey("dbo.ChatRoomUser", "UserId", "dbo.User");
            DropForeignKey("dbo.Message", "ChatRoomId", "dbo.ChatRoom");
            DropForeignKey("dbo.ChatRoomUser", "ChatRoomId", "dbo.ChatRoom");
            DropForeignKey("dbo.User", "UserRoleId", "dbo.UserRole");
            DropForeignKey("dbo.User", "Project_Id", "dbo.Project");
            DropForeignKey("dbo.Task", "ExtraInfoId", "dbo.Customer");
            DropIndex("dbo.Task", new[] { "ExtraInfoId" });
            DropIndex("dbo.User", new[] { "Project_Id" });
            DropIndex("dbo.User", new[] { "UserRoleId" });
            DropPrimaryKey("dbo.UserRole");
            DropPrimaryKey("dbo.Message");
            DropPrimaryKey("dbo.Mail");
            DropPrimaryKey("dbo.Language");
            DropPrimaryKey("dbo.Customer");
            DropPrimaryKey("dbo.Dashboard");
            DropPrimaryKey("dbo.KanbanState");
            DropPrimaryKey("dbo.Task");
            DropPrimaryKey("dbo.ColumnOfTask");
            DropPrimaryKey("dbo.Project");
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.ChatRoom");
            AlterColumn("dbo.UserRole", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Message", "ID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Mail", "ID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Language", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Customer", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Dashboard", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.KanbanState", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Task", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.ColumnOfTask", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Project", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.User", "UserRoleId", c => c.Guid());
            AlterColumn("dbo.User", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.ChatRoom", "ID", c => c.Guid(nullable: false));
            DropColumn("dbo.Task", "CreatedAt");
            DropColumn("dbo.Task", "ExtraInfoId");
            DropColumn("dbo.Task", "Description");
            AddPrimaryKey("dbo.UserRole", "Id");
            AddPrimaryKey("dbo.Message", "ID");
            AddPrimaryKey("dbo.Mail", "ID");
            AddPrimaryKey("dbo.Language", "Id");
            AddPrimaryKey("dbo.Customer", "Id");
            AddPrimaryKey("dbo.Dashboard", "Id");
            AddPrimaryKey("dbo.KanbanState", "Id");
            AddPrimaryKey("dbo.Task", "Id");
            AddPrimaryKey("dbo.ColumnOfTask", "Id");
            AddPrimaryKey("dbo.Project", "Id");
            AddPrimaryKey("dbo.User", "Id");
            AddPrimaryKey("dbo.ChatRoom", "ID");
            RenameColumn(table: "dbo.User", name: "UserRoleId", newName: "UserRole_Id");
            RenameColumn(table: "dbo.User", name: "Project_Id", newName: "ProjectId");
            CreateIndex("dbo.UsersProject", "ProjectId");
            CreateIndex("dbo.UsersProject", "UserId");
            CreateIndex("dbo.User", "UserRole_Id");
            AddForeignKey("dbo.Project", "DashboardId", "dbo.Dashboard", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Task", "KanbanStateId", "dbo.KanbanState", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Task", "ColumnId", "dbo.Column", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Column", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Message", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Mail", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ChatRoomUser", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Message", "ChatRoomId", "dbo.ChatRoom", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ChatRoomUser", "ChatRoomId", "dbo.ChatRoom", "ID", cascadeDelete: true);
            AddForeignKey("dbo.User", "UserRole_Id", "dbo.UserRole", "Id");
            AddForeignKey("dbo.UsersProject", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersProject", "UserId", "dbo.User", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.ColumnOfTask", newName: "Column");
        }
    }
}
