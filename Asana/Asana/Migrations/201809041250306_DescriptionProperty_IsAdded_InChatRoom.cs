namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescriptionProperty_IsAdded_InChatRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatRooms", "Desc", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChatRooms", "Desc");
        }
    }
}
