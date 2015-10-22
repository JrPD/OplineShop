namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productTableChange : DbMigration
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
                        Cart_DataCreation = c.DateTime(),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.Cart_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Pr_Id = c.Long(nullable: false, identity: true),
                        Pr_Cat_Id = c.Long(nullable: false),
                        Pr_Descr_Id = c.Long(),
                        Pr_Name = c.String(nullable: false, maxLength: 200),
                        Pr_Price = c.Double(nullable: false),
                        Pr_IsAviable = c.Boolean(nullable: false),
                        Pr_Count = c.Int(),
                    })
                .PrimaryKey(t => t.Pr_Id)
                .ForeignKey("dbo.Categories", t => t.Pr_Cat_Id, cascadeDelete: true)
                .ForeignKey("dbo.Descriptions", t => t.Pr_Descr_Id)
                .Index(t => t.Pr_Cat_Id)
                .Index(t => t.Pr_Descr_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Cat_Id = c.Long(nullable: false, identity: true),
                        Cat_Level = c.Byte(nullable: false),
                        Cat_Parent_Cat_Id = c.Long(),
                        Cat_Name = c.String(nullable: false, maxLength: 200),
                        IsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Cat_Id);
            
            CreateTable(
                "dbo.Descriptions",
                c => new
                    {
                        Desc_Id = c.Long(nullable: false, identity: true),
                        Desc_Path = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Desc_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Img_Id = c.Long(nullable: false, identity: true),
                        Img_Path = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Img_Id);
            
            CreateTable(
                "dbo.ProductsCarts",
                c => new
                    {
                        Pr_Id = c.Long(nullable: false),
                        Cart_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pr_Id, t.Cart_Id })
                .ForeignKey("dbo.Products", t => t.Pr_Id, cascadeDelete: true)
                .ForeignKey("dbo.Carts", t => t.Cart_Id, cascadeDelete: true)
                .Index(t => t.Pr_Id)
                .Index(t => t.Cart_Id);
            
            CreateTable(
                "dbo.ProductsImages",
                c => new
                    {
                        Pr_Id = c.Long(nullable: false),
                        Img_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pr_Id, t.Img_Id })
                .ForeignKey("dbo.Products", t => t.Pr_Id, cascadeDelete: true)
                .ForeignKey("dbo.Images", t => t.Img_Id, cascadeDelete: true)
                .Index(t => t.Pr_Id)
                .Index(t => t.Img_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsImages", "Img_Id", "dbo.Images");
            DropForeignKey("dbo.ProductsImages", "Pr_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Pr_Descr_Id", "dbo.Descriptions");
            DropForeignKey("dbo.Products", "Pr_Cat_Id", "dbo.Categories");
            DropForeignKey("dbo.ProductsCarts", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.ProductsCarts", "Pr_Id", "dbo.Products");
            DropIndex("dbo.ProductsImages", new[] { "Img_Id" });
            DropIndex("dbo.ProductsImages", new[] { "Pr_Id" });
            DropIndex("dbo.ProductsCarts", new[] { "Cart_Id" });
            DropIndex("dbo.ProductsCarts", new[] { "Pr_Id" });
            DropIndex("dbo.Products", new[] { "Pr_Descr_Id" });
            DropIndex("dbo.Products", new[] { "Pr_Cat_Id" });
            DropTable("dbo.ProductsImages");
            DropTable("dbo.ProductsCarts");
            DropTable("dbo.Images");
            DropTable("dbo.Descriptions");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
        }
    }
}
