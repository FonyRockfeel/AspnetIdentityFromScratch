namespace AspnetIdentityFromScratch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuidDatabaseGenerate : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.RqCategories");
            DropPrimaryKey("dbo.SupportRequests");
            AlterColumn("dbo.RqCategories", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AlterColumn("dbo.SupportRequests", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AddPrimaryKey("dbo.RqCategories", "Id");
            AddPrimaryKey("dbo.SupportRequests", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.SupportRequests");
            DropPrimaryKey("dbo.RqCategories");
            AlterColumn("dbo.SupportRequests", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.RqCategories", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.SupportRequests", "Id");
            AddPrimaryKey("dbo.RqCategories", "Id");
        }
    }
}
