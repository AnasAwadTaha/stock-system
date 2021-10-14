namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelOpenBM : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OpenningBalanceM", "ItemCards_ItemCardMasterID", "dbo.ItemCardMaster");
            DropIndex("dbo.OpenningBalanceM", new[] { "ItemCards_ItemCardMasterID" });
            RenameColumn(table: "dbo.OpenningBalanceM", name: "ItemCards_ItemCardMasterID", newName: "ItemCardMasterID");
            AlterColumn("dbo.OpenningBalanceM", "ItemCardMasterID", c => c.Long(nullable: false));
            CreateIndex("dbo.OpenningBalanceM", "ItemCardMasterID");
            AddForeignKey("dbo.OpenningBalanceM", "ItemCardMasterID", "dbo.ItemCardMaster", "ItemCardMasterID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OpenningBalanceM", "ItemCardMasterID", "dbo.ItemCardMaster");
            DropIndex("dbo.OpenningBalanceM", new[] { "ItemCardMasterID" });
            AlterColumn("dbo.OpenningBalanceM", "ItemCardMasterID", c => c.Long());
            RenameColumn(table: "dbo.OpenningBalanceM", name: "ItemCardMasterID", newName: "ItemCards_ItemCardMasterID");
            CreateIndex("dbo.OpenningBalanceM", "ItemCards_ItemCardMasterID");
            AddForeignKey("dbo.OpenningBalanceM", "ItemCards_ItemCardMasterID", "dbo.ItemCardMaster", "ItemCardMasterID");
        }
    }
}
