namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicioandoExcluidoaContrato : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contrato", "Excluido", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contrato", "Excluido");
        }
    }
}
