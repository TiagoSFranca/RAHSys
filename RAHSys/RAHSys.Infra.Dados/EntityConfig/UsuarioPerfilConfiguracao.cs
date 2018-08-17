using RAHSys.Entidades.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class UsuarioPerfilConfiguracao : EntityTypeConfiguration<UsuarioPerfilModel>
    {
        public UsuarioPerfilConfiguracao()
        {
            ToTable("AspNetUserRoles");

            HasKey(a => new { a.IdUsuario, a.IdPerfil });

            Property(a => a.IdUsuario)
                .HasColumnName("UserId")
                .HasColumnType("nvarchar");

            Property(a => a.IdPerfil)
                .HasColumnName("RoleId")
                .HasColumnType("nvarchar");

            Property(ppc => ppc.IdUsuario)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(ppc => ppc.IdPerfil)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
