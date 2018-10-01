namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxStringLength_For_Title_IsChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Project", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Column", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Task", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Task", "Title", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Column", "Title", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Project", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
