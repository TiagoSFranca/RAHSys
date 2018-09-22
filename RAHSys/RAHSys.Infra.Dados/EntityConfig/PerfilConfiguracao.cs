using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class PerfilConfiguracao : EntityTypeConfiguration<PerfilModel>
    {
        public PerfilConfiguracao()
        {
            ToTable("AspNetRoles");

            HasKey(a => a.IdPerfil);

            Property(a => a.IdPerfil)
                .HasColumnName("Id")
                .HasColumnType("nvarchar");

            Property(a => a.Nome)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(256);

            HasMany(pt => pt.UsuarioPerfis)
                .WithRequired(s => s.Perfil)
                .HasForeignKey(s => s.IdPerfil);
        }
    }
}
