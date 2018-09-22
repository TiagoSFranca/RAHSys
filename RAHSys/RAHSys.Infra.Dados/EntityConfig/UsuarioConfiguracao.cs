using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class UsuarioConfiguracao : EntityTypeConfiguration<UsuarioModel>
    {
        public UsuarioConfiguracao()
        {
            ToTable("AspNetUsers");

            HasKey(a => a.IdUsuario);

            Property(a => a.IdUsuario)
                .HasColumnName("Id")
                .HasColumnType("nvarchar");

            Property(a => a.Email)
                .IsOptional()
                .HasColumnName("Email")
                .HasMaxLength(256);

            Property(a => a.UserName)
                .IsRequired()
                .HasColumnName("UserName")
                .HasMaxLength(256);

            HasMany(pt => pt.UsuarioPerfis)
                .WithRequired(s => s.Usuario)
                .HasForeignKey(s => s.IdUsuario);

            HasMany(pt => pt.Equipes)
                .WithRequired(s => s.Lider)
                .HasForeignKey(s => s.IdLider)
                .WillCascadeOnDelete(true);

            HasMany(pt => pt.EquipeUsuarios)
                .WithRequired(s => s.Usuario)
                .HasForeignKey(s => s.IdUsuario);
        }
    }
}
