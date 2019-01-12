using System.Collections.Generic;

namespace RAHSys.Entidades.Entidades
{
    public class TipoAtividadeModel
    {
        public int IdTipoAtividade { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<AtividadeModel> Atividades { get; set; }
    }
}
