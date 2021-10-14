namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolveProblem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemCardDetalis", "ItemCardMasterID", "dbo.ItemCardMaster");
            DropIndex("dbo.ItemCardDetalis", new[] { "ItemCardMasterID" });
            DropPrimaryKey("dbo.ItemCardMaster");
            AddColumn("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID", c => c.Long());
            AlterColumn("dbo.ItemCardMaster", "ItemCardMasterID", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.ItemCardMaster", "ItemCardMasterID");
            CreateIndex("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID");
            AddForeignKey("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID", "dbo.ItemCardMaster", "ItemCardMasterID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID", "dbo.ItemCardMaster");
            DropIndex("dbo.ItemCardDetalis", new[] { "ItemCardMaster_ItemCardMasterID" });
            DropPrimaryKey("dbo.ItemCardMaster");
            AlterColumn("dbo.ItemCardMaster", "ItemCardMasterID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.ItemCardDetalis", "ItemCardMaster_ItemCardMasterID");
            AddPrimaryKey("dbo.ItemCardMaster", "ItemCardMasterID");
            CreateIndex("dbo.ItemCardDetalis", "ItemCardMasterID");
            AddForeignKey("dbo.ItemCardDetalis", "ItemCardMasterID", "dbo.ItemCardMaster", "ItemCardMasterID", cascadeDelete: true);
        }
    }
}
