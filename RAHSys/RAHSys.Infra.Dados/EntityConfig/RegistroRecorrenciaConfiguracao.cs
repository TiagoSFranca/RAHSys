using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    class RegistroRecorrenciaConfiguracao : EntityTypeConfiguration<RegistroRecorrenciaModel>
    {
        public RegistroRecorrenciaConfiguracao()
        {
            ToTable("RegistroRecorrencia");

            HasKey(c => c.IdRegistroRecorrencia);

            HasRequired(e => e.Atividade)
                .WithMany(s => s.RegistroRecorrencias)
                .HasForeignKey(e => e.IdAtividade);

            Property(e => e.DataPrevista)
                .IsRequired();

            Property(e => e.DataRealizacao)
                .IsRequired();

            Property(e => e.Observacao)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
