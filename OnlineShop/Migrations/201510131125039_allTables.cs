namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Cart_Id = c.Long(nullable: false, identity: true),
                        Cart_Pr_Id = c.Long(nullable: false),
                        Cart_Count = c.Byte(nullable: false),
                        Cart_DataCreation = c.DateTime(nullable: false),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.Cart_Id);
            
            CreateTable(
                "dbo.Descriptions",
                c => new
                    {
                        Desc_Id = c.Long(nullable: false, identity: true),
                        Desc_Path = c.String(),
                    })
                .PrimaryKey(t => t.Desc_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Im_Id = c.Long(nullable: false, identity: true),
                        Im_Path = c.String(),
                        Product_Pr_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Im_Id)
                .ForeignKey("dbo.Products", t => t.Product_Pr_Id)
                .Index(t => t.Product_Pr_Id);
            
            AddColumn("dbo.Products", "Pr_Description_Desc_Id", c => c.Long());
            AddColumn("dbo.Products", "Cart_Cart_Id", c => c.Long());
            CreateIndex("dbo.Products", "Pr_Description_Desc_Id");
            CreateIndex("dbo.Products", "Cart_Cart_Id");
            AddForeignKey("dbo.Products", "Pr_Description_Desc_Id", "dbo.Descriptions", "Desc_Id");
            AddForeignKey("dbo.Products", "Cart_Cart_Id", "dbo.Carts", "Cart_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Cart_Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.Images", "Product_Pr_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Pr_Description_Desc_Id", "dbo.Descriptions");
            DropIndex("dbo.Images", new[] { "Product_Pr_Id" });
            DropIndex("dbo.Products", new[] { "Cart_Cart_Id" });
            DropIndex("dbo.Products", new[] { "Pr_Description_Desc_Id" });
            DropColumn("dbo.Products", "Cart_Cart_Id");
            DropColumn("dbo.Products", "Pr_Description_Desc_Id");
            DropTable("dbo.Images");
            DropTable("dbo.Descriptions");
            DropTable("dbo.Carts");
        }
    }
}
