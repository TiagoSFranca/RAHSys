using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class TipoContatoConfiguracao : EntityTypeConfiguration<TipoContatoModel>
    {
        public TipoContatoConfiguracao()
        {
            ToTable("TipoContato");

            HasKey(c => c.IdTipoContato);

            Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
