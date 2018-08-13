using RAHSys.Entidades.Entidades;
using RAHSys.Infra.Dados.EntityConfig;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RAHSys.Infra.Dados.Contexto
{
    public class RAHSysContexto : DbContext
    {
        public RAHSysContexto()
            : base("RAHSysConexao")
        {
        }

        public DbSet<CameraModel> Camera { get; set; }
        public DbSet<LogErrorModel> LogError { get; set; }
        public DbSet<TipoTelhadoModel> TipoTelhado { get; set; }
        public DbSet<TipoContatoModel> TipoContato { get; set; }
        public DbSet<ContratoModel> Contrato { get; set; }
        public DbSet<EstadoModel> Estado { get; set; }
        public DbSet<EnderecoModel> Endereco { get; set; }
        public DbSet<ContratoEnderecoModel> ContratoEndereco { get; set; }
        public DbSet<CidadeModel> Cidade { get; set; }
        public DbSet<AnaliseInvestimentoModel> AnaliseInvestimento { get; set; }
        public DbSet<ClienteModel> Cliente { get; set; }
        public DbSet<FiadorModel> Fiador { get; set; }
        public DbSet<FiadorEnderecoModel> FiadorEndereco { get; set; }
        public DbSet<DocumentoModel> Documento { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Generic Config

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == "Id" + p.ReflectedType.Name)
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("VARCHAR"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(256));

            #endregion

            modelBuilder.Configurations.Add(new CameraConfiguracao());
            modelBuilder.Configurations.Add(new LogErrorConfiguracao());
            modelBuilder.Configurations.Add(new TipoTelhadoConfiguracao());
            modelBuilder.Configurations.Add(new TipoContatoConfiguracao());
            modelBuilder.Configurations.Add(new ContratoConfiguracao());
            modelBuilder.Configurations.Add(new EstadoConfiguracao());
            modelBuilder.Configurations.Add(new EnderecoConfiguracao());
            modelBuilder.Configurations.Add(new ContratoEnderecoConfiguracao());
            modelBuilder.Configurations.Add(new CidadeConfiguracao());
            modelBuilder.Configurations.Add(new AnaliseInvestimentoConfiguracao());
            modelBuilder.Configurations.Add(new ClienteConfiguracao());
            modelBuilder.Configurations.Add(new FiadorConfiguracao());
            modelBuilder.Configurations.Add(new FiadorEnderecoConfiguracao());
            modelBuilder.Configurations.Add(new DocumentoConfiguracao());
        }
    }
}
