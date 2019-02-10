using RAHSys.Extras.Enums;
using System;

namespace RAHSys.Aplicacao.AppModels
{
    public class AtividadeRecorrenciaAppModel
    {
        public int IdAtividade { get; set; }
        public int IdTipoAtividade { get; set; }
        public int? IdTipoRecorrencia { get; set; }
        public int IdEquipe { get; set; }
        public int IdContrato { get; set; }
        public string IdUsuario { get; set; }
        public string Descricao { get; set; }
        public bool Realizada { get; set; }
        public bool Encerrar { get; set; }
        public DateTime DataRealizacaoPrevista { get; set; }
        public DateTime? DataRealizacao { get; set; }
        public string Observacao { get; set; }
        public int NumeroRecorrencia { get; set; }
        public bool EquipeInteira { get; set; }
        public bool TemEvidencias { get; set; }

        public string TipoAtividade { get; set; }

        public string TipoRecorrencia { get; set; }

        public string Contrato { get; set; }

        public virtual EquipeAppModel Equipe { get; set; }

        public virtual UsuarioAppModel Usuario { get; set; }

        public virtual SituacaoAtividade SituacaoRecorrencia
        {
            get
            {
                if (Realizada)
                    return SituacaoAtividadeEnum.RecorrenciaRealizada;
                else
                {
                    if (DataRealizacaoPrevista.Date < DateTime.Now.Date)
                        return SituacaoAtividadeEnum.RecorrenciaAtrasada;
                    else
                        return SituacaoAtividadeEnum.RecorrenciaNaoRealizada;
                }
            }
        }
    }
}
