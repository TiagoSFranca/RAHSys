using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class ContratoServico : ServicoBase<ContratoModel>, IContratoServico
    {
        private readonly IContratoRepositorio _contratoRepositorio;
        private readonly IAnaliseInvestimentoRepositorio _analiseInvestimentoRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IDocumentoRepositorio _documentoRepositorio;

        private readonly string Rota = "/Contratos/";
        private readonly string RootPath = AppDomain.CurrentDomain.BaseDirectory;

        public ContratoServico(IContratoRepositorio contratoRepositorio, IAnaliseInvestimentoRepositorio analiseInvestimentoRepositorio,
            IClienteRepositorio clienteRepositorio, IDocumentoRepositorio documentoRepositorio)
            : base(contratoRepositorio)
        {
            _contratoRepositorio = contratoRepositorio;
            _analiseInvestimentoRepositorio = analiseInvestimentoRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _documentoRepositorio = documentoRepositorio;
        }

        private bool ExisteAnaliseInvestimento(AnaliseInvestimentoModel analiseInvestimento)
        {
            var query = _analiseInvestimentoRepositorio.Consultar().Where(e => e.IdContrato == analiseInvestimento.IdContrato);

            return query.Count() > 0;
        }

        private bool ExisteFichaCliente(ClienteModel cliente)
        {
            var query = _clienteRepositorio.Consultar().Where(e => e.IdAnaliseInvestimento == cliente.IdAnaliseInvestimento);

            return query.Count() > 0;
        }

        public void AdicionarAnaliseInvestimento(AnaliseInvestimentoModel analiseInvestimentoModel)
        {
            if (ExisteAnaliseInvestimento(analiseInvestimentoModel))
                throw new CustomBaseException(new Exception(), "Análise de Investimento já cadastrada");

            _analiseInvestimentoRepositorio.Adicionar(analiseInvestimentoModel);
        }

        public ConsultaModel<ContratoModel> Consultar(IEnumerable<int> idList, IEnumerable<int> idEstadoList, string nomeEmpresa, string cidade,
            string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<ContratoModel>(pagina, quantidade);

            var query = _contratoRepositorio.Consultar().Where(e => !e.Excluido);
            if (idList?.Count() > 0)
                query = query.Where(c => idList.Contains(c.IdContrato));

            if (idEstadoList?.Count() > 0)
                query = query.Where(c => idEstadoList.Contains(c.ContratoEndereco.Endereco.Cidade.IdEstado));

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
            var p = query.ToList();
            var resultado = query.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = query.Count();
            consultaModel.Resultado = resultado;

            return consultaModel;
        }

        public void AdicionarFichaCliente(ClienteModel clienteModel)
        {
            if (ExisteFichaCliente(clienteModel))
                throw new CustomBaseException(new Exception(), "Cliente já cadastrado para o contrato");

            _clienteRepositorio.Adicionar(clienteModel);
        }

        public void AdicionarDocumento(int idContrato, ArquivoModel arquivo)
        {
            CriarDiretorio(idContrato);
            string caminhoArquivo = AdicionarArquivo(idContrato, arquivo);
            _documentoRepositorio.Adicionar(new DocumentoModel()
            {
                IdContrato = idContrato,
                NomeArquivo = arquivo.FileName,
                DataUpload = DateTime.Now,
                CaminhoArquivo = caminhoArquivo
            });
        }

        private string AdicionarArquivo(int idContrato, ArquivoModel arquivo)
        {
            var rotaArquivo = string.Empty;
            var extensao = Path.GetExtension(arquivo.FileName);
            int count = 0;
            do
            {
                count++;
                rotaArquivo = string.Format("{0}/{1}{2}", Rota + idContrato.ToString(), count.ToString(), extensao);
            } while (File.Exists(RootPath + rotaArquivo));
            var fileStream = File.Create(RootPath + rotaArquivo);
            arquivo.InputStream.Seek(0, SeekOrigin.Begin);
            arquivo.InputStream.CopyTo(fileStream);
            fileStream.Close();

            return rotaArquivo;
        }

        private void CriarDiretorio(int idContrato)
        {
            if (!Directory.Exists(RootPath + Rota))
                Directory.CreateDirectory(RootPath + Rota);

            if (!Directory.Exists(RootPath + Rota + idContrato.ToString()))
                Directory.CreateDirectory(RootPath + Rota + idContrato.ToString());
        }

        public void Remover(ContratoModel obj)
        {
            var contrato = _contratoRepositorio.ObterPorId(obj.IdContrato, true);
            contrato.Excluido = true;
            _contratoRepositorio.Atualizar(contrato);
        }

        public ContratoModel ObterPorId(int id)
        {
            var contrato = _contratoRepositorio.ObterPorId(id, false, true);
            if (contrato?.Excluido == true)
                return null;
            return contrato;
        }
    }
}
