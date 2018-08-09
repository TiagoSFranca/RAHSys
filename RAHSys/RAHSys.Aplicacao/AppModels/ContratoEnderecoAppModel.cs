namespace RAHSys.Aplicacao.AppModels
{
    public class ContratoEnderecoAppModel
    {
        public int IdContratoEndereco { get; set; }
        public int IdContrato { get; set; }
        public int IdEndereco { get; set; }

        public ContratoAppModel Contrato { get; set; }
        public EnderecoAppModel Endereco { get; set; }
    }
}
