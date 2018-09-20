namespace RAHSys.Extras.Enums
{
    public class SituacaoPagamentoEnum
    {
        public static SituacaoPagamento NenhumPagamento { get { return new SituacaoPagamento("Nenhum Pagamento Registrado", "grey"); } }
        public static SituacaoPagamento Pago { get { return new SituacaoPagamento("Pago", "green-jungle"); } }
        public static SituacaoPagamento NaoPago { get { return new SituacaoPagamento("Não Pago", "red-thunderbird"); } }

    }

    public class SituacaoPagamento
    {
        public string Nome { get; set; }
        public string Classe { get; set; }

        public SituacaoPagamento(string nome, string classe)
        {
            Nome = nome;
            Classe = classe;
        }
    }
}
