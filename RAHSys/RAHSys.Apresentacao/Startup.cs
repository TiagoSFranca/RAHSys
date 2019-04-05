using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using RAHSys.Apresentacao.Models;
using RAHSys.Extras.Enums;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(RAHSys.Apresentacao.Startup))]
namespace RAHSys.Apresentacao
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        // Cria os papéis Admin, Comercial e Engenharia e seus usuários padrões
        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Criando o perfil Admin
            CriarPerfil(roleManager, PerfilEnum.Admin.Nome);

            // Busca o nome e o email do Admin principal configurado na Aplicação
            string emailAdmin = ConfigurationManager.AppSettings["EmailAdmin"];
            string nameAdmin = ConfigurationManager.AppSettings["NameAdmin"];

            CriarUsuario(userManager, emailAdmin, nameAdmin, "rasys@123", PerfilEnum.Admin.Nome);

            // Criando perfil Comercial    
            CriarPerfil(roleManager, PerfilEnum.Comercial.Nome);

            // Criando usuário exemplo Comercial
            CriarUsuario(userManager, "comercialrahsys@gmail.com", "Comercial", "rasys@123", PerfilEnum.Comercial.Nome);

            // Criando perfil Engenharia
            CriarPerfil(roleManager, PerfilEnum.Engenharia.Nome);

            // Criando usuário exemplo Engenharia
            CriarUsuario(userManager, "engenhariarahsys@gmail.com", "Engenheiro", "rasys@123", PerfilEnum.Engenharia.Nome);

            //// Se não encontrar o usuário padrão do engenharia, cria-o agora com perfil Engenharia
            //if (userManager.FindByEmail("engenhariarahsys@gmail.com") == null)
            //{
            //    //Criando um SuperUsuário admin
            //    var user = new ApplicationUser();
            //    user.UserName = "Engenheiro";
            //    user.Email = "engenhariarahsys@gmail.com";

            //    string userPWD = "rahsys@123";

            //    var chkUser = userManager.Create(user, userPWD);

            //    //Adciona rahsys@gmail.com ao Admin   
            //    if (chkUser.Succeeded)
            //    {
            //        var result1 = userManager.AddToRole(user.Id, "Engenharia");
            //    }
            //}

            // Criando perfil Financeiro
            CriarPerfil(roleManager, PerfilEnum.Financeiro.Nome);

            CriarUsuario(userManager, "giuliabfsantos@gmail.com", "Giulia", "rasys@123", PerfilEnum.Engenharia.Nome);
            CriarUsuario(userManager, "irisbf@gmail.com", "Iris", "rasys@123", PerfilEnum.Engenharia.Nome);
            CriarUsuario(userManager, "janainabf@gmail.com", "Janaina", "rasys@123", PerfilEnum.Financeiro.Nome);
        }

        private static void CriarPerfil(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!roleManager.RoleExists(roleName))
            {
                // Criando o papel
                var role = new IdentityRole();
                role.Name = roleName;
                roleManager.Create(role);
            }
        }

        private static void CriarUsuario(UserManager<ApplicationUser> userManager, string email, string n, string pwd, string roleName)
        {
            // Se não encontrar o usuário, cria-o agora
            if (userManager.FindByEmail(email) == null)
            {
                string name = n;

                //Criando um usuário
                var user = new ApplicationUser();
                user.UserName = name;
                user.Email = email;

                string userPWD = pwd;

                var chkUser = userManager.Create(user, userPWD);

                //Adiciona o usuário ao perfil desejado   
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, roleName);
                }
            }
        }
    }
}
