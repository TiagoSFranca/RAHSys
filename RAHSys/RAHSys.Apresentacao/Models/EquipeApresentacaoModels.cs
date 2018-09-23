using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Apresentacao.Models
{
    public class EquipeAdicionarEditarModel
    {
        public EquipeAppModel Equipe { get; set; }
        public List<UsuarioAppModel> Lideres { get; set; }
        public List<List<UsuarioAppModel>> Integrantes { get; set; }
        public List<string> IdIntegrantes { get; set; }
    }

    public class UsuarioMultiSelectList
    {
        public string Perfil { get; set; }
        public List<UsuarioMultiSelect> Usuarios { get; set; }

        public UsuarioMultiSelectList()
        {
            Usuarios = new List<UsuarioMultiSelect>();
        }
    }

    public class UsuarioMultiSelect
    {
        public string IdUsuario { get; set; }
        public string EmailEUserName { get; set; }

        public UsuarioMultiSelect(string id, string email)
        {
            this.IdUsuario = id;
            this.EmailEUserName = email;
        }
    }
}