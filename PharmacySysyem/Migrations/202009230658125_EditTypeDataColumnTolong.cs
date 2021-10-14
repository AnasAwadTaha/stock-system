namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTypeDataColumnTolong : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ItemCardDetalis");
            AlterColumn("dbo.ItemCardDetalis", "ItemCardDetalisID", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.ItemCardDetalis", "ItemCardDetalisID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ItemCardDetalis");
            AlterColumn("dbo.ItemCardDetalis", "ItemCardDetalisID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ItemCardDetalis", "ItemCardDetalisID");
        }
    }
}
