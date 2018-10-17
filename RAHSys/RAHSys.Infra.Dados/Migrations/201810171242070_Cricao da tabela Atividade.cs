namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CricaodatabelaAtividade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Atividade",
                c => new
                    {
                        IdAtividade = c.Int(nullable: false, identity: true),
                        IdTipoAtividade = c.Int(nullable: false),
                        IdEquipe = c.Int(nullable: false),
                        IdContrato = c.Int(nullable: false),
                        IdUsuario = c.String(maxLength: 128),
                        Descricao = c.String(nullable: false, maxLength: 256, unicode: false),
                        Observacao = c.String(nullable: false, maxLength: 256, unicode: false),
                        Realizada = c.Boolean(nullable: false),
                        DataPrevista = c.DateTime(nullable: false),
                        DataRealizacao = c.DateTime(),
                    })
                .PrimaryKey(t => t.IdAtividade)
                .ForeignKey("dbo.Contrato", t => t.IdContrato, cascadeDelete: true)
                .ForeignKey("dbo.Equipe", t => t.IdEquipe, cascadeDelete: true)
                .ForeignKey("dbo.TipoAtividade", t => t.IdTipoAtividade, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.IdUsuario, cascadeDelete: true)
                .Index(t => t.IdContrato)
                .Index(t => t.IdEquipe)
                .Index(t => t.IdTipoAtividade)
                .Index(t => t.IdUsuario);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Atividade", "IdUsuario", "dbo.AspNetUsers");
            DropForeignKey("dbo.Atividade", "IdTipoAtividade", "dbo.TipoAtividade");
            DropForeignKey("dbo.Atividade", "IdEquipe", "dbo.Equipe");
            DropForeignKey("dbo.Atividade", "IdContrato", "dbo.Contrato");
            DropIndex("dbo.Atividade", new[] { "IdUsuario" });
            DropIndex("dbo.Atividade", new[] { "IdTipoAtividade" });
            DropIndex("dbo.Atividade", new[] { "IdEquipe" });
            DropIndex("dbo.Atividade", new[] { "IdContrato" });
            DropTable("dbo.Atividade");
        }
    }
}
