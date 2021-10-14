namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPurchaseMasterID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrderD", "PurchaseMasterID", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseOrderD", "PurchaseMasterID");
        }
    }
}
