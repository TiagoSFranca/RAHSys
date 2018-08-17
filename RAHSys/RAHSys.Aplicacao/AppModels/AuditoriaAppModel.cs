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
        [Display(Name = "Função")]
        public string Funcao { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Ação")]
        public string Acao { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Data e Hora")]
        public DateTime DataHora { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Endereço de IP")]
        public string EnderecoIP { get; set; }

        [Required]
        [Display(Name = "Dados")]
        public string Dados { get; set; }

    }
}
