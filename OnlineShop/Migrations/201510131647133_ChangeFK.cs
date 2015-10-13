namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Pr_IsAviable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Pr_IsAviable");
        }
    }
}
