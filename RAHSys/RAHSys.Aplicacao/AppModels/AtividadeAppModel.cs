using System;

namespace RAHSys.Aplicacao.AppModels
{
    public class AtividadeAppModel
    {
        public int IdAtividade { get; set; }
        public int IdTipoAtividade { get; set; }
        public int IdEquipe { get; set; }
        public int IdContrato { get; set; }
        public int? IdUsuario { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public bool Realizada { get; set; }
        public DateTime DataPrevista { get; set; }
        public DateTime? DataRealizacao { get; set; }

        public virtual TipoAtividadeAppModel TipoAtividade { get; set; }
        public virtual EquipeAppModel Equipe { get; set; }
        public virtual ContratoAppModel Contrato { get; set; }
        public virtual UsuarioAppModel Usuario { get; set; }
    }
}
