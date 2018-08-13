using RAHSys.Entidades.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    class EstadoCivilConfiguracao : EntityTypeConfiguration<EstadoCivilModel>
    {
        public EstadoCivilConfiguracao()
        {
            ToTable("EstadoCivil");

            HasKey(c => c.IdEstadoCivil);

            Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(32);
            
            HasMany(e => e.Fiadores)
                .WithRequired(es => es.EstadoCivil)
                .HasForeignKey(es => es.IdEstadoCivil);

            Property(pt => pt.IdEstadoCivil)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
