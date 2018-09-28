namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Description_Property_IsAdded_Project : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "Description");
        }
    }
}
