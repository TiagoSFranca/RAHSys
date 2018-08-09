namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodatabelaContratoEndereco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContratoEndereco",
                c => new
                    {
                        IdContratoEndereco = c.Int(nullable: false, identity: true),
                        IdContrato = c.Int(nullable: false),
                        IdEndereco = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdContratoEndereco)
                .ForeignKey("dbo.Endereco", t => t.IdEndereco)
                .ForeignKey("dbo.Contrato", t => t.IdContratoEndereco)
                .Index(t => t.IdEndereco)
                .Index(t => t.IdContratoEndereco);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContratoEndereco", "IdContratoEndereco", "dbo.Contrato");
            DropForeignKey("dbo.ContratoEndereco", "IdEndereco", "dbo.Endereco");
            DropIndex("dbo.ContratoEndereco", new[] { "IdContratoEndereco" });
            DropIndex("dbo.ContratoEndereco", new[] { "IdEndereco" });
            DropTable("dbo.ContratoEndereco");
        }
    }
}
