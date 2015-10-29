namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
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
                        Pr_IsAvailable = c.Boolean(nullable: false),
                        Pr_Count = c.Int(),
                    })
                .PrimaryKey(t => t.Pr_Id)
                .ForeignKey("dbo.Categories", t => t.Pr_Cat_Id, cascadeDelete: true)
                .ForeignKey("dbo.Descriptions", t => t.Pr_Descr_Id)
                .Index(t => t.Pr_Cat_Id)
                .Index(t => t.Pr_Descr_Id)
                .Index(t => t.Pr_Name, unique: true, name: "Pr_Name_UN");
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Cat_Id = c.Long(nullable: false, identity: true),
                        Cat_Level = c.Byte(nullable: false),
                        Cat_Parent_Cat_Id = c.Long(nullable: false),
                        Cat_Name = c.String(nullable: false, maxLength: 200),
                        Cat_HasChild = c.Boolean(nullable: false),
                        Cat_Img_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Cat_Id)
                .Index(t => t.Cat_Name, unique: true, name: "Cat_Name_UN");
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Img_Id = c.Long(nullable: false, identity: true),
                        Img_Path = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Img_Id);
            
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Link_Id = c.Long(nullable: false, identity: true),
                        Link_Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Link_Id)
                .Index(t => t.Link_Name, unique: true, name: "Link_Name_UN");
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        Prop_Id = c.Long(nullable: false, identity: true),
                        Prop_Name = c.String(nullable: false, maxLength: 200),
                        Prop_Link_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Prop_Id)
                .ForeignKey("dbo.Links", t => t.Prop_Link_Id, cascadeDelete: true)
                .Index(t => t.Prop_Name, unique: true, name: "Prop_Name_UN")
                .Index(t => t.Prop_Link_Id);
            
            CreateTable(
                "dbo.Descriptions",
                c => new
                    {
                        Desc_Id = c.Long(nullable: false, identity: true),
                        Desc_Path = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Desc_Id);
            
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
                "dbo.CategoriesImages",
                c => new
                    {
                        Cat_Id = c.Long(nullable: false),
                        Img_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Cat_Id, t.Img_Id })
                .ForeignKey("dbo.Categories", t => t.Cat_Id, cascadeDelete: true)
                .ForeignKey("dbo.Images", t => t.Img_Id, cascadeDelete: true)
                .Index(t => t.Cat_Id)
                .Index(t => t.Img_Id);
            
            CreateTable(
                "dbo.LinkCategories",
                c => new
                    {
                        Link_Link_Id = c.Long(nullable: false),
                        Category_Cat_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Link_Link_Id, t.Category_Cat_Id })
                .ForeignKey("dbo.Links", t => t.Link_Link_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Cat_Id, cascadeDelete: true)
                .Index(t => t.Link_Link_Id)
                .Index(t => t.Category_Cat_Id);
            
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
            
            CreateTable(
                "dbo.ProductsLinks",
                c => new
                    {
                        Pr_Id = c.Long(nullable: false),
                        Link_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pr_Id, t.Link_Id })
                .ForeignKey("dbo.Products", t => t.Pr_Id, cascadeDelete: true)
                .ForeignKey("dbo.Links", t => t.Link_Id, cascadeDelete: true)
                .Index(t => t.Pr_Id)
                .Index(t => t.Link_Id);
            
            CreateTable(
                "dbo.ProductsProperties",
                c => new
                    {
                        Pr_Id = c.Long(nullable: false),
                        Prop_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pr_Id, t.Prop_Id })
                .ForeignKey("dbo.Products", t => t.Pr_Id, cascadeDelete: true)
                .ForeignKey("dbo.Properties", t => t.Prop_Id, cascadeDelete: true)
                .Index(t => t.Pr_Id)
                .Index(t => t.Prop_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsProperties", "Prop_Id", "dbo.Properties");
            DropForeignKey("dbo.ProductsProperties", "Pr_Id", "dbo.Products");
            DropForeignKey("dbo.ProductsLinks", "Link_Id", "dbo.Links");
            DropForeignKey("dbo.ProductsLinks", "Pr_Id", "dbo.Products");
            DropForeignKey("dbo.ProductsImages", "Img_Id", "dbo.Images");
            DropForeignKey("dbo.ProductsImages", "Pr_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Pr_Descr_Id", "dbo.Descriptions");
            DropForeignKey("dbo.Products", "Pr_Cat_Id", "dbo.Categories");
            DropForeignKey("dbo.Properties", "Prop_Link_Id", "dbo.Links");
            DropForeignKey("dbo.LinkCategories", "Category_Cat_Id", "dbo.Categories");
            DropForeignKey("dbo.LinkCategories", "Link_Link_Id", "dbo.Links");
            DropForeignKey("dbo.CategoriesImages", "Img_Id", "dbo.Images");
            DropForeignKey("dbo.CategoriesImages", "Cat_Id", "dbo.Categories");
            DropForeignKey("dbo.ProductsCarts", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.ProductsCarts", "Pr_Id", "dbo.Products");
            DropIndex("dbo.ProductsProperties", new[] { "Prop_Id" });
            DropIndex("dbo.ProductsProperties", new[] { "Pr_Id" });
            DropIndex("dbo.ProductsLinks", new[] { "Link_Id" });
            DropIndex("dbo.ProductsLinks", new[] { "Pr_Id" });
            DropIndex("dbo.ProductsImages", new[] { "Img_Id" });
            DropIndex("dbo.ProductsImages", new[] { "Pr_Id" });
            DropIndex("dbo.LinkCategories", new[] { "Category_Cat_Id" });
            DropIndex("dbo.LinkCategories", new[] { "Link_Link_Id" });
            DropIndex("dbo.CategoriesImages", new[] { "Img_Id" });
            DropIndex("dbo.CategoriesImages", new[] { "Cat_Id" });
            DropIndex("dbo.ProductsCarts", new[] { "Cart_Id" });
            DropIndex("dbo.ProductsCarts", new[] { "Pr_Id" });
            DropIndex("dbo.Properties", new[] { "Prop_Link_Id" });
            DropIndex("dbo.Properties", "Prop_Name_UN");
            DropIndex("dbo.Links", "Link_Name_UN");
            DropIndex("dbo.Categories", "Cat_Name_UN");
            DropIndex("dbo.Products", "Pr_Name_UN");
            DropIndex("dbo.Products", new[] { "Pr_Descr_Id" });
            DropIndex("dbo.Products", new[] { "Pr_Cat_Id" });
            DropTable("dbo.ProductsProperties");
            DropTable("dbo.ProductsLinks");
            DropTable("dbo.ProductsImages");
            DropTable("dbo.LinkCategories");
            DropTable("dbo.CategoriesImages");
            DropTable("dbo.ProductsCarts");
            DropTable("dbo.Descriptions");
            DropTable("dbo.Properties");
            DropTable("dbo.Links");
            DropTable("dbo.Images");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
        }
    }
}
