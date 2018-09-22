namespace RAHSys.Entidades.Entidades
{
    public class EquipeUsuarioModel
    {
        public string IdUsuario { get; set; }
        public int IdEquipe { get; set; }

        public virtual UsuarioModel Usuario { get; set; }
        public virtual EquipeModel Equipe { get; set; }
    }
}