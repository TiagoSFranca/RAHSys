using RAHSys.Entidades.Entidades;

namespace RAHSys.Dominio.Servicos.Interfaces.Repositorios
{
    public interface IAtividadeRepositorio : IRepositorioBase<AtividadeModel>
    {
        void CopiarAtividade(int idAtividade);
    }
}
