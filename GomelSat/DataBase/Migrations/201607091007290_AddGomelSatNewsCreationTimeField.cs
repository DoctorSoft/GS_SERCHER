namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGomelSatNewsCreationTimeField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GomelSatNews", "CreationDateTimeOffset", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GomelSatNews", "CreationDateTimeOffset");
        }
    }
}
