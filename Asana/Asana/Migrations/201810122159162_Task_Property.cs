namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task_Property : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Task", "CurrentKanbanStateId", "dbo.KanbanState");
            DropIndex("dbo.Task", new[] { "CurrentKanbanStateId" });
            AddColumn("dbo.Task", "CurrentKanbanState", c => c.String());
            DropColumn("dbo.Task", "CurrentKanbanStateId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Task", "CurrentKanbanStateId", c => c.Guid(nullable: false));
            DropColumn("dbo.Task", "CurrentKanbanState");
            CreateIndex("dbo.Task", "CurrentKanbanStateId");
            AddForeignKey("dbo.Task", "CurrentKanbanStateId", "dbo.KanbanState", "Id", cascadeDelete: true);
        }
    }
}
