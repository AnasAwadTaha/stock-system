namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransationModelAndDeleteColFromOpenningMaster : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transaction", "ItemCM_ItemCardMasterID", "dbo.ItemCardMaster");
            DropForeignKey("dbo.Transaction", "UnitID", "dbo.Unit");
            DropIndex("dbo.Transaction", new[] { "UnitID" });
            DropIndex("dbo.Transaction", new[] { "ItemCM_ItemCardMasterID" });
            AddColumn("dbo.Transaction", "TransactionDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Transaction", "MyProperty");
            DropColumn("dbo.Transaction", "ItemCM_ItemCardMasterID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transaction", "ItemCM_ItemCardMasterID", c => c.Long());
            AddColumn("dbo.Transaction", "MyProperty", c => c.DateTime(nullable: false));
            DropColumn("dbo.Transaction", "TransactionDate");
            CreateIndex("dbo.Transaction", "ItemCM_ItemCardMasterID");
            CreateIndex("dbo.Transaction", "UnitID");
            AddForeignKey("dbo.Transaction", "UnitID", "dbo.Unit", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Transaction", "ItemCM_ItemCardMasterID", "dbo.ItemCardMaster", "ItemCardMasterID");
        }
    }
}
