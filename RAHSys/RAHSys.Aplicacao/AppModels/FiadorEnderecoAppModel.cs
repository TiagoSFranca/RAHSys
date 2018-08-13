namespace RAHSys.Aplicacao.AppModels
{
    public class FiadorEnderecoAppModel
    {
        public int IdFiadorEndereco { get; set; }
        public int IdFiador { get; set; }
        public int IdEndereco { get; set; }
        
        public virtual EnderecoAppModel Endereco { get; set; }
    }
}
