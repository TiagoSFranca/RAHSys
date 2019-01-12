using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    class AtividadeDiaSemanaConfiguracao : EntityTypeConfiguration<AtividadeDiaSemanaModel>
    {
        public AtividadeDiaSemanaConfiguracao()
        {
            ToTable("AtividadeDiaSemana");

            HasKey(c => c.IdAtividadeDiaSemana);

            HasRequired(e => e.ConfiguracaoAtividade)
                .WithMany(s => s.AtividadeDiaSemanas)
                .HasForeignKey(e => e.IdConfiguracaoAtividade);

            HasRequired(e => e.DiaSemana)
                .WithMany(s => s.AtividadeDiaSemanas)
                .HasForeignKey(e => e.IdDiaSemana);
        }
    }
}
