using RAHSys.Entidades.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    class TipoRecorrenciaConfiguracao : EntityTypeConfiguration<TipoRecorrenciaModel>
    {
        public TipoRecorrenciaConfiguracao()
        {
            ToTable("TipoRecorrencia");

            HasKey(c => c.IdTipoRecorrencia);

            Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(32);

            HasMany(pt => pt.Atividades)
                .WithOptional(s => s.TipoRecorrencia)
                .HasForeignKey(s => s.IdTipoRecorrencia)
                .WillCascadeOnDelete(true);

            Property(pt => pt.IdTipoRecorrencia)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
