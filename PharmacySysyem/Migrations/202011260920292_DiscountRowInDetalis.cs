namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscountRowInDetalis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrderD", "DiscountItem", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.PurchaseOrderD", "Discount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrderD", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.PurchaseOrderD", "DiscountItem");
        }
    }
}
