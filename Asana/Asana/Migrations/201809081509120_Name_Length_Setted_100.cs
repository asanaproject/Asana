namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Name_Length_Setted_100 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChatRoom", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ChatRoom", "Name", c => c.String(nullable: false, maxLength: 25));
        }
    }
}
