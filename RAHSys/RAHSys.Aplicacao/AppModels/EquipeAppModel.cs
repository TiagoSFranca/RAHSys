using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public virtual string ObterEquipe()
        {
            var equipe = string.Empty;

            foreach (var usuario in (EquipeUsuarios ?? new List<EquipeUsuarioAppModel>()))
            {
                equipe += usuario.Usuario.EmailEUserName + "<br/>";
            }

            return equipe;
        }

        public virtual string ObterLider
        {
            get
            {
                return Lider.EmailEUserName;
            }
        }

        public virtual string ObterEquipeFormatada()
        {
            string equipe = string.Empty;

            equipe += "<strong>Líder:</strong><br>" + ObterLider + "<br/>";
            if (EquipeUsuarios?.Count > 0)
                equipe += "<strong>Integrantes:</strong><br>" + ObterEquipe();

            return equipe;
        }

        public virtual List<UsuarioAppModel> Usuarios
        {
            get
            {
                var lista = new List<UsuarioAppModel>
                {
                    Lider
                };
                lista.AddRange(this.EquipeUsuarios?.Select(e => e.Usuario).ToList());
                return lista;
            }
        }

    }
}
