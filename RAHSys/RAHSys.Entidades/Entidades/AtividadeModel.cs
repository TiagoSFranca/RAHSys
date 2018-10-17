using System;

namespace RAHSys.Entidades.Entidades
{
    public class AtividadeModel
    {
        public int IdAtividade { get; set; }
        public int IdTipoAtividade { get; set; }
        public int IdEquipe { get; set; }
        public int IdContrato { get; set; }
        public string IdUsuario { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public bool Realizada { get; set; }
        public DateTime DataPrevista { get; set; }
        public DateTime? DataRealizacao { get; set; }

        public virtual TipoAtividadeModel TipoAtividade { get; set; }
        public virtual EquipeModel Equipe { get; set; }
        public virtual ContratoModel Contrato { get; set; }
        public virtual UsuarioModel Usuario { get; set; }
    }
}
