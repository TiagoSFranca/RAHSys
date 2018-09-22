namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadoEquipeeEquipeUsuario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipe",
                c => new
                    {
                        IdEquipe = c.Int(nullable: false, identity: true),
                        IdLider = c.String(nullable: false, maxLength: 128),
                        Descricao = c.String(maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.IdEquipe)
                .ForeignKey("dbo.AspNetUsers", t => t.IdLider, cascadeDelete: true)
                .Index(t => t.IdLider);
            
            CreateTable(
                "dbo.EquipeUsuario",
                c => new
                    {
                        IdEquipe = c.Int(nullable: false),
                        IdUsuario = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.IdEquipe, t.IdUsuario })
                .ForeignKey("dbo.AspNetUsers", t => t.IdUsuario)
                .ForeignKey("dbo.Equipe", t => t.IdEquipe, cascadeDelete: true)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdEquipe);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipe", "IdLider", "dbo.AspNetUsers");
            DropForeignKey("dbo.EquipeUsuario", "IdEquipe", "dbo.Equipe");
            DropForeignKey("dbo.EquipeUsuario", "IdUsuario", "dbo.AspNetUsers");
            DropIndex("dbo.Equipe", new[] { "IdLider" });
            DropIndex("dbo.EquipeUsuario", new[] { "IdEquipe" });
            DropIndex("dbo.EquipeUsuario", new[] { "IdUsuario" });
            DropTable("dbo.EquipeUsuario");
            DropTable("dbo.Equipe");
        }
    }
}
