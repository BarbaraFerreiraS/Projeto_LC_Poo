using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoIntermediario
{

    public class ContaSalario: Conta
    {
        public Holerite Holerite { get; set; }

        public ContaSalario(Holerite holerite, Cliente cliente ): base(cliente)
        {
            Holerite = holerite;
            this.taxa = 0.3;
            AlteraTipoConta("Conta Salario");
        }

        public bool depositarSalario(double quantia)
        {

            ulong cnpj = Holerite.CNPJ;
            bool verificacao = base.AcessoDeposito(quantia, $"Deposito salario - CNPJ: {cnpj}");

            return verificacao;
            
            // this.saldo += quantia;
            // this.Movimentacoes.Add(new Movimentacao(quantia, DateTime.Now, "Salario"));
        }

       
    }
}