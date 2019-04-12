namespace RAHSys.Infra.Dados.Migrations.Audit
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoColunaDadosAuditoriaPara4000 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Auditoria", "Dados", c => c.String(nullable: false, maxLength: 4000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Auditoria", "Dados", c => c.String(nullable: false, maxLength: 8000, unicode: false));
        }
    }
}
