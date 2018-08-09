namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodastabelasenderecocidadeestadoecontratoEndereco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        IdCidade = c.Int(nullable: false),
                        IdEstado = c.Int(nullable: false),
                        CodCidade = c.String(nullable: false, maxLength: 5, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.IdCidade)
                .ForeignKey("dbo.Estado", t => t.IdEstado)
                .Index(t => t.IdEstado);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        IdEndereco = c.Int(nullable: false, identity: true),
                        IdCidade = c.Int(nullable: false),
                        Logradouro = c.String(maxLength: 256, unicode: false),
                        Numero = c.String(maxLength: 12, unicode: false),
                        Bairro = c.String(maxLength: 128, unicode: false),
                        CEP = c.String(maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.IdEndereco)
                .ForeignKey("dbo.Cidade", t => t.IdCidade)
                .Index(t => t.IdCidade);
            
            CreateTable(
                "dbo.ContratoEndereco",
                c => new
                    {
                        IdContratoEndereco = c.Int(nullable: false, identity: true),
                        IdContrato = c.Int(nullable: false),
                        IdEndereco = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdContratoEndereco)
                .ForeignKey("dbo.Contrato", t => t.IdContrato)
                .ForeignKey("dbo.Endereco", t => t.IdEndereco)
                .Index(t => t.IdContrato)
                .Index(t => t.IdEndereco);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        IdEstado = c.Int(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 32, unicode: false),
                        Sigla = c.String(nullable: false, maxLength: 2, unicode: false),
                    })
                .PrimaryKey(t => t.IdEstado);
            
            AddColumn("dbo.Contrato", "ContratoEndereco_IdContratoEndereco", c => c.Int());
            CreateIndex("dbo.Contrato", "ContratoEndereco_IdContratoEndereco");
            AddForeignKey("dbo.Contrato", "ContratoEndereco_IdContratoEndereco", "dbo.ContratoEndereco", "IdContratoEndereco");
            DropColumn("dbo.Contrato", "Endereco");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contrato", "Endereco", c => c.String(nullable: false, maxLength: 256, unicode: false));
            DropForeignKey("dbo.Cidade", "IdEstado", "dbo.Estado");
            DropForeignKey("dbo.Endereco", "IdCidade", "dbo.Cidade");
            DropForeignKey("dbo.ContratoEndereco", "IdEndereco", "dbo.Endereco");
            DropForeignKey("dbo.ContratoEndereco", "IdContrato", "dbo.Contrato");
            DropForeignKey("dbo.Contrato", "ContratoEndereco_IdContratoEndereco", "dbo.ContratoEndereco");
            DropIndex("dbo.Cidade", new[] { "IdEstado" });
            DropIndex("dbo.Endereco", new[] { "IdCidade" });
            DropIndex("dbo.ContratoEndereco", new[] { "IdEndereco" });
            DropIndex("dbo.ContratoEndereco", new[] { "IdContrato" });
            DropIndex("dbo.Contrato", new[] { "ContratoEndereco_IdContratoEndereco" });
            DropColumn("dbo.Contrato", "ContratoEndereco_IdContratoEndereco");
            DropTable("dbo.Estado");
            DropTable("dbo.ContratoEndereco");
            DropTable("dbo.Endereco");
            DropTable("dbo.Cidade");
        }
    }
}
