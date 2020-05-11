using CashbackBusiness;
using CashbackDomain.Utils;
using Xunit;

namespace CashbackTest
{
    public class RevendedorTest
    {
        RevendedorBusiness revendedorBusiness;
        public RevendedorTest()
        {
            revendedorBusiness = new RevendedorBusiness();
        }
        [Fact]
        public void Nome_Vazio_Retorna_Msg_Erro()
        {
            var sud = revendedorBusiness.ValidarNomeRevendedor("");
            Assert.Equal(MensagensSistema.NomeVazio, sud);
        }

        [Fact]
        public void Nome_Null_Retorna_Msg_Erro()
        {
            var sud = revendedorBusiness.ValidarNomeRevendedor(null);
            Assert.Equal(MensagensSistema.NomeVazio, sud);
        }

        [Fact]
        public void Nome_Retorna_EmptyString()
        {
            var sud = revendedorBusiness.ValidarNomeRevendedor("Renan");
            Assert.Equal("", sud);
        }
        [Fact]
        public void Sobrenome_Vazio_Retorna_Msg_Erro()
        {
            var sud = revendedorBusiness.ValidarSobrenomeRevendedor("");
            Assert.Equal(MensagensSistema.SobrenomeVazio, sud);
        }

        [Fact]
        public void Sobrenome_Null_Retorna_Msg_Erro()
        {
            var sud = revendedorBusiness.ValidarSobrenomeRevendedor(null);
            Assert.Equal(MensagensSistema.SobrenomeVazio, sud);
        }

        [Fact]
        public void Sobrenome_Retorna_EmptyString()
        {
            var sud = revendedorBusiness.ValidarSobrenomeRevendedor("Oliveira");
            Assert.Equal("", sud);
        }

        [Fact]
        public void Nome_Incompleto_Retorna_Msg_Erro()
        {
            var sud = revendedorBusiness.ValidarNomeCompletoRevendedor("Renan ");
            Assert.Equal(MensagensSistema.NomeCompletoQuantidadeMinimaPalavras, sud);
        }

        [Fact]
        public void Nome_Completo_Null_Retorna_Msg_Erro()
        {
            var sud = revendedorBusiness.ValidarNomeCompletoRevendedor(null);
            Assert.Equal(MensagensSistema.NomeCompletoQuantidadeMinimaPalavras, sud);
        }
        [Fact]
        public void Nome_Completo_Retorna_EmptyString()
        {
            var sud = revendedorBusiness.ValidarSobrenomeRevendedor("Renan Oliveira");
            Assert.Equal("", sud);
        }
        [Fact]
        public void Email_preenchido_nao_retorna_erro()
        {
            var sud = revendedorBusiness.ValidarEmailVazioRevendedor("renanoliveiraone@gmail.com");
            Assert.Equal("", sud);
        }
        [Fact]
        public void Email_nao_preenchido_retorna_erro()
        {
            var sud = revendedorBusiness.ValidarEmailVazioRevendedor("O email não pode estar vazio");
            Assert.Equal("", sud);
        }

        [Fact]
        public void CPF_preenchido_nao_retorna_erro()
        {
            var sud = revendedorBusiness.ValidarCPFRevendedor("123.456.789-00");
            Assert.Equal("", sud);
        }
        [Fact]
        public void CPF_nao_preenchido_retorna_erro()
        {
            var sud = revendedorBusiness.ValidarEmailVazioRevendedor("");
            Assert.Equal("O email não pode estar vazio", sud);
        }
    }
}
