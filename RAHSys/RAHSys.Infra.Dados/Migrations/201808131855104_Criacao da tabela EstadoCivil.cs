namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodatabelaEstadoCivil : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EstadoCivil",
                c => new
                    {
                        IdEstadoCivil = c.Int(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 32, unicode: false),
                    })
                .PrimaryKey(t => t.IdEstadoCivil);
            
            AddColumn("dbo.Fiador", "IdEstadoCivil", c => c.Int(nullable: false));
            CreateIndex("dbo.Fiador", "IdEstadoCivil");
            AddForeignKey("dbo.Fiador", "IdEstadoCivil", "dbo.EstadoCivil", "IdEstadoCivil");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fiador", "IdEstadoCivil", "dbo.EstadoCivil");
            DropIndex("dbo.Fiador", new[] { "IdEstadoCivil" });
            DropColumn("dbo.Fiador", "IdEstadoCivil");
            DropTable("dbo.EstadoCivil");
        }
    }
}
