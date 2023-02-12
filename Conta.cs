
using System.Runtime.CompilerServices;

namespace BancoIntermediario
{
    public abstract class Conta
    {
        public int numeroConta { get; private set; }
        private double saldo { get; set; }

        protected double taxa { get; set; }
        public string tipoConta { get; private set; }

        protected Cliente dadosClientes {get; private set; }
        public List<Movimentacao> Movimentacoes { get; set; }
        public Conta(Cliente novoCliente) //Cliente novoCliente
        {
            //this.tipoConta = tipoConta;
            this.dadosClientes = novoCliente;
            setarNumeroConta();
            Movimentacoes = new List<Movimentacao>();

        }
        private void setarNumeroConta()
        {
            Random numRandomico = new Random();
            numeroConta = numRandomico.Next(1000, 9999);
        }
        public void InformacoesdaConta(){
            Console.WriteLine($"Tipo de conta :{this.tipoConta}");
            Console.WriteLine($"Numero da Conta: {this.numeroConta}");
            Console.WriteLine($"Nome: {this.dadosClientes.NomeCliente}");
            Console.WriteLine($"Data de Nascimento: {this.dadosClientes.DataNascimento}");
        }
        public bool AcessoDeposito(double valor, string tipo="Depósito")
        {
            return this.depositar(valor, tipo);
        }
        private bool depositar(double valor, string tipo)
        {
            if (valor > 0)
            {
                saldo += valor;
                

                Movimentacao movimentacao = new Movimentacao(valor, DateTime.Now, tipo);
                
                Movimentacoes.Add(movimentacao);
                
                return true;
            }
            else
            {
                Console.WriteLine("Apenas valores positivos são aceitos");
                return false;
            }

        }

        public bool AcessoSacar(double valor, string tipo="Saque")
        {
            return this.sacar(valor, tipo);
        }
        private bool sacar(double valor, string tipo)
        {
            if(valor > 0)
            {
                if (valor > saldo)
                {
                    Console.WriteLine("Saldo insuficiente");
                    return false;
                }
                else
                {
                    double tarifa = CalcularTarifa(valor, taxa);
                    saldo -= valor + tarifa;
                    this.Movimentacoes.Add(new Movimentacao(tarifa, DateTime.Now, "Tarifa"));   
                    this.Movimentacoes.Add(new Movimentacao(valor, DateTime.Now, tipo));   

                    Console.WriteLine($"Transação realizada com sucesso!\n  Saldo atual: {Math.Round(saldo,2)}\n  Tarifa Cobrada: {Math.Round(tarifa,2)}");
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Apenas valores positivos são aceitos");
                return false;
            }
            
        }
        protected double CalcularTarifa(double valor, double taxaMetodo)
        {
            return valor * (taxaMetodo / 100);
        }

        public void MostrarSaldo() 
        {
            Console.WriteLine($"Saldo atual: {Math.Round(this.saldo,2)}");
        }
        public void ExtratoBancario()
        {
            foreach (var movimentacao in Movimentacoes)
            {
                movimentacao.MostrarExtrato();
                System.Console.WriteLine("");
            }
        }

        public void AcessoPoupanca(int numero, double valor,List<ContaPoupanca> listacontaPoupanca)
        {
            this.TransferenciaParaPoupanca(numero, valor, listacontaPoupanca);
        }
        
        private void TransferenciaParaPoupanca(int numeroContaPoupanca, double valor, List<ContaPoupanca> listacontaPoupanca)
        {   
            
            bool verificacao =false;
            foreach (ContaPoupanca cont  in listacontaPoupanca)
            {
                if(cont.numeroConta == numeroContaPoupanca && cont.tipoConta == "Conta Poupanca")
                {
                    if( this.numeroConta== numeroContaPoupanca){

                        System.Console.WriteLine("Impossivel realizar a transferencia para propria conta ");
                    }
                    else
                    {   if(this.saldo >valor){
                            this.AcessoSacar(valor, $"Transferencia para poupanca {numeroContaPoupanca} ");//tira da conta origem
                            cont.AcessoDeposito(valor,$"Deposito recebido da conta {this.numeroConta}");
                            verificacao =true;
                        }
                        else{
                            System.Console.WriteLine("ERRO!! Não foi possivel realizar a transferencia para a poupanca");
                        }
                    }
                    
                    
                }
            }
            /*if(verificacao == false){
                System.Console.WriteLine("ERRO!! Não foi possivel realizar a transferencia para a poupanca");
            }*/
             
        }

        public  void AlteraTipoConta(string nomeConta){
            this.tipoConta = nomeConta;
        }
        
    }

}

