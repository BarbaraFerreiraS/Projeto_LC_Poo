using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoIntermediario
{
    public class ContaInvestimento: Conta
    {
        public string perfilInvestidor{get; private set;}
        public ContaInvestimento(string perfil, Cliente novoCliente): base(novoCliente)
        {
            this.perfilInvestidor = perfil;
            this.taxa = 0.8;
            AlteraTipoConta("Conta Investimento");
        }
        public void AcessarPerfil (string perfil ){
                this.perfilInvestidor = perfil;
                Console.WriteLine($"O seu perfil é {this.perfilInvestidor}");
        }
        public bool InvestirEmAcoes(double valorInvestido, string empresa)
        {
            bool verificacao = this.AcessoSacar(valorInvestido, $"Investimento na empresa {empresa}");

            return verificacao;
        }

        
        
    }
}




