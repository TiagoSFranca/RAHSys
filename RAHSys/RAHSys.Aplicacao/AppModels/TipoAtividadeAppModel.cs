using System.ComponentModel.DataAnnotations;

namespace RAHSys.Aplicacao.AppModels
{
    public class TipoAtividadeAppModel
    {
        public int IdTipoAtividade { get; set; }

        [Required]
        [MaxLength(64)]
        public string Descricao { get; set; }
    }
}
