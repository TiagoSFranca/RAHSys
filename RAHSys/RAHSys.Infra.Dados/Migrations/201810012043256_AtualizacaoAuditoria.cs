namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizacaoAuditoria : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cidade", "IdCidade", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cidade", "IdCidade", c => c.Int(nullable: false));
        }
    }
}
