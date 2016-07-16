namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWordTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Word",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Word = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Word");
        }
    }
}
