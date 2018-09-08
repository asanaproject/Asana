namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Resolved_All_Problems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatRoom",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Desc = c.String(nullable: false, maxLength: 500),
                        ChatRoomType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ChatRoomUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ChatRoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChatRoom", t => t.ChatRoomId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ChatRoomId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Image = c.Binary(),
                        Password = c.String(nullable: false),
                        UserRole_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRole", t => t.UserRole_Id)
                .Index(t => t.UserRole_Id);
            
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
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ProjectEmail = c.String(nullable: false, maxLength: 50),
                        DashboardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dashboard", t => t.DashboardId, cascadeDelete: true)
                .Index(t => t.DashboardId);
            
            CreateTable(
                "dbo.Column",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 25),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 25),
                        ColumnId = c.Int(nullable: false),
                        KanbanStateId = c.Int(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Column", t => t.ColumnId, cascadeDelete: true)
                .ForeignKey("dbo.KanbanState", t => t.KanbanStateId, cascadeDelete: true)
                .Index(t => t.ColumnId)
                .Index(t => t.KanbanStateId);
            
            CreateTable(
                "dbo.KanbanState",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dashboard",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Body = c.String(nullable: false, maxLength: 500),
                        SenderEmail = c.String(nullable: false, maxLength: 100),
                        SendTime = c.DateTime(nullable: false),
                        Marked = c.Boolean(nullable: false),
                        Favorite = c.Boolean(nullable: false),
                        BodyHtml = c.Binary(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false, maxLength: 500),
                        SendTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        ChatRoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ChatRoom", t => t.ChatRoomId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ChatRoomId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "UserRole_Id", "dbo.UserRole");
            DropForeignKey("dbo.Message", "UserId", "dbo.User");
            DropForeignKey("dbo.Message", "ChatRoomId", "dbo.ChatRoom");
            DropForeignKey("dbo.Mail", "UserId", "dbo.User");
            DropForeignKey("dbo.ChatRoomUser", "UserId", "dbo.User");
            DropForeignKey("dbo.UsersProject", "UserId", "dbo.User");
            DropForeignKey("dbo.UsersProject", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Project", "DashboardId", "dbo.Dashboard");
            DropForeignKey("dbo.Task", "KanbanStateId", "dbo.KanbanState");
            DropForeignKey("dbo.Task", "ColumnId", "dbo.Column");
            DropForeignKey("dbo.Column", "ProjectId", "dbo.Project");
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
            DropTable("dbo.UserRole");
            DropTable("dbo.Message");
            DropTable("dbo.Mail");
            DropTable("dbo.Language");
            DropTable("dbo.Customer");
            DropTable("dbo.Dashboard");
            DropTable("dbo.KanbanState");
            DropTable("dbo.Task");
            DropTable("dbo.Column");
            DropTable("dbo.Project");
            DropTable("dbo.UsersProject");
            DropTable("dbo.User");
            DropTable("dbo.ChatRoomUser");
            DropTable("dbo.ChatRoom");
        }
    }
}
