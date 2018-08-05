using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace RAHSys.Dominio.Servicos.Servicos
{
    //TODO: Verificar se há regras de negócio quanto a duplicidade
    //TODO: Validação de exclusão
    public class TipoTelhadoServico : ServicoBase<TipoTelhadoModel>, ITipoTelhadoServico
    {
        private readonly ITipoTelhadoRepositorio _tipoTelhadoRepositorio;

        public TipoTelhadoServico(ITipoTelhadoRepositorio tipoTelhadoRepositorio) : base(tipoTelhadoRepositorio)
        {
            _tipoTelhadoRepositorio = tipoTelhadoRepositorio;
        }

        public ConsultaModel<TipoTelhadoModel> Consultar(IEnumerable<int> idList, string descricao, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<TipoTelhadoModel>(pagina, quantidade);

            var query = _tipoTelhadoRepositorio.Consultar();
            if (idList?.Count() > 0)
                query = query.Where(c => idList.Contains(c.IdTipoTelhado));

            if (!string.IsNullOrEmpty(descricao))
                query = query.Where(c => c.Descricao.ToLower().Contains(descricao.ToLower()));

            switch ((ordenacao ?? string.Empty).ToLower())
            {
                case "descrição":
                    query = crescente ? query.OrderBy(c => c.Descricao) : query.OrderByDescending(c => c.Descricao);
                    break;
                default:
                    query = crescente ? query.OrderBy(c => c.IdTipoTelhado) : query.OrderByDescending(c => c.IdTipoTelhado);
                    break;

            }

            var resultado = query.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = query.Count();
            consultaModel.Resultado = resultado;

            return consultaModel;
        }
    }
}
