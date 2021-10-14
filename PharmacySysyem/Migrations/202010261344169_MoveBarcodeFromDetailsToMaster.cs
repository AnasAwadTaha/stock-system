namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveBarcodeFromDetailsToMaster : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemCardMaster", "Barcode", c => c.String());
            DropColumn("dbo.ItemCardDetalis", "Barcode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemCardDetalis", "Barcode", c => c.String());
            DropColumn("dbo.ItemCardMaster", "Barcode");
        }
    }
}
