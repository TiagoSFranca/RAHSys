using System.ComponentModel.DataAnnotations;

namespace RAHSys.Aplicacao.AppModels
{
    public class TipoContatoAppModel
    {
        public int IdTipoContato { get; set; }

        [Required]
        [MaxLength(256)]
        public string Descricao { get; set; }
    }
}
