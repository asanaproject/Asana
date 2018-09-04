namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ICollectionProperty_ISAdded_InChatRoom : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "ChatRoom_ID", "dbo.ChatRooms");
            DropIndex("dbo.Users", new[] { "ChatRoom_ID" });
            CreateTable(
                "dbo.UserChatRooms",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        ChatRoom_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.ChatRoom_ID })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.ChatRooms", t => t.ChatRoom_ID, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.ChatRoom_ID);
            
            DropColumn("dbo.Users", "ChatRoom_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ChatRoom_ID", c => c.Int());
            DropForeignKey("dbo.UserChatRooms", "ChatRoom_ID", "dbo.ChatRooms");
            DropForeignKey("dbo.UserChatRooms", "User_Id", "dbo.Users");
            DropIndex("dbo.UserChatRooms", new[] { "ChatRoom_ID" });
            DropIndex("dbo.UserChatRooms", new[] { "User_Id" });
            DropTable("dbo.UserChatRooms");
            CreateIndex("dbo.Users", "ChatRoom_ID");
            AddForeignKey("dbo.Users", "ChatRoom_ID", "dbo.ChatRooms", "ID");
        }
    }
}
