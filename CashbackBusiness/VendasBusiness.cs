using CashbackDomain.Model;
using CashbackDomain.Utils;
using System.Collections.Generic;

namespace CashbackBusiness
{
    public class VendasBusiness
    {

        private Venda venda;        
        public VendasBusiness()
        {
            venda = new Venda();
        }

        public List<string> ValidarDadosVenda(Venda venda)
        {
            List<string> erros = new List<string>();

            var resultadoValidacao = ValidarCodigoVenda(venda);
            if (resultadoValidacao != "")
            {
                erros.Add(resultadoValidacao);
            }

            resultadoValidacao = ValidarDataVenda(venda);
            if (resultadoValidacao != "")
            {
                erros.Add(resultadoValidacao);
            }

            resultadoValidacao = ValidarCPFVenda(venda);
            if (resultadoValidacao != "")
            {
                erros.Add(resultadoValidacao);
            }

            resultadoValidacao = ValidarValorCompra(venda.Valor);
            if (resultadoValidacao != "")
            {
                erros.Add(resultadoValidacao);
            }
            return erros;
        }


        public string ValidarCodigoVenda(Venda venda)
        {
            if (string.IsNullOrEmpty((venda.CodigoId).ToString()))
                return MensagensSistema.CodigoVenda;
            return "";
        }

        public string ValidarDataVenda(Venda venda)
        {
            if (string.IsNullOrEmpty((venda.Data).ToString()))
                return MensagensSistema.DataVenda;
            return "";
        }

        public string ValidarCPFVenda(Venda venda)
        {
            if (string.IsNullOrEmpty(venda.CPF))
                return MensagensSistema.CPFVazio;
            return "";
        }


        public string ValidarValorCompra(decimal ValorCompra)
        {
            if (ValorCompra <= 0)
                return "Valor Inválido";

            return "";
        }

        public Venda CashBackCalculadoStatus(Venda venda)
        {
            DefinirCashback(venda);
            if (venda.CPF.Equals("153.509.460-56") || venda.CPF.Equals("15350946056"))
            {
                venda.Status = "Aprovado";
            } else
            {
                venda.Status = "Em validação";
            }

            return venda;
        }

        public Venda DefinirCashback(Venda venda)
        {
            if (venda.Valor <= 1000)
            {                
                venda.Cashback = venda.Valor * 0.10M;
                venda.PorcentagemCashbackAplicado = 10; 
                return venda;
            }

            else if (venda.Valor >= 1001 && venda.Valor <= 1500)
            {
                venda.Cashback = venda.Valor * 0.15M;
                venda.PorcentagemCashbackAplicado = 15;
                return venda;
            }

            else if (venda.Valor >= 1501)
            {
                venda.Cashback = venda.Valor * 0.20M;
                venda.PorcentagemCashbackAplicado = 20;
                return venda;
            }

            else return venda;
        }
    }
}
