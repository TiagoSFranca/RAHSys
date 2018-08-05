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
    //TODO: Validação de exclusão
    public class TipoContatoServico : ServicoBase<TipoContatoModel>, ITipoContatoServico
    {
        private readonly ITipoContatoRepositorio _tipoContatoRepositorio;

        public TipoContatoServico(ITipoContatoRepositorio tipoContatoRepositorio) : base(tipoContatoRepositorio)
        {
            _tipoContatoRepositorio = tipoContatoRepositorio;
        }

        public ConsultaModel<TipoContatoModel> Consultar(IEnumerable<int> idList, string descricao, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<TipoContatoModel>(pagina, quantidade);

            var query = _tipoContatoRepositorio.Consultar();
            if (idList?.Count() > 0)
                query = query.Where(c => idList.Contains(c.IdTipoContato));

            if (!string.IsNullOrEmpty(descricao))
                query = query.Where(c => c.Descricao.ToLower().Contains(descricao.ToLower()));

            switch ((ordenacao ?? string.Empty).ToLower())
            {
                case "descrição":
                    query = crescente ? query.OrderBy(c => c.Descricao) : query.OrderByDescending(c => c.Descricao);
                    break;
                default:
                    query = crescente ? query.OrderBy(c => c.IdTipoContato) : query.OrderByDescending(c => c.IdTipoContato);
                    break;

            }
            var resultado = query.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = query.Count();
            consultaModel.Resultado = resultado;

            return consultaModel;
        }

        private bool Existe(TipoContatoModel obj)
        {
            var consulta = _tipoContatoRepositorio.Consultar();
            var resultado = consulta.Where(tc => tc.Descricao.ToLower().Equals(obj.Descricao.ToLower()) && obj.IdTipoContato != tc.IdTipoContato);

            return resultado.Count() > 0;
        }

        public void Adicionar(TipoContatoModel obj)
        {
            if (Existe(obj))
                throw new CustomBaseException(new Exception(), "Tipo de Contato com a mesma descrição já cadastrado!");

            _tipoContatoRepositorio.Adicionar(obj);
        }

        public void Atualizar(TipoContatoModel obj)
        {
            if (Existe(obj))
                throw new CustomBaseException(new Exception(), "Tipo de Contato com a mesma descrição já cadastrado!");

            _tipoContatoRepositorio.Atualizar(obj);
        }
    }
}
