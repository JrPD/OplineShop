namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Product_Pr_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Cart_Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.Products", "Pr_Category_Cat_Id", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "Pr_Category_Cat_Id" });
            DropIndex("dbo.Products", new[] { "Cart_Cart_Id" });
            DropIndex("dbo.Images", new[] { "Product_Pr_Id" });
            RenameColumn(table: "dbo.Products", name: "Pr_Category_Cat_Id", newName: "Pr_Cat_Id");
            RenameColumn(table: "dbo.Products", name: "Pr_Description_Desc_Id", newName: "Description_Desc_Id");
            RenameIndex(table: "dbo.Products", name: "IX_Pr_Description_Desc_Id", newName: "IX_Description_Desc_Id");
            DropPrimaryKey("dbo.Images");
            CreateTable(
                "dbo.ProductCounters",
                c => new
                    {
                        Pr_Id = c.Long(nullable: false),
                        Pr_Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Pr_Id)
                .ForeignKey("dbo.Products", t => t.Pr_Id)
                .Index(t => t.Pr_Id);
            
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
            
            AddColumn("dbo.Products", "Pr_Descr_Id", c => c.Long(nullable: false));
            AddColumn("dbo.Images", "Img_Id", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.Images", "Img_Path", c => c.String(nullable: false));
            AlterColumn("dbo.Carts", "Cart_Pr_Id", c => c.Long());
            AlterColumn("dbo.Carts", "Cart_DataCreation", c => c.DateTime());
            AlterColumn("dbo.Products", "Pr_Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Products", "Pr_Cat_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.Categories", "Cat_Parent_Cat_Id", c => c.Long());
            AlterColumn("dbo.Descriptions", "Desc_Path", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Images", "Img_Id");
            CreateIndex("dbo.Products", "Pr_Cat_Id");
            AddForeignKey("dbo.Products", "Pr_Cat_Id", "dbo.Categories", "Cat_Id", cascadeDelete: true);
            DropColumn("dbo.Products", "Cart_Cart_Id");
            DropColumn("dbo.Images", "Im_Id");
            DropColumn("dbo.Images", "Im_Path");
            DropColumn("dbo.Images", "Product_Pr_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Product_Pr_Id", c => c.Long());
            AddColumn("dbo.Images", "Im_Path", c => c.String());
            AddColumn("dbo.Images", "Im_Id", c => c.Long(nullable: false, identity: true));
            AddColumn("dbo.Products", "Cart_Cart_Id", c => c.Long());
            DropForeignKey("dbo.Products", "Pr_Cat_Id", "dbo.Categories");
            DropForeignKey("dbo.ProductCounters", "Pr_Id", "dbo.Products");
            DropForeignKey("dbo.ProductsImages", "Img_Id", "dbo.Images");
            DropForeignKey("dbo.ProductsImages", "Pr_Id", "dbo.Products");
            DropForeignKey("dbo.ProductsCarts", "Cart_Id", "dbo.Carts");
            DropForeignKey("dbo.ProductsCarts", "Pr_Id", "dbo.Products");
            DropIndex("dbo.ProductsImages", new[] { "Img_Id" });
            DropIndex("dbo.ProductsImages", new[] { "Pr_Id" });
            DropIndex("dbo.ProductsCarts", new[] { "Cart_Id" });
            DropIndex("dbo.ProductsCarts", new[] { "Pr_Id" });
            DropIndex("dbo.ProductCounters", new[] { "Pr_Id" });
            DropIndex("dbo.Products", new[] { "Pr_Cat_Id" });
            DropPrimaryKey("dbo.Images");
            AlterColumn("dbo.Descriptions", "Desc_Path", c => c.String());
            AlterColumn("dbo.Categories", "Cat_Parent_Cat_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.Products", "Pr_Cat_Id", c => c.Long());
            AlterColumn("dbo.Products", "Pr_Name", c => c.String(nullable: false));
            AlterColumn("dbo.Carts", "Cart_DataCreation", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Carts", "Cart_Pr_Id", c => c.Long(nullable: false));
            DropColumn("dbo.Images", "Img_Path");
            DropColumn("dbo.Images", "Img_Id");
            DropColumn("dbo.Products", "Pr_Descr_Id");
            DropTable("dbo.ProductsImages");
            DropTable("dbo.ProductsCarts");
            DropTable("dbo.ProductCounters");
            AddPrimaryKey("dbo.Images", "Im_Id");
            RenameIndex(table: "dbo.Products", name: "IX_Description_Desc_Id", newName: "IX_Pr_Description_Desc_Id");
            RenameColumn(table: "dbo.Products", name: "Description_Desc_Id", newName: "Pr_Description_Desc_Id");
            RenameColumn(table: "dbo.Products", name: "Pr_Cat_Id", newName: "Pr_Category_Cat_Id");
            CreateIndex("dbo.Images", "Product_Pr_Id");
            CreateIndex("dbo.Products", "Cart_Cart_Id");
            CreateIndex("dbo.Products", "Pr_Category_Cat_Id");
            AddForeignKey("dbo.Products", "Pr_Category_Cat_Id", "dbo.Categories", "Cat_Id");
            AddForeignKey("dbo.Products", "Cart_Cart_Id", "dbo.Carts", "Cart_Id");
            AddForeignKey("dbo.Images", "Product_Pr_Id", "dbo.Products", "Pr_Id");
        }
    }
}
