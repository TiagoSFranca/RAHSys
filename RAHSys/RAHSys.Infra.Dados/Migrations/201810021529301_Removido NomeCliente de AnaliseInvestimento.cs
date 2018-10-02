namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovidoNomeClientedeAnaliseInvestimento : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AnaliseInvestimento", "NomeCliente");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AnaliseInvestimento", "NomeCliente", c => c.String(nullable: false, maxLength: 256, unicode: false));
        }
    }
}
