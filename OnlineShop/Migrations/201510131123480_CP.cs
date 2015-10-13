namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Cat_Id = c.Long(nullable: false, identity: true),
                        Cat_Level = c.Byte(nullable: false),
                        Cat_Parent_Cat_Id = c.Long(nullable: false),
                        Cat_Name = c.String(nullable: false, maxLength: 200),
                        Availability = c.String(),
                    })
                .PrimaryKey(t => t.Cat_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Pr_Id = c.Long(nullable: false, identity: true),
                        Pr_Cat_Id = c.Long(nullable: false),
                        Pr_Name = c.String(nullable: false),
                        Pr_Price = c.Double(nullable: false),
                        Pr_Charact = c.Long(nullable: false),
                        Pr_Category_Cat_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Pr_Id)
                .ForeignKey("dbo.Categories", t => t.Pr_Category_Cat_Id)
                .Index(t => t.Pr_Category_Cat_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Pr_Category_Cat_Id", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "Pr_Category_Cat_Id" });
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
