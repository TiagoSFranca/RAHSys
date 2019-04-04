using RAHSys.Entidades.Entidades;
using System;

namespace RAHSys.Entidades
{
    public class AtividadeRecorrenciaModel
    {
        public int IdAtividade { get; set; }
        public int IdTipoAtividade { get; set; }
        public int? IdTipoRecorrencia { get; set; }
        public int IdEquipe { get; set; }
        public int IdContrato { get; set; }
        public int? IdRecorrencia { get; set; }
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

        public virtual ContratoModel Contrato { get; set; }

        public virtual EquipeModel Equipe { get; set; }

        public virtual UsuarioModel Usuario { get; set; }

        public AtividadeRecorrenciaModel(int idAtividade, string descricao, TipoAtividadeModel tipoAtividade, ContratoModel contrato,
            EquipeModel equipe, UsuarioModel usuario, TipoRecorrenciaModel tipoRecorrencia, RegistroRecorrenciaModel registroRecorrencia, bool encerrar, int numeroRecorrencia, bool temEvidencias)
        {
            IdAtividade = idAtividade;
            Descricao = descricao;

            IdTipoAtividade = tipoAtividade.IdTipoAtividade;
            TipoAtividade = tipoAtividade.Descricao;

            IdContrato = contrato.IdContrato;
            Contrato = contrato;

            IdEquipe = equipe.IdEquipe;
            Equipe = equipe;

            if (registroRecorrencia.DataRealizacao != DateTime.MinValue)
                DataRealizacao = registroRecorrencia.DataRealizacao;

            DataRealizacaoPrevista = registroRecorrencia.DataPrevista;
            Observacao = registroRecorrencia.Observacao;

            if (usuario != null)
            {
                IdUsuario = usuario.IdUsuario;
                Usuario = usuario;
            }

            if (tipoRecorrencia != null)
            {
                IdTipoRecorrencia = tipoRecorrencia.IdTipoRecorrencia;
                TipoRecorrencia = tipoRecorrencia.Descricao;
            }

            IdRecorrencia = registroRecorrencia.IdRegistroRecorrencia > 0 ? (int?)registroRecorrencia.IdRegistroRecorrencia : null;

            Encerrar = encerrar;
            NumeroRecorrencia = numeroRecorrencia;
            TemEvidencias = temEvidencias;
        }
    }
}
