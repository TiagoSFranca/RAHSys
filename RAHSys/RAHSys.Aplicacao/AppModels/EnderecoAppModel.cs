namespace RAHSys.Aplicacao.AppModels
{
    public class EnderecoAppModel
    {
        public int IdEndereco { get; set; }
        public int IdCidade { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }

        public virtual CidadeAppModel Cidade { get; set; }

    }
}
