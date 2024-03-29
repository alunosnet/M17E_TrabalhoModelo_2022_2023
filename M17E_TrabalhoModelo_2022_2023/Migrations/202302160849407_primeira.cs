﻿namespace M17E_TrabalhoModelo_2022_2023.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class primeira : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 80),
                        Morada = c.String(nullable: false, maxLength: 80),
                        CP = c.String(nullable: false, maxLength: 8),
                        Email = c.String(nullable: false),
                        Telefone = c.String(nullable: false, maxLength: 15),
                        DataNascimento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Clientes");
        }
    }
}
