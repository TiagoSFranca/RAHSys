using System.Collections.Generic;

namespace RAHSys.Aplicacao.AppModels
{
    public class UsuarioAppModel
    {
        public string IdUsuario { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public virtual List<UsuarioPerfilAppModel> UsuarioPerfis { get; set; }

        public virtual string EmailEUserName
        {
            get
            {
                return string.Format("{0} | {1}", this.UserName, this.Email);
            }
        }
    }
}
