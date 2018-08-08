using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using RAHSys.Apresentacao.Models;
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

            // Criar o perfil Admin
            if (!roleManager.RoleExists("Admin"))
            {
                // Criando o papel Admin   
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            // Busca o nome e o email do Admin principal configurado na Aplicação
            string emailAdmin = ConfigurationManager.AppSettings["EmailAdmin"];

            // Se não encontrar o super usuário, cria-o agora com perfil Admin
            if (userManager.FindByEmail(emailAdmin) == null)
            {
                string nameAdmin = ConfigurationManager.AppSettings["NameAdmin"];

                //Criando um SuperUsuário admin
                var user = new ApplicationUser();
                user.UserName = nameAdmin;
                user.Email = emailAdmin;

                string userPWD = "rahsys@123";

                var chkUser = userManager.Create(user, userPWD);

                //Adciona rahsys@gmail.com ao Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");
                }
            }

            // Criando perfil Comercial    
            if (!roleManager.RoleExists("Comercial"))
            {
                var role = new IdentityRole();
                role.Name = "Comercial";
                roleManager.Create(role);
            }

            // Se não encontrar o usuário padrão do comercial, cria-o agora com perfil Comercial
            if (userManager.FindByEmail("comercialrahsys@gmail.com") == null)
            {
                //Criando um SuperUsuário admin
                var user = new ApplicationUser();
                user.UserName = "Comercial";
                user.Email = "comercialrahsys@gmail.com";

                string userPWD = "rahsys@123";

                var chkUser = userManager.Create(user, userPWD);

                //Adciona rahsys@gmail.com ao Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Comercial");
                }
            }

            // Criando perfil Engenharia
            if (!roleManager.RoleExists("Engenharia"))
            {
                var role = new IdentityRole();
                role.Name = "Engenharia";
                roleManager.Create(role);
            }

            // Se não encontrar o usuário padrão do engenharia, cria-o agora com perfil Engenharia
            if (userManager.FindByEmail("engenhariarahsys@gmail.com") == null)
            {
                //Criando um SuperUsuário admin
                var user = new ApplicationUser();
                user.UserName = "Engenheiro";
                user.Email = "engenhariarahsys@gmail.com";

                string userPWD = "rahsys@123";

                var chkUser = userManager.Create(user, userPWD);

                //Adciona rahsys@gmail.com ao Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Engenharia");
                }
            }
        }
    }
}
