namespace RAHSys.Extras.Enums
{
    public class SituacaoContratoEnum
    {
        public static SituacaoContrato NovoContrato { get { return new SituacaoContrato("Novo Contrato", "font-green-jungle"); } }
        public static SituacaoContrato EmAnalise { get { return new SituacaoContrato("Em análise/Estudo", "font-red"); } }
        public static SituacaoContrato ContratoAssinado { get { return new SituacaoContrato("Contrato assinado", "font-blue"); } }
        
    }

    public class SituacaoContrato
    {
        public string Nome { get; set; }
        public string Classe { get; set; }

        public SituacaoContrato(string nome, string classe)
        {
            Nome = nome;
            Classe = classe;
        }
    }
}
