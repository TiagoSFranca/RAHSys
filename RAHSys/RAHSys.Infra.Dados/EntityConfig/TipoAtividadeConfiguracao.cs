using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class TipoAtividadeConfiguracao : EntityTypeConfiguration<TipoAtividadeModel>
    {
        public TipoAtividadeConfiguracao()
        {
            ToTable("TipoAtividade");

            HasKey(c => c.IdTipoAtividade);

            Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(64);

            HasMany(pt => pt.Atividades)
                .WithRequired(s => s.TipoAtividade)
                .HasForeignKey(s => s.IdTipoAtividade)
                .WillCascadeOnDelete(true);
        }
    }
}
