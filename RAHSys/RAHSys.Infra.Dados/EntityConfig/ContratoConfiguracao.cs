﻿using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class ContratoConfiguracao : EntityTypeConfiguration<ContratoModel>
    {
        public ContratoConfiguracao()
        {
            ToTable("Contrato");

            HasKey(c => c.IdContrato);

            Property(c => c.NomeEmpresa)
                .IsRequired()
                .HasMaxLength(256);

            Property(c => c.ContatoInicial)
                .IsRequired()
                .HasMaxLength(256);

            HasOptional(x => x.ContratoEndereco)
                .WithMany()
                .HasForeignKey(x => x.IdContratoEndereco)
                .WillCascadeOnDelete(true);

            Property(e => e.Excluido)
                .IsRequired();

            HasRequired(e => e.AnaliseInvestimento)
                .WithRequiredPrincipal(s => s.Contrato)
                .WillCascadeOnDelete(true);

            HasMany(e => e.Documentos)
                .WithRequired(es => es.Contrato)
                .HasForeignKey(es => es.IdContrato)
                .WillCascadeOnDelete(true);
        }
    }
}
