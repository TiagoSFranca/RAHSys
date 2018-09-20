using System;
using System.Collections.Generic;
using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System.Linq;
using RAHSys.Infra.CrossCutting.Exceptions;
using System.Globalization;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class PagamentoServico : ServicoBase<PagamentoModel>, IPagamentoServico
    {
        private readonly IPagamentoRepositorio _pagamentoRepositorio;

        public PagamentoServico(IPagamentoRepositorio PagamentoRepositorio) : base(PagamentoRepositorio)
        {
            _pagamentoRepositorio = PagamentoRepositorio;
        }

        public ConsultaModel<PagamentoModel> Consultar(int idContrato, IEnumerable<int> idList, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<PagamentoModel>(pagina, quantidade);

            var query = _pagamentoRepositorio.Consultar().Where(e => e.IdContrato == idContrato);

            if (idList?.Count() > 0)
                query = query.Where(c => idList.Contains(c.IdPagamento));

            switch ((ordenacao ?? string.Empty).ToLower())
            {
                case "data":
                    query = crescente ? query.OrderBy(c => c.DataPagamento) : query.OrderByDescending(c => c.DataPagamento);
                    break;
                default:
                    query = crescente ? query.OrderBy(c => c.IdPagamento) : query.OrderByDescending(c => c.IdPagamento);
                    break;

            }
            var resultado = query.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = query.Count();
            consultaModel.Resultado = resultado;

            return consultaModel;
        }

        private bool VerificarExistenciaPagamento(PagamentoModel obj)
        {
            var query = _pagamentoRepositorio.Consultar()
                .Where(e => e.DataPagamento.Month == obj.DataPagamento.Month
                && e.DataPagamento.Year == obj.DataPagamento.Year
                && e.IdPagamento != obj.IdPagamento
                && e.IdContrato == obj.IdContrato);

            return query.Count() > 0;
        }

        public void Adicionar(PagamentoModel obj)
        {
            var menorData = DateTime.Parse("01/01/1901", new CultureInfo("pt-BR"), DateTimeStyles.None);
            obj.DataCriacao = DateTime.Now;
            if (VerificarExistenciaPagamento(obj))
                throw new CustomBaseException(new Exception(), string.Format("Pagamento já realizado para o mês [{0}/{1}]", obj.DataPagamento.Month, obj.DataPagamento.Year));
            if (obj.DataPagamento < menorData)
                throw new CustomBaseException(new Exception(), string.Format("Data [{0}] inválida", obj.DataPagamento.ToShortDateString()));

            _pagamentoRepositorio.Adicionar(obj);
        }
    }
}
