namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodastabelasdaAtividade : DbMigration
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
            
            CreateTable(
                "dbo.RegistroRecorrencia",
                c => new
                    {
                        IdRegistroRecorrencia = c.Int(nullable: false, identity: true),
                        IdAtividade = c.Int(nullable: false),
                        DataPrevista = c.DateTime(nullable: false),
                        DataRealizacao = c.DateTime(nullable: false),
                        Observacao = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.IdRegistroRecorrencia)
                .ForeignKey("dbo.Atividade", t => t.IdAtividade, cascadeDelete: true)
                .Index(t => t.IdAtividade);
            
            CreateTable(
                "dbo.TipoRecorrencia",
                c => new
                    {
                        IdTipoRecorrencia = c.Int(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 32, unicode: false),
                    })
                .PrimaryKey(t => t.IdTipoRecorrencia);
            
            AddColumn("dbo.Atividade", "IdTipoRecorrencia", c => c.Int());
            AddColumn("dbo.Atividade", "Finalizada", c => c.Boolean(nullable: false));
            AddColumn("dbo.Atividade", "DataInicial", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Atividade", "IdTipoRecorrencia");
            AddForeignKey("dbo.Atividade", "IdTipoRecorrencia", "dbo.TipoRecorrencia", "IdTipoRecorrencia", cascadeDelete: true);
            DropColumn("dbo.Atividade", "Observacao");
            DropColumn("dbo.Atividade", "Realizada");
            DropColumn("dbo.Atividade", "DataPrevista");
            DropColumn("dbo.Atividade", "DataRealizacao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Atividade", "DataRealizacao", c => c.DateTime());
            AddColumn("dbo.Atividade", "DataPrevista", c => c.DateTime(nullable: false));
            AddColumn("dbo.Atividade", "Realizada", c => c.Boolean(nullable: false));
            AddColumn("dbo.Atividade", "Observacao", c => c.String(nullable: false, maxLength: 256, unicode: false));
            DropForeignKey("dbo.Atividade", "IdTipoRecorrencia", "dbo.TipoRecorrencia");
            DropForeignKey("dbo.RegistroRecorrencia", "IdAtividade", "dbo.Atividade");
            DropForeignKey("dbo.ConfiguracaoAtividade", "IdConfiguracaoAtividade", "dbo.Atividade");
            DropForeignKey("dbo.AtividadeDiaSemana", "IdDiaSemana", "dbo.DiaSemana");
            DropForeignKey("dbo.AtividadeDiaSemana", "IdConfiguracaoAtividade", "dbo.ConfiguracaoAtividade");
            DropIndex("dbo.Atividade", new[] { "IdTipoRecorrencia" });
            DropIndex("dbo.RegistroRecorrencia", new[] { "IdAtividade" });
            DropIndex("dbo.ConfiguracaoAtividade", new[] { "IdConfiguracaoAtividade" });
            DropIndex("dbo.AtividadeDiaSemana", new[] { "IdDiaSemana" });
            DropIndex("dbo.AtividadeDiaSemana", new[] { "IdConfiguracaoAtividade" });
            DropColumn("dbo.Atividade", "DataInicial");
            DropColumn("dbo.Atividade", "Finalizada");
            DropColumn("dbo.Atividade", "IdTipoRecorrencia");
            DropTable("dbo.TipoRecorrencia");
            DropTable("dbo.RegistroRecorrencia");
            DropTable("dbo.DiaSemana");
            DropTable("dbo.AtividadeDiaSemana");
            DropTable("dbo.ConfiguracaoAtividade");
        }
    }
}
