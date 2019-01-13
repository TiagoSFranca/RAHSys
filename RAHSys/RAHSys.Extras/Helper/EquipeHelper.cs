namespace RAHSys.Extras.Helper
{
    public class EquipeHelper
    {
        public static EquipeItem EquipeInteira { get { return new EquipeItem() { Codigo = "EQUIPE_INTEIRA", Descricao = "Equipe Inteira" }; } }
    }

    public class EquipeItem
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
