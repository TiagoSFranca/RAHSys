using RAHSys.Entidades.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class ClienteConfiguracao : EntityTypeConfiguration<ClienteModel>
    {
        public ClienteConfiguracao()
        {
            ToTable("Cliente");

            HasKey(c => c.IdAnaliseInvestimento);

            Property(c => c.MediaKW)
                .IsRequired();

            Property(c => c.ConsumoTotal)
                .IsRequired();

            HasRequired(c => c.AnaliseInvestimento)
                .WithRequiredDependent(cc => cc.Cliente);

            HasMany(e => e.Fiadores)
                .WithRequired(es => es.Cliente)
                .HasForeignKey(es => es.IdCliente)
                .WillCascadeOnDelete(true);

            Property(pt => pt.IdAnaliseInvestimento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

        }
    }
}
