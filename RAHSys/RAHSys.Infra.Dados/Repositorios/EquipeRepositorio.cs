using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Entidades.Entidades;
using System.Data.Entity;
using System.Linq;

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

        public void Atualizar(EquipeModel obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            AdicionarOuRemoverUsuarios(obj);
            _context.SaveChanges();
        }


        private void AdicionarOuRemoverUsuarios(EquipeModel obj)
        {
            RemoverUsuarios(obj);
            AdicionarUsuarios(obj);
        }

        private void AdicionarUsuarios(EquipeModel obj)
        {
            foreach (var item in obj.EquipeUsuarios)
            {
                var usuario = _context.EquipeUsuario.Any(e => e.IdUsuario == item.IdUsuario && e.IdEquipe == obj.IdEquipe);
                if (!usuario)
                    _context.EquipeUsuario.Add(item);
            }
        }

        private void RemoverUsuarios(EquipeModel obj)
        {
            var usuariosIdList = obj.EquipeUsuarios.Select(e => e.IdUsuario).ToList();
            var usuarios = _context.EquipeUsuario.Where(
                e => e.IdEquipe == obj.IdEquipe &&
                !usuariosIdList.Contains(e.IdUsuario));
            _context.EquipeUsuario.RemoveRange(usuarios);
        }
    }
}
