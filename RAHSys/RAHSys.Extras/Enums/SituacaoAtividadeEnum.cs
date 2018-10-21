namespace RAHSys.Extras.Enums
{
    public class SituacaoAtividadeEnum
    {
        public static SituacaoAtividade AtividadeRealizada { get { return new SituacaoAtividade("Atividade Realizada", "success", "#3598DC"); } }
        public static SituacaoAtividade AtividadeNaoRealizada { get { return new SituacaoAtividade("Atividade Não Realizada", "warning", "#F4D03F"); } }
        public static SituacaoAtividade AtividadeAtrasada { get { return new SituacaoAtividade("Atividade Atrasada", "danger", "#E7505A"); } }
    }

    public class SituacaoAtividade
    {
        public string Nome { get; set; }
        public string Classe { get; set; }
        public string BGCor { get; set; }

        public SituacaoAtividade(string nome, string classe, string bg)
        {
            Nome = nome;
            Classe = classe;
            BGCor = bg;
        }
    }
}
