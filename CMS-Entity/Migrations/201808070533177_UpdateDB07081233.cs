namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB07081233 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Products", "ProductExtraPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CMS_News", "Publisher", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_News", "Publisher");
            DropColumn("dbo.CMS_Products", "ProductExtraPrice");
        }
    }
}
