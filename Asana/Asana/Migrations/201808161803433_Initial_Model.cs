namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Model : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Dashboards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        CompanyName = c.String(nullable: false, maxLength: 50),
                        Image = c.Binary(),
                        PhoneNumber = c.String(maxLength: 20),
                        Password = c.String(nullable: false),
                        CompanySize = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserUserRoles",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        UserRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.UserRole_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserRoles", t => t.UserRole_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.UserRole_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserUserRoles", "UserRole_Id", "dbo.UserRoles");
            DropForeignKey("dbo.UserUserRoles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Tasks", "KanbanStateId", "dbo.KanbanStates");
            DropForeignKey("dbo.Tasks", "ColumnId", "dbo.Columns");
            DropForeignKey("dbo.Columns", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "DashboardId", "dbo.Dashboards");
            DropIndex("dbo.UserUserRoles", new[] { "UserRole_Id" });
            DropIndex("dbo.UserUserRoles", new[] { "User_Id" });
            DropIndex("dbo.Tasks", new[] { "KanbanStateId" });
            DropIndex("dbo.Tasks", new[] { "ColumnId" });
            DropIndex("dbo.Projects", new[] { "DashboardId" });
            DropIndex("dbo.Columns", new[] { "ProjectId" });
            DropTable("dbo.UserUserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Languages");
            DropTable("dbo.ExtraInfoes");
            DropTable("dbo.KanbanStates");
            DropTable("dbo.Tasks");
            DropTable("dbo.Dashboards");
            DropTable("dbo.Projects");
            DropTable("dbo.Columns");
        }
    }
}
