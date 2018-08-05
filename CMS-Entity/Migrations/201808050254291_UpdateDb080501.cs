namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDb080501 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CMS_Products", "BrandId", "dbo.CMS_Brands");
            DropIndex("dbo.CMS_Products", new[] { "BrandId" });
            AlterColumn("dbo.CMS_Products", "BrandId", c => c.String(maxLength: 60, unicode: false));
            CreateIndex("dbo.CMS_Products", "BrandId");
            AddForeignKey("dbo.CMS_Products", "BrandId", "dbo.CMS_Brands", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CMS_Products", "BrandId", "dbo.CMS_Brands");
            DropIndex("dbo.CMS_Products", new[] { "BrandId" });
            AlterColumn("dbo.CMS_Products", "BrandId", c => c.String(nullable: false, maxLength: 60, unicode: false));
            CreateIndex("dbo.CMS_Products", "BrandId");
            AddForeignKey("dbo.CMS_Products", "BrandId", "dbo.CMS_Brands", "Id", cascadeDelete: true);
        }
    }
}
