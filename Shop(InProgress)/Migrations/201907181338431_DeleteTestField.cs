namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteTestField : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Course", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Course", "Test", c => c.String());
        }
    }
}
