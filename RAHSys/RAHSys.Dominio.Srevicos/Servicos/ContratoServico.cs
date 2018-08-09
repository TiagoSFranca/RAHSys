using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class ContratoServico : ServicoBase<ContratoModel>, IContratoServico
    {
        private readonly IContratoRepositorio _cameraRepositorio;

        public ContratoServico(IContratoRepositorio cameraRepositorio) : base(cameraRepositorio)
        {
            _cameraRepositorio = cameraRepositorio;
        }

        public ConsultaModel<ContratoModel> Consultar(IEnumerable<int> idList, string nomeEmpresa, string cidade, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<ContratoModel>(pagina, quantidade);

            var query = _cameraRepositorio.Consultar();
            if (idList?.Count() > 0)
                query = query.Where(c => idList.Contains(c.IdContrato));

            if (!string.IsNullOrEmpty(nomeEmpresa))
                query = query.Where(c => c.NomeEmpresa.ToLower().Contains(nomeEmpresa.ToLower()));

            if (!string.IsNullOrEmpty(cidade))
                query = query.Where(c => c.ContratoEndereco.Endereco.Cidade.Nome.ToLower().Contains(cidade.ToLower()));

            switch ((ordenacao ?? string.Empty).ToLower())
            {
                case "nomeempresa":
                    query = crescente ? query.OrderBy(c => c.NomeEmpresa) : query.OrderByDescending(c => c.NomeEmpresa);
                    break;
                case "cidade":
                    query = crescente ? query.OrderBy(c => c.ContratoEndereco.Endereco.Cidade.Nome) : query.OrderByDescending(c => c.ContratoEndereco.Endereco.Cidade.Nome);
                    break;
                default:
                    query = crescente ? query.OrderBy(c => c.IdContrato) : query.OrderByDescending(c => c.IdContrato);
                    break;

            }
            var resultado = query.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = query.Count();
            consultaModel.Resultado = resultado;

            return consultaModel;
        }
    }
}
