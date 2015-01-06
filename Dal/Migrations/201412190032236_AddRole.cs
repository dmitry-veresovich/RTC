namespace Rtc.Dal.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddRole : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "User");
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "RoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.User", "RoleId");
            AddForeignKey("dbo.User", "RoleId", "dbo.Role", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropIndex("dbo.User", new[] { "RoleId" });
            DropColumn("dbo.User", "RoleId");
            DropTable("dbo.Role");
            RenameTable(name: "dbo.User", newName: "Users");
        }
    }
}
