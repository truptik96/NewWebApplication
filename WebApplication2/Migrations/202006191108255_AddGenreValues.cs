namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenreValues : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id,Name) VALUES (1,'Action')");
            Sql("INSERT INTO Genres (Id,Name) VALUES (2,'Thrill')");
        }
        
        public override void Down()
        {
        }
    }
}
