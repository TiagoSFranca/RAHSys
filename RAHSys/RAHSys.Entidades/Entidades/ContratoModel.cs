using System.Collections.Generic;

namespace RAHSys.Entidades.Entidades
{
    public class ContratoModel
    {
        public int IdContrato { get; set; }
        public string NomeEmpresa { get; set; }
        public string ContatoInicial { get; set; }
        public int? IdContratoEndereco { get; set; }
        public bool Excluido { get; set; }

        public virtual ContratoEnderecoModel ContratoEndereco { get; set; }
        public virtual AnaliseInvestimentoModel AnaliseInvestimento { get; set; }

        public virtual ICollection<DocumentoModel> Documentos { get; set; }
    }
}
