namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodatabelaEvidencia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Evidencia",
                c => new
                    {
                        IdEvidencia = c.Int(nullable: false, identity: true),
                        CaminhoArquivo = c.String(nullable: false, maxLength: 256, unicode: false),
                        DataUpload = c.DateTime(nullable: false),
                        NomeArquivo = c.String(nullable: false, maxLength: 256, unicode: false),
                        IdRegistroRecorrencia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdEvidencia)
                .ForeignKey("dbo.RegistroRecorrencia", t => t.IdRegistroRecorrencia, cascadeDelete: true)
                .Index(t => t.IdRegistroRecorrencia);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evidencia", "IdRegistroRecorrencia", "dbo.RegistroRecorrencia");
            DropIndex("dbo.Evidencia", new[] { "IdRegistroRecorrencia" });
            DropTable("dbo.Evidencia");
        }
    }
}
