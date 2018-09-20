namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoPagamento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pagamento",
                c => new
                    {
                        IdPagamento = c.Int(nullable: false, identity: true),
                        IdContrato = c.Int(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataPagamento = c.DateTime(nullable: false),
                        Observacao = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.IdPagamento)
                .ForeignKey("dbo.Contrato", t => t.IdContrato, cascadeDelete: true)
                .Index(t => t.IdContrato);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pagamento", "IdContrato", "dbo.Contrato");
            DropIndex("dbo.Pagamento", new[] { "IdContrato" });
            DropTable("dbo.Pagamento");
        }
    }
}
