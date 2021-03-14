namespace SchoolBankProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcommit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Balance = c.Single(nullable: false),
                        AccountNumber = c.String(nullable: false),
                        ClearingNumber = c.String(nullable: false),
                        IBANNumber = c.String(nullable: false),
                        AccountTypeId = c.Int(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountTypes", t => t.AccountTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.AccountTypeId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PersonalNumber = c.Int(nullable: false),
                        Address = c.String(),
                        Country = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        AccountNumber = c.String(),
                        Action = c.String(),
                        BankAccountId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId, cascadeDelete: true)
                .Index(t => t.BankAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.BankAccounts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.BankAccounts", "AccountTypeId", "dbo.AccountTypes");
            DropIndex("dbo.Transactions", new[] { "BankAccountId" });
            DropIndex("dbo.BankAccounts", new[] { "CustomerId" });
            DropIndex("dbo.BankAccounts", new[] { "AccountTypeId" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Customers");
            DropTable("dbo.BankAccounts");
            DropTable("dbo.AccountTypes");
        }
    }
}
