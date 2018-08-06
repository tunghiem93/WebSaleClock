namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDb0608 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CMS_Brands", "Short_Description", c => c.String(maxLength: 200));
            AddColumn("dbo.CMS_Categories", "Short_Description", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CMS_Categories", "Short_Description");
            DropColumn("dbo.CMS_Brands", "Short_Description");
        }
    }
}
