using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class AuditoriaConfiguracao : EntityTypeConfiguration<AuditoriaModel>
    {
        public AuditoriaConfiguracao()
        {
            ToTable("Auditoria");

            HasKey(a => a.IdAuditoria);

            Property(a => a.Usuario)
                .IsRequired()
                .HasMaxLength(256);

            Property(a => a.Funcao)
                .IsRequired()
                .HasMaxLength(100);

            Property(a => a.EnderecoIP)
                .IsRequired()
                .HasMaxLength(30);

            Property(a => a.Acao)
                .IsRequired()
                .HasMaxLength(10);

            Property(a => a.Dados)
                .IsRequired()
                .IsMaxLength();

            Property(a => a.DataHora)
                .IsOptional();
        }
    }
}
