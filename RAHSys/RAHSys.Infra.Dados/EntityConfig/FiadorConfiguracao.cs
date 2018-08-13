using RAHSys.Entidades.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    class FiadorConfiguracao : EntityTypeConfiguration<FiadorModel>
    {
        public FiadorConfiguracao()
        {
            ToTable("Fiador");

            HasKey(c => c.IdFiador);

            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(256);

            Property(c => c.Conjuge)
                .IsRequired();

            Property(c => c.Telefone)
                .IsRequired()
                .HasMaxLength(20);

            Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(256);

            HasRequired(e => e.Cliente)
                .WithMany(es => es.Fiadores)
                .HasForeignKey(e => e.IdCliente);

            HasRequired(e => e.EstadoCivil)
                .WithMany(es => es.Fiadores)
                .HasForeignKey(e => e.IdEstadoCivil);

            HasOptional(x => x.FiadorEndereco)
                .WithMany()
                .HasForeignKey(x => x.IdFiadorEndereco);

            Property(pt => pt.IdFiador)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
