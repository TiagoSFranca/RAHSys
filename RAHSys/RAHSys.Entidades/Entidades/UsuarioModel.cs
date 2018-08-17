using System;
using System.Collections.Generic;

namespace RAHSys.Entidades.Entidades
{
    public class UsuarioModel
    {
        public string IdUsuario { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<UsuarioPerfilModel> UsuarioPerfis { get; set; }
        public virtual ICollection<EquipeModel> Equipes { get; set; }
        public virtual ICollection<EquipeUsuarioModel> EquipeUsuarios { get; set; }
    }
}
