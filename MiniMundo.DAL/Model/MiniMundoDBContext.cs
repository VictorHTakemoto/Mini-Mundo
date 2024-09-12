using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MiniMundo.DAL.Model.Entity;
using Newtonsoft.Json.Linq;
using System.Threading.Channels;

namespace MiniMundo.DAL.Model;

//Contexto banco de dados
public partial class MiniMundoDBContext : DbContext
{
    public MiniMundoDBContext() { }
    public MiniMundoDBContext
        (DbContextOptions<MiniMundoDBContext> options) : base(options) { }

    //Configuracao de conexao com DB
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Desserialização do arquivo json que contem a string de conexao
        string paramsJson = AppContext.BaseDirectory + "\\params.json";
        JObject globalParams = JObject.Parse(File.ReadAllText(paramsJson));
        try
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(globalParams["NomeAppSettings"].ToString())
                .Build();
            if (!optionsBuilder.IsConfigured)
            {
                var connString = config.GetConnectionString(globalParams["DBConnection"].ToString());
                optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Deu Ruim: " + ex);
        }
    }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Contrato> Contratos { get; set; }
}
