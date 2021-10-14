namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditOPenningBalDAddMasterOpenID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OpenningBalanceD", "OpenningBalanceM_ID", "dbo.OpenningBalanceM");
            DropIndex("dbo.OpenningBalanceD", new[] { "OpenningBalanceM_ID" });
            RenameColumn(table: "dbo.OpenningBalanceD", name: "OpenningBalanceM_ID", newName: "OpenningBalanceMID");
            AlterColumn("dbo.OpenningBalanceD", "OpenningBalanceMID", c => c.Long(nullable: false));
            CreateIndex("dbo.OpenningBalanceD", "OpenningBalanceMID");
            AddForeignKey("dbo.OpenningBalanceD", "OpenningBalanceMID", "dbo.OpenningBalanceM", "ID", cascadeDelete: true);
            DropColumn("dbo.OpenningBalanceD", "StockName");
            DropColumn("dbo.OpenningBalanceD", "UnitName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OpenningBalanceD", "UnitName", c => c.String());
            AddColumn("dbo.OpenningBalanceD", "StockName", c => c.String());
            DropForeignKey("dbo.OpenningBalanceD", "OpenningBalanceMID", "dbo.OpenningBalanceM");
            DropIndex("dbo.OpenningBalanceD", new[] { "OpenningBalanceMID" });
            AlterColumn("dbo.OpenningBalanceD", "OpenningBalanceMID", c => c.Long());
            RenameColumn(table: "dbo.OpenningBalanceD", name: "OpenningBalanceMID", newName: "OpenningBalanceM_ID");
            CreateIndex("dbo.OpenningBalanceD", "OpenningBalanceM_ID");
            AddForeignKey("dbo.OpenningBalanceD", "OpenningBalanceM_ID", "dbo.OpenningBalanceM", "ID");
        }
    }
}
