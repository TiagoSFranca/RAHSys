namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoApenasDiaUtilaConfiguracaoAtividade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConfiguracaoAtividade", "ApenasDiaUtil", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConfiguracaoAtividade", "ApenasDiaUtil");
        }
    }
}
