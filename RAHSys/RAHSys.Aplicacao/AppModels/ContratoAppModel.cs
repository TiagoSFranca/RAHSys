using RAHSys.Extras.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RAHSys.Aplicacao.AppModels
{
    public class ContratoAppModel
    {
        [Display(Name = "Código do Contrato")]
        public int IdContrato { get; set; }
        [Required]
        [Display(Name = "Nome da Empresa")]
        [MaxLength(256)]
        public string NomeEmpresa { get; set; }
        [Required]
        [Display(Name = "Contato Inicial")]
        [MaxLength(256)]
        public string ContatoInicial { get; set; }

        public int? IdContratoEndereco { get; set; }

        public ContratoEnderecoAppModel ContratoEndereco { get; set; }

        public AnaliseInvestimentoAppModel AnaliseInvestimento { get; set; }

        public virtual List<DocumentoAppModel> Documentos { get; set; }

        public virtual List<PagamentoAppModel> Pagamentos { get; set; }

        public SituacaoContrato ObterSituacaoContrato()
        {
            if (AnaliseInvestimento?.Cliente != null)
                return SituacaoContratoEnum.ContratoAssinado;
            else if (AnaliseInvestimento != null)
                return SituacaoContratoEnum.EmAnalise;
            return SituacaoContratoEnum.NovoContrato;
        }

        public SituacaoPagamento ObterSituacaoPagamento()
        {
            if (Pagamentos?.Count > 0)
            {
                if (Pagamentos?.FirstOrDefault(e => e.DataPagamento.Month == DateTime.Now.Month && e.DataPagamento.Year == DateTime.Now.Year) != null)
                    return SituacaoPagamentoEnum.Pago;
                else
                    return SituacaoPagamentoEnum.NaoPago;
            }
            return SituacaoPagamentoEnum.NenhumPagamento;
        }
    }
}
