using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class EquipeConfiguracao : EntityTypeConfiguration<EquipeModel>
    {
        public EquipeConfiguracao()
        {
            ToTable("Equipe");

            HasKey(a => a.IdEquipe);

            Property(a => a.IdLider)
                .HasColumnType("nvarchar");

            Property(a => a.Descricao)
                .IsOptional()
                .HasMaxLength(256);

            HasRequired(e => e.Lider)
                .WithMany(es => es.Equipes)
                .HasForeignKey(e => e.IdLider);

            HasMany(pt => pt.EquipeUsuarios)
                .WithRequired(s => s.Equipe)
                .HasForeignKey(s => s.IdEquipe)
                .WillCascadeOnDelete(true);

            HasMany(pt => pt.Clientes)
                .WithOptional(s => s.Equipe)
                .HasForeignKey(s => s.IdEquipe);
        }
    }
}
