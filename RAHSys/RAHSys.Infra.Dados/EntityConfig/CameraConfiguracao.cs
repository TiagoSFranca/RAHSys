using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class CameraConfiguracao : EntityTypeConfiguration<CameraModel>
    {
        public CameraConfiguracao()
        {
            ToTable("Camera");

            HasKey(c => c.IdCamera);

            Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(256);

            Property(c => c.Localizacao)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
