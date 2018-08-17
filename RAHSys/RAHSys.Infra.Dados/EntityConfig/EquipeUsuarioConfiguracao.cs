using RAHSys.Entidades.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class EquipeUsuarioConfiguracao : EntityTypeConfiguration<EquipeUsuarioModel>
    {
        public EquipeUsuarioConfiguracao()
        {
            ToTable("EquipeUsuario");

            HasKey(a => new { a.IdEquipe, a.IdUsuario });

            Property(a => a.IdEquipe)
                .IsRequired();

            Property(a => a.IdUsuario)
                .HasColumnType("nvarchar");

            Property(ppc => ppc.IdUsuario)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(ppc => ppc.IdEquipe)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
