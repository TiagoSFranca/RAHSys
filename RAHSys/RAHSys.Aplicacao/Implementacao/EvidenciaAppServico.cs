using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Extensoes;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades.Entidades;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RAHSys.Aplicacao.Implementacao
{
    public class EvidenciaAppServico : AppServicoBase<EvidenciaModel>, IEvidenciaAppServico
    {
        private readonly IEvidenciaServico _evidenciaServico;

        public EvidenciaAppServico(IEvidenciaServico evidenciaServico) : base(evidenciaServico)
        {
            _evidenciaServico = evidenciaServico;
        }

        public void Adicionar(EvidenciaAppModel obj)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (CustomBaseException ex)
            {
                LogExceptions(ex);
                throw;
            }
            catch (Exception ex)
            {
                var nex = new CustomBaseException(ex);
                LogExceptions(nex);
                throw nex;
            }
        }

        public EvidenciaAppModel ObterPorId(int id)
        {
            try
            {
                return _evidenciaServico.ObterPorId(id).MapearParaAplicacao();
            }
            catch (Exception ex)
            {
                var nex = new CustomBaseException(ex);
                LogExceptions(nex);
                throw nex;
            }
        }

        public void Remover(int id)
        {
            try
            {
                var evidencia = _evidenciaServico.ObterPorId(id);
                _evidenciaServico.Remover(evidencia);
            }
            catch (CustomBaseException ex)
            {
                LogExceptions(ex);
                throw;
            }
            catch (Exception ex)
            {
                var nex = new CustomBaseException(ex);
                LogExceptions(nex);
                throw nex;
            }
        }

        public void Atualizar(EvidenciaAppModel obj)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (CustomBaseException ex)
            {
                LogExceptions(ex);
                throw;
            }
            catch (Exception ex)
            {
                var nex = new CustomBaseException(ex);
                LogExceptions(nex);
                throw nex;
            }
        }

        public void AdicionarEvidencias(int idAtividade, int idRegistroRecorrencia, List<ArquivoAppModel> evidencias)
        {
            try
            {
                _evidenciaServico.AdicionarEvidencias(idAtividade, idRegistroRecorrencia, evidencias.Select(e => e.MapearParaDominio()).ToList());
            }
            catch (CustomBaseException ex)
            {
                LogExceptions(ex);
                throw;
            }
            catch (Exception ex)
            {
                var nex = new CustomBaseException(ex);
                LogExceptions(nex);
                throw nex;
            }
        }
    }
}
