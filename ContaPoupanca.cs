using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoIntermediario
{
    public class ContaPoupanca: Conta
    {
        public ContaPoupanca(double ValorMinimo, Cliente novoCliente): base(novoCliente)
        {
            AlteraTipoConta("Conta Poupanca");
            this.taxa = 0.35;
            this.AcessoDeposito(ValorMinimo);
            
        }
    }
}