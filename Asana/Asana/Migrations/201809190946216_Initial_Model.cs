namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Model : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "UserRole_Id", "dbo.UserRole");
            DropIndex("dbo.User", new[] { "UserRole_Id" });
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Task", "Description", c => c.String());
            AddColumn("dbo.Task", "ExtraInfoId", c => c.Guid());
            AddColumn("dbo.Task", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Task", "StarPath", c => c.String());
            CreateIndex("dbo.Task", "ExtraInfoId");
            AddForeignKey("dbo.Task", "ExtraInfoId", "dbo.Customer", "Id");
            DropColumn("dbo.User", "UserRole_Id");
            DropTable("dbo.UserRole");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "UserRole_Id", c => c.Guid());
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Task", "ExtraInfoId", "dbo.Customer");
            DropIndex("dbo.Task", new[] { "ExtraInfoId" });
            DropIndex("dbo.UserRoles", new[] { "ProjectId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropColumn("dbo.Task", "StarPath");
            DropColumn("dbo.Task", "CreatedAt");
            DropColumn("dbo.Task", "ExtraInfoId");
            DropColumn("dbo.Task", "Description");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            CreateIndex("dbo.User", "UserRole_Id");
            AddForeignKey("dbo.User", "UserRole_Id", "dbo.UserRole", "Id");
        }
    }
}
