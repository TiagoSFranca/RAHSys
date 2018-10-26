using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Entidades.Entidades;
using System.Data.Entity;
using System.Linq;

namespace RAHSys.Infra.Dados.Repositorios
{
    public class ClienteRepositorio : RepositorioBase<ClienteModel>, IClienteRepositorio
    {
        public void Adicionar(ClienteModel obj)
        {
            var fiadores = obj.Fiadores;
            var responsavelFinanceiro = obj.ResponsavelFinanceiro;
            obj.Fiadores = null;

            _context.Cliente.Add(obj);

            foreach (var fiador in fiadores.Where(e => !string.IsNullOrEmpty(e.Email)))
            {
                var fiadorEndereco = fiador.FiadorEndereco;

                fiador.IdCliente = obj.IdAnaliseInvestimento;
                fiador.FiadorEndereco = null;

                _context.Fiador.Add(fiador);

                var endereco = fiadorEndereco.Endereco;
                endereco.Cidade = null;

                _context.Endereco.Add(endereco);

                fiadorEndereco.Endereco = null;
                fiadorEndereco.Fiador = null;
                fiadorEndereco.IdFiador = fiador.IdFiador;
                fiadorEndereco.IdEndereco = endereco.IdEndereco;

                _context.FiadorEndereco.Add(fiadorEndereco);

                _context.SaveChanges();

                fiador.IdFiadorEndereco = fiadorEndereco.IdFiadorEndereco;

                _context.Entry(fiador).State = EntityState.Modified;

                _context.SaveChanges();
            }
            _context.SaveChanges();
        }
    }
}
