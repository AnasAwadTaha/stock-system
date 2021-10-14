namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateItemCardMasterID_inDetalis : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID", "dbo.ItemCardMaster");
            DropIndex("dbo.ItemCardDetalis", new[] { "ItemCardMaster_ItemCardMasterID" });
            RenameColumn(table: "dbo.ItemCardDetalis", name: "ItemCardMaster_ItemCardMasterID", newName: "ItemCardMasterID");
            AlterColumn("dbo.ItemCardDetalis", "ItemCardMasterID", c => c.Int(nullable: false));
            CreateIndex("dbo.ItemCardDetalis", "ItemCardMasterID");
            AddForeignKey("dbo.ItemCardDetalis", "ItemCardMasterID", "dbo.ItemCardMaster", "ItemCardMasterID", cascadeDelete: true);
            DropColumn("dbo.ItemCardDetalis", "MasterID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemCardDetalis", "MasterID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ItemCardDetalis", "ItemCardMasterID", "dbo.ItemCardMaster");
            DropIndex("dbo.ItemCardDetalis", new[] { "ItemCardMasterID" });
            AlterColumn("dbo.ItemCardDetalis", "ItemCardMasterID", c => c.Int());
            RenameColumn(table: "dbo.ItemCardDetalis", name: "ItemCardMasterID", newName: "ItemCardMaster_ItemCardMasterID");
            CreateIndex("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID");
            AddForeignKey("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID", "dbo.ItemCardMaster", "ItemCardMasterID");
        }
    }
}
