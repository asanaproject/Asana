namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskLog_Class_IsEdited : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TaskLogs", "TitleIsChanged");
            DropColumn("dbo.TaskLogs", "ImageIsChanged");
            DropColumn("dbo.TaskLogs", "CurrentKanbanStateIsChanged");
            DropColumn("dbo.TaskLogs", "DescriptionIsChanged");
            DropColumn("dbo.TaskLogs", "ExtraInfoIsChanged");
            DropColumn("dbo.TaskLogs", "IsAdded");
            DropColumn("dbo.TaskLogs", "DeadlineIsChanged");
            DropColumn("dbo.TaskLogs", "StarredIsChanged");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskLogs", "StarredIsChanged", c => c.Boolean(nullable: false));
            AddColumn("dbo.TaskLogs", "DeadlineIsChanged", c => c.Boolean(nullable: false));
            AddColumn("dbo.TaskLogs", "IsAdded", c => c.Boolean(nullable: false));
            AddColumn("dbo.TaskLogs", "ExtraInfoIsChanged", c => c.Boolean(nullable: false));
            AddColumn("dbo.TaskLogs", "DescriptionIsChanged", c => c.Boolean(nullable: false));
            AddColumn("dbo.TaskLogs", "CurrentKanbanStateIsChanged", c => c.Boolean(nullable: false));
            AddColumn("dbo.TaskLogs", "ImageIsChanged", c => c.Boolean(nullable: false));
            AddColumn("dbo.TaskLogs", "TitleIsChanged", c => c.Boolean(nullable: false));
        }
    }
}
