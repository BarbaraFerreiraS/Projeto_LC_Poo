using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoIntermediario
{
    public class Movimentacao
    {
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public String Nota { get; set; }

        public Movimentacao(double valor, DateTime data, String note)
        {   
            this.Valor = valor;
            this.Data = data;
            this.Nota = note;
        }
        public void MostrarExtrato()
        {
            Console.WriteLine($"Tipo: {this.Nota}\n" + 
            $"Data da movimentacao: {Data.ToString()}\n" + 
            $"R$ {Math.Round(Valor, 2)}");
        }
    }
}