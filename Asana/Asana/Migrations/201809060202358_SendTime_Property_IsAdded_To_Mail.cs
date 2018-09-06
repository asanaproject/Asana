namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SendTime_Property_IsAdded_To_Mail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mails", "SendTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mails", "SendTime");
        }
    }
}
