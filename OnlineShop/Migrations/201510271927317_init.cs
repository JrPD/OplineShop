namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Cat_Parent_Cat_Id", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Cat_Parent_Cat_Id", c => c.Long());
        }
    }
}
