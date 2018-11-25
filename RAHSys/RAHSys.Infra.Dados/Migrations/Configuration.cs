namespace RAHSys.Infra.Dados.Migrations
{
    using Entidades.Entidades;
    using Entidades.Seeds;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<RAHSys.Infra.Dados.Contexto.RAHSysContexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Contexto.RAHSysContexto context)
        {
            //context.Estado.AddOrUpdate(new EstadoModel() { IdEstado = 1, Descricao = "TESTE 01", Sigla = "T1" });
            //context.Estado.AddOrUpdate(new EstadoModel() { IdEstado = 2, Descricao = "TESTE 02", Sigla = "T2" });

            //context.Cidade.AddOrUpdate(new CidadeModel() { IdCidade = 1, IdEstado = 1, CodCidade = "CT01", Nome = "Cidade Teste 01" });
            //context.Cidade.AddOrUpdate(new CidadeModel() { IdCidade = 2, IdEstado = 1, CodCidade = "CT02", Nome = "Cidade Teste 02" });
            //context.Cidade.AddOrUpdate(new CidadeModel() { IdCidade = 3, IdEstado = 1, CodCidade = "CT03", Nome = "Cidade Teste 03" });

            //context.Cidade.AddOrUpdate(new CidadeModel() { IdCidade = 11, IdEstado = 2, CodCidade = "CT01", Nome = "Cidade Teste 11" });
            //context.Cidade.AddOrUpdate(new CidadeModel() { IdCidade = 12, IdEstado = 2, CodCidade = "CT02", Nome = "Cidade Teste 12" });
            //context.Cidade.AddOrUpdate(new CidadeModel() { IdCidade = 13, IdEstado = 2, CodCidade = "CT03", Nome = "Cidade Teste 13" });

            context.TipoTelhado.AddOrUpdate(new TipoTelhadoModel() { IdTipoTelhado = 1, Descricao = "Tipo 01" });
            context.TipoTelhado.AddOrUpdate(new TipoTelhadoModel() { IdTipoTelhado = 2, Descricao = "Tipo 02" });

            context.EstadoCivil.AddOrUpdate(EstadoCivilSeed.Seeds.ToArray());
            context.TipoRecorrencia.AddOrUpdate(TipoRecorrenciaSeed.Seeds.ToArray());
            context.DiaSemana.AddOrUpdate(DiaSemanaSeed.Seeds.ToArray());
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
