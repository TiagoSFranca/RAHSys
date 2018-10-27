namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodatabelaTipoRecorrencia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoRecorrencia",
                c => new
                    {
                        IdTipoRecorrencia = c.Int(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 32, unicode: false),
                    })
                .PrimaryKey(t => t.IdTipoRecorrencia);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TipoRecorrencia");
        }
    }
}
