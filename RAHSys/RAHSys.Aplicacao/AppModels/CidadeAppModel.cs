namespace RAHSys.Aplicacao.AppModels
{
    public class CidadeAppModel
    {
        public int IdCidade { get; set; }
        public int IdEstado { get; set; }
        public string CodCidade { get; set; }
        public string Nome { get; set; }

        public virtual EstadoAppModel Estado { get; set; }

    }
}
