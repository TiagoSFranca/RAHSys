using RAHSys.Entidades.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    class PagamentoConfiguracao : EntityTypeConfiguration<PagamentoModel>
    {
        public PagamentoConfiguracao()
        {
            ToTable("Pagamento");

            HasKey(c => c.IdPagamento);

            Property(c => c.DataPagamento)
                .IsRequired();

            Property(c => c.DataCriacao)
                .IsRequired();

            Property(c => c.Observacao)
                .IsRequired()
                .HasMaxLength(256);

            HasRequired(e => e.Contrato)
                .WithMany(es => es.Pagamentos)
                .HasForeignKey(e => e.IdContrato);
            
            Property(pt => pt.IdPagamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
