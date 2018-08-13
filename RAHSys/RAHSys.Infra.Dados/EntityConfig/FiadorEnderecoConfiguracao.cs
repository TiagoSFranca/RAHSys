using RAHSys.Entidades.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class FiadorEnderecoConfiguracao : EntityTypeConfiguration<FiadorEnderecoModel>
    {
        public FiadorEnderecoConfiguracao()
        {
            ToTable("FiadorEndereco");

            HasKey(c => c.IdFiadorEndereco);

            HasRequired(e => e.Fiador)
                .WithMany()
                .HasForeignKey(es => es.IdFiador);

            HasRequired(e => e.Endereco)
                .WithMany(es => es.FiadorEnderecos)
                .HasForeignKey(e => e.IdEndereco);

            Property(pt => pt.IdFiador)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(pt => pt.IdEndereco)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(pt => pt.IdFiadorEndereco)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
