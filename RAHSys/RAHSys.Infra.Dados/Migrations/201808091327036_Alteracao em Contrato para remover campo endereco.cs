namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoemContratopararemovercampoendereco : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Contrato", "Endereco");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contrato", "Endereco", c => c.String(nullable: false, maxLength: 256, unicode: false));
        }
    }
}
