namespace MiniMundo.WebApp.Model
{
    public class ClienteModel
    {
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string TelefoneCliente { get; set; }
        public DateTime DataCadastroCliente { get; set; }
    }
}
