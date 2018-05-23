namespace Prypo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigrationsv1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 256));
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 256));
            AddColumn("dbo.AspNetUsers", "Patronymic", c => c.String(maxLength: 256));
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Gender", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropColumn("dbo.AspNetUsers", "Patronymic");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "LastName");
        }
    }
}
