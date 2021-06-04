namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SwapFollowingColunmOrder : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.Followings");
            //AddPrimaryKey("dbo.Followings", new[] { "FollowerId", "FolloweeId" });
            CreateTable(
                "dbo.Followings",
              c => new
              {
                  FollowerId = c.String(nullable: false, maxLength: 128),
                  FolloweeId = c.String(nullable: false, maxLength: 128),
              })
                .PrimaryKey(t => new
                {
                    t.FollowerId,
                    t.FolloweeId
                })

            .ForeignKey("dbo.AspNetUsers", t => t.FollowerId)
            .ForeignKey("dbo.AspNetUsers", t => t.FolloweeId)
            .Index(t => t.FollowerId)
            .Index(t => t.FolloweeId);
        }
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "FollowerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "FolloweeId", "dbo.AspNetUsers");

            DropIndex("dbo.Followings", new[] { "FollowerId" });
            DropIndex("dbo.Followings", new[] { "FolloweeId" });
            DropTable("dbo.Followings");
        }
    }

    //public override void Down()
    //{
    //    //DropPrimaryKey("dbo.Followings");
    //    //AddPrimaryKey("dbo.Followings", new[] { "FolloweeId", "FollowerId" });
    //}




}

