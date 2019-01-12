namespace RAHSys.Entidades.Entidades
{
    public class AtividadeDiaSemanaModel
    {
        public int IdAtividadeDiaSemana { get; set; }
        public int IdConfiguracaoAtividade { get; set; }
        public int IdDiaSemana { get; set; }

        public virtual ConfiguracaoAtividadeModel ConfiguracaoAtividade { get; set; }
        public virtual DiaSemanaModel DiaSemana { get; set; }
    }
}
