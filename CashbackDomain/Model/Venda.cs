using System;
using System.ComponentModel.DataAnnotations;

namespace CashbackDomain.Model
{
    public class Venda
    {
        [Key]
        public int ID { get; set; }
        public int CodigoId { get; set; }
        public decimal Valor { get; set; }

        public DateTime Data { get; set; }
        public decimal Cashback { get; set; }

        public int PorcentagemCashbackAplicado { get; set; }

        public string Status { get; set; }
        public string CPF { get; set; }
        public string Username { get; set; }
    }
}
