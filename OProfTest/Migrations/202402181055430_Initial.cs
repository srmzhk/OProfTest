namespace OProfTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TestID = c.Int(nullable: false),
                        AnswerType = c.Int(nullable: false),
                        Title = c.String(),
                        ValueKey = c.String(maxLength: 50),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tests", t => t.TestID, cascadeDelete: true)
                .Index(t => t.TestID);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TestID = c.Int(nullable: false),
                        QuestionType = c.Int(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tests", t => t.TestID)
                .Index(t => t.TestID);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        TestID = c.Int(nullable: false),
                        Description = c.String(),
                        FilePath = c.String(),
                        ResultDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tests", t => t.TestID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.TestID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Password = c.String(nullable: false, maxLength: 50),
                        Login = c.String(maxLength: 50),
                        Role = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Age = c.Int(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TestsImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Image = c.Binary(),
                        FileExtension = c.String(),
                        Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FilePath = c.String(),
                        TestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tests", t => t.TestID, cascadeDelete: true)
                .Index(t => t.TestID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestsImages", "TestID", "dbo.Tests");
            DropForeignKey("dbo.Results", "UserID", "dbo.Users");
            DropForeignKey("dbo.Results", "TestID", "dbo.Tests");
            DropForeignKey("dbo.Questions", "TestID", "dbo.Tests");
            DropForeignKey("dbo.Answers", "TestID", "dbo.Tests");
            DropIndex("dbo.TestsImages", new[] { "TestID" });
            DropIndex("dbo.Results", new[] { "TestID" });
            DropIndex("dbo.Results", new[] { "UserID" });
            DropIndex("dbo.Questions", new[] { "TestID" });
            DropIndex("dbo.Answers", new[] { "TestID" });
            DropTable("dbo.TestsImages");
            DropTable("dbo.Users");
            DropTable("dbo.Results");
            DropTable("dbo.Questions");
            DropTable("dbo.Tests");
            DropTable("dbo.Answers");
        }
    }
}
