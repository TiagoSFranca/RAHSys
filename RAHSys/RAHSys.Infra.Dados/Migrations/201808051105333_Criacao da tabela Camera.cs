namespace RAHSys.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaodatabelaCamera : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Camera",
                c => new
                    {
                        IdCamera = c.Int(nullable: false, identity: true),
                        Localizacao = c.String(nullable: false, maxLength: 256, unicode: false),
                        Descricao = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.IdCamera);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Camera");
        }
    }
}
