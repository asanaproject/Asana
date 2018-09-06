namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatRooms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        Desc = c.String(nullable: false, maxLength: 500),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ChatRoomUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ChatRoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChatRooms", t => t.ChatRoomId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ChatRoomId);
            
            CreateTable(
                "dbo.Users",
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
                .ForeignKey("dbo.UserRoles", t => t.UserRole_Id)
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
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ProjectEmail = c.String(nullable: false, maxLength: 50),
                        DashboardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dashboards", t => t.DashboardId, cascadeDelete: true)
                .Index(t => t.DashboardId);
            
            CreateTable(
                "dbo.Columns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 25),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 25),
                        ColumnId = c.Int(nullable: false),
                        KanbanStateId = c.Int(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Columns", t => t.ColumnId, cascadeDelete: true)
                .ForeignKey("dbo.KanbanStates", t => t.KanbanStateId, cascadeDelete: true)
                .Index(t => t.ColumnId)
                .Index(t => t.KanbanStateId);
            
            CreateTable(
                "dbo.KanbanStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dashboards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExtraInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Body = c.String(nullable: false, maxLength: 500),
                        SenderEmail = c.String(nullable: false, maxLength: 100),
                        SendTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false, maxLength: 500),
                        SendTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        ChatRoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ChatRooms", t => t.ChatRoomId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ChatRoomId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserRole_Id", "dbo.UserRoles");
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ChatRoomId", "dbo.ChatRooms");
            DropForeignKey("dbo.Mails", "UserId", "dbo.Users");
            DropForeignKey("dbo.ChatRoomUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersProject", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersProject", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "DashboardId", "dbo.Dashboards");
            DropForeignKey("dbo.Tasks", "KanbanStateId", "dbo.KanbanStates");
            DropForeignKey("dbo.Tasks", "ColumnId", "dbo.Columns");
            DropForeignKey("dbo.Columns", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ChatRoomUsers", "ChatRoomId", "dbo.ChatRooms");
            DropIndex("dbo.Messages", new[] { "ChatRoomId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.Mails", new[] { "UserId" });
            DropIndex("dbo.Tasks", new[] { "KanbanStateId" });
            DropIndex("dbo.Tasks", new[] { "ColumnId" });
            DropIndex("dbo.Columns", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "DashboardId" });
            DropIndex("dbo.UsersProject", new[] { "ProjectId" });
            DropIndex("dbo.UsersProject", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "UserRole_Id" });
            DropIndex("dbo.ChatRoomUsers", new[] { "ChatRoomId" });
            DropIndex("dbo.ChatRoomUsers", new[] { "UserId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.Mails");
            DropTable("dbo.Languages");
            DropTable("dbo.ExtraInfoes");
            DropTable("dbo.Dashboards");
            DropTable("dbo.KanbanStates");
            DropTable("dbo.Tasks");
            DropTable("dbo.Columns");
            DropTable("dbo.Projects");
            DropTable("dbo.UsersProject");
            DropTable("dbo.Users");
            DropTable("dbo.ChatRoomUsers");
            DropTable("dbo.ChatRooms");
        }
    }
}
