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
            var cliente = obj.AnaliseInvestimento?.Cliente;
            if (cliente != null)
            {
                var fiadores = cliente.Fiadores;
                foreach (var fiador in fiadores)
                {
                    var fiadorEndereco = fiador.FiadorEndereco;
                    var fEndereco = fiadorEndereco.Endereco;
                    fiador.IdFiadorEndereco = null;
                    _context.Entry(fiador).State = EntityState.Modified;
                    _context.SaveChanges();

                    _context.Set<FiadorEnderecoModel>().Remove(fiadorEndereco);
                    _context.Set<EnderecoModel>().Remove(fEndereco);
                    _context.SaveChanges();
                }
                _context.Set<FiadorModel>().RemoveRange(fiadores);
                _context.SaveChanges();
            }
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
