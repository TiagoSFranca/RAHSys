namespace RAHSys.Aplicacao.AppModels
{
    public class UsuarioPerfilAppModel
    {
        public string IdUsuario { get; set; }
        public string IdPerfil { get; set; }

        public PerfilAppModel Perfil { get; set; }
    }
}
