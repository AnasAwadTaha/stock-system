namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteOPBalID : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OpenningBalanceD", "OpBalMID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OpenningBalanceD", "OpBalMID", c => c.Long(nullable: false));
        }
    }
}
