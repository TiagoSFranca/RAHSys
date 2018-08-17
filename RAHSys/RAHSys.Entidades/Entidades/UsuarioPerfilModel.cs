using System;

namespace RAHSys.Entidades.Entidades
{
    public class UsuarioPerfilModel
    {
        public string IdUsuario { get; set; }
        public string IdPerfil { get; set; }

        public virtual UsuarioModel Usuario { get; set; }
        public virtual PerfilModel Perfil { get; set; }
    }
}
