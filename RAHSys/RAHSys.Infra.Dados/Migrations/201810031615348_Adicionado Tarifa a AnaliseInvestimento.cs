namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoTarifaaAnaliseInvestimento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnaliseInvestimento", "Tarifa", c => c.Decimal(nullable: false, precision: 9, scale: 6));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnaliseInvestimento", "Tarifa");
        }
    }
}
