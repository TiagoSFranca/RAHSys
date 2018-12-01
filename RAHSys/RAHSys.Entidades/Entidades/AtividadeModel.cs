using System;
using System.Collections.Generic;

namespace RAHSys.Entidades.Entidades
{
    public class AtividadeModel
    {
        public int IdAtividade { get; set; }
        public int IdTipoAtividade { get; set; }
        public int? IdTipoRecorrencia { get; set; }
        public int IdEquipe { get; set; }
        public int IdContrato { get; set; }
        public string IdUsuario { get; set; }
        public string Descricao { get; set; }
        public bool Finalizada { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime? DataFinalizacao { get; set; }

        public virtual ConfiguracaoAtividadeModel ConfiguracaoAtividade { get; set; }
        public virtual TipoAtividadeModel TipoAtividade { get; set; }
        public virtual TipoRecorrenciaModel TipoRecorrencia { get; set; }
        public virtual EquipeModel Equipe { get; set; }
        public virtual ContratoModel Contrato { get; set; }
        public virtual UsuarioModel Usuario { get; set; }

        public virtual ICollection<RegistroRecorrenciaModel> RegistroRecorrencias { get; set; }
    }
}
