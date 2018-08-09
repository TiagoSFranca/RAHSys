using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Apresentacao.Models
{
    public class ContratoAdicionarModel
    {
        public ContratoAppModel Contrato { get; set; }
        public List<CidadeAppModel> Cidades { get; set; }
        public List<EstadoAppModel> Estados { get; set; }

        public ContratoAdicionarModel()
        {
            Cidades = new List<CidadeAppModel>();
            Estados = new List<EstadoAppModel>();
        }
    }
}