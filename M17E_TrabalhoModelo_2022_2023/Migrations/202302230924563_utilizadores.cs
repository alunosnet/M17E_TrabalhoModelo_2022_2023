namespace M17E_TrabalhoModelo_2022_2023.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class utilizadores : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Utilizadors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Perfil = c.Int(nullable: false),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Utilizadors");
        }
    }
}
