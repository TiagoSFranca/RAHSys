using RAHSys.Entidades.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class CidadeConfiguracao : EntityTypeConfiguration<CidadeModel>
    {
        public CidadeConfiguracao()
        {
            ToTable("Cidade");

            HasKey(c => c.IdCidade);

            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(256);

            Property(c => c.CodCidade)
                .IsRequired()
                .HasMaxLength(5);

            HasMany(e => e.Enderecos)
                .WithRequired(es => es.Cidade)
                .HasForeignKey(es => es.IdCidade);

            HasRequired(e => e.Estado)
                .WithMany(es => es.Cidades)
                .HasForeignKey(e => e.IdEstado);

            Property(c => c.IdCidade)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
