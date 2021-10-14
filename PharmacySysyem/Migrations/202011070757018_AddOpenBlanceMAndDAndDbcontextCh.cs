namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOpenBlanceMAndDAndDbcontextCh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OpenningBalanceD",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        stockID = c.Int(nullable: false),
                        StockName = c.String(),
                        PatchID = c.Int(nullable: false),
                        ExpertDate = c.DateTime(nullable: false),
                        UnitID = c.Int(nullable: false),
                        UnitName = c.String(),
                        Qty = c.Int(nullable: false),
                        OpBalMID = c.Long(nullable: false),
                        OpenningBalanceM_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.OpenningBalanceM", t => t.OpenningBalanceM_ID)
                .Index(t => t.OpenningBalanceM_ID);
            
            CreateTable(
                "dbo.OpenningBalanceM",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ItemNo = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.ItemCardMaster", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemCardMaster", "Image", c => c.String());
            DropForeignKey("dbo.OpenningBalanceD", "OpenningBalanceM_ID", "dbo.OpenningBalanceM");
            DropIndex("dbo.OpenningBalanceD", new[] { "OpenningBalanceM_ID" });
            DropTable("dbo.OpenningBalanceM");
            DropTable("dbo.OpenningBalanceD");
        }
    }
}
