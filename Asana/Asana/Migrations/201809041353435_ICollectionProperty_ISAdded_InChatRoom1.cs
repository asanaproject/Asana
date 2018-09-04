namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ICollectionProperty_ISAdded_InChatRoom1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserChatRooms", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserChatRooms", "ChatRoom_ID", "dbo.ChatRooms");
            DropIndex("dbo.UserChatRooms", new[] { "User_Id" });
            DropIndex("dbo.UserChatRooms", new[] { "ChatRoom_ID" });
            CreateTable(
                "dbo.ChatRoomUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ChatRoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChatRooms", t => t.ChatRoomId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ChatRoomId);
            
            DropTable("dbo.UserChatRooms");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserChatRooms",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        ChatRoom_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.ChatRoom_ID });
            
            DropForeignKey("dbo.ChatRoomUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.ChatRoomUsers", "ChatRoomId", "dbo.ChatRooms");
            DropIndex("dbo.ChatRoomUsers", new[] { "ChatRoomId" });
            DropIndex("dbo.ChatRoomUsers", new[] { "UserId" });
            DropTable("dbo.ChatRoomUsers");
            CreateIndex("dbo.UserChatRooms", "ChatRoom_ID");
            CreateIndex("dbo.UserChatRooms", "User_Id");
            AddForeignKey("dbo.UserChatRooms", "ChatRoom_ID", "dbo.ChatRooms", "ID", cascadeDelete: true);
            AddForeignKey("dbo.UserChatRooms", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
