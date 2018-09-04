namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Message_Class_IsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "SendTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Messages", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "Body", c => c.String(nullable: false, maxLength: 500));
            CreateIndex("dbo.Messages", "UserId");
            CreateIndex("dbo.Messages", "ChatRoomId");
            AddForeignKey("dbo.Messages", "ChatRoomId", "dbo.ChatRooms", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Messages", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            DropColumn("dbo.Messages", "ChatUserID");
            DropColumn("dbo.Messages", "Timestap");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Timestap", c => c.DateTime(nullable: false));
            AddColumn("dbo.Messages", "ChatUserID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Messages", "UserId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ChatRoomId", "dbo.ChatRooms");
            DropIndex("dbo.Messages", new[] { "ChatRoomId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            AlterColumn("dbo.Messages", "Body", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.Messages", "UserId");
            DropColumn("dbo.Messages", "SendTime");
        }
    }
}
