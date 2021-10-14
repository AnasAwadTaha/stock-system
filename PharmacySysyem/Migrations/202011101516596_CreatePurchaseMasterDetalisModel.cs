namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePurchaseMasterDetalisModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseOrderD",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemCardMasterID = c.Long(nullable: false),
                        UnitID = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PurchaseOrderM",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        SupplierID = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark = c.String(),
                        Confirmed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Supplier", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.SupplierID);
            
            AddColumn("dbo.OpenningBalanceD", "PurchaseOrderM_ID", c => c.Long());
            CreateIndex("dbo.OpenningBalanceD", "PurchaseOrderM_ID");
            AddForeignKey("dbo.OpenningBalanceD", "PurchaseOrderM_ID", "dbo.PurchaseOrderM", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseOrderM", "SupplierID", "dbo.Supplier");
            DropForeignKey("dbo.OpenningBalanceD", "PurchaseOrderM_ID", "dbo.PurchaseOrderM");
            DropIndex("dbo.PurchaseOrderM", new[] { "SupplierID" });
            DropIndex("dbo.OpenningBalanceD", new[] { "PurchaseOrderM_ID" });
            DropColumn("dbo.OpenningBalanceD", "PurchaseOrderM_ID");
            DropTable("dbo.PurchaseOrderM");
            DropTable("dbo.PurchaseOrderD");
        }
    }
}
