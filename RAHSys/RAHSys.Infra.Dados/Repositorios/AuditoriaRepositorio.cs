using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAHSys.Infra.Dados.Repositorios
{
    public class AuditoriaRepositorio : RepositorioAuditBase<AuditoriaModel>, IAuditoriaRepositorio
    {
    }
}
