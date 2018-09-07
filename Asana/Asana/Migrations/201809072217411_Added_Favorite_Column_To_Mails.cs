namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Favorite_Column_To_Mails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mail", "Favorite", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mail", "Favorite");
        }
    }
}
