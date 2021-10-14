namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPackageToItemCardDetalis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemCardDetalis", "Package", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemCardDetalis", "Package");
        }
    }
}
