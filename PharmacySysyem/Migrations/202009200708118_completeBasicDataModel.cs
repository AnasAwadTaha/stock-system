namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class completeBasicDataModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Countries", newName: "Country");
            RenameTable(name: "dbo.Customers", newName: "Customer");
            RenameTable(name: "dbo.expences", newName: "expence");
            RenameTable(name: "dbo.Units", newName: "Unit");
            RenameTable(name: "dbo.Stocks", newName: "Stock");
            RenameTable(name: "dbo.Suppliers", newName: "Supplier");
            CreateTable(
                "dbo.Classification",
                c => new
                    {
                        ClassificationID = c.Int(nullable: false, identity: true),
                        ClassificationName = c.String(),
                    })
                .PrimaryKey(t => t.ClassificationID);
            
            CreateTable(
                "dbo.ItemCardMaster",
                c => new
                    {
                        ItemCardMasterID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        CountryID = c.Int(nullable: false),
                        SupplierID = c.Int(nullable: false),
                        ClassificationID = c.Int(nullable: false),
                        CommName = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ItemCardMasterID)
                .ForeignKey("dbo.Classification", t => t.ClassificationID, cascadeDelete: true)
                .ForeignKey("dbo.Country", t => t.CountryID, cascadeDelete: true)
                .ForeignKey("dbo.Supplier", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.CountryID)
                .Index(t => t.SupplierID)
                .Index(t => t.ClassificationID);
            
            AddColumn("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID", c => c.Int());
            CreateIndex("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID");
            AddForeignKey("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID", "dbo.ItemCardMaster", "ItemCardMasterID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemCardMaster", "SupplierID", "dbo.Supplier");
            DropForeignKey("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID", "dbo.ItemCardMaster");
            DropForeignKey("dbo.ItemCardMaster", "CountryID", "dbo.Country");
            DropForeignKey("dbo.ItemCardMaster", "ClassificationID", "dbo.Classification");
            DropIndex("dbo.ItemCardMaster", new[] { "ClassificationID" });
            DropIndex("dbo.ItemCardMaster", new[] { "SupplierID" });
            DropIndex("dbo.ItemCardMaster", new[] { "CountryID" });
            DropIndex("dbo.ItemCardDetalis", new[] { "ItemCardMaster_ItemCardMasterID" });
            DropColumn("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID");
            DropTable("dbo.ItemCardMaster");
            DropTable("dbo.Classification");
            RenameTable(name: "dbo.Supplier", newName: "Suppliers");
            RenameTable(name: "dbo.Stock", newName: "Stocks");
            RenameTable(name: "dbo.Unit", newName: "Units");
            RenameTable(name: "dbo.expence", newName: "expences");
            RenameTable(name: "dbo.Customer", newName: "Customers");
            RenameTable(name: "dbo.Country", newName: "Countries");
        }
    }
}
