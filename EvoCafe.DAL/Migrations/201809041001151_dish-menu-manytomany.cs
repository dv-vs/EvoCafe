namespace EvoCafe.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dishmenumanytomany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dishes", "Menu_Id", "dbo.Menus");
            DropIndex("dbo.Dishes", new[] { "Menu_Id" });
            CreateTable(
                "dbo.MenuDishes",
                c => new
                    {
                        Menu_Id = c.Int(nullable: false),
                        Dish_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Menu_Id, t.Dish_Id })
                .ForeignKey("dbo.Menus", t => t.Menu_Id, cascadeDelete: true)
                .ForeignKey("dbo.Dishes", t => t.Dish_Id, cascadeDelete: true)
                .Index(t => t.Menu_Id)
                .Index(t => t.Dish_Id);
            
            DropColumn("dbo.Dishes", "Menu_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dishes", "Menu_Id", c => c.Int());
            DropForeignKey("dbo.MenuDishes", "Dish_Id", "dbo.Dishes");
            DropForeignKey("dbo.MenuDishes", "Menu_Id", "dbo.Menus");
            DropIndex("dbo.MenuDishes", new[] { "Dish_Id" });
            DropIndex("dbo.MenuDishes", new[] { "Menu_Id" });
            DropTable("dbo.MenuDishes");
            CreateIndex("dbo.Dishes", "Menu_Id");
            AddForeignKey("dbo.Dishes", "Menu_Id", "dbo.Menus", "Id");
        }
    }
}
