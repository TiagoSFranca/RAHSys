using System.Collections.Generic;
using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades.Entidades;
using System.Linq;
using RAHSys.Entidades;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class TipoAtividadeServico : ServicoBase<TipoAtividadeModel>, ITipoAtividadeServico
    {
        private readonly ITipoAtividadeRepositorio _tipoAtividadeRepositorio;

        public TipoAtividadeServico(ITipoAtividadeRepositorio tipoAtividadeRepositorio) : base(tipoAtividadeRepositorio)
        {
            _tipoAtividadeRepositorio = tipoAtividadeRepositorio;
        }

        public ConsultaModel<TipoAtividadeModel> Consultar(IEnumerable<int> idList, string descricao, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<TipoAtividadeModel>(pagina, quantidade);

            var query = _tipoAtividadeRepositorio.Consultar();
            if (idList?.Count() > 0)
                query = query.Where(c => idList.Contains(c.IdTipoAtividade));

            if (!string.IsNullOrEmpty(descricao))
                query = query.Where(c => c.Descricao.ToLower().Contains(descricao.ToLower()));

            switch ((ordenacao ?? string.Empty).ToLower())
            {
                case "descrição":
                    query = crescente ? query.OrderBy(c => c.Descricao) : query.OrderByDescending(c => c.Descricao);
                    break;
                default:
                    query = crescente ? query.OrderBy(c => c.IdTipoAtividade) : query.OrderByDescending(c => c.IdTipoAtividade);
                    break;

            }
            var resultado = query.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = query.Count();
            consultaModel.Resultado = resultado;

            return consultaModel;
        }

        public void Adicionar(TipoAtividadeModel obj)
        {
            if (ExisteTipoAtividade(obj))
                throw new CustomBaseException(new Exception(), string.Format("Já existe um tipo de atividade com a descrição [{0}]", obj.Descricao));
            _tipoAtividadeRepositorio.Adicionar(obj);
        }

        public void Atualizar(TipoAtividadeModel obj)
        {
            if (ExisteTipoAtividade(obj))
                throw new CustomBaseException(new Exception(), string.Format("Já existe um tipo de atividade com a descrição [{0}]", obj.Descricao));
            _tipoAtividadeRepositorio.Atualizar(obj);
        }

        public void Remover(TipoAtividadeModel obj)
        {
            if (PossuiAtividades(obj))
                throw new CustomBaseException(new Exception(), string.Format("Não é possível excluir o tipo de atividade [{0}], pois já possui atividades associadas", obj.Descricao));

            _tipoAtividadeRepositorio.Remover(obj);
        }

        private bool PossuiAtividades(TipoAtividadeModel obj)
        {
            return obj.Atividades?.Count > 0;
        }

        private bool ExisteTipoAtividade(TipoAtividadeModel obj)
        {
            var query = _tipoAtividadeRepositorio.Consultar();
            query = query.Where(e => e.Descricao.ToLower().Equals(obj.Descricao) && e.IdTipoAtividade != obj.IdTipoAtividade);

            return query.Count() > 0;
        }

        public IEnumerable<TipoAtividadeModel> ListarTodos()
        {
            return _tipoAtividadeRepositorio.Consultar().OrderBy(e => e.Descricao).ToList();
        }
    }
}
