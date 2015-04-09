using System.Data.Entity.Migrations;

namespace AutoRss.Models.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MediaItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DownloadLink = c.String(),
                        Created = c.DateTime(nullable: false),
                        Size = c.Long(nullable: false),
                        MimeType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MediaItems");
        }
    }
}
