﻿namespace M17E_TrabalhoModelo_2022_2023.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Estadias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estadias",
                c => new
                    {
                        EstadiaId = c.Int(nullable: false, identity: true),
                        data_entrada = c.DateTime(nullable: false),
                        data_saida = c.DateTime(nullable: false),
                        valor_pago = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClienteID = c.Int(nullable: false),
                        QuartoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EstadiaId)
                .ForeignKey("dbo.Clientes", t => t.ClienteID, cascadeDelete: true)
                .ForeignKey("dbo.Quartos", t => t.QuartoID, cascadeDelete: true)
                .Index(t => t.ClienteID)
                .Index(t => t.QuartoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Estadias", "QuartoID", "dbo.Quartos");
            DropForeignKey("dbo.Estadias", "ClienteID", "dbo.Clientes");
            DropIndex("dbo.Estadias", new[] { "QuartoID" });
            DropIndex("dbo.Estadias", new[] { "ClienteID" });
            DropTable("dbo.Estadias");
        }
    }
}
