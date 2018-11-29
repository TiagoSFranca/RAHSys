namespace RAHSys.Extras.Enums
{
    public class SituacaoAtividadeEnum
    {
        public static SituacaoAtividade RecorrenciaRealizada { get { return new SituacaoAtividade("Recorrência Realizada", "success", "#3598DC"); } }
        public static SituacaoAtividade RecorrenciaNaoRealizada { get { return new SituacaoAtividade("Recorrência Não Realizada", "warning", "#F4D03F"); } }
        public static SituacaoAtividade RecorrenciaAtrasada { get { return new SituacaoAtividade("Recorrência Atrasada", "danger", "#E7505A"); } }

        public static SituacaoAtividade AtividadeFinalizada { get { return new SituacaoAtividade("Atividade Finalizada", "success", "#3598DC"); } }
        public static SituacaoAtividade AtividadeNaoFinalizada { get { return new SituacaoAtividade("Atividade Não Finalizada", "warning", "#F4D03F"); } }
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
