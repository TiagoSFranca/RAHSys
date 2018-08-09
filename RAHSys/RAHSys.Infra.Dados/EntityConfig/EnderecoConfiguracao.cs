﻿using RAHSys.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class EnderecoConfiguracao : EntityTypeConfiguration<EnderecoModel>
    {
        public EnderecoConfiguracao()
        {
            ToTable("Endereco");

            HasKey(c => c.IdEndereco);

            Property(c => c.Logradouro)
                .IsOptional()
                .HasMaxLength(256);

            Property(c => c.Numero)
                .IsOptional()
                .HasMaxLength(12);

            Property(c => c.Bairro)
                .IsOptional()
                .HasMaxLength(128);

            Property(c => c.Cidade)
                .IsOptional()
                .HasMaxLength(128);

            Property(c => c.CEP)
                .IsOptional()
                .HasMaxLength(10);

            HasRequired(e => e.Estado)
                .WithMany(es => es.Enderecos)
                .HasForeignKey(e => e.IdEstado);

            HasMany(e => e.ContratoEnderecos)
                .WithRequired(es => es.Endereco)
                .HasForeignKey(es => es.IdEndereco);
        }
    }
}
