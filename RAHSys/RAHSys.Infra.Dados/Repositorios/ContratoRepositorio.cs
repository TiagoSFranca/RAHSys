using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Entidades.Entidades;
using System.Data.Entity;

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

            obj.IdContratoEndereco = contratoEndereco.IdContratoEndereco;

            _context.Entry(obj).State = EntityState.Modified;

            _context.SaveChanges();
        }

        public void Remover(ContratoModel obj)
        {
            var contratoEndereco = obj.ContratoEndereco;
            var endereco = contratoEndereco.Endereco;

            obj.IdContratoEndereco = null;
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            _context.Set<ContratoEnderecoModel>().Remove(contratoEndereco);
            _context.Set<EnderecoModel>().Remove(endereco);
            _context.Set<ContratoModel>().Remove(obj);
            _context.SaveChanges();
        }
    }
}
