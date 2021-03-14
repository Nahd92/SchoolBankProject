namespace SchoolBankProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "PersonalNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "PersonalNumber", c => c.Int(nullable: false));
        }
    }
}
