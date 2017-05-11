namespace VhakoneWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PersonalDetails", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PersonalDetails", "UserId", c => c.String(nullable: false));
        }
    }
}
