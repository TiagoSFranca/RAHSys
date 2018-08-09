﻿using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    class EstadoConfiguracao : EntityTypeConfiguration<EstadoModel>
    {
        public EstadoConfiguracao()
        {
            ToTable("Estado");

            HasKey(c => c.IdEstado);

            Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(32);

            Property(c => c.Sigla)
                .IsRequired()
                .HasMaxLength(2);

            HasMany(e => e.Enderecos)
                .WithRequired(es => es.Estado)
                .HasForeignKey(es => es.IdEstado);
        }
    }
}
