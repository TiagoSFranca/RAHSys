using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    class LogErrorConfiguracao : EntityTypeConfiguration<LogErrorModel>
    {
        public LogErrorConfiguracao()
        {
            ToTable("LogError");

            HasKey(pt => pt.IdLogError);

            Property(pt => pt.Excecao)
                .IsRequired()
                .HasMaxLength(8000);

            Property(pt => pt.Tipo)
                .IsRequired()
                .HasMaxLength(50);

            Property(pt => pt.Metodo)
                .IsRequired()
                .HasMaxLength(255);

            Property(pt => pt.Mensagem)
                .IsRequired()
                .HasMaxLength(4000);

            Property(pt => pt.DataOcorrencia)
                .IsRequired();

            Property(pt => pt.CodErro)
                .IsRequired();

        }
    }
}
