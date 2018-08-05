namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb0805 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CMS_Brands", "Description", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.CMS_Products", "Short_Description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.CMS_Products", "Description", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.CMS_Products", "Information", c => c.String(maxLength: 1000));
            AlterColumn("dbo.CMS_Products", "Vendor", c => c.String(maxLength: 1000));
            AlterColumn("dbo.CMS_Categories", "Description", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.CMS_News", "Short_Description", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CMS_News", "Short_Description", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.CMS_Categories", "Description", c => c.String());
            AlterColumn("dbo.CMS_Products", "Vendor", c => c.String());
            AlterColumn("dbo.CMS_Products", "Information", c => c.String());
            AlterColumn("dbo.CMS_Products", "Description", c => c.String());
            AlterColumn("dbo.CMS_Products", "Short_Description", c => c.String());
            AlterColumn("dbo.CMS_Brands", "Description", c => c.String());
        }
    }
}
