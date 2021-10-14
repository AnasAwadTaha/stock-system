namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStockIDToOrderDetalis : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OpenningBalanceD", "PurchaseOrderM_ID", "dbo.PurchaseOrderM");
            DropIndex("dbo.OpenningBalanceD", new[] { "PurchaseOrderM_ID" });
            AddColumn("dbo.PurchaseOrderD", "StockID", c => c.Int(nullable: false));
            AddColumn("dbo.PurchaseOrderD", "PurchaseOrderM_ID", c => c.Long());
            CreateIndex("dbo.PurchaseOrderD", "PurchaseOrderM_ID");
            AddForeignKey("dbo.PurchaseOrderD", "PurchaseOrderM_ID", "dbo.PurchaseOrderM", "ID");
            DropColumn("dbo.OpenningBalanceD", "PurchaseOrderM_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OpenningBalanceD", "PurchaseOrderM_ID", c => c.Long());
            DropForeignKey("dbo.PurchaseOrderD", "PurchaseOrderM_ID", "dbo.PurchaseOrderM");
            DropIndex("dbo.PurchaseOrderD", new[] { "PurchaseOrderM_ID" });
            DropColumn("dbo.PurchaseOrderD", "PurchaseOrderM_ID");
            DropColumn("dbo.PurchaseOrderD", "StockID");
            CreateIndex("dbo.OpenningBalanceD", "PurchaseOrderM_ID");
            AddForeignKey("dbo.OpenningBalanceD", "PurchaseOrderM_ID", "dbo.PurchaseOrderM", "ID");
        }
    }
}
