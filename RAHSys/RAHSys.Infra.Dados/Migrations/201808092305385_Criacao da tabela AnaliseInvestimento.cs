namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodatabelaAnaliseInvestimento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnaliseInvestimento",
                c => new
                    {
                        IdContrato = c.Int(nullable: false),
                        IdTipoTelhado = c.Int(nullable: false),
                        NomeCliente = c.String(nullable: false, maxLength: 256, unicode: false),
                        Potencia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Investimento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumoTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumeroPlacas = c.Int(nullable: false),
                        TipoPlacas = c.String(nullable: false, maxLength: 256, unicode: false),
                        QtdInversores = c.Int(nullable: false),
                        TipoInversores = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.IdContrato)
                .ForeignKey("dbo.Contrato", t => t.IdContrato, cascadeDelete: true)
                .ForeignKey("dbo.TipoTelhado", t => t.IdTipoTelhado)
                .Index(t => t.IdContrato)
                .Index(t => t.IdTipoTelhado);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnaliseInvestimento", "IdTipoTelhado", "dbo.TipoTelhado");
            DropForeignKey("dbo.AnaliseInvestimento", "IdContrato", "dbo.Contrato");
            DropIndex("dbo.AnaliseInvestimento", new[] { "IdTipoTelhado" });
            DropIndex("dbo.AnaliseInvestimento", new[] { "IdContrato" });
            DropTable("dbo.AnaliseInvestimento");
        }
    }
}
