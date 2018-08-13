namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodatabelaClienteFiadorFiadorEnderecoeDocumento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        IdAnaliseInvestimento = c.Int(nullable: false),
                        MediaKW = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumoTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.IdAnaliseInvestimento)
                .ForeignKey("dbo.AnaliseInvestimento", t => t.IdAnaliseInvestimento, cascadeDelete: true)
                .Index(t => t.IdAnaliseInvestimento);
            
            CreateTable(
                "dbo.Fiador",
                c => new
                    {
                        IdFiador = c.Int(nullable: false, identity: true),
                        IdCliente = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 256, unicode: false),
                        Conjuge = c.Boolean(nullable: false),
                        Telefone = c.String(nullable: false, maxLength: 20, unicode: false),
                        Email = c.String(nullable: false, maxLength: 256, unicode: false),
                        IdFiadorEndereco = c.Int(),
                    })
                .PrimaryKey(t => t.IdFiador)
                .ForeignKey("dbo.FiadorEndereco", t => t.IdFiadorEndereco)
                .ForeignKey("dbo.Cliente", t => t.IdCliente, cascadeDelete: true)
                .Index(t => t.IdFiadorEndereco)
                .Index(t => t.IdCliente);
            
            CreateTable(
                "dbo.FiadorEndereco",
                c => new
                    {
                        IdFiadorEndereco = c.Int(nullable: false, identity: true),
                        IdFiador = c.Int(nullable: false),
                        IdEndereco = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdFiadorEndereco)
                .ForeignKey("dbo.Endereco", t => t.IdEndereco)
                .ForeignKey("dbo.Fiador", t => t.IdFiador)
                .Index(t => t.IdEndereco)
                .Index(t => t.IdFiador);
            
            CreateTable(
                "dbo.Documento",
                c => new
                    {
                        IdDocumento = c.Int(nullable: false, identity: true),
                        CaminhoArquivo = c.String(nullable: false, maxLength: 256, unicode: false),
                        DataUpload = c.DateTime(nullable: false),
                        NomeArquivo = c.String(nullable: false, maxLength: 256, unicode: false),
                        IdContrato = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdDocumento)
                .ForeignKey("dbo.Contrato", t => t.IdContrato, cascadeDelete: true)
                .Index(t => t.IdContrato);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cliente", "IdAnaliseInvestimento", "dbo.AnaliseInvestimento");
            DropForeignKey("dbo.Fiador", "IdCliente", "dbo.Cliente");
            DropForeignKey("dbo.Fiador", "IdFiadorEndereco", "dbo.FiadorEndereco");
            DropForeignKey("dbo.FiadorEndereco", "IdFiador", "dbo.Fiador");
            DropForeignKey("dbo.FiadorEndereco", "IdEndereco", "dbo.Endereco");
            DropForeignKey("dbo.Documento", "IdContrato", "dbo.Contrato");
            DropIndex("dbo.Cliente", new[] { "IdAnaliseInvestimento" });
            DropIndex("dbo.Fiador", new[] { "IdCliente" });
            DropIndex("dbo.Fiador", new[] { "IdFiadorEndereco" });
            DropIndex("dbo.FiadorEndereco", new[] { "IdFiador" });
            DropIndex("dbo.FiadorEndereco", new[] { "IdEndereco" });
            DropIndex("dbo.Documento", new[] { "IdContrato" });
            DropTable("dbo.Documento");
            DropTable("dbo.FiadorEndereco");
            DropTable("dbo.Fiador");
            DropTable("dbo.Cliente");
        }
    }
}
