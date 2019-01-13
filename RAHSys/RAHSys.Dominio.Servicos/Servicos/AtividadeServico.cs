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
using RAHSys.Extras.Helper;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class AtividadeServico : ServicoBase<AtividadeModel>, IAtividadeServico
    {
        private readonly IAtividadeRepositorio _atividadeRepositorio;
        private readonly IRegistroRecorrenciaRepositorio _registroRecorrenciaRepositorio;
        private readonly IConfiguracaoAtividadeRepositorio _configuracaoAtividadeRepositorio;

        public AtividadeServico(IAtividadeRepositorio atividadeRepositorio, IRegistroRecorrenciaRepositorio registroRecorrenciaRepositorio,
            IConfiguracaoAtividadeRepositorio configuracaoAtividadeRepositorio) : base(atividadeRepositorio)
        {
            _atividadeRepositorio = atividadeRepositorio;
            _registroRecorrenciaRepositorio = registroRecorrenciaRepositorio;
            _configuracaoAtividadeRepositorio = configuracaoAtividadeRepositorio;
        }

        public ConsultaModel<AtividadeRecorrenciaModel> Consultar(IEnumerable<int> idList, IEnumerable<int> idTipoAtividadeList, IEnumerable<int> idEquipeList,
            IEnumerable<int> idContratoList, IEnumerable<string> idUsuarioList, string mesAno,
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

            switch ((ordenacao ?? string.Empty).ToLower())
            {
                case "tipoatividade":
                    query = crescente ? query.OrderBy(c => c.TipoAtividade.Descricao) : query.OrderByDescending(c => c.TipoAtividade.Descricao);
                    break;
                case "atribuidopara":
                    query = crescente ? query.OrderBy(c => c.Usuario.UserName) : query.OrderByDescending(c => c.Usuario.UserName);
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

        public void Adicionar(AtividadeModel obj)
        {
            if (obj.ConfiguracaoAtividade != null)
                obj.ConfiguracaoAtividade.Frequencia = obj.ConfiguracaoAtividade.Frequencia == 0 ? 1 : obj.ConfiguracaoAtividade.Frequencia;

            if (obj.ConfiguracaoAtividade?.QtdRepeticoes > 0 && obj.ConfiguracaoAtividade?.TerminaEm != null)
                throw new CustomBaseException(new Exception(), "Deve definir OU [Quantidade de Repetições] OU [Termina em]");

            if (obj.EquipeInteira)
                obj.IdUsuario = null;

            _atividadeRepositorio.Adicionar(obj);
        }

        public void Atualizar(AtividadeModel obj)
        {
            var idAtividade = obj.IdAtividade;

            if (obj.EquipeInteira)
                obj.IdUsuario = null;

            if (ObterConfiguracaoAtividade(idAtividade) == null)
            {
                if (ObterRecorrenciasAtividade(idAtividade).Count > 0)
                    _atividadeRepositorio.Adicionar(obj);
                else
                    _atividadeRepositorio.Atualizar(obj);
            }
            else
            {
                var atividade = _atividadeRepositorio.ObterPorId(idAtividade, false, true);
                _atividadeRepositorio.Adicionar(obj);

                atividade.ConfiguracaoAtividade.TerminaEm = DateTime.Now;
                _atividadeRepositorio.Atualizar(atividade);
            }
        }

        public void FinalizarRecorrencia(int idAtividade, DateTime dataRealizacaoPrevista, DateTime dataRealizacao, string observacao)
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
            //FinalizarAtividade(idAtividade);
        }

        public void CopiarAtividade(int idAtividade)
        {
            _atividadeRepositorio.CopiarAtividade(idAtividade);
        }

        public void TransferirAtividade(int idAtividade, string idUsuario)
        {
            var atividade = _atividadeRepositorio.ObterPorId(idAtividade, true);
            if (string.IsNullOrEmpty(idUsuario))
            {
                atividade.IdUsuario = null;
                atividade.EquipeInteira = false;
            }
            else if (idUsuario.Equals(EquipeHelper.EquipeInteira.Codigo))
            {
                atividade.IdUsuario = null;
                atividade.EquipeInteira = true;
            }
            else
            {
                atividade.IdUsuario = idUsuario;
                atividade.EquipeInteira = false;
            }
            _atividadeRepositorio.Atualizar(atividade);
        }

        public void EncerrarAtividade(int idAtividade, DateTime dataEncerramento)
        {
            var atividade = _atividadeRepositorio.ObterPorId(idAtividade, false);
            if (atividade.ConfiguracaoAtividade == null)
                throw new CustomBaseException(new Exception(), string.Format("Atividade de código [{0}] não pode ser encerrada", idAtividade));
            atividade.ConfiguracaoAtividade.TerminaEm = dataEncerramento;
            _atividadeRepositorio.Atualizar(atividade);
        }

        //public Dictionary<string, int> ObterRecorrenciasAtrasadas(string mesAno, int? idContrato, int? idEquipe, int? idAtividade, string usuario)
        //{
        //    Dictionary<string, int> recorrenciasAtrasadas = new Dictionary<string, int>();

        //    var query = _atividadeRepositorio.Consultar().Where(c => !c.Contrato.Excluido);

        //    mesAno = mesAno ?? string.Format("{0}/{1}", DateTime.Now.Month, DateTime.Now.Year);

        //    int mes = 0;
        //    int ano = 0;

        //    ValidarMesAno(mesAno, ref mes, ref ano);
        //    DateTime dataInicioMesAno = new DateTime(ano, mes, 1);
        //    DateTime dataFim = dataInicioMesAno.AddDays(-1);

        //    query = query.Where(e => e.DataInicial <= dataFim);

        //    if (idAtividade != null)
        //        query = query.Where(c => idAtividade == c.IdAtividade);

        //    if (idEquipe != null)
        //        query = query.Where(c => idEquipe == c.IdEquipe);

        //    if (idContrato != null)
        //        query = query.Where(c => idContrato == c.IdContrato);

        //    if (!string.IsNullOrEmpty(usuario))
        //        query = query.Where(c => usuario == c.IdUsuario);

        //    var atividades = query.ToList();

        //    return recorrenciasAtrasadas;
        //}

        private List<RegistroRecorrenciaModel> ObterRecorrenciasAtividade(int idAtividade)
        {
            var query = _registroRecorrenciaRepositorio.Consultar().Where(e => e.IdAtividade == idAtividade);
            return query.ToList();
        }

        private ConfiguracaoAtividadeModel ObterConfiguracaoAtividade(int idAtividade)
        {
            return _configuracaoAtividadeRepositorio.ObterPorId(idAtividade);
        }

        private bool ValidarRecorrencia(int idAtividade, DateTime dataPrevista)
        {
            var query = _registroRecorrenciaRepositorio.Consultar().Where(e => e.IdAtividade == idAtividade && e.DataPrevista.Year == dataPrevista.Year
            && e.DataPrevista.Month == dataPrevista.Month
            && e.DataPrevista.Day == dataPrevista.Day);
            return query.Count() > 0;
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

        #region Registro de Recorrência

        private AtividadeRecorrenciaModel MontarRegistroRecorrencia(AtividadeModel atividade, DateTime data, int numeroAtividade)
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
                atividade.TipoRecorrencia, registroRecorrencia, atividade.ConfiguracaoAtividade.TerminaEm == null &&
                (atividade.ConfiguracaoAtividade.QtdRepeticoes == null || atividade.ConfiguracaoAtividade.QtdRepeticoes == 0), numeroAtividade)
            {
                Realizada = realizada,
                EquipeInteira = atividade.EquipeInteira
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
                        false, 1)
                    {
                        Realizada = e.RegistroRecorrencias.Count > 0,
                        EquipeInteira = e.EquipeInteira
                    }
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

            var datas = Enumerable.Range(0, qtdDias).Select(i => dataInicio.AddDays(i * frequencia)).ToList();

            List<DateTime> datasExatas = new List<DateTime>();

            if (atividade.ConfiguracaoAtividade.QtdRepeticoes != null && atividade.ConfiguracaoAtividade.QtdRepeticoes > 0)
                datasExatas = datas.Skip(0).Take((int)atividade.ConfiguracaoAtividade.QtdRepeticoes).ToList();
            else
                datasExatas = datas.Where(e => e.Date >= dataInicial.Date && e.Date <= dataFinal.Date).ToList();

            datasExatas.ForEach(data =>
            {
                var indice = datas.IndexOf(data);
                AtividadeRecorrenciaModel recorrencia = MontarRegistroRecorrencia(atividade, data, indice + 1);
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

            if (diasDaSemana.Count == 0)
                diasDaSemana.Add(atividade.DataInicial.DayOfWeek);

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
                        if (i + count < todasDatas.Count && todasDatas[i + count] != null)
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
                var indice = datas.IndexOf(data);
                AtividadeRecorrenciaModel recorrencia = MontarRegistroRecorrencia(atividade, data, indice + 1);
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

            if (atividade.ConfiguracaoAtividade.TerminaEm != null)
                dataFinal = dataFinal < (DateTime)atividade.ConfiguracaoAtividade.TerminaEm ? dataFinal : (DateTime)atividade.ConfiguracaoAtividade.TerminaEm;

            do
            {
                if (configuracaoAtividade.TerminaEm != null && dataAtividade > configuracaoAtividade.TerminaEm)
                    break;
                if (configuracaoAtividade.QtdRepeticoes != null && configuracaoAtividade.QtdRepeticoes <= contador)
                    break;
                if (contador > 0)
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
                    AtividadeRecorrenciaModel recorrencia = MontarRegistroRecorrencia(atividade, dataAtividade, contador + 1);
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

        #endregion
    }
}
