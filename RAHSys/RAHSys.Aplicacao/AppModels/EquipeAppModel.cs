using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RAHSys.Aplicacao.AppModels
{
    public class EquipeAppModel
    {
        [Display(Name = "Código da Equipe")]
        public int IdEquipe { get; set; }

        [Display(Name = "Líder da Equipe")]
        [Required]
        public string IdLider { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public UsuarioAppModel Lider { get; set; }

        public virtual List<EquipeUsuarioAppModel> EquipeUsuarios { get; set; }

        public virtual string OnterEquipe()
        {
            var equipe = string.Empty;

            foreach (var usuario in (EquipeUsuarios ?? new List<EquipeUsuarioAppModel>()))
            {
                equipe += usuario.Usuario.EmailEUserName + "<br/>";
            }

            return equipe;
        }
    }
}
