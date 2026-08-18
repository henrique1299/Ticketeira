using SearchService.Enderecos;

namespace SearchService
{
    public class Endereco
    {

        public string rua { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string UF { get; set; }
        public string pais { get; set; }
        public CEP cep { get; set; }

        public Endereco() { }

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
