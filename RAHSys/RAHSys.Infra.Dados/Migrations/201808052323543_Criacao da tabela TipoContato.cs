namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodatabelaTipoContato : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoContato",
                c => new
                    {
                        IdTipoContato = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.IdTipoContato);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TipoContato");
        }
    }
}
