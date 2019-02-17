using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.IO;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class EvidenciaServico : ServicoBase<EvidenciaModel>, IEvidenciaServico
    {
        private readonly IEvidenciaRepositorio _evidenciaRepositorio;

        public EvidenciaServico(IEvidenciaRepositorio evidenciaRepositorio) : base(evidenciaRepositorio)
        {
            _evidenciaRepositorio = evidenciaRepositorio;
        }

        public void Remover(EvidenciaModel obj)
        {
            ExcluirArquivo(obj.CaminhoArquivo);
            _evidenciaRepositorio.Remover(obj);
        }

        public void AdicionarEvidencias(int idAtividade, int idRegistroRecorrencia, List<ArquivoModel> evidencias)
        {
            foreach (var evidencia in evidencias)
            {
                AdicionarDocumento(idAtividade, idRegistroRecorrencia, evidencia);
            }
        }

        private void AdicionarDocumento(int idAtividade, int idRegistroRecorrencia, ArquivoModel arquivo)
        {
            string rota = CriarDiretorio(idAtividade, idRegistroRecorrencia);
            string caminhoArquivo = UploadArquivo(arquivo, rota);
            _evidenciaRepositorio.Adicionar(new EvidenciaModel()
            {
                IdRegistroRecorrencia = idRegistroRecorrencia,
                NomeArquivo = arquivo.FileName,
                DataUpload = DateTime.Now,
                CaminhoArquivo = caminhoArquivo
            });
        }

        private string UploadArquivo(ArquivoModel arquivo, string rota)
        {
            var rotaArquivo = string.Empty;
            var extensao = Path.GetExtension(arquivo.FileName);
            int count = 0;
            do
            {
                count++;
                rotaArquivo = string.Format("{0}/{1}{2}", rota, count.ToString(), extensao);
            } while (File.Exists(RootPath + rotaArquivo));
            var fileStream = File.Create(RootPath + rotaArquivo);
            arquivo.InputStream.Seek(0, SeekOrigin.Begin);
            arquivo.InputStream.CopyTo(fileStream);
            fileStream.Close();

            return rotaArquivo;
        }

        private string CriarDiretorio(int idAtividade, int idRegistroRecorrencia)
        {
            var rotaAtividades = RotaAtividade;
            if (!Directory.Exists(MontarRotaArquivo(rotaAtividades)))
                Directory.CreateDirectory(MontarRotaArquivo(rotaAtividades));

            var rotaAtividade = rotaAtividades + idAtividade;
            if (!Directory.Exists(MontarRotaArquivo(rotaAtividade)))
                Directory.CreateDirectory(MontarRotaArquivo(rotaAtividade));

            var rotaRegistroRecorrencias = rotaAtividade + RotaRegistroRecorrencia;
            if (!Directory.Exists(MontarRotaArquivo(rotaRegistroRecorrencias)))
                Directory.CreateDirectory(MontarRotaArquivo(rotaRegistroRecorrencias));

            var rotaRegistroRecorrencia = rotaRegistroRecorrencias + idRegistroRecorrencia;
            if (!Directory.Exists(MontarRotaArquivo(rotaRegistroRecorrencia)))
                Directory.CreateDirectory(MontarRotaArquivo(rotaRegistroRecorrencia));

            var rotaEvidencias = rotaRegistroRecorrencia + RotaEvidencias;
            if (!Directory.Exists(MontarRotaArquivo(rotaEvidencias)))
                Directory.CreateDirectory(MontarRotaArquivo(rotaEvidencias));

            return rotaEvidencias;
        }

        private void ExcluirArquivo(string caminho)
        {
            if (!string.IsNullOrEmpty(caminho) && File.Exists(MontarRotaArquivo(caminho)))
                File.Delete(MontarRotaArquivo(caminho));
        }
    }
}
