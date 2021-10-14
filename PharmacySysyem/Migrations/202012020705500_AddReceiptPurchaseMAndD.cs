namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReceiptPurchaseMAndD : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReceiptPurchaseD",
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
                        PurchaseMasterID = c.Long(nullable: false),
                        ReceiptPurchaseM_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ReceiptPurchaseM", t => t.ReceiptPurchaseM_ID)
                .Index(t => t.ReceiptPurchaseM_ID);
            
            CreateTable(
                "dbo.ReceiptPurchaseM",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ReceiptDate = c.DateTime(nullable: false),
                        PurchaseOrderMsID = c.Long(nullable: false),
                        SupplierID = c.Int(nullable: false),
                        SupplierName = c.String(),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark = c.String(),
                        Confirmed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceiptPurchaseD", "ReceiptPurchaseM_ID", "dbo.ReceiptPurchaseM");
            DropIndex("dbo.ReceiptPurchaseD", new[] { "ReceiptPurchaseM_ID" });
            DropTable("dbo.ReceiptPurchaseM");
            DropTable("dbo.ReceiptPurchaseD");
        }
    }
}
