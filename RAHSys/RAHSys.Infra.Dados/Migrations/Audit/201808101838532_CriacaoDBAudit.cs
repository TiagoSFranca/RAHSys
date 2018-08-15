namespace RAHSys.Infra.Dados.Migrations.Audit
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoDBAudit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auditoria",
                c => new
                    {
                        IdAuditoria = c.Int(nullable: false, identity: true),
                        Usuario = c.String(nullable: false, maxLength: 256, unicode: false),
                        Funcao = c.String(nullable: false, maxLength: 100, unicode: false),
                        Acao = c.String(nullable: false, maxLength: 10, unicode: false),
                        DataHora = c.DateTime(),
                        EnderecoIP = c.String(nullable: false, maxLength: 30, unicode: false),
                        Dados = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.IdAuditoria);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Auditoria");
        }
    }
}
