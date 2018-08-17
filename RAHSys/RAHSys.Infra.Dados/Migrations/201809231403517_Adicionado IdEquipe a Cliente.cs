namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoIdEquipeaCliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "IdEquipe", c => c.Int());
            CreateIndex("dbo.Cliente", "IdEquipe");
            AddForeignKey("dbo.Cliente", "IdEquipe", "dbo.Equipe", "IdEquipe");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cliente", "IdEquipe", "dbo.Equipe");
            DropIndex("dbo.Cliente", new[] { "IdEquipe" });
            DropColumn("dbo.Cliente", "IdEquipe");
        }
    }
}
