using RAHSys.Entidades.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class ConfiguracaoAtividadeConfiguracao : EntityTypeConfiguration<ConfiguracaoAtividadeModel>
    {
        public ConfiguracaoAtividadeConfiguracao()
        {
            ToTable("ConfiguracaoAtividade");

            HasKey(c => c.IdConfiguracaoAtividade);
            
            Property(c => c.Frequencia)
                .IsRequired();

            Property(c => c.QtdRepeticoes)
                .IsOptional();

            Property(c => c.TerminaEm)
                .IsOptional();

            Property(c => c.DiaMes)
                .IsOptional();

            HasMany(pt => pt.AtividadeDiaSemanas)
                .WithRequired(s => s.ConfiguracaoAtividade)
                .HasForeignKey(s => s.IdConfiguracaoAtividade)
                .WillCascadeOnDelete(true);

            Property(pt => pt.IdConfiguracaoAtividade)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

        }
    }
}
