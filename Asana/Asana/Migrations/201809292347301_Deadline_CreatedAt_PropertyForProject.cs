namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Deadline_CreatedAt_PropertyForProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "Deadline", c => c.DateTime(nullable: false));
            AddColumn("dbo.Project", "CreatedAt", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "CreatedAt");
            DropColumn("dbo.Project", "Deadline");
        }
    }
}
