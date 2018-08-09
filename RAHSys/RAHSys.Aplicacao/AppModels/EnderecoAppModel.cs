namespace RAHSys.Aplicacao.AppModels
{
    public class EnderecoAppModel
    {
        public int IdEndereco { get; set; }
        public int IdEstado { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }

        public EstadoAppModel Estado { get; set; }
    }
}
