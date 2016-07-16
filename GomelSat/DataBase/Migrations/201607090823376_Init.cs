namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GomelSatNews",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Link = c.String(),
                        HeaderName = c.String(),
                        HeaderText = c.String(),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RequestRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SiteName = c.Int(nullable: false),
                        LastRequest = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RequestRecords");
            DropTable("dbo.GomelSatNews");
        }
    }
}
