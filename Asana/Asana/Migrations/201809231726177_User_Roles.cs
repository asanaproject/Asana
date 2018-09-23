namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_Roles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.User");
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            AddColumn("dbo.UserRoles", "Email", c => c.String());
            AddColumn("dbo.UserRoles", "Phone", c => c.String());
            DropColumn("dbo.UserRoles", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRoles", "UserId", c => c.Guid(nullable: false));
            DropColumn("dbo.UserRoles", "Phone");
            DropColumn("dbo.UserRoles", "Email");
            CreateIndex("dbo.UserRoles", "UserId");
            AddForeignKey("dbo.UserRoles", "UserId", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
