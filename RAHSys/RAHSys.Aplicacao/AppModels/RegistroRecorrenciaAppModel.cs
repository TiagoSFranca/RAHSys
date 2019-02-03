using System;

namespace RAHSys.Aplicacao.AppModels
{
    public class RegistroRecorrenciaAppModel
    {
        public int IdRegistroRecorrencia { get; set; }
        public int IdAtividade { get; set; }
        public DateTime DataPrevista { get; set; }
        public DateTime DataRealizacao { get; set; }
        public string Observacao { get; set; }

        public virtual AtividadeAppModel Atividade { get; set; }
    }
}
