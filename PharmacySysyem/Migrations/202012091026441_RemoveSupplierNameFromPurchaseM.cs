namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSupplierNameFromPurchaseM : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PurchaseOrderM", "SupplierName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrderM", "SupplierName", c => c.String());
        }
    }
}
