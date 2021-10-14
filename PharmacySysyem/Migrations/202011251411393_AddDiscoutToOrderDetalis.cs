namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDiscoutToOrderDetalis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrderD", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseOrderD", "Discount");
        }
    }
}
