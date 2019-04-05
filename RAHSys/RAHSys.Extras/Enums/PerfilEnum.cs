namespace RAHSys.Extras.Enums
{
    public class PerfilEnum
    {
        public string Nome { get; set; }

        public static PerfilEnum Admin => new PerfilEnum() { Nome = "Admin" };

        public static PerfilEnum Comercial => new PerfilEnum() { Nome = "Comercial" };

        public static PerfilEnum Engenharia => new PerfilEnum() { Nome = "Engenharia" };

        public static PerfilEnum Financeiro => new PerfilEnum() { Nome = "Financeiro" };

    }
}
