namespace Asana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnIsAdded_Property_isRemoved_Column : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Column", "ColumnIsAdded");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Column", "ColumnIsAdded", c => c.Boolean(nullable: false));
        }
    }
}
