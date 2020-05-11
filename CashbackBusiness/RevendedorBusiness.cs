using CashbackDomain.Model;
using CashbackDomain.Utils;
using System.Collections.Generic;

namespace CashbackBusiness
{
    public class RevendedorBusiness
    {    
        private Revendedor revendedor;
        public RevendedorBusiness()
        {
            revendedor = new Revendedor();
        }
        public List<string> ValidarDadosRevendedor(Revendedor revendedor)
        {

            revendedor.NomeCompleto = revendedor.Nome + " " + revendedor.Sobrenome;
            List<string> erros = new List<string>();

            var resultadoValidacao = ValidarNomeRevendedor(revendedor.Nome);
            if (resultadoValidacao != "")
            {
                erros.Add(resultadoValidacao);
            }

            resultadoValidacao = ValidarSobrenomeRevendedor(revendedor.Sobrenome);
            if (resultadoValidacao != "")
            {
                erros.Add(resultadoValidacao);
            }

            resultadoValidacao = ValidarNomeCompletoRevendedor(revendedor.NomeCompleto);
            if (resultadoValidacao != "")
            {
                erros.Add(resultadoValidacao);
            }

            resultadoValidacao = ValidarEmailRevendedor(revendedor.Email);
            if (resultadoValidacao != "")
            {
                erros.Add(resultadoValidacao);
            }

            resultadoValidacao = ValidarEmailVazioRevendedor(revendedor.Email);
            if (resultadoValidacao != "")
            {
                erros.Add(resultadoValidacao);
            }

            resultadoValidacao = ValidarSenhaRevendedor(revendedor.Email);
            if (resultadoValidacao != "")
            {
                erros.Add(resultadoValidacao);
            }

            return erros;
        }

        public string ValidarSenhaRevendedor(string senha)
        {
            if (string.IsNullOrEmpty(senha))
                return MensagensSistema.SenhaVazio;
            return "";
        }

        public string ValidarEmailVazioRevendedor(string email)
        {
            if (string.IsNullOrEmpty(email))
                return MensagensSistema.EmailVazio;
            return "";
        }

        public string ValidarEmailRevendedor(string email)
        {
            if (string.IsNullOrEmpty(email))
                return MensagensSistema.EmailValido;
            return "";
        }

        public string ValidarNomeRevendedor(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return MensagensSistema.NomeVazio;
            return "";
        }

        public string ValidarSobrenomeRevendedor(string sobrenome)
        {
            if (string.IsNullOrEmpty(sobrenome))
                return MensagensSistema.SobrenomeVazio;
            return "";
        }


        public string ValidarCPFRevendedor(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return MensagensSistema.CPFVazio;
            return "";
        }
        
        public string ValidarNomeCompletoRevendedor(string nomeCompleto)
        {
            if (string.IsNullOrEmpty(nomeCompleto))
                return MensagensSistema.NomeCompletoQuantidadeMinimaPalavras;
            if (nomeCompleto.Trim().Split(' ').Length < 2)
                return MensagensSistema.NomeCompletoQuantidadeMinimaPalavras;
            return "";
        }        
    }
}
