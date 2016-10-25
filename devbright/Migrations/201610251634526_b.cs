namespace devbright.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leads", "homeID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leads", "homeID");
        }
    }
}
