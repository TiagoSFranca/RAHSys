using System;

namespace RAHSys.Infra.CrossCutting.Exceptions
{
    public class CustomBaseException : Exception
    {
        public Guid CodExcecao { get; private set; }
        public string Mensagem { get; private set; }

        public CustomBaseException(Exception ex, string mensagem = null)
            : base(ex.Message, ex)
        {
            CodExcecao = Guid.NewGuid();
            Mensagem = mensagem ?? string.Format("Ocorreu um erro! Entre em contato com o administrador e informe o seguinte código: [{0}].", CodExcecao);
        }
    }
}
