namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePurInvmAndPurInvDModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseInvoiceD",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ItemCardMasterID = c.Long(nullable: false),
                        StockID = c.Int(nullable: false),
                        UnitID = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountItem = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseInvoiceMID = c.Long(nullable: false),
                        PurchaseOrderD_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PurchaseInvoiceM", t => t.PurchaseInvoiceMID, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseOrderD", t => t.PurchaseOrderD_ID)
                .Index(t => t.PurchaseInvoiceMID)
                .Index(t => t.PurchaseOrderD_ID);
            
            CreateTable(
                "dbo.PurchaseInvoiceM",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        PurchaseDate = c.DateTime(nullable: false),
                        SupplierID = c.Int(nullable: false),
                        SupplierName = c.String(),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseInvoiceD", "PurchaseOrderD_ID", "dbo.PurchaseOrderD");
            DropForeignKey("dbo.PurchaseInvoiceD", "PurchaseInvoiceMID", "dbo.PurchaseInvoiceM");
            DropIndex("dbo.PurchaseInvoiceD", new[] { "PurchaseOrderD_ID" });
            DropIndex("dbo.PurchaseInvoiceD", new[] { "PurchaseInvoiceMID" });
            DropTable("dbo.PurchaseInvoiceM");
            DropTable("dbo.PurchaseInvoiceD");
        }
    }
}
