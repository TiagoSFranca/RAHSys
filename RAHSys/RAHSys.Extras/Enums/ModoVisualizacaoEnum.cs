namespace RAHSys.Extras.Enums
{
    public class ModoVisualizacaoEnum
    {
        public static ModoVisualizacaoEnum Dia { get { return new ModoVisualizacaoEnum() { Nome = "basicDay" }; } }

        protected ModoVisualizacaoEnum()
        {
        }

        public string Nome { get; set; }
    }
}
