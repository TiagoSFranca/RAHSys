namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizaçãodaTabelaAtividade : DbMigration
    {
        public override void Up()
        {
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
            DropIndex("dbo.Atividade", new[] { "IdTipoRecorrencia" });
            DropColumn("dbo.Atividade", "DataInicial");
            DropColumn("dbo.Atividade", "Finalizada");
            DropColumn("dbo.Atividade", "IdTipoRecorrencia");
        }
    }
}
