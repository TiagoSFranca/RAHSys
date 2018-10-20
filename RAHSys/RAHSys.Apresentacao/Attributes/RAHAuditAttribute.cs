using RAHSys.Apresentacao.Models;
using System;
using System.Web.Mvc;

namespace RAHSys.Apresentacao.Attributes
{
    /// <summary>
    /// Permite a auditoria de ações de Insert, Update 
    /// e Delete em base de dados separada.
    /// </summary>
    public class RAHAuditAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            string actionName = filterContext.ActionDescriptor.ActionName;
            if(actionName.ToUpper().Contains("ADICIONAR") || actionName.ToUpper().Contains("EDITAR") || actionName.ToUpper().Contains("EXCLUIR"))
            {
                var formData = filterContext.HttpContext.Request.Form;
                // Verifica se houve postagem de dados no form
                if (formData.HasKeys())
                {
                    var request = filterContext.HttpContext.Request;
                    var userName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anônimo";
                    string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                    string ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;
                    
                    // Busca os dados do Registro
                    string dataToSave = String.Empty;
                    foreach (string key in formData.Keys)
                    {
                        // Exclui o ResgistrationToken (desnecessário)
                        if (!key.StartsWith("_"))
                        {
                            string[] nomeCampoArr = key.Split('.');
                            string campo = nomeCampoArr[nomeCampoArr.Length - 1];
                            dataToSave += campo + ":" + formData[key] + ";\n";
                        }
                    }

                    // Insere os dados na tabela de auditoria
                    var contexto = new ApplicationAuditoriaDbContext();
                    var model = new AuditoriaModel
                    {
                        Usuario = userName,
                        Funcao = controllerName,
                        Acao = actionName,
                        EnderecoIP = ipAddress,
                        DataHora = DateTime.Now,
                        Dados = dataToSave
                    };

                    contexto.Auditoria.Add(model);
                    //contexto.SaveChanges();
                }
            }
        }
    }
}