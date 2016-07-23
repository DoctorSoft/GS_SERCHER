namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReviewingModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageLinks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SourceLinks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AnalizingText", "ImageLinkId", c => c.Long());
            AddColumn("dbo.AnalizingText", "SourceLinkId", c => c.Long());
            CreateIndex("dbo.AnalizingText", "ImageLinkId");
            CreateIndex("dbo.AnalizingText", "SourceLinkId");
            AddForeignKey("dbo.AnalizingText", "ImageLinkId", "dbo.ImageLinks", "Id");
            AddForeignKey("dbo.AnalizingText", "SourceLinkId", "dbo.SourceLinks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnalizingText", "SourceLinkId", "dbo.SourceLinks");
            DropForeignKey("dbo.AnalizingText", "ImageLinkId", "dbo.ImageLinks");
            DropIndex("dbo.AnalizingText", new[] { "SourceLinkId" });
            DropIndex("dbo.AnalizingText", new[] { "ImageLinkId" });
            DropColumn("dbo.AnalizingText", "SourceLinkId");
            DropColumn("dbo.AnalizingText", "ImageLinkId");
            DropTable("dbo.SourceLinks");
            DropTable("dbo.ImageLinks");
        }
    }
}
