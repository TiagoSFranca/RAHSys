namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adicionadodropcascadeparaasentidadescontratocontratoenderecoeendereco : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contrato", "IdContratoEndereco", "dbo.ContratoEndereco");
            DropForeignKey("dbo.ContratoEndereco", "IdEndereco", "dbo.Endereco");
            DropIndex("dbo.Contrato", new[] { "IdContratoEndereco" });
            DropIndex("dbo.ContratoEndereco", new[] { "IdEndereco" });
            CreateIndex("dbo.Contrato", "IdContratoEndereco");
            CreateIndex("dbo.ContratoEndereco", "IdEndereco");
            AddForeignKey("dbo.Contrato", "IdContratoEndereco", "dbo.ContratoEndereco", "IdContratoEndereco", cascadeDelete: true);
            AddForeignKey("dbo.ContratoEndereco", "IdEndereco", "dbo.Endereco", "IdEndereco", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContratoEndereco", "IdEndereco", "dbo.Endereco");
            DropForeignKey("dbo.Contrato", "IdContratoEndereco", "dbo.ContratoEndereco");
            DropIndex("dbo.ContratoEndereco", new[] { "IdEndereco" });
            DropIndex("dbo.Contrato", new[] { "IdContratoEndereco" });
            CreateIndex("dbo.ContratoEndereco", "IdEndereco");
            CreateIndex("dbo.Contrato", "IdContratoEndereco");
            AddForeignKey("dbo.ContratoEndereco", "IdEndereco", "dbo.Endereco", "IdEndereco");
            AddForeignKey("dbo.Contrato", "IdContratoEndereco", "dbo.ContratoEndereco", "IdContratoEndereco");
        }
    }
}
