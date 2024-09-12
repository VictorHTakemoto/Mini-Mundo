using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MiniMundo.DAL.Model;
using MiniMundo.DAL.Model.Entity;
using MiniMundo.WebApp.Model;

namespace MiniMundo.WebApp.Controller
{
    //Arquivo de funcoes CRUD
    public class ClienteController
    {
        //ClienteModel clienteModel = new();
        Cliente clienteToSave = new();
        
        
        public async Task<string> NovoCadCliente(ClienteModel cliente)
        {
            using MiniMundoDBContext db = new();
            try
            {
                if (db.Clientes.Any(c => c.NomeCliente == cliente.NomeCliente))
                {
                    return "Cliente já cadastrado";
                }
                clienteToSave.NomeCliente = cliente.NomeCliente;
                clienteToSave.EmailCliente = cliente.EmailCliente;
                clienteToSave.CEP = cliente.CEP;
                clienteToSave.Rua = cliente.Rua;
                clienteToSave.Numero = cliente.Numero;
                clienteToSave.Bairro = cliente.Bairro;
                clienteToSave.Cidade = cliente.Cidade;
                clienteToSave.Estado = cliente.Estado;
                clienteToSave.TelefoneCliente = cliente.TelefoneCliente;
                clienteToSave.DataCadastroCliente = DateTime.Now.ToUniversalTime();
                db.Clientes.Add(clienteToSave);
                await db.SaveChangesAsync();
                return "Usuario cadastrado com sucesso";
            }
            catch (Exception ex)
            {
                return $"Erro ao cadastrar usuario {ex.Message}";
            }
        }

        public async Task<List<Cliente>> BuscarClientes()
        {
            using MiniMundoDBContext db = new();
            return await db.Clientes.ToListAsync();
        }
    }
}