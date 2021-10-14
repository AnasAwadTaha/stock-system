namespace PharmacySysyem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOpBaDSingle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OpenningBalanceM", "OpBaDSin_ID", c => c.Long());
            CreateIndex("dbo.OpenningBalanceM", "OpBaDSin_ID");
            AddForeignKey("dbo.OpenningBalanceM", "OpBaDSin_ID", "dbo.OpenningBalanceD", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OpenningBalanceM", "OpBaDSin_ID", "dbo.OpenningBalanceD");
            DropIndex("dbo.OpenningBalanceM", new[] { "OpBaDSin_ID" });
            DropColumn("dbo.OpenningBalanceM", "OpBaDSin_ID");
        }
    }
}
