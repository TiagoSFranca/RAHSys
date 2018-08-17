namespace RAHSys.Aplicacao.AppModels
{
    public class EquipeUsuarioAppModel
    {
        public string IdUsuario { get; set; }
        public int IdEquipe { get; set; }

        public UsuarioAppModel Usuario { get; set; }
    }
}
