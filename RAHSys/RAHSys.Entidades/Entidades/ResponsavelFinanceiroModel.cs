namespace RAHSys.Entidades.Entidades
{
    public class ResponsavelFinanceiroModel
    {
        public int IdResponsavelFinanceiro { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public virtual ClienteModel Cliente { get; set; }
    }
}
