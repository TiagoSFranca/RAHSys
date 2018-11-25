namespace RAHSys.Aplicacao.AppModels
{
    public class AtividadeDiaSemanaAppModel
    {
        public int IdAtividadeDiaSemana { get; set; }
        public int IdConfiguracaoAtividade { get; set; }
        public int IdDiaSemana { get; set; }

        public virtual DiaSemanaAppModel DiaSemana { get; set; }
    }
}
