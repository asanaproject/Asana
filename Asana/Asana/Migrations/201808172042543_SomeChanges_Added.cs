namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeChanges_Added : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserUserRoles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserUserRoles", "UserRole_Id", "dbo.UserRoles");
            DropIndex("dbo.UserUserRoles", new[] { "User_Id" });
            DropIndex("dbo.UserUserRoles", new[] { "UserRole_Id" });
            AddColumn("dbo.Users", "UserRole_Id", c => c.Int());
            CreateIndex("dbo.Users", "UserRole_Id");
            AddForeignKey("dbo.Users", "UserRole_Id", "dbo.UserRoles", "Id");
            DropTable("dbo.UserUserRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserUserRoles",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        UserRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.UserRole_Id });
            
            DropForeignKey("dbo.Users", "UserRole_Id", "dbo.UserRoles");
            DropIndex("dbo.Users", new[] { "UserRole_Id" });
            DropColumn("dbo.Users", "UserRole_Id");
            CreateIndex("dbo.UserUserRoles", "UserRole_Id");
            CreateIndex("dbo.UserUserRoles", "User_Id");
            AddForeignKey("dbo.UserUserRoles", "UserRole_Id", "dbo.UserRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserUserRoles", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
