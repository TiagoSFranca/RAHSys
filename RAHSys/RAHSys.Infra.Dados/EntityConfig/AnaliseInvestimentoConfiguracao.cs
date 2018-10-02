using RAHSys.Entidades.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class AnaliseInvestimentoConfiguracao : EntityTypeConfiguration<AnaliseInvestimentoModel>
    {
        public AnaliseInvestimentoConfiguracao()
        {
            ToTable("AnaliseInvestimento");

            HasKey(c => c.IdContrato);

            Property(c => c.Investimento)
                .IsRequired();
            
            Property(c => c.NumeroPlacas)
                .IsRequired();

            Property(c => c.Potencia)
                .IsRequired();

            Property(c => c.QtdInversores)
                .IsRequired();

            Property(c => c.TipoInversores)
                .IsRequired()
                .HasMaxLength(256);

            Property(c => c.TipoPlacas)
                .IsRequired()
                .HasMaxLength(256);

            HasRequired(c => c.TipoTelhado)
                .WithMany(tt => tt.AnaliseInvestimentos)
                .HasForeignKey(c => c.IdTipoTelhado);

            HasRequired(e => e.Cliente)
                .WithRequiredPrincipal(s => s.AnaliseInvestimento)
                .WillCascadeOnDelete(true);

            HasRequired(c => c.Contrato)
                .WithRequiredDependent(cc => cc.AnaliseInvestimento);

            Property(pt => pt.IdContrato)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

        }
    }
}
