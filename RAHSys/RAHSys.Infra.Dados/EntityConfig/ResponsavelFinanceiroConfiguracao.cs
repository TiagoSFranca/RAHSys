using RAHSys.Entidades.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class ResponsavelFinanceiroConfiguracao : EntityTypeConfiguration<ResponsavelFinanceiroModel>
    {
        public ResponsavelFinanceiroConfiguracao()
        {
            ToTable("ResponsavelFinanceiro");

            HasKey(a => a.IdResponsavelFinanceiro);

            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(256);

            Property(c => c.Telefone)
                .IsRequired()
                .HasMaxLength(20);

            Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(256);

            Property(pt => pt.IdResponsavelFinanceiro)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
