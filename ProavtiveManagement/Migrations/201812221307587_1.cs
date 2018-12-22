namespace ProavtiveManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scenarios", "Efficiency", c => c.Int(nullable: false));
            DropColumn("dbo.Scenarios", "Priority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Scenarios", "Priority", c => c.Int(nullable: false));
            DropColumn("dbo.Scenarios", "Efficiency");
        }
    }
}
