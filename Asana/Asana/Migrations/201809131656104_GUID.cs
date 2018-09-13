namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GUID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Column", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.UsersProject", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Task", "ColumnId", "dbo.Column");
            DropForeignKey("dbo.Project", "DashboardId", "dbo.Dashboard");
            DropIndex("dbo.UsersProject", new[] { "ProjectId" });
            DropIndex("dbo.Project", new[] { "DashboardId" });
            DropIndex("dbo.Column", new[] { "ProjectId" });
            DropIndex("dbo.Task", new[] { "ColumnId" });
            DropPrimaryKey("dbo.Project");
            DropPrimaryKey("dbo.Column");
            DropPrimaryKey("dbo.Task");
            DropPrimaryKey("dbo.Dashboard");
            AddColumn("dbo.Column", "IsColumnAdded", c => c.Boolean(nullable: false));
            AddColumn("dbo.Task", "IsTaskAdded", c => c.Boolean(nullable: false));
            AddColumn("dbo.Task", "IsStarred", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UsersProject", "ProjectId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Project", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Project", "DashboardId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Column", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Column", "ProjectId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Task", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Task", "ColumnId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Dashboard", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Project", "Id");
            AddPrimaryKey("dbo.Column", "Id");
            AddPrimaryKey("dbo.Task", "Id");
            AddPrimaryKey("dbo.Dashboard", "Id");
            CreateIndex("dbo.UsersProject", "ProjectId");
            CreateIndex("dbo.Project", "DashboardId");
            CreateIndex("dbo.Column", "ProjectId");
            CreateIndex("dbo.Task", "ColumnId");
            AddForeignKey("dbo.Column", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersProject", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Task", "ColumnId", "dbo.Column", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Project", "DashboardId", "dbo.Dashboard", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project", "DashboardId", "dbo.Dashboard");
            DropForeignKey("dbo.Task", "ColumnId", "dbo.Column");
            DropForeignKey("dbo.UsersProject", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.Column", "ProjectId", "dbo.Project");
            DropIndex("dbo.Task", new[] { "ColumnId" });
            DropIndex("dbo.Column", new[] { "ProjectId" });
            DropIndex("dbo.Project", new[] { "DashboardId" });
            DropIndex("dbo.UsersProject", new[] { "ProjectId" });
            DropPrimaryKey("dbo.Dashboard");
            DropPrimaryKey("dbo.Task");
            DropPrimaryKey("dbo.Column");
            DropPrimaryKey("dbo.Project");
            AlterColumn("dbo.Dashboard", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Task", "ColumnId", c => c.Int(nullable: false));
            AlterColumn("dbo.Task", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Column", "ProjectId", c => c.Int(nullable: false));
            AlterColumn("dbo.Column", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Project", "DashboardId", c => c.Int(nullable: false));
            AlterColumn("dbo.Project", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UsersProject", "ProjectId", c => c.Int(nullable: false));
            DropColumn("dbo.Task", "IsStarred");
            DropColumn("dbo.Task", "IsTaskAdded");
            DropColumn("dbo.Column", "IsColumnAdded");
            AddPrimaryKey("dbo.Dashboard", "Id");
            AddPrimaryKey("dbo.Task", "Id");
            AddPrimaryKey("dbo.Column", "Id");
            AddPrimaryKey("dbo.Project", "Id");
            CreateIndex("dbo.Task", "ColumnId");
            CreateIndex("dbo.Column", "ProjectId");
            CreateIndex("dbo.Project", "DashboardId");
            CreateIndex("dbo.UsersProject", "ProjectId");
            AddForeignKey("dbo.Project", "DashboardId", "dbo.Dashboard", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Task", "ColumnId", "dbo.Column", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersProject", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Column", "ProjectId", "dbo.Project", "Id", cascadeDelete: true);
        }
    }
}
