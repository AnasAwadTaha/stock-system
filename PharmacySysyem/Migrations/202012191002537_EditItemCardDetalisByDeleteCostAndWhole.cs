namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditItemCardDetalisByDeleteCostAndWhole : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ItemCardDetalis", "CostPriceAll");
            DropColumn("dbo.ItemCardDetalis", "Distrbut");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemCardDetalis", "Distrbut", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ItemCardDetalis", "CostPriceAll", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
