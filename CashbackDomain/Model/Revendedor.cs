using System.ComponentModel.DataAnnotations;

namespace CashbackDomain.Model
{
    public class Revendedor
    {
        [Key]        
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string NomeCompleto{ get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string UserName { get; set; }
    }
}
