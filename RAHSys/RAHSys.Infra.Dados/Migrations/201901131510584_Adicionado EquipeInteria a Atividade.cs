namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoEquipeInteriaaAtividade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Atividade", "EquipeInteira", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Atividade", "EquipeInteira");
        }
    }
}
