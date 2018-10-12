namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task_Properties_Updated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskKanbanStates", "KanbanStateId", "dbo.KanbanState");
            DropForeignKey("dbo.TaskKanbanStates", "TaskId", "dbo.Task");
            DropIndex("dbo.TaskKanbanStates", new[] { "KanbanStateId" });
            DropIndex("dbo.TaskKanbanStates", new[] { "TaskId" });
            DropTable("dbo.TaskKanbanStates");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.TaskKanbanStates", "TaskId");
            CreateIndex("dbo.TaskKanbanStates", "KanbanStateId");
            AddForeignKey("dbo.TaskKanbanStates", "TaskId", "dbo.Task", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TaskKanbanStates", "KanbanStateId", "dbo.KanbanState", "Id", cascadeDelete: true);
        }
    }
}
