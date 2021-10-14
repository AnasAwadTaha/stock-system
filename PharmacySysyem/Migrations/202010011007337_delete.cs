namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID", "dbo.ItemCardMaster");
            DropIndex("dbo.ItemCardDetalis", new[] { "ItemCardMaster_ItemCardMasterID" });
            DropColumn("dbo.ItemCardDetalis", "ItemCardMasterID");
            RenameColumn(table: "dbo.ItemCardDetalis", name: "ItemCardMaster_ItemCardMasterID", newName: "ItemCardMasterID");
            AlterColumn("dbo.ItemCardDetalis", "ItemCardMasterID", c => c.Long(nullable: false));
            AlterColumn("dbo.ItemCardDetalis", "ItemCardMasterID", c => c.Long(nullable: false));
            CreateIndex("dbo.ItemCardDetalis", "ItemCardMasterID");
            AddForeignKey("dbo.ItemCardDetalis", "ItemCardMasterID", "dbo.ItemCardMaster", "ItemCardMasterID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemCardDetalis", "ItemCardMasterID", "dbo.ItemCardMaster");
            DropIndex("dbo.ItemCardDetalis", new[] { "ItemCardMasterID" });
            AlterColumn("dbo.ItemCardDetalis", "ItemCardMasterID", c => c.Long());
            AlterColumn("dbo.ItemCardDetalis", "ItemCardMasterID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ItemCardDetalis", name: "ItemCardMasterID", newName: "ItemCardMaster_ItemCardMasterID");
            AddColumn("dbo.ItemCardDetalis", "ItemCardMasterID", c => c.Int(nullable: false));
            CreateIndex("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID");
            AddForeignKey("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID", "dbo.ItemCardMaster", "ItemCardMasterID");
        }
    }
}
