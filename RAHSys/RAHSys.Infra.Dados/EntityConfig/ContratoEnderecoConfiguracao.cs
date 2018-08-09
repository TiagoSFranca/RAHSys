using RAHSys.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class ContratoEnderecoConfiguracao : EntityTypeConfiguration<ContratoEnderecoModel>
    {
        public ContratoEnderecoConfiguracao()
        {
            ToTable("ContratoEndereco");

            HasKey(c => c.IdContratoEndereco);

            HasRequired(e => e.Contrato)
                .WithRequiredDependent(es => es.ContratoEndereco);

            HasRequired(e => e.Endereco)
                .WithMany(es => es.ContratoEnderecos)
                .HasForeignKey(e => e.IdEndereco);

            Property(pt => pt.IdContrato)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(pt => pt.IdEndereco)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(pt => pt.IdContratoEndereco)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
