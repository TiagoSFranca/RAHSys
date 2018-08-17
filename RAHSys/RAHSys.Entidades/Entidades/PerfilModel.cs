using System;
using System.Collections.Generic;

namespace RAHSys.Entidades.Entidades
{
    public class PerfilModel
    {
        public string IdPerfil { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<UsuarioPerfilModel> UsuarioPerfis { get; set; }
    }
}
