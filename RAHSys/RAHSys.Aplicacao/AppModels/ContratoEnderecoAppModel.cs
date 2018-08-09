namespace RAHSys.Aplicacao.AppModels
{
    public class ContratoEnderecoAppModel
    {
        public int IdContratoEndereco { get; set; }
        public int IdContrato { get; set; }
        public int IdEndereco { get; set; }

        public virtual ContratoAppModel Contrato { get; set; }
        public virtual EnderecoAppModel Endereco { get; set; }
    }
}
