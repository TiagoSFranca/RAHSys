namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodaTabelaResponsavelFinanceiro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResponsavelFinanceiro",
                c => new
                    {
                        IdResponsavelFinanceiro = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 256, unicode: false),
                        Email = c.String(nullable: false, maxLength: 256, unicode: false),
                        Telefone = c.String(nullable: false, maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.IdResponsavelFinanceiro)
                .ForeignKey("dbo.Cliente", t => t.IdResponsavelFinanceiro)
                .Index(t => t.IdResponsavelFinanceiro);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResponsavelFinanceiro", "IdResponsavelFinanceiro", "dbo.Cliente");
            DropIndex("dbo.ResponsavelFinanceiro", new[] { "IdResponsavelFinanceiro" });
            DropTable("dbo.ResponsavelFinanceiro");
        }
    }
}
