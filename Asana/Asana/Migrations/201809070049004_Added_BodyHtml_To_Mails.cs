namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_BodyHtml_To_Mails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mail", "BodyHtml", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mail", "BodyHtml");
        }
    }
}
