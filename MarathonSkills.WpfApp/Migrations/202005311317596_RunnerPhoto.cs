namespace MarathonSkills.WpfApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RunnerPhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Runner", "Photo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Runner", "Photo");
        }
    }
}
