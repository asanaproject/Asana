namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nullable_Deadline_ForTask : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Task", "Deadline", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Task", "Deadline", c => c.DateTime(nullable: false));
        }
    }
}
