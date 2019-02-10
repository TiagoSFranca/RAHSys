using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class RegistroRecorrenciaServico : ServicoBase<RegistroRecorrenciaModel>, IRegistroRecorrenciaServico
    {
        private readonly IRegistroRecorrenciaRepositorio _registroRecorrenciaRepositorio;
        private readonly IEvidenciaRepositorio _evidenciaRepositorio;

        private readonly string RotaAtividade = "/Atividades/";
        private readonly string RotaRegistroRecorrencia = "/RegistroRecorrencias/";
        private readonly string RotaEvidencias = "/Evidencias/";

        public RegistroRecorrenciaServico(IRegistroRecorrenciaRepositorio registroRecorrenciaRepositorio, IEvidenciaRepositorio evidenciaRepositorio)
            : base(registroRecorrenciaRepositorio)
        {
            _registroRecorrenciaRepositorio = registroRecorrenciaRepositorio;
            _evidenciaRepositorio = evidenciaRepositorio;
        }


        public ConsultaModel<RegistroRecorrenciaModel> Consultar(int idAtividade, IEnumerable<int> idList, DateTime? dataPrevista, DateTime? dataRealizacao, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<RegistroRecorrenciaModel>(pagina, quantidade);

            var query = _registroRecorrenciaRepositorio.Consultar().Where(e => e.IdAtividade == idAtividade);

            if (idList?.Count() > 0)
                query.Where(e => idList.Contains(e.IdRegistroRecorrencia));

            if (dataPrevista.HasValue)
                query = query.Where(e => DbFunctions.TruncateTime(e.DataPrevista) == DbFunctions.TruncateTime(dataPrevista.Value));

            if (dataRealizacao.HasValue)
                query = query.Where(e => DbFunctions.TruncateTime(e.DataRealizacao) == DbFunctions.TruncateTime(dataRealizacao.Value));

            switch ((ordenacao ?? string.Empty).ToLower())
            {
                case "data":
                    query = crescente ? query.OrderBy(c => c.DataPrevista) : query.OrderByDescending(c => c.DataPrevista);
                    break;
                default:
                    query = crescente ? query.OrderBy(c => c.IdRegistroRecorrencia) : query.OrderByDescending(c => c.IdRegistroRecorrencia);
                    break;

            }

            var resultado = query.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = query.Count();
            consultaModel.Resultado = resultado;

            return consultaModel;
        }

        public void FinalizarRegistroRecorrencia(int idAtividade, DateTime dataRealizacaoPrevista, DateTime dataRealizacao, string observacao, List<ArquivoModel> evidencias)
        {
            if (ValidarRecorrencia(idAtividade, dataRealizacaoPrevista))
                throw new CustomBaseException(new Exception(), string.Format("Já existe um registro para [{0}].", dataRealizacaoPrevista));
            var recorrencia = new RegistroRecorrenciaModel()
            {
                IdAtividade = idAtividade,
                DataPrevista = dataRealizacaoPrevista,
                DataRealizacao = dataRealizacao,
                Observacao = observacao
            };
            _registroRecorrenciaRepositorio.Adicionar(recorrencia);

            foreach (var evidencia in evidencias)
            {
                AdicionarDocumento(idAtividade, recorrencia.IdRegistroRecorrencia, evidencia);
            }
        }

        private bool ValidarRecorrencia(int idAtividade, DateTime dataPrevista)
        {
            var query = _registroRecorrenciaRepositorio.Consultar().Where(e => e.IdAtividade == idAtividade && e.DataPrevista.Year == dataPrevista.Year
            && e.DataPrevista.Month == dataPrevista.Month
            && e.DataPrevista.Day == dataPrevista.Day);
            return query.Count() > 0;
        }

        private void AdicionarDocumento(int idAtividade, int idRegistroRecorrencia, ArquivoModel arquivo)
        {
            string rota = CriarDiretorio(idAtividade, idRegistroRecorrencia);
            string caminhoArquivo = AdicionarArquivo(arquivo, rota);
            _evidenciaRepositorio.Adicionar(new EvidenciaModel()
            {
                IdRegistroRecorrencia = idRegistroRecorrencia,
                NomeArquivo = arquivo.FileName,
                DataUpload = DateTime.Now,
                CaminhoArquivo = caminhoArquivo
            });
        }

        private string AdicionarArquivo(ArquivoModel arquivo, string rota)
        {
            var rotaArquivo = string.Empty;
            var extensao = Path.GetExtension(arquivo.FileName);
            int count = 0;
            do
            {
                count++;
                rotaArquivo = string.Format("{0}/{1}{2}", rota, count.ToString(), extensao);
            } while (File.Exists(RootPath + rotaArquivo));
            var fileStream = File.Create(rotaArquivo);
            arquivo.InputStream.Seek(0, SeekOrigin.Begin);
            arquivo.InputStream.CopyTo(fileStream);
            fileStream.Close();

            return rotaArquivo;
        }

        private string CriarDiretorio(int idAtividade, int idRegistroRecorrencia)
        {
            var rotaAtividades = RootPath + RotaAtividade;
            if (!Directory.Exists(rotaAtividades))
                Directory.CreateDirectory(rotaAtividades);

            var rotaAtividade = rotaAtividades + idAtividade;
            if (!Directory.Exists(rotaAtividade))
                Directory.CreateDirectory(rotaAtividade);

            var rotaRegistroRecorrencias = rotaAtividade + RotaRegistroRecorrencia;
            if (!Directory.Exists(rotaRegistroRecorrencias))
                Directory.CreateDirectory(rotaRegistroRecorrencias);

            var rotaRegistroRecorrencia = rotaRegistroRecorrencias + idRegistroRecorrencia;
            if (!Directory.Exists(rotaRegistroRecorrencia))
                Directory.CreateDirectory(rotaRegistroRecorrencia);

            var rotaEvidencias = rotaRegistroRecorrencia + RotaEvidencias;
            if (!Directory.Exists(rotaEvidencias))
                Directory.CreateDirectory(rotaEvidencias);

            return rotaEvidencias;
        }
    }
}
