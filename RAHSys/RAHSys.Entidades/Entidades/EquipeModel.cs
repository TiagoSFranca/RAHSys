using System.Collections.Generic;

namespace RAHSys.Entidades.Entidades
{
    public class EquipeModel
    {
        public int IdEquipe { get; set; }
        public string IdLider { get; set; }
        public string Descricao { get; set; }

        public virtual UsuarioModel Lider { get; set; }

        public virtual ICollection<EquipeUsuarioModel> EquipeUsuarios { get; set; }
        public virtual ICollection<ClienteModel> Clientes { get; set; }
    }
}
