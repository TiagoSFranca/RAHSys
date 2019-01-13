using RAHSys.Entidades.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace RAHSys.Infra.Dados.EntityConfig
{
    public class AtividadeConfiguracao : EntityTypeConfiguration<AtividadeModel>
    {
        public AtividadeConfiguracao()
        {
            ToTable("Atividade");

            HasKey(c => c.IdAtividade);

            Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(256);

            Property(a => a.IdUsuario)
                .HasColumnType("nvarchar");

            Property(c => c.DataInicial)
                .IsRequired();

            Property(c => c.EquipeInteira)
                .IsRequired();

            HasRequired(e => e.TipoAtividade)
                .WithMany(s => s.Atividades)
                .HasForeignKey(e => e.IdTipoAtividade);

            HasRequired(e => e.Equipe)
                .WithMany(s => s.Atividades)
                .HasForeignKey(e => e.IdEquipe);

            HasRequired(e => e.Contrato)
                .WithMany(s => s.Atividades)
                .HasForeignKey(e => e.IdContrato);

            HasOptional(e => e.Usuario)
                .WithMany(s => s.Atividades)
                .HasForeignKey(e => e.IdUsuario);

            HasOptional(e => e.TipoRecorrencia)
                .WithMany(s => s.Atividades)
                .HasForeignKey(e => e.IdTipoRecorrencia);

            HasRequired(e => e.ConfiguracaoAtividade)
                .WithRequiredPrincipal(s => s.Atividade)
                .WillCascadeOnDelete(true);

            HasMany(e => e.RegistroRecorrencias)
                .WithRequired(es => es.Atividade)
                .HasForeignKey(es => es.IdAtividade)
                .WillCascadeOnDelete(true);
        }
    }
}
