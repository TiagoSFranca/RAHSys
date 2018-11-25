namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodatabelaRegistroRecorrencia : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegistroRecorrencia", "IdAtividade", "dbo.Atividade");
            DropIndex("dbo.RegistroRecorrencia", new[] { "IdAtividade" });
            DropTable("dbo.RegistroRecorrencia");
        }
    }
}
