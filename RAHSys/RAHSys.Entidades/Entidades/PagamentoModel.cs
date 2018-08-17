using System;

namespace RAHSys.Entidades.Entidades
{
    public class PagamentoModel
    {
        public int IdPagamento { get; set; }
        public int IdContrato { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataPagamento { get; set; }
        public string Observacao { get; set; }

        public virtual ContratoModel Contrato { get; set; }
    }
}
