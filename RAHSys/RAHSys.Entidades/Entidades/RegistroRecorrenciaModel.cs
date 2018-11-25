using System;

namespace RAHSys.Entidades.Entidades
{
    public class RegistroRecorrenciaModel
    {
        public int IdRegistroRecorrencia { get; set; }
        public int IdAtividade { get; set; }
        public DateTime DataPrevista { get; set; }
        public DateTime DataRealizacao { get; set; }
        public string Observacao { get; set; }

        public virtual AtividadeModel Atividade { get; set; }
    }
}
