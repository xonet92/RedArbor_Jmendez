namespace RedArbor_Jmendez_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtableLogApiCalls : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogApiCalls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Controller = c.String(nullable: false),
                        Method = c.String(),
                        Parameters = c.String(),
                        Response = c.String(),
                        CreateUser = c.String(),
                        CreatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Employees", "StatusId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "StatusId");
            DropTable("dbo.LogApiCalls");
        }
    }
}
