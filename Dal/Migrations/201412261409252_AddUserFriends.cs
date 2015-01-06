namespace Rtc.Dal.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddUserFriends : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserFriend",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FriendId = c.Int(nullable: false),
                        FriendshipStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "UserFriend_Id", c => c.Int());
            CreateIndex("dbo.User", "UserFriend_Id");
            AddForeignKey("dbo.User", "UserFriend_Id", "dbo.UserFriend", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "UserFriend_Id", "dbo.UserFriend");
            DropIndex("dbo.User", new[] { "UserFriend_Id" });
            DropColumn("dbo.User", "UserFriend_Id");
            DropTable("dbo.UserFriend");
        }
    }
}
