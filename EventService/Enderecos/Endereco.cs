using EventService.Enderecos;

namespace EventService
{
    public class Endereco
    {

        private string rua;
        private string bairro;
        private string cidade;
        private string UF;
        private string pais;
        private CEP cep;

        public Endereco(string rua, string bairro, string cidade, string UF, string pais, CEP cep)
        {
            this.rua = rua;
            this.bairro = bairro;
            this.cidade = cidade;
            this.UF = UF;
            this.pais = pais;
            this.cep = cep;
        }
    }
}
