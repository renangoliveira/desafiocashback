using CashbackBusiness;
using CashbackDomain.Model;
using Xunit;

namespace CashbackTest
{
    public class VendaTest
    {
        VendasBusiness vendasBusiness;
        Venda venda;        
        public VendaTest()
        {
            vendasBusiness = new VendasBusiness();
            venda = new Venda();            
        }

        [Fact]
        public void Valor_abaixo_de_1000_deve_ser_10_porcento()
        {
            venda.Valor = 1000.00M;
            var sut = vendasBusiness.DefinirCashback(venda);
            Assert.Equal(100M, sut.Cashback);
        }

        [Fact]
        public void Valor_entre_de_1001_e_1500_deve_ser_15_porcento()
        {
            venda.Valor = 1500.00M;
            var sut = vendasBusiness.DefinirCashback(venda);
            Assert.Equal(225M, sut.Cashback);
        }

        [Fact]
        public void Valor_acima_de_1501_deve_ser_20_porcento()
        {
            venda.Valor = 2000.00M;
            var sut = vendasBusiness.DefinirCashback(venda);
            Assert.Equal(400M, sut.Cashback);
        }

        [Fact]
        public void Valor_de_compra_0_deve_retornar_mensagem_de_erro()
        {
            var sut = vendasBusiness.ValidarValorCompra(0);
            Assert.Equal("Valor Inválido", sut);
        }

        [Fact]
        public void Valor_de_compra_negativo_deve_retornar_mensagem_de_erro()
        {
            var sut = vendasBusiness.ValidarValorCompra(-10);
            Assert.Equal("Valor Inválido", sut);
        }

        [Fact]
        public void Valor_de_compra_positivo_nao_deve_retornar_mensagem_de_erro()
        {
            var sut = vendasBusiness.ValidarValorCompra(100);
            Assert.Equal("", sut);
        }
    }
}
