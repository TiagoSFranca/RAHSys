namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CricaodatabelaTipoAtividade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoAtividade",
                c => new
                    {
                        IdTipoAtividade = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 64, unicode: false),
                    })
                .PrimaryKey(t => t.IdTipoAtividade);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TipoAtividade");
        }
    }
}
