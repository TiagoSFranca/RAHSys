namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodatabelaContrato : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contrato",
                c => new
                    {
                        IdContrato = c.Int(nullable: false, identity: true),
                        NomeEmpresa = c.String(nullable: false, maxLength: 256, unicode: false),
                        ContatoInicial = c.String(nullable: false, maxLength: 256, unicode: false),
                        Endereco = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.IdContrato);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contrato");
        }
    }
}
