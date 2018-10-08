namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatRoom", "ProjectId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ChatRoom", "ProjectId");
            AddForeignKey("dbo.ChatRoom", "ProjectId", "dbo.Project", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatRoom", "ProjectId", "dbo.Project");
            DropIndex("dbo.ChatRoom", new[] { "ProjectId" });
            DropColumn("dbo.ChatRoom", "ProjectId");
        }
    }
}
