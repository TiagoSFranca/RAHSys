namespace RAHSys.Entidades.Entidades
{
    public class FiadorEnderecoModel
    {
        public int IdFiadorEndereco { get; set; }
        public int IdFiador { get; set; }
        public int IdEndereco { get; set; }

        public virtual FiadorModel Fiador { get; set; }
        public virtual EnderecoModel Endereco { get; set; }
    }
}
