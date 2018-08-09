namespace RAHSys.Entidades.Entidades
{
    public class ContratoEnderecoModel
    {
        public int IdContratoEndereco { get; set; }
        public int IdContrato { get; set; }
        public int IdEndereco { get; set; }

        public virtual ContratoModel Contrato { get; set; }
        public virtual EnderecoModel Endereco { get; set; }
    }
}
