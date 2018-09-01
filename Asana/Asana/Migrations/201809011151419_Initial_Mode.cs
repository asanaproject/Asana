namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Mode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatRooms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false, maxLength: 250),
                        ChatUserID = c.Int(nullable: false),
                        Timestap = c.DateTime(nullable: false),
                        ChatRoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Users", "Username", c => c.String(nullable: false));
            AddColumn("dbo.Users", "ChatRoom_ID", c => c.Int());
            CreateIndex("dbo.Users", "ChatRoom_ID");
            AddForeignKey("dbo.Users", "ChatRoom_ID", "dbo.ChatRooms", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ChatRoom_ID", "dbo.ChatRooms");
            DropIndex("dbo.Users", new[] { "ChatRoom_ID" });
            DropColumn("dbo.Users", "ChatRoom_ID");
            DropColumn("dbo.Users", "Username");
            DropTable("dbo.Messages");
            DropTable("dbo.ChatRooms");
        }
    }
}
