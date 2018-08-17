using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class EquipeServico : ServicoBase<EquipeModel>, IEquipeServico
    {
        private readonly IEquipeRepositorio _equipeRepositorio;
        private readonly IUsuarioServico _usuarioServico;

        public EquipeServico(IEquipeRepositorio equipeRepositorio, IUsuarioServico usuarioServico) : base(equipeRepositorio)
        {
            _equipeRepositorio = equipeRepositorio;
            _usuarioServico = usuarioServico;
        }

        public ConsultaModel<EquipeModel> Consultar(IEnumerable<int> idList, string email, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<EquipeModel>(pagina, quantidade);

            var query = _equipeRepositorio.Consultar();
            if (idList?.Count() > 0)
                query = query.Where(c => idList.Contains(c.IdEquipe));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(c => c.Lider.Email.ToLower().Contains(email.ToLower()) ||
                c.EquipeUsuarios.Where(e => e.Usuario.Email.ToLower().Contains(email.ToLower())).Count() > 0);

            switch ((ordenacao ?? string.Empty).ToLower())
            {
                case "lider":
                    query = crescente ? query.OrderBy(c => c.Lider.Email) : query.OrderByDescending(c => c.Lider.Email);
                    break;
                default:
                    query = crescente ? query.OrderBy(c => c.IdEquipe) : query.OrderByDescending(c => c.IdEquipe);
                    break;

            }
            var resultado = query.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = query.Count();
            consultaModel.Resultado = resultado;

            return consultaModel;
        }

        public void Adicionar(EquipeModel obj)
        {
            ValidarUsuario(obj);
            _equipeRepositorio.Adicionar(obj);
        }

        public void Atualizar(EquipeModel obj)
        {
            ValidarUsuario(obj);
            _equipeRepositorio.Atualizar(obj);
        }

        public void Remover(EquipeModel obj)
        {
            if (VerificarContratos(obj))
                throw new CustomBaseException(new Exception(), "Não foi possível excluir pois a equipe está associada à contratos");

            _equipeRepositorio.Remover(obj);
        }

        private bool VerificarContratos(EquipeModel obj)
        {
            var query = _equipeRepositorio.Consultar().Where(e => e.IdEquipe == obj.IdEquipe && e.Clientes.Where(f => !f.AnaliseInvestimento.Contrato.Excluido).Count() > 0);

            return query.Count() > 0;
        }

        private void ValidarUsuario(EquipeModel equipe)
        {
            if (equipe.EquipeUsuarios?.Count > 0)
                foreach (var equipeUsuario in equipe.EquipeUsuarios)
                {
                    var usuario = _usuarioServico.ObterPorId(equipeUsuario.IdUsuario);
                    if (usuario == null)
                        throw new CustomBaseException(new Exception(), "Usuário não encontrato");
                }
        }

        public IEnumerable<EquipeModel> ListarTodos()
        {
            return _equipeRepositorio.Consultar().OrderBy(e => e.Lider.Email).ToList();
        }
    }
}
