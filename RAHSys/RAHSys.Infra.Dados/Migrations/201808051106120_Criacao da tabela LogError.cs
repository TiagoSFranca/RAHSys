namespace RAHSys.Infra.Dados.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CriacaodatabelaLogError : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogError",
                c => new
                    {
                        IdLogError = c.Int(nullable: false, identity: true),
                        Excecao = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Tipo = c.String(nullable: false, maxLength: 50, unicode: false),
                        Metodo = c.String(nullable: false, maxLength: 255, unicode: false),
                        Mensagem = c.String(nullable: false, maxLength: 4000, unicode: false),
                        DataOcorrencia = c.DateTime(nullable: false),
                        CodErro = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.IdLogError);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogError");
        }
    }
}
