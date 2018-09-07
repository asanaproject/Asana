namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Marked_Column_To_Mails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mail", "Marked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mail", "Marked");
        }
    }
}
