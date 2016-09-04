namespace Multi_language.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201609041819327_AutomaticMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Languages",
                c => new
                {
                    IdLanguage = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Initials = c.String(nullable: false, maxLength: 10),
                    Culture = c.String(nullable: false, maxLength: 20),
                    Picture = c.Binary(storeType: "image"),
                    IsActive = c.Boolean(nullable: false),
                    UserName = c.String(maxLength: 50),
                    Datechanged = c.DateTime(),
                    DateCreated = c.DateTime(),
                })
                .PrimaryKey(t => t.IdLanguage);

        }
        public override void Down()
        {
            DropTable("dbo.Languages");
        }
    }
}
