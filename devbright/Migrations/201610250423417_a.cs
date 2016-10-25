namespace devbright.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Homes", "title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Homes", "title");
        }
    }
}
