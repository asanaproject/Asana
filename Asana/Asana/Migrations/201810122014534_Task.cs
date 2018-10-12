namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Task : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KanbanState", "Task_Id", "dbo.Task");
            DropIndex("dbo.KanbanState", new[] { "Task_Id" });
            DropColumn("dbo.KanbanState", "Task_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KanbanState", "Task_Id", c => c.Guid());
            CreateIndex("dbo.KanbanState", "Task_Id");
            AddForeignKey("dbo.KanbanState", "Task_Id", "dbo.Task", "Id");
        }
    }
}
