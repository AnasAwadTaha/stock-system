namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CountryID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.expences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExpenceName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ItemCardDetalis",
                c => new
                    {
                        ItemCardDetalisID = c.Int(nullable: false, identity: true),
                        UnitID = c.Int(nullable: false),
                        CostPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrntPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvrgeCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalesPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CostPriceAll = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Distrbut = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Barcode = c.String(),
                        MasterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemCardDetalisID)
                .ForeignKey("dbo.Units", t => t.UnitID, cascadeDelete: true)
                .Index(t => t.UnitID);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UnitName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StockName = c.String(),
                        CheckStockMain = c.Boolean(nullable: false),
                        StockID = c.Int(nullable: false),
                        CheckStockUser = c.Boolean(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stocks", t => t.StockID)
                .Index(t => t.StockID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        fax = c.String(),
                        Resp1 = c.String(),
                        PhoneResp1 = c.String(),
                        Resp2 = c.String(),
                        PhoneResp2 = c.String(),
                        CheckStop = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "StockID", "dbo.Stocks");
            DropForeignKey("dbo.ItemCardDetalis", "UnitID", "dbo.Units");
            DropIndex("dbo.Stocks", new[] { "StockID" });
            DropIndex("dbo.ItemCardDetalis", new[] { "UnitID" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.Stocks");
            DropTable("dbo.Units");
            DropTable("dbo.ItemCardDetalis");
            DropTable("dbo.expences");
            DropTable("dbo.Customers");
            DropTable("dbo.Countries");
        }
    }
}
