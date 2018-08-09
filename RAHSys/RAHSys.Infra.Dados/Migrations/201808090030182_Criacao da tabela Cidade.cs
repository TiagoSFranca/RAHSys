namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodatabelaCidade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Endereco", "IdEstado", "dbo.Estado");
            DropIndex("dbo.Endereco", new[] { "IdEstado" });
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        IdCidade = c.Int(nullable: false),
                        IdEstado = c.Int(nullable: false),
                        CodCidade = c.String(nullable: false, maxLength: 5, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.IdCidade)
                .ForeignKey("dbo.Estado", t => t.IdEstado)
                .Index(t => t.IdEstado);
            
            AddColumn("dbo.Endereco", "IdCidade", c => c.Int(nullable: false));
            AlterColumn("dbo.Estado", "IdEstado", c => c.Int(nullable: false));
            CreateIndex("dbo.Endereco", "IdCidade");
            AddForeignKey("dbo.Endereco", "IdCidade", "dbo.Cidade", "IdCidade");
            DropColumn("dbo.Endereco", "IdEstado");
            DropColumn("dbo.Endereco", "Cidade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Endereco", "Cidade", c => c.String(maxLength: 128, unicode: false));
            AddColumn("dbo.Endereco", "IdEstado", c => c.Int(nullable: false));
            DropForeignKey("dbo.Cidade", "IdEstado", "dbo.Estado");
            DropForeignKey("dbo.Endereco", "IdCidade", "dbo.Cidade");
            DropIndex("dbo.Cidade", new[] { "IdEstado" });
            DropIndex("dbo.Endereco", new[] { "IdCidade" });
            AlterColumn("dbo.Estado", "IdEstado", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Endereco", "IdCidade");
            DropTable("dbo.Cidade");
            CreateIndex("dbo.Endereco", "IdEstado");
            AddForeignKey("dbo.Endereco", "IdEstado", "dbo.Estado", "IdEstado");
        }
    }
}
