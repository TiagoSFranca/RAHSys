using System;

namespace RAHSys.Entidades.Entidades
{
    public class ContratoModel
    {
        public int IdContrato { get; set; }
        public string NomeEmpresa { get; set; }
        public string ContatoInicial { get; set; }

        public virtual ContratoEnderecoModel ContratoEndereco { get; set; }
    }
}
