using System.ComponentModel.DataAnnotations;

namespace RAHSys.Aplicacao.AppModels
{
    public class TipoTelhadoAppModel
    {
        public int IdTipoTelhado { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string Descricao { get; set; }
    }
}
