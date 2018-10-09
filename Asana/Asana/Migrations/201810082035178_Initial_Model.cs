namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatRoom",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Desc = c.String(nullable: false, maxLength: 500),
                        ChatRoomType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ChatRoomUser",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ChatRoomId = c.Guid(nullable: false),
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
                        Id = c.Guid(nullable: false),
                        FullName = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        ProjectEmail = c.String(nullable: false, maxLength: 50),
                        UserId = c.Guid(nullable: false),
                        Deadline = c.DateTime(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        ProjectManager = c.String(),
                        Description = c.String(),
                        Position = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Column",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        IsColumnAdded = c.Boolean(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Position = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        ColumnId = c.Guid(nullable: false),
                        Position = c.Int(nullable: false),
                        Image = c.Binary(),
                        AssignedTo = c.String(),
                        Description = c.String(),
                        Deadline = c.DateTime(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        StarPath = c.String(),
                        IsTaskAdded = c.Boolean(nullable: false),
                        IsStarred = c.Boolean(nullable: false),
                        CurrentKanbanState_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Column", t => t.ColumnId, cascadeDelete: true)
                .ForeignKey("dbo.KanbanState", t => t.CurrentKanbanState_Id)
                .Index(t => t.ColumnId)
                .Index(t => t.CurrentKanbanState_Id);
            
            CreateTable(
                "dbo.KanbanState",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Task_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Task", t => t.Task_Id)
                .Index(t => t.Task_Id);
            
            CreateTable(
                "dbo.TaskKanbanStates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        KanbanStateId = c.Guid(nullable: false),
                        ChangedBy = c.String(),
                        TaskId = c.Guid(nullable: false),
                        Date = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KanbanState", t => t.KanbanStateId, cascadeDelete: true)
                .ForeignKey("dbo.Task", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.KanbanStateId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.TaskLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TaskId = c.Guid(nullable: false),
                        Message = c.String(),
                        ChangedBy = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        TitleIsChanged = c.Boolean(nullable: false),
                        ImageIsChanged = c.Boolean(nullable: false),
                        CurrentKanbanStateIsChanged = c.Boolean(nullable: false),
                        DescriptionIsChanged = c.Boolean(nullable: false),
                        ExtraInfoIsChanged = c.Boolean(nullable: false),
                        IsAdded = c.Boolean(nullable: false),
                        DeadlineIsChanged = c.Boolean(nullable: false),
                        StarredIsChanged = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Task", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                        FullName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mail",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        Body = c.String(nullable: false, maxLength: 500),
                        SenderEmail = c.String(nullable: false, maxLength: 100),
                        SendTime = c.DateTime(nullable: false),
                        Marked = c.Boolean(nullable: false),
                        Favorite = c.Boolean(nullable: false),
                        BodyHtml = c.Binary(),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Body = c.String(nullable: false, maxLength: 500),
                        SendTime = c.DateTime(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ChatRoomId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ChatRoom", t => t.ChatRoomId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ChatRoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "UserId", "dbo.User");
            DropForeignKey("dbo.Message", "ChatRoomId", "dbo.ChatRoom");
            DropForeignKey("dbo.Mail", "UserId", "dbo.User");
            DropForeignKey("dbo.ChatRoomUser", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRoles", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Project", "UserId", "dbo.User");
            DropForeignKey("dbo.TaskLogs", "TaskId", "dbo.Task");
            DropForeignKey("dbo.TaskKanbanStates", "TaskId", "dbo.Task");
            DropForeignKey("dbo.TaskKanbanStates", "KanbanStateId", "dbo.KanbanState");
            DropForeignKey("dbo.KanbanState", "Task_Id", "dbo.Task");
            DropForeignKey("dbo.Task", "CurrentKanbanState_Id", "dbo.KanbanState");
            DropForeignKey("dbo.Task", "ColumnId", "dbo.Column");
            DropForeignKey("dbo.Column", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.ChatRoomUser", "ChatRoomId", "dbo.ChatRoom");
            DropIndex("dbo.Message", new[] { "ChatRoomId" });
            DropIndex("dbo.Message", new[] { "UserId" });
            DropIndex("dbo.Mail", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "ProjectId" });
            DropIndex("dbo.TaskLogs", new[] { "TaskId" });
            DropIndex("dbo.TaskKanbanStates", new[] { "TaskId" });
            DropIndex("dbo.TaskKanbanStates", new[] { "KanbanStateId" });
            DropIndex("dbo.KanbanState", new[] { "Task_Id" });
            DropIndex("dbo.Task", new[] { "CurrentKanbanState_Id" });
            DropIndex("dbo.Task", new[] { "ColumnId" });
            DropIndex("dbo.Column", new[] { "ProjectId" });
            DropIndex("dbo.Project", new[] { "UserId" });
            DropIndex("dbo.ChatRoomUser", new[] { "ChatRoomId" });
            DropIndex("dbo.ChatRoomUser", new[] { "UserId" });
            DropTable("dbo.Message");
            DropTable("dbo.Mail");
            DropTable("dbo.Language");
            DropTable("dbo.UserRoles");
            DropTable("dbo.TaskLogs");
            DropTable("dbo.TaskKanbanStates");
            DropTable("dbo.KanbanState");
            DropTable("dbo.Task");
            DropTable("dbo.Column");
            DropTable("dbo.Project");
            DropTable("dbo.User");
            DropTable("dbo.ChatRoomUser");
            DropTable("dbo.ChatRoom");
        }
    }
}
