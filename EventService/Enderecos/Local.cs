using EventService.Eventos;

namespace EventService.Enderecos
{

    public class Local
    {
        private string Id;
        private string nome;
        private int capacidade;
        private Endereco endereco;
        private Assento assento;

        public Local(string Id, string nome, Endereco endereco, int capacidade)
        {
            this.Id = Id;
            this.nome = nome;
            this.endereco = endereco;
            this.capacidade = capacidade;
        }
    }

    public class LocalDto
    {
        public string Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Capacidade { get; set; }
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;

        public string Pais { get; set; } = string.Empty;
    }
}
