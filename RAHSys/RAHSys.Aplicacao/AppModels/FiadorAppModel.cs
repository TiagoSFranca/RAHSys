namespace RAHSys.Aplicacao.AppModels
{
    public class FiadorAppModel
    {
        public int IdFiador { get; set; }
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public bool Conjuge { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int? IdFiadorEndereco { get; set; }

        public virtual FiadorEnderecoAppModel FiadorEndereco { get; set; }
    }
}
