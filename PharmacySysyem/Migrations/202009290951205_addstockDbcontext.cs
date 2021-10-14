namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstockDbcontext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stockmodel",
                c => new
                    {
                        stockId = c.Int(nullable: false, identity: true),
                        StockName = c.String(),
                        StockPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.stockId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stockmodel");
        }
    }
}
