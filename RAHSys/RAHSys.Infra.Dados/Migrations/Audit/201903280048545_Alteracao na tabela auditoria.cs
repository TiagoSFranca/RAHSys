namespace RAHSys.Infra.Dados.Migrations.Audit
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alteracaonatabelaauditoria : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Auditoria", "Funcao", c => c.String(nullable: false, maxLength: 3000, unicode: false));
            AlterColumn("dbo.Auditoria", "Acao", c => c.String(nullable: false, maxLength: 3000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Auditoria", "Acao", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Auditoria", "Funcao", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
    }
}
