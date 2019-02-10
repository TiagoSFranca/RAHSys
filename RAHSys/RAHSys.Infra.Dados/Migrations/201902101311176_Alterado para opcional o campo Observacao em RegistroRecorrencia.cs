namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteradoparaopcionalocampoObservacaoemRegistroRecorrencia : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegistroRecorrencia", "Observacao", c => c.String(maxLength: 256, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegistroRecorrencia", "Observacao", c => c.String(nullable: false, maxLength: 256, unicode: false));
        }
    }
}
