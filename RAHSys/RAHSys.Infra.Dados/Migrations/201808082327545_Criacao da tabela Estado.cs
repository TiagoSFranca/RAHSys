namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodatabelaEstado : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        IdEstado = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 32, unicode: false),
                        Sigla = c.String(nullable: false, maxLength: 2, unicode: false),
                    })
                .PrimaryKey(t => t.IdEstado);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Estado");
        }
    }
}
