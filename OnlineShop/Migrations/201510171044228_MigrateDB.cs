namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Products", new[] { "Description_Desc_Id" });
            DropColumn("dbo.Products", "Pr_Descr_Id");
            RenameColumn(table: "dbo.Products", name: "Description_Desc_Id", newName: "Pr_Descr_Id");
            AddColumn("dbo.Categories", "IsAvailable", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Carts", "Cart_Pr_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.Products", "Pr_Descr_Id", c => c.Long());
            CreateIndex("dbo.Products", "Pr_Descr_Id");
            DropColumn("dbo.Categories", "Availability");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Availability", c => c.String());
            DropIndex("dbo.Products", new[] { "Pr_Descr_Id" });
            AlterColumn("dbo.Products", "Pr_Descr_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.Carts", "Cart_Pr_Id", c => c.Long());
            DropColumn("dbo.Categories", "IsAvailable");
            RenameColumn(table: "dbo.Products", name: "Pr_Descr_Id", newName: "Description_Desc_Id");
            AddColumn("dbo.Products", "Pr_Descr_Id", c => c.Long(nullable: false));
            CreateIndex("dbo.Products", "Description_Desc_Id");
        }
    }
}
