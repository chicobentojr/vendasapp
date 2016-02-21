using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendasWPF.Models
{
    public class Venda
    {
        public int VendaId { get; set; }
        public int ClienteId { get; set; }
        public string Data { get; set; }
        public double Total1 { get; set; }
        public double Total2 { get; set; }
        public double Desconto { get; set; }
        public Cliente Cliente { get; set; }
    }
}
