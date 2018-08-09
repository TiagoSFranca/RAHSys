namespace RAHSys.Aplicacao.AppModels
{
    public class AnaliseInvestimentoAppModel
    {
        public int IdContrato { get; set; }
        public int IdTipoTelhado { get; set; }
        public string NomeCliente { get; set; }
        public decimal Potencia { get; set; }
        public decimal Investimento { get; set; }
        public decimal ConsumoTotal { get; set; }
        public int NumeroPlacas { get; set; }
        public string TipoPlacas { get; set; }
        public int QtdInversores { get; set; }
        public string TipoInversores { get; set; }

        public virtual TipoTelhadoAppModel TipoTelhado { get; set; }
    }
}
