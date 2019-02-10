using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using RAHSys.Entidades.Seeds;
using RAHSys.Extras.Helper;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class AtividadeServico : ServicoBase<AtividadeModel>, IAtividadeServico
    {
        private readonly IAtividadeRepositorio _atividadeRepositorio;
        private readonly IRegistroRecorrenciaRepositorio _registroRecorrenciaRepositorio;
        private readonly IConfiguracaoAtividadeRepositorio _configuracaoAtividadeRepositorio;

        private Dictionary<int, DayOfWeek> _dictDias
        {
            get
            {
                Dictionary<int, DayOfWeek> dicDias = new Dictionary<int, DayOfWeek>
                {
                    { DiaSemanaSeed.Domingo.IdDiaSemana, DayOfWeek.Sunday },
                    { DiaSemanaSeed.Segunda.IdDiaSemana, DayOfWeek.Monday },
                    { DiaSemanaSeed.Terca.IdDiaSemana, DayOfWeek.Tuesday },
                    { DiaSemanaSeed.Quarta.IdDiaSemana, DayOfWeek.Wednesday },
                    { DiaSemanaSeed.Quinta.IdDiaSemana, DayOfWeek.Thursday },
                    { DiaSemanaSeed.Sexta.IdDiaSemana, DayOfWeek.Friday },
                    { DiaSemanaSeed.Sabado.IdDiaSemana, DayOfWeek.Saturday }
                };
                return dicDias;
            }
        }

        public AtividadeServico(IAtividadeRepositorio atividadeRepositorio, IRegistroRecorrenciaRepositorio registroRecorrenciaRepositorio,
            IConfiguracaoAtividadeRepositorio configuracaoAtividadeRepositorio)
            : base(atividadeRepositorio)
        {
            _atividadeRepositorio = atividadeRepositorio;
            _registroRecorrenciaRepositorio = registroRecorrenciaRepositorio;
            _configuracaoAtividadeRepositorio = configuracaoAtividadeRepositorio;
        }

        public ConsultaModel<AtividadeRecorrenciaModel> Consultar(IEnumerable<int> idList, IEnumerable<int> idTipoAtividadeList, IEnumerable<int> idEquipeList,
            IEnumerable<int> idContratoList, IEnumerable<string> idUsuarioList, DateTime dataInicial, DateTime dataFinal,
            string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<AtividadeRecorrenciaModel>(pagina, quantidade);

            var query = _atividadeRepositorio.Consultar().Where(c => !c.Contrato.Excluido);

            query = query.Where(e => (DbFunctions.TruncateTime(e.DataInicial) <= DbFunctions.TruncateTime(dataFinal) && e.ConfiguracaoAtividade != null) ||
                (DbFunctions.TruncateTime(e.DataInicial) >= DbFunctions.TruncateTime(dataInicial) &&
                    DbFunctions.TruncateTime(e.DataInicial) <= DbFunctions.TruncateTime(dataFinal) &&
                    e.ConfiguracaoAtividade == null)
            );

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
            var recorrencia = ObterRecorrenciaAtividades(resultado, dataInicial, dataFinal);
            recorrencia = recorrencia.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = recorrencia.Count();
            consultaModel.Resultado = recorrencia;

            return consultaModel;
        }

        public void Adicionar(AtividadeModel obj)
        {
            if (obj.ConfiguracaoAtividade != null)
            {
                obj.ConfiguracaoAtividade.Frequencia = obj.ConfiguracaoAtividade.Frequencia == 0 ? 1 : obj.ConfiguracaoAtividade.Frequencia;
                if (obj.ConfiguracaoAtividade.ApenasDiaUtil && obj.IdTipoRecorrencia == TipoRecorrenciaSeed.Semanal.IdTipoRecorrencia)
                {
                    if (obj.ConfiguracaoAtividade.AtividadeDiaSemanas?.Count == 0)
                        throw new CustomBaseException(new Exception(), "Para atividades semanais deve definir [Dia da semana].");
                    else if (obj.ConfiguracaoAtividade.AtividadeDiaSemanas.Where(e => new[] { DiaSemanaSeed.Domingo.IdDiaSemana, DiaSemanaSeed.Sabado.IdDiaSemana }.Contains(e.IdDiaSemana)).Count() > 0)
                        throw new CustomBaseException(new Exception(), "Para atividades semanais com [Apenas dia útil] marcado NÃO pode definir [Dia da semana] como [Sábado] ou [Domingo].");
                }
            }

            if (obj.ConfiguracaoAtividade?.QtdRepeticoes > 0 && obj.ConfiguracaoAtividade?.TerminaEm != null)
                throw new CustomBaseException(new Exception(), "Deve definir OU [Quantidade de Repetições] OU [Termina em].");


            if (obj.EquipeInteira)
                obj.IdUsuario = null;

            _atividadeRepositorio.Adicionar(obj);
        }

        public void Atualizar(AtividadeModel obj)
        {
            var idAtividade = obj.IdAtividade;

            if (obj.EquipeInteira)
                obj.IdUsuario = null;

            //if (ObterConfiguracaoAtividade(idAtividade) == null)
            //{
            //    if (ObterRecorrenciasAtividade(idAtividade).Count > 0)
            //        _atividadeRepositorio.Adicionar(obj);
            //    else
            //        _atividadeRepositorio.Atualizar(obj);
            //}
            //else
            //{
            var atividade = _atividadeRepositorio.ObterPorId(idAtividade, false, true);
            this.Adicionar(obj);

            atividade.ConfiguracaoAtividade.TerminaEm = DateTime.Now;
            _atividadeRepositorio.Atualizar(atividade);
            //}
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
                throw new CustomBaseException(new Exception(), string.Format("Atividade de código [{0}] não pode ser encerrada.", idAtividade));
            atividade.ConfiguracaoAtividade.TerminaEm = dataEncerramento;
            _atividadeRepositorio.Atualizar(atividade);
        }

        public void AlterarEquipe(int idAtividade, int idEquipe)
        {
            var atividade = _atividadeRepositorio.ObterPorId(idAtividade, false);

            if (atividade.IdEquipe == idEquipe)
                return;

            atividade.IdEquipe = idEquipe;
            atividade.IdUsuario = null;

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

        private void ValidarMesAno(string mesAno, ref int mes, ref int ano)
        {
            string erroDataInvalida = string.Format("Data [{0}] inválida.", mesAno);
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

            foreach (var dia in atividadeDiaSemanas)
            {
                var diaSemana = _dictDias[dia.IdDiaSemana];
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
                (atividade.ConfiguracaoAtividade.QtdRepeticoes == null || atividade.ConfiguracaoAtividade.QtdRepeticoes == 0), numeroAtividade, registroRecorrencia.Evidencias?.Count > 0)
            {
                Realizada = realizada,
                EquipeInteira = atividade.EquipeInteira
            };

            return recorrencia;
        }

        private List<AtividadeRecorrenciaModel> ObterRecorrenciaAtividades(List<AtividadeModel> atividades, DateTime dataInicial, DateTime dataFinal)
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
                        false, 1, (e.RegistroRecorrencias.FirstOrDefault() ?? new RegistroRecorrenciaModel() { DataPrevista = e.DataInicial }).Evidencias?.Count > 0)
                    {
                        Realizada = e.RegistroRecorrencias.Count > 0,
                        EquipeInteira = e.EquipeInteira
                    }
                    ).ToList());

            foreach (var atividadeComRecorrencia in atividadesComRecorrencia)
            {
                listaRecorrencia.AddRange(CalcularRecorrencia(atividadeComRecorrencia, dataInicial, dataFinal));
            }

            return listaRecorrencia;
        }

        private List<AtividadeRecorrenciaModel> CalcularRecorrenciaDiaria(AtividadeModel atividade, DateTime dataInicial, DateTime dataFinal)
        {
            List<AtividadeRecorrenciaModel> lista = new List<AtividadeRecorrenciaModel>();

            var configuracaoAtividade = atividade.ConfiguracaoAtividade;
            var recorrenciasAtividade = atividade.RegistroRecorrencias.ToList();
            var frequencia = configuracaoAtividade.Frequencia;
            frequencia = frequencia > 0 ? frequencia : 1;
            var dataAtividade = atividade.DataInicial;

            if (atividade.ConfiguracaoAtividade.TerminaEm != null)
                dataFinal = dataFinal < (DateTime)atividade.ConfiguracaoAtividade.TerminaEm ? dataFinal : (DateTime)atividade.ConfiguracaoAtividade.TerminaEm;

            var contador = 0;

            do
            {
                if (configuracaoAtividade.TerminaEm != null && dataAtividade > configuracaoAtividade.TerminaEm)
                    break;
                if (configuracaoAtividade.QtdRepeticoes != null && configuracaoAtividade.QtdRepeticoes <= contador)
                    break;

                if (contador > 0)
                    dataAtividade = dataAtividade.AddDays(frequencia);

                if (configuracaoAtividade.ApenasDiaUtil)
                    VerificarDiaUtil(ref dataAtividade);

                if (dataAtividade >= dataInicial && dataAtividade <= dataFinal)
                {
                    AtividadeRecorrenciaModel recorrencia = MontarRegistroRecorrencia(atividade, dataAtividade, contador + 1);
                    lista.Add(recorrencia);
                }

                contador++;
            } while (dataAtividade < dataFinal);

            return lista;
        }

        private List<AtividadeRecorrenciaModel> CalcularRecorrenciaSemanal(AtividadeModel atividade, DateTime dataInicial, DateTime dataFinal)
        {
            List<AtividadeRecorrenciaModel> lista = new List<AtividadeRecorrenciaModel>();
            var configuracaoAtividade = atividade.ConfiguracaoAtividade;
            var recorrenciasAtividade = atividade.RegistroRecorrencias.ToList();
            var frequencia = configuracaoAtividade.Frequencia;
            frequencia = frequencia > 0 ? frequencia : 1;
            var dataInicio = atividade.DataInicial;

            if (atividade.ConfiguracaoAtividade.TerminaEm != null)
                dataFinal = dataFinal < (DateTime)atividade.ConfiguracaoAtividade.TerminaEm ? dataFinal : (DateTime)atividade.ConfiguracaoAtividade.TerminaEm;

            int qtdDias = dataFinal.Subtract(dataInicio).Days + 1;

            var diasDaSemana = ObterDiasDaSemana(atividade.ConfiguracaoAtividade.AtividadeDiaSemanas.ToList());

            if (diasDaSemana.Count == 0)
                return lista;

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

        private List<AtividadeRecorrenciaModel> CalcularRecorrenciaMensal(AtividadeModel atividade, DateTime dataInicial, DateTime dataFinal)
        {
            //TODO: CALCULO DE RECORRÊNCIA MENSAL
            List<AtividadeRecorrenciaModel> lista = new List<AtividadeRecorrenciaModel>();
            var contador = 0;
            var dataAtividade = atividade.DataInicial;
            var configuracaoAtividade = atividade.ConfiguracaoAtividade;
            var recorrenciasAtividade = atividade.RegistroRecorrencias.ToList();
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

                if (dataAtividade.Day < dia)
                {
                    var ultimoDiaMes = DateTime.DaysInMonth(dataAtividade.Year, dataAtividade.Month);
                    if (ultimoDiaMes >= dia)
                        dataAtividade = new DateTime(dataAtividade.Year, dataAtividade.Month, dia);
                }

                var dataAtividadeClone = dataAtividade;

                if (configuracaoAtividade.ApenasDiaUtil)
                    VerificarDiaUtil(ref dataAtividadeClone);

                if (dataAtividadeClone >= dataInicial && dataAtividadeClone <= dataFinal)
                {
                    AtividadeRecorrenciaModel recorrencia = MontarRegistroRecorrencia(atividade, dataAtividadeClone, contador + 1);
                    lista.Add(recorrencia);
                }
                contador++;
            }
            while (dataAtividade <= dataFinal);

            return lista;
        }

        private void VerificarDiaUtil(ref DateTime data)
        {
            var diasUteis = new List<DayOfWeek>
            {
                _dictDias[DiaSemanaSeed.Segunda.IdDiaSemana],
                _dictDias[DiaSemanaSeed.Terca.IdDiaSemana],
                _dictDias[DiaSemanaSeed.Quarta.IdDiaSemana],
                _dictDias[DiaSemanaSeed.Quinta.IdDiaSemana],
                _dictDias[DiaSemanaSeed.Sexta.IdDiaSemana]
            };

            if (!diasUteis.Contains(data.DayOfWeek))
            {
                while (!diasUteis.Contains(data.DayOfWeek))
                {
                    data = data.AddDays(1);
                }
            }
        }

        private List<AtividadeRecorrenciaModel> CalcularRecorrencia(AtividadeModel atividade, DateTime dataInicial, DateTime dataFinal)
        {
            if (atividade.IdTipoRecorrencia == TipoRecorrenciaSeed.Diaria.IdTipoRecorrencia)
            {
                return CalcularRecorrenciaDiaria(atividade, dataInicial, dataFinal);
            }
            else if (atividade.IdTipoRecorrencia == TipoRecorrenciaSeed.Semanal.IdTipoRecorrencia)
            {
                return CalcularRecorrenciaSemanal(atividade, dataInicial, dataFinal);
            }
            else
            {
                return CalcularRecorrenciaMensal(atividade, dataInicial, dataFinal);
            }

        }

        #endregion
    }
}
