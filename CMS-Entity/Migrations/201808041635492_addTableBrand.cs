namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableBrand : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CMS_Brands",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 60, unicode: false),
                        BrandName = c.String(nullable: false, maxLength: 250),
                        BrandCode = c.String(nullable: false, maxLength: 50, unicode: false),
                        Description = c.String(),
                        ImageURL = c.String(maxLength: 60, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 60, unicode: false),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 60, unicode: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CMS_Products", "BrandId", c => c.String(nullable: false, maxLength: 60, unicode: false));
            CreateIndex("dbo.CMS_Products", "BrandId");
            AddForeignKey("dbo.CMS_Products", "BrandId", "dbo.CMS_Brands", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CMS_Products", "BrandId", "dbo.CMS_Brands");
            DropIndex("dbo.CMS_Products", new[] { "BrandId" });
            DropColumn("dbo.CMS_Products", "BrandId");
            DropTable("dbo.CMS_Brands");
        }
    }
}
