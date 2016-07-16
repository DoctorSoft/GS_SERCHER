namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnalizingTextModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnalizingText",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        HeaderText = c.String(),
                        ContentText = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AnalizingText");
        }
    }
}
