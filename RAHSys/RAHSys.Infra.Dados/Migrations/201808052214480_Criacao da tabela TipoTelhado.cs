namespace RAHSys.Infra.Dados.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CriacaodatabelaTipoTelhado : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoTelhado",
                c => new
                    {
                        IdTipoTelhado = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.IdTipoTelhado);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TipoTelhado");
        }
    }
}
