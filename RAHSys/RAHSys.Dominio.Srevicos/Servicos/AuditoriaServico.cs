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
    public class AuditoriaServico : ServicoAuditoriaBase<AuditoriaModel>, IAuditoriaServico
    {
        private readonly IAuditoriaRepositorio _auditoriaRepositorio;

        public AuditoriaServico(IAuditoriaRepositorio auditoriaRepositorio) : base(auditoriaRepositorio)
        {
            _auditoriaRepositorio = auditoriaRepositorio;
        }

        public ConsultaModel<AuditoriaModel> Consultar(IEnumerable<int> idList, string usuario, string funcao, string acao, string enderecoIp, DateTime dataHora, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<AuditoriaModel>(pagina, quantidade);

            var query = _auditoriaRepositorio.Consultar();
            if (idList?.Count() > 0)
                query = query.Where(a => idList.Contains(a.IdAuditoria));

            if (!string.IsNullOrEmpty(usuario))
                query = query.Where(a => a.Usuario.ToLower().Contains(usuario.ToLower()));

            if (!string.IsNullOrEmpty(funcao))
                query = query.Where(a => a.Funcao.ToLower().Contains(funcao.ToLower()));

            if (!string.IsNullOrEmpty(acao))
                query = query.Where(a => a.Acao.ToLower().Contains(acao.ToLower()));

            if (!string.IsNullOrEmpty(enderecoIp))
                query = query.Where(a => a.EnderecoIP.ToLower().Contains(enderecoIp.ToLower()));

            //if (dataHora == null)
            //    query = query.Where(a => a.DataHora == dataHora);

            switch ((ordenacao ?? string.Empty).ToLower())
            {
                case "usuário":
                    query = crescente ? query.OrderBy(a => a.Usuario) : query.OrderByDescending(a => a.Usuario);
                    break;
                case "função":
                    query = crescente ? query.OrderBy(a => a.Funcao) : query.OrderByDescending(a => a.Funcao);
                    break;
                case "ação":
                    query = crescente ? query.OrderBy(a => a.Acao) : query.OrderByDescending(a => a.Acao);
                    break;
                case "endereço ip":
                    query = crescente ? query.OrderBy(a => a.EnderecoIP) : query.OrderByDescending(a => a.EnderecoIP);
                    break;
                case "data/hora":
                    query = crescente ? query.OrderBy(a => a.DataHora) : query.OrderByDescending(a => a.DataHora);
                    break;
                default:
                    query = crescente ? query.OrderBy(a => a.IdAuditoria) : query.OrderByDescending(a => a.IdAuditoria);
                    break;
            }

            var resultado = query.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = query.Count();
            consultaModel.Resultado = resultado;

            return consultaModel;
        }
    }
}
