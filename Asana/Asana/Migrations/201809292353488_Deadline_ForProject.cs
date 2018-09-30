namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Deadline_ForProject : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Project", "Deadline", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Project", "Deadline", c => c.DateTime(nullable: false));
        }
    }
}
