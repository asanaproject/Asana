namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Password_Property_RemovedFromExtraInfo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customer", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customer", "Password", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
