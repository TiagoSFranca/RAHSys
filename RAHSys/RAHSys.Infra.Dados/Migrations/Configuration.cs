namespace RAHSys.Infra.Dados.Migrations
{
    using Entidades.Entidades;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<RAHSys.Infra.Dados.Contexto.RAHSysContexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Contexto.RAHSysContexto context)
        {
            context.Estado.AddOrUpdate(new EstadoModel() { IdEstado = 1, Descricao = "TESTE 01", Sigla = "T1" });
            context.Estado.AddOrUpdate(new EstadoModel() { IdEstado = 2, Descricao = "TESTE 02", Sigla = "T2" });

            context.Cidade.AddOrUpdate(new CidadeModel() { IdCidade = 1, IdEstado = 1, CodCidade = "CT01", Nome = "Cidade Teste 01" });
            context.Cidade.AddOrUpdate(new CidadeModel() { IdCidade = 2, IdEstado = 1, CodCidade = "CT02", Nome = "Cidade Teste 02" });
            context.Cidade.AddOrUpdate(new CidadeModel() { IdCidade = 3, IdEstado = 1, CodCidade = "CT03", Nome = "Cidade Teste 03" });

            context.Cidade.AddOrUpdate(new CidadeModel() { IdCidade = 11, IdEstado = 2, CodCidade = "CT01", Nome = "Cidade Teste 11" });
            context.Cidade.AddOrUpdate(new CidadeModel() { IdCidade = 12, IdEstado = 2, CodCidade = "CT02", Nome = "Cidade Teste 12" });
            context.Cidade.AddOrUpdate(new CidadeModel() { IdCidade = 13, IdEstado = 2, CodCidade = "CT03", Nome = "Cidade Teste 13" });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
