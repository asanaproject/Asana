namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task_Properties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Task", "CurrentKanbanState_Id", "dbo.KanbanState");
            DropIndex("dbo.Task", new[] { "CurrentKanbanState_Id" });
            RenameColumn(table: "dbo.Task", name: "CurrentKanbanState_Id", newName: "CurrentKanbanStateId");
            AlterColumn("dbo.Task", "CurrentKanbanStateId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Task", "CurrentKanbanStateId");
            AddForeignKey("dbo.Task", "CurrentKanbanStateId", "dbo.KanbanState", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Task", "CurrentKanbanStateId", "dbo.KanbanState");
            DropIndex("dbo.Task", new[] { "CurrentKanbanStateId" });
            AlterColumn("dbo.Task", "CurrentKanbanStateId", c => c.Guid());
            RenameColumn(table: "dbo.Task", name: "CurrentKanbanStateId", newName: "CurrentKanbanState_Id");
            CreateIndex("dbo.Task", "CurrentKanbanState_Id");
            AddForeignKey("dbo.Task", "CurrentKanbanState_Id", "dbo.KanbanState", "Id");
        }
    }
}
