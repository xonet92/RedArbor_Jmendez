namespace RedArbor_Jmendez_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableEmployees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        DeleteOn = c.DateTime(),
                        Email = c.String(nullable: false, maxLength: 255),
                        Fax = c.String(maxLength: 255),
                        Name = c.String(maxLength: 255),
                        Lastlogin = c.DateTime(),
                        Password = c.String(nullable: false),
                        PortalId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        Telephone = c.String(maxLength: 255),
                        UpdateOn = c.DateTime(),
                        Username = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
