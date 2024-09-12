namespace MiniMundo.DAL.Model.Entity;

public class Contrato
{
    // Chave primária
    public int ContratoId { get; set; }  
    public string Descricao { get; set; }
    // Chave estrangeira
    public int ClienteId { get; set; }
    // Navegação para o cliente associado
    public Cliente Cliente { get; set; }
}