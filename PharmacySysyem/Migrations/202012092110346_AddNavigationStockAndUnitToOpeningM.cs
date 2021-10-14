namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNavigationStockAndUnitToOpeningM : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.OpenningBalanceD", "stockID");
            CreateIndex("dbo.OpenningBalanceD", "UnitID");
            AddForeignKey("dbo.OpenningBalanceD", "stockID", "dbo.Stock", "ID", cascadeDelete: true);
            AddForeignKey("dbo.OpenningBalanceD", "UnitID", "dbo.Unit", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OpenningBalanceD", "UnitID", "dbo.Unit");
            DropForeignKey("dbo.OpenningBalanceD", "stockID", "dbo.Stock");
            DropIndex("dbo.OpenningBalanceD", new[] { "UnitID" });
            DropIndex("dbo.OpenningBalanceD", new[] { "stockID" });
        }
    }
}
