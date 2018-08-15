using RAHSys.Entidades.Entidades;
using RAHSys.Infra.Dados.EntityConfig;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAHSys.Infra.Dados.Contexto
{
    public class RAHSysAuditContexto : DbContext
    {
        public RAHSysAuditContexto() : base ("RAHSysAuditConexao")
        {
        }

        public DbSet<AuditoriaModel> Auditoria { get; set; }

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

            modelBuilder.Configurations.Add(new AuditoriaConfiguracao());
        }
    }
}
