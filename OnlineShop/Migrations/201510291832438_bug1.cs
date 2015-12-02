namespace OnlineShop.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class bug1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "Cat_Img_Id");
        }

        public override void Down()
        {
            AddColumn("dbo.Categories", "Cat_Img_Id", c => c.Long());
        }
    }
}