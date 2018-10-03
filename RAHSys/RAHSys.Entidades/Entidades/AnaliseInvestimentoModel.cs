namespace RAHSys.Entidades.Entidades
{
    public class AnaliseInvestimentoModel
    {
        public int IdContrato { get; set; }
        public int IdTipoTelhado { get; set; }
        public decimal Potencia { get; set; }
        public decimal Investimento { get; set; }
        public decimal ConsumoTotal { get; set; }
        public decimal Tarifa { get; set; }
        public int NumeroPlacas { get; set; }
        public string TipoPlacas { get; set; }
        public int QtdInversores { get; set; }
        public string TipoInversores { get; set; }

        public virtual TipoTelhadoModel TipoTelhado { get; set; }
        public virtual ContratoModel Contrato { get; set; }
        public virtual ClienteModel Cliente { get; set; }
    }
}
