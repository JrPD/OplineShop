namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllFixRelations : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Pr_Cat_Id");
            DropColumn("dbo.Products", "Pr_Charact");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Pr_Charact", c => c.Long(nullable: false));
            AddColumn("dbo.Products", "Pr_Cat_Id", c => c.Long(nullable: false));
        }
    }
}
