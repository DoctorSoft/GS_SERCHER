namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGomelSatSiteLinkModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GomelSatSiteLinks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Link = c.String(),
                        Priority = c.Long(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GomelSatSiteLinks");
        }
    }
}
