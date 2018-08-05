using RAHSys.Entidades;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.AppModels
{
    public class ConsultaAppModel<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Resultado { get; set; }
        public int PaginaAtual { get; set; }
        public int ItensPorPagina { get; set; }
        public int TotalPaginas { get; set; }
        public int TotalItens { get; set; }
    }
}
