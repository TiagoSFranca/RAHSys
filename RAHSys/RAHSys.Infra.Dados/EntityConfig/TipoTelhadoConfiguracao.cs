using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class TipoTelhadoConfiguracao : EntityTypeConfiguration<TipoTelhadoModel>
    {
        public TipoTelhadoConfiguracao()
        {
            ToTable("TipoTelhado");

            HasKey(c => c.IdTipoTelhado);

            Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(256);

            HasMany(e => e.AnaliseInvestimentos)
                .WithRequired(es => es.TipoTelhado)
                .HasForeignKey(es => es.IdTipoTelhado);

        }
    }
}
