namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnIsAdded_Property_isAdded_Column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Column", "ColumnIsAdded", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Column", "ColumnIsAdded");
        }
    }
}
