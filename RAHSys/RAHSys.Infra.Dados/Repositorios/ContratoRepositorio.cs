using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Entidades.Entidades;

namespace RAHSys.Infra.Dados.Repositorios
{
    public class ContratoRepositorio : RepositorioBase<ContratoModel>, IContratoRepositorio
    {
        public void Adicionar(ContratoModel obj)
        {
            var contratoEndereco = obj.ContratoEndereco;

            obj.ContratoEndereco = null;
            _context.Contrato.Add(obj);

            var endereco = contratoEndereco.Endereco;
            endereco.Cidade = null;

            _context.Endereco.Add(endereco);

            contratoEndereco.Endereco = null;
            contratoEndereco.Contrato = null;
            contratoEndereco.IdContrato = obj.IdContrato;
            contratoEndereco.IdEndereco = endereco.IdEndereco;

            _context.ContratoEndereco.Add(contratoEndereco);

            _context.SaveChanges();
        }
    }
}
