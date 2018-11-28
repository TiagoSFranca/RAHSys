using System.Collections.Generic;
using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades.Entidades;
using System.Linq;
using RAHSys.Entidades;
using System;
using System.Globalization;
using RAHSys.Entidades.Seeds;
using RAHSys.Infra.CrossCutting.Exceptions;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class AtividadeServico : ServicoBase<AtividadeModel>, IAtividadeServico
    {
        private readonly IAtividadeRepositorio _atividadeRepositorio;
        private readonly IRegistroRecorrenciaRepositorio _registroRecorrenciaRepositorio;

        public AtividadeServico(IAtividadeRepositorio atividadeRepositorio, IRegistroRecorrenciaRepositorio registroRecorrenciaRepositorio) : base(atividadeRepositorio)
        {
            _atividadeRepositorio = atividadeRepositorio;
            _registroRecorrenciaRepositorio = registroRecorrenciaRepositorio;
        }

        public ConsultaModel<AtividadeRecorrenciaModel> Consultar(IEnumerable<int> idList, IEnumerable<int> idTipoAtividadeList, IEnumerable<int> idEquipeList,
            IEnumerable<int> idContratoList, IEnumerable<string> idUsuarioList, string mesAno, bool? realizada,
            string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<AtividadeRecorrenciaModel>(pagina, quantidade);

            var query = _atividadeRepositorio.Consultar().Where(c => !c.Contrato.Excluido);

            mesAno = mesAno ?? string.Format("{0}/{1}", DateTime.Now.Month, DateTime.Now.Year);

            int mes = 0;
            int ano = 0;

            ValidarMesAno(mesAno, ref mes, ref ano);
            DateTime dataInicioMesAno = new DateTime(ano, mes, 1);
            DateTime dataFimMesAno = dataInicioMesAno.AddMonths(1).AddDays(-1);

            query = query.Where(e => (e.DataInicial <= dataFimMesAno && e.ConfiguracaoAtividade != null) ||
            (e.DataInicial.Month == dataInicioMesAno.Month && e.DataInicial.Year == dataInicioMesAno.Year && e.ConfiguracaoAtividade == null));

            if (idList?.Count() > 0)
                query = query.Where(c => idList.Contains(c.IdAtividade));

            if (idTipoAtividadeList?.Count() > 0)
                query = query.Where(c => idTipoAtividadeList.Contains(c.IdTipoAtividade));

            if (idEquipeList?.Count() > 0)
                query = query.Where(c => idEquipeList.Contains(c.IdEquipe));

            if (idContratoList?.Count() > 0)
                query = query.Where(c => idContratoList.Contains(c.IdContrato));

            if (idUsuarioList?.Count() > 0)
                query = query.Where(c => idUsuarioList.Contains(c.IdUsuario));

            if (realizada != null)
                query = query.Where(c => c.Finalizada == realizada);

            switch ((ordenacao ?? string.Empty).ToLower())
            {
                case "tipoatividade":
                    query = crescente ? query.OrderBy(c => c.TipoAtividade.Descricao) : query.OrderByDescending(c => c.TipoAtividade.Descricao);
                    break;
                case "atribuidopara":
                    query = crescente ? query.OrderBy(c => c.Usuario.UserName) : query.OrderByDescending(c => c.Usuario.UserName);
                    break;
                case "Realizada":
                    query = crescente ? query.OrderBy(c => c.Finalizada) : query.OrderByDescending(c => c.Finalizada);
                    break;
                default:
                    query = crescente ? query.OrderBy(c => c.IdAtividade) : query.OrderByDescending(c => c.IdAtividade);
                    break;
            }

            var resultado = query.ToList();
            var recorrencia = ObterRecorrenciaAtividades(resultado, dataInicioMesAno);
            recorrencia = recorrencia.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = recorrencia.Count();
            consultaModel.Resultado = recorrencia;

            return consultaModel;
        }

        private AtividadeRecorrenciaModel MontarRegistroRecorrencia(AtividadeModel atividade, DateTime data)
        {
            var recorrenciasAtividade = atividade.RegistroRecorrencias;
            var registroRecorrencia = recorrenciasAtividade.FirstOrDefault(e => e.DataPrevista == data);
            bool realizada = registroRecorrencia != null;
            if (registroRecorrencia == null)
                registroRecorrencia = new RegistroRecorrenciaModel()
                {
                    DataPrevista = data
                };

            AtividadeRecorrenciaModel recorrencia = new AtividadeRecorrenciaModel(atividade.IdAtividade, atividade.Descricao, atividade.TipoAtividade,
                atividade.Contrato, atividade.Equipe, atividade.Usuario,
                atividade.TipoRecorrencia, registroRecorrencia, atividade.Finalizada)
            {
                Realizada = realizada
            };

            return recorrencia;
        }

        private List<AtividadeRecorrenciaModel> ObterRecorrenciaAtividades(List<AtividadeModel> atividades, DateTime mes)
        {
            var atividadesSemRecorrencia = atividades.Where(e => e.ConfiguracaoAtividade == null).ToList();
            var atividadesComRecorrencia = atividades.Where(e => e.ConfiguracaoAtividade != null).ToList();

            var listaRecorrencia = new List<AtividadeRecorrenciaModel>();

            listaRecorrencia.AddRange(
                atividadesSemRecorrencia
                .Select(
                    e => new AtividadeRecorrenciaModel(
                        e.IdAtividade,
                        e.Descricao,
                        e.TipoAtividade,
                        e.Contrato,
                        e.Equipe,
                        e.Usuario,
                        e.TipoRecorrencia,
                        e.RegistroRecorrencias.FirstOrDefault() ?? new RegistroRecorrenciaModel() { DataPrevista = e.DataInicial },
                        e.Finalizada)
                    { Realizada = e.RegistroRecorrencias.Count > 0 }
                    ).ToList());

            foreach (var atividadeComRecorrencia in atividadesComRecorrencia)
            {
                listaRecorrencia.AddRange(CalcularRecorrencia(atividadeComRecorrencia, mes));
            }

            return listaRecorrencia;
        }

        private List<AtividadeRecorrenciaModel> CalcularRecorrenciaDiaria(AtividadeModel atividade, DateTime dataInicial)
        {
            List<AtividadeRecorrenciaModel> lista = new List<AtividadeRecorrenciaModel>();

            var configuracaoAtividade = atividade.ConfiguracaoAtividade;
            var recorrenciasAtividade = atividade.RegistroRecorrencias.ToList();
            var dataFinal = dataInicial.AddMonths(1).AddDays(-1);
            var frequencia = configuracaoAtividade.Frequencia;
            frequencia = frequencia > 0 ? frequencia : 1;
            var dataInicio = atividade.DataInicial;

            if (atividade.ConfiguracaoAtividade.TerminaEm != null)
                dataFinal = dataFinal < (DateTime)atividade.ConfiguracaoAtividade.TerminaEm ? dataFinal : (DateTime)atividade.ConfiguracaoAtividade.TerminaEm;

            int qtdDias = dataFinal.Subtract(dataInicio).Days + 1;

            var diasDaSemana = ObterDiasDaSemana(atividade.ConfiguracaoAtividade.AtividadeDiaSemanas.ToList());

            var datas = Enumerable.Range(0, qtdDias).Select(i => dataInicio.AddDays(i * frequencia)).ToList();

            List<DateTime> datasExatas = new List<DateTime>();

            if (atividade.ConfiguracaoAtividade.QtdRepeticoes != null && atividade.ConfiguracaoAtividade.QtdRepeticoes > 0)
                datasExatas = datas.Skip(0).Take((int)atividade.ConfiguracaoAtividade.QtdRepeticoes).ToList();
            else
                datasExatas = datas.Where(e => e.Date >= dataInicial.Date && e.Date <= dataFinal.Date).ToList();

            datasExatas.ForEach(data =>
            {
                AtividadeRecorrenciaModel recorrencia = MontarRegistroRecorrencia(atividade, data);
                lista.Add(recorrencia);
            });

            return lista;
        }

        private List<AtividadeRecorrenciaModel> CalcularRecorrenciaSemanal(AtividadeModel atividade, DateTime dataInicial)
        {
            List<AtividadeRecorrenciaModel> lista = new List<AtividadeRecorrenciaModel>();
            var configuracaoAtividade = atividade.ConfiguracaoAtividade;
            var recorrenciasAtividade = atividade.RegistroRecorrencias.ToList();
            var dataFinal = dataInicial.AddMonths(1).AddDays(-1);
            var frequencia = configuracaoAtividade.Frequencia;
            frequencia = frequencia > 0 ? frequencia : 1;
            var dataInicio = atividade.DataInicial;

            if (atividade.ConfiguracaoAtividade.TerminaEm != null)
                dataFinal = dataFinal < (DateTime)atividade.ConfiguracaoAtividade.TerminaEm ? dataFinal : (DateTime)atividade.ConfiguracaoAtividade.TerminaEm;

            int qtdDias = dataFinal.Subtract(dataInicio).Days + 1;

            var diasDaSemana = ObterDiasDaSemana(atividade.ConfiguracaoAtividade.AtividadeDiaSemanas.ToList());

            var todasDatas = Enumerable.Range(0, qtdDias)
                                  .Select(i => dataInicio.AddDays(i))
                                  .Where(d => diasDaSemana.Contains(d.DayOfWeek)).ToList();
            List<DateTime> datas = new List<DateTime>();
            for (int i = 0; i < todasDatas.Count; i += (frequencia * diasDaSemana.Count))
            {
                if (todasDatas[i] != null)
                {
                    datas.Add(todasDatas[i]);
                    var count = 1;
                    while (count < diasDaSemana.Count)
                    {
                        if (todasDatas[i + count] != null)
                            datas.Add(todasDatas[i + count]);
                        else
                            break;
                        count++;
                    }
                }
                else
                    break;
            }
            List<DateTime> datasExatas = new List<DateTime>();

            if (atividade.ConfiguracaoAtividade.QtdRepeticoes != null && atividade.ConfiguracaoAtividade.QtdRepeticoes > 0)
                datasExatas = datas.Skip(0).Take((int)atividade.ConfiguracaoAtividade.QtdRepeticoes).ToList();
            else
                datasExatas = datas.Where(e => e.Date >= dataInicial.Date && e.Date <= dataFinal.Date).ToList();

            datasExatas.ForEach(data =>
            {
                AtividadeRecorrenciaModel recorrencia = MontarRegistroRecorrencia(atividade, data);
                lista.Add(recorrencia);
            });

            return lista;
        }

        private List<AtividadeRecorrenciaModel> CalcularRecorrenciaMensal(AtividadeModel atividade, DateTime dataInicial)
        {
            List<AtividadeRecorrenciaModel> lista = new List<AtividadeRecorrenciaModel>();
            var contador = 0;
            var dataAtividade = atividade.DataInicial;
            var configuracaoAtividade = atividade.ConfiguracaoAtividade;
            var recorrenciasAtividade = atividade.RegistroRecorrencias.ToList();
            var dataFinal = dataInicial.AddMonths(1).AddDays(-1);
            var frequencia = configuracaoAtividade.Frequencia;
            frequencia = frequencia > 0 ? frequencia : 1;
            int dia = configuracaoAtividade.DiaMes > 0 ? (int)configuracaoAtividade.DiaMes : atividade.DataInicial.Day;

            do
            {
                if (configuracaoAtividade.TerminaEm != null && dataAtividade > configuracaoAtividade.TerminaEm)
                    break;
                if (configuracaoAtividade.QtdRepeticoes != null && configuracaoAtividade.QtdRepeticoes <= contador)
                    break;
                dataAtividade = dataAtividade.AddMonths(frequencia);
                if (configuracaoAtividade.DiaMes > 0)
                {
                    var diaAtividade = dataAtividade.Day;
                    if (diaAtividade > dia)
                        dataAtividade = new DateTime(dataAtividade.Year, dataAtividade.Month, dia);
                    else
                    {
                        var ultimoDiaMes = dataFinal.Day;
                        if (ultimoDiaMes < dia)
                            dataAtividade = new DateTime(dataAtividade.Year, dataAtividade.Month, ultimoDiaMes);
                    }
                }
                if ((dataAtividade >= dataInicial && dataAtividade <= dataFinal))
                {
                    AtividadeRecorrenciaModel recorrencia = MontarRegistroRecorrencia(atividade, dataAtividade);
                    lista.Add(recorrencia);
                }
                contador++;
            }
            while (dataAtividade <= dataFinal);

            return lista;
        }

        private List<AtividadeRecorrenciaModel> CalcularRecorrencia(AtividadeModel atividade, DateTime dataInicial)
        {
            if (atividade.IdTipoRecorrencia == TipoRecorrenciaSeed.Diaria.IdTipoRecorrencia)
            {
                return CalcularRecorrenciaDiaria(atividade, dataInicial);
            }
            else if (atividade.IdTipoRecorrencia == TipoRecorrenciaSeed.Semanal.IdTipoRecorrencia)
            {
                return CalcularRecorrenciaSemanal(atividade, dataInicial);
            }
            else
            {
                return CalcularRecorrenciaMensal(atividade, dataInicial);
            }

        }

        public void Adicionar(AtividadeModel obj)
        {
            if (obj.ConfiguracaoAtividade != null)
                obj.ConfiguracaoAtividade.Frequencia = obj.ConfiguracaoAtividade.Frequencia == 0 ? 1 : obj.ConfiguracaoAtividade.Frequencia;

            if (obj.ConfiguracaoAtividade?.QtdRepeticoes > 0 && obj.ConfiguracaoAtividade?.TerminaEm != null)
                throw new CustomBaseException(new Exception(), "Deve definir OU [Quantidade de Repetições] OU [Termina em]");

            _atividadeRepositorio.Adicionar(obj);
        }

        public void FinalizarAtividade(int idAtividade, DateTime dataRealizacaoPrevista, DateTime dataRealizacao, string observacao)
        {
            if (ValidarRecorrencia(idAtividade, dataRealizacaoPrevista))
                throw new CustomBaseException(new Exception(), string.Format("Já existe um registro para [{0}]", dataRealizacaoPrevista));
            var recorrencia = new RegistroRecorrenciaModel()
            {
                IdAtividade = idAtividade,
                DataPrevista = dataRealizacaoPrevista,
                DataRealizacao = dataRealizacao,
                Observacao = observacao
            };
            _registroRecorrenciaRepositorio.Adicionar(recorrencia);
        }

        public void CopiarAtividade(int idAtividade)
        {
            _atividadeRepositorio.CopiarAtividade(idAtividade);
        }

        private bool ValidarRecorrencia(int idAtividade, DateTime dataPrevista)
        {
            var query = _registroRecorrenciaRepositorio.Consultar().Where(e => e.IdAtividade == idAtividade && e.DataPrevista.Year == dataPrevista.Year
            && e.DataPrevista.Month == dataPrevista.Month
            && e.DataPrevista.Day == dataPrevista.Day);
            return query.Count() > 0;
        }

        public void TransferirAtividade(int idAtividade, string idUsuario)
        {
            var atividade = _atividadeRepositorio.ObterPorId(idAtividade, true);
            atividade.IdUsuario = string.IsNullOrEmpty(idUsuario) ? null : idUsuario;
            _atividadeRepositorio.Atualizar(atividade);
        }

        private void ValidarMesAno(string mesAno, ref int mes, ref int ano)
        {
            string erroDataInvalida = string.Format("Data [{0}] inválida", mesAno);
            var datas = mesAno.Split('/');

            if (datas.Count() != 2)
                throw new CustomBaseException(new Exception(), erroDataInvalida);

            if (!Int32.TryParse(datas[0], out mes) || (mes < 1 && mes > 12))
                throw new CustomBaseException(new Exception(), erroDataInvalida);

            if (!Int32.TryParse(datas[1], out ano))
                throw new CustomBaseException(new Exception(), erroDataInvalida);

        }

        private List<DayOfWeek> ObterDiasDaSemana(List<AtividadeDiaSemanaModel> atividadeDiaSemanas)
        {
            List<DayOfWeek> retorno = new List<DayOfWeek>();

            Dictionary<int, DayOfWeek> dicDias = new Dictionary<int, DayOfWeek>();
            dicDias.Add(DiaSemanaSeed.Domingo.IdDiaSemana, DayOfWeek.Sunday);
            dicDias.Add(DiaSemanaSeed.Segunda.IdDiaSemana, DayOfWeek.Monday);
            dicDias.Add(DiaSemanaSeed.Terca.IdDiaSemana, DayOfWeek.Tuesday);
            dicDias.Add(DiaSemanaSeed.Quarta.IdDiaSemana, DayOfWeek.Wednesday);
            dicDias.Add(DiaSemanaSeed.Quinta.IdDiaSemana, DayOfWeek.Thursday);
            dicDias.Add(DiaSemanaSeed.Sexta.IdDiaSemana, DayOfWeek.Friday);
            dicDias.Add(DiaSemanaSeed.Sabado.IdDiaSemana, DayOfWeek.Saturday);

            foreach (var dia in atividadeDiaSemanas)
            {
                var diaSemana = dicDias[dia.IdDiaSemana];
                retorno.Add(diaSemana);
            }

            return retorno;
        }
    }
}
