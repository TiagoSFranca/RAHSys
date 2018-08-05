using System.Collections.Generic;

namespace RAHSys.Entidades
{
    public class ConsultaModel<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Resultado { get; set; }
        public int PaginaAtual { get; set; }
        public int ItensPorPagina { get; set; }
        public int TotalPaginas
        {
            get
            {
                int total = 0;
                total = TotalItens / ItensPorPagina + (TotalItens % ItensPorPagina > 0 ? 1 : 0);
                return total;
            }
        }
        public int TotalItens { get; set; }

        public ConsultaModel(int paginaAtual, int qtdItens)
        {
            PaginaAtual = paginaAtual;
            ItensPorPagina = qtdItens;
        }
    }
}
