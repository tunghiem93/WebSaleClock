namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDBProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Products", "Short_Description", c => c.String());
            AddColumn("dbo.CMS_Products", "Information", c => c.String());
            AddColumn("dbo.CMS_Products", "Vendor", c => c.String());
            AddColumn("dbo.CMS_Products", "TypeSize", c => c.Int());
            AddColumn("dbo.CMS_Products", "TypeState", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Products", "TypeState");
            DropColumn("dbo.CMS_Products", "TypeSize");
            DropColumn("dbo.CMS_Products", "Vendor");
            DropColumn("dbo.CMS_Products", "Information");
            DropColumn("dbo.CMS_Products", "Short_Description");
        }
    }
}
