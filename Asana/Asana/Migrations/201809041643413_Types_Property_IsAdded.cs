namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Types_Property_IsAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChatRooms", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ChatRooms", "Type", c => c.String());
        }
    }
}
