using System;
using System.ComponentModel.DataAnnotations;

namespace RAHSys.Aplicacao.AppModels
{
    public class AuditoriaAppModel
    {
        [Display(Name = "Código Registro Auditado")]
        public int IdAuditoria { get; set; }

        [Required]
        [MaxLength(256)]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string Funcao { get; set; }

        [Required]
        [MaxLength(10)]
        public string Acao { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; }

        [Required]
        [MaxLength(30)]
        public string EnderecoIP { get; set; }

        [Required]
        public string Dados { get; set; }

    }
}
