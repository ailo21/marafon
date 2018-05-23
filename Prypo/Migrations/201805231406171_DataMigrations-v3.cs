namespace Prypo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigrationsv3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
        }
    }
}
