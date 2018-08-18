namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Model1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserUserRoles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserUserRoles", "UserRole_Id", "dbo.UserRoles");
            DropIndex("dbo.UserUserRoles", new[] { "User_Id" });
            DropIndex("dbo.UserUserRoles", new[] { "UserRole_Id" });
            AddColumn("dbo.Users", "FullName", c => c.String(nullable: false));
            AddColumn("dbo.Users", "UserRole_Id", c => c.Int());
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            CreateIndex("dbo.Users", "UserRole_Id");
            AddForeignKey("dbo.Users", "UserRole_Id", "dbo.UserRoles", "Id");
            DropColumn("dbo.Users", "FirstName");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "CompanyName");
            DropColumn("dbo.Users", "PhoneNumber");
            DropColumn("dbo.Users", "CompanySize");
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
            
            AddColumn("dbo.Users", "CompanySize", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "PhoneNumber", c => c.String(maxLength: 20));
            AddColumn("dbo.Users", "CompanyName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Users", "LastName", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 20));
            DropForeignKey("dbo.Users", "UserRole_Id", "dbo.UserRoles");
            DropIndex("dbo.Users", new[] { "UserRole_Id" });
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Users", "UserRole_Id");
            DropColumn("dbo.Users", "FullName");
            CreateIndex("dbo.UserUserRoles", "UserRole_Id");
            CreateIndex("dbo.UserUserRoles", "User_Id");
            AddForeignKey("dbo.UserUserRoles", "UserRole_Id", "dbo.UserRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserUserRoles", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
