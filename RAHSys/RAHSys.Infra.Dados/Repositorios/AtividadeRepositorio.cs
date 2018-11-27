using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Entidades.Entidades;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;

namespace RAHSys.Infra.Dados.Repositorios
{
    public class AtividadeRepositorio : RepositorioBase<AtividadeModel>, IAtividadeRepositorio
    {

        public void CopiarAtividade(int idAtividade)
        {
            var atividade = ObterPorId(idAtividade, true);

            var configuracaoAtividade = _context.ConfiguracaoAtividade.Find(idAtividade);
            _context.Entry(configuracaoAtividade).State = EntityState.Detached;

            var dias = _context.AtividadeDiaSemana.Where(e => e.IdConfiguracaoAtividade == idAtividade).ToList();

            var registros = _context.RegistroRecorrencia.Where(e => e.IdAtividade == idAtividade).ToList();

            atividade.IdAtividade = 0;
            configuracaoAtividade.IdConfiguracaoAtividade = 0;
            dias.ForEach(dia =>
            {
                _context.Entry(dia).State = EntityState.Detached;
                dia.IdConfiguracaoAtividade = dia.IdAtividadeDiaSemana = 0;
            });
            registros.ForEach(registro =>
            {
                _context.Entry(registro).State = EntityState.Detached;
                registro.IdRegistroRecorrencia = registro.IdAtividade = 0;
            });

            if (configuracaoAtividade != null)
                configuracaoAtividade.AtividadeDiaSemanas = dias;

            atividade.ConfiguracaoAtividade = configuracaoAtividade;
            atividade.RegistroRecorrencias = registros;
            Adicionar(atividade);
        }
    }
}
