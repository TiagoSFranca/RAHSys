namespace RAHSys.Aplicacao.AppModels
{
    public class ContratoAppModel
    {
        public int IdContrato { get; set; }
        public string NomeEmpresa { get; set; }
        public string ContatoInicial { get; set; }

        public ContratoEnderecoAppModel ContratoEndereco { get; set; }
    }
}
