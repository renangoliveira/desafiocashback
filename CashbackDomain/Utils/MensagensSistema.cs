
namespace CashbackDomain.Utils
{
    public static class MensagensSistema
    {
        public static string NomeVazio { get; set; } = "O nome não pode ser vazio";
        public static string SobrenomeVazio { get; set; } = "O sobrenome não pode ser vazio";
        public static string NomeCompletoQuantidadeMinimaPalavras { get; set; } = "O nome completo deve ser composto de pelo menos 2 palavras (Nome e Sobrenome)";
        public static string CPFVazio { get; set; } = "O CPF não pode ser vazio"; 
        public static string CPFInvalido { get; set; } = "O CPF é invalido"; 
        public static string EmailVazio { get; set; } = "O email não pode estar vazio";
        public static string EmailValido { get; set; } = "O email deve ser valido"; 
        public static string SenhaVazio { get; set; } = "A senha não pode ser vazio";
        public static string CodigoVenda { get; set; } = "O código não pode ser vazio";
        public static string DataVenda { get; set; } = "A data não pode ser vazio";
    }
}
