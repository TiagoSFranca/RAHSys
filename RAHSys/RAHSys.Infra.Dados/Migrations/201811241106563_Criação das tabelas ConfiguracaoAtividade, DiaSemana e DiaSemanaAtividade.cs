namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriaçãodastabelasConfiguracaoAtividadeDiaSemanaeDiaSemanaAtividade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConfiguracaoAtividade",
                c => new
                    {
                        IdConfiguracaoAtividade = c.Int(nullable: false),
                        Frequencia = c.Int(nullable: false),
                        QtdRepeticoes = c.Int(),
                        TerminaEm = c.DateTime(),
                        DiaMes = c.Int(),
                    })
                .PrimaryKey(t => t.IdConfiguracaoAtividade)
                .ForeignKey("dbo.Atividade", t => t.IdConfiguracaoAtividade, cascadeDelete: true)
                .Index(t => t.IdConfiguracaoAtividade);
            
            CreateTable(
                "dbo.AtividadeDiaSemana",
                c => new
                    {
                        IdAtividadeDiaSemana = c.Int(nullable: false, identity: true),
                        IdConfiguracaoAtividade = c.Int(nullable: false),
                        IdDiaSemana = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdAtividadeDiaSemana)
                .ForeignKey("dbo.ConfiguracaoAtividade", t => t.IdConfiguracaoAtividade, cascadeDelete: true)
                .ForeignKey("dbo.DiaSemana", t => t.IdDiaSemana, cascadeDelete: true)
                .Index(t => t.IdConfiguracaoAtividade)
                .Index(t => t.IdDiaSemana);
            
            CreateTable(
                "dbo.DiaSemana",
                c => new
                    {
                        IdDiaSemana = c.Int(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 32, unicode: false),
                    })
                .PrimaryKey(t => t.IdDiaSemana);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConfiguracaoAtividade", "IdConfiguracaoAtividade", "dbo.Atividade");
            DropForeignKey("dbo.AtividadeDiaSemana", "IdDiaSemana", "dbo.DiaSemana");
            DropForeignKey("dbo.AtividadeDiaSemana", "IdConfiguracaoAtividade", "dbo.ConfiguracaoAtividade");
            DropIndex("dbo.ConfiguracaoAtividade", new[] { "IdConfiguracaoAtividade" });
            DropIndex("dbo.AtividadeDiaSemana", new[] { "IdDiaSemana" });
            DropIndex("dbo.AtividadeDiaSemana", new[] { "IdConfiguracaoAtividade" });
            DropTable("dbo.DiaSemana");
            DropTable("dbo.AtividadeDiaSemana");
            DropTable("dbo.ConfiguracaoAtividade");
        }
    }
}
