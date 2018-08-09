namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodatabelaEndereco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        IdEndereco = c.Int(nullable: false, identity: true),
                        IdEstado = c.Int(nullable: false),
                        Logradouro = c.String(maxLength: 256, unicode: false),
                        Numero = c.String(maxLength: 12, unicode: false),
                        Bairro = c.String(maxLength: 128, unicode: false),
                        Cidade = c.String(maxLength: 128, unicode: false),
                        CEP = c.String(maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.IdEndereco)
                .ForeignKey("dbo.Estado", t => t.IdEstado)
                .Index(t => t.IdEstado);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Endereco", "IdEstado", "dbo.Estado");
            DropIndex("dbo.Endereco", new[] { "IdEstado" });
            DropTable("dbo.Endereco");
        }
    }
}
