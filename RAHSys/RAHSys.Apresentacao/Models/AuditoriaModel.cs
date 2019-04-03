using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace RAHSys.Apresentacao.Models
{
    [Table("Auditoria")]
    public class AuditoriaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAuditoria { get; set; }

        [Required]
        [MaxLength(256)]
        public string Usuario { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Funcao { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Acao { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; }

        [Required]
        [MaxLength(30)]
        public string EnderecoIP { get; set; }

        [Required]
        public string Dados { get; set; }

    }

    public class ApplicationAuditoriaDbContext : DbContext
    {
        public ApplicationAuditoriaDbContext()
            : base("RAHSysAuditConexao")
        {
        }

        public DbSet<AuditoriaModel> Auditoria { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Generic Config

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == "Id" + p.ReflectedType.Name)
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("VARCHAR"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(256));

            #endregion
        }
    }

}