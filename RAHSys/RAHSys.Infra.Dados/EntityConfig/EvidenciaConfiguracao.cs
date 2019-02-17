using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class EvidenciaConfiguracao : EntityTypeConfiguration<EvidenciaModel>
    {
        public EvidenciaConfiguracao()
        {
            ToTable("Evidencia");

            HasKey(c => c.IdEvidencia);

            Property(c => c.CaminhoArquivo)
                .IsRequired()
                .HasMaxLength(256);

            Property(c => c.NomeArquivo)
                .IsRequired()
                .HasMaxLength(256);

            Property(c => c.DataUpload)
                .IsRequired();

            HasRequired(e => e.RegistroRecorrencia)
                .WithMany(es => es.Evidencias)
                .HasForeignKey(e => e.IdRegistroRecorrencia);

        }
    }
}
