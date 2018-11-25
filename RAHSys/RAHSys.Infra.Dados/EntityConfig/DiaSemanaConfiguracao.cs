using RAHSys.Entidades.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    class DiaSemanaConfiguracao : EntityTypeConfiguration<DiaSemanaModel>
    {
        public DiaSemanaConfiguracao()
        {
            ToTable("DiaSemana");

            HasKey(c => c.IdDiaSemana);

            Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(32);

            HasMany(pt => pt.AtividadeDiaSemanas)
                .WithRequired(s => s.DiaSemana)
                .HasForeignKey(s => s.IdDiaSemana)
                .WillCascadeOnDelete(true);

            Property(pt => pt.IdDiaSemana)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
