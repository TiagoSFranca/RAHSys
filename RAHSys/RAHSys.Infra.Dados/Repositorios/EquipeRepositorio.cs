using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Infra.Dados.Repositorios
{
    public class EquipeRepositorio : RepositorioBase<EquipeModel>, IEquipeRepositorio
    {
        public void Adicionar(EquipeModel obj)
        {
            var equipeUsuarios = obj.EquipeUsuarios;

            obj.EquipeUsuarios = null;

            _context.Equipe.Add(obj);

            if (equipeUsuarios?.Count > 0)
            {
                foreach (var equipeUsuario in equipeUsuarios)
                {
                    equipeUsuario.Equipe = null;
                    equipeUsuario.Usuario = null;
                    equipeUsuario.IdEquipe = obj.IdEquipe;
                    _context.EquipeUsuario.Add(equipeUsuario);
                }
            }
            _context.SaveChanges();
        }
    }
}
