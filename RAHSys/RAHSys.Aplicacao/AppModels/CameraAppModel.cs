using System.ComponentModel.DataAnnotations;

namespace RAHSys.Aplicacao.AppModels
{
    public class CameraAppModel
    {
        public int IdCamera { get; set; }

        [Required]
        [MaxLength(256)]
        public string Localizacao { get; set; }

        [Required]
        [MaxLength(256)]
        public string Descricao { get; set; }
    }
}
