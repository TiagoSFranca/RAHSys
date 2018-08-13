using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class DocumentoConfiguracao : EntityTypeConfiguration<DocumentoModel>
    {
        public DocumentoConfiguracao()
        {
            ToTable("Documento");

            HasKey(c => c.IdDocumento);

            Property(c => c.CaminhoArquivo)
                .IsRequired()
                .HasMaxLength(256);

            Property(c => c.NomeArquivo)
                .IsRequired()
                .HasMaxLength(256);

            Property(c => c.DataUpload)
                .IsRequired();

            HasRequired(e => e.Contrato)
                .WithMany(es => es.Documentos)
                .HasForeignKey(e => e.IdContrato);

        }
    }
}
