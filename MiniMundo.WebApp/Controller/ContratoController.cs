using Microsoft.EntityFrameworkCore;
using MiniMundo.DAL.Model;
using MiniMundo.DAL.Model.Entity;

namespace MiniMundo.WebApp.Controller
{
    public class ContratoController
    {
        Contrato contrato = new();
        public async Task<bool> NovoCadContratoAsync(Contrato contrato)
        {
            try
            {
                using MiniMundoDBContext db = new();
                db.Contratos.Add(contrato);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Erro ao salvar contrato: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Contrato>> BuscarContratosAsync()
        {
            using MiniMundoDBContext db = new();
            return await db.Contratos.ToListAsync();
        }

        public async Task AtualizarContratoAsync(Contrato AttContrato)
        {
            try
            {
                using MiniMundoDBContext db = new();
                var contratoExistente = await db.Contratos.FirstOrDefaultAsync(c => c.ContratoId == AttContrato.ContratoId);
                if (contratoExistente == null)
                {
                    throw new Exception("Contrato não encontrado.");
                }
                contratoExistente.Descricao = AttContrato.Descricao;
                db.Contratos.Update(contratoExistente);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar o contrato: {ex.Message}");
            }
        }

        public async Task ExcluirContrato(int contratoId)
        {
            using MiniMundoDBContext db = new();
            var contrato = await db.Contratos.FindAsync(contratoId);
            if(contrato != null)
            {
                db.Contratos.Remove(contrato);
                await db.SaveChangesAsync();
            }
        }
    }
}
