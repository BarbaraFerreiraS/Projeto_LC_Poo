// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using BancoIntermediario;

public class Program{
    static void Main(string[] args)
    {

        string perfil;
        string nomeConta;
        int numeroAcessoConta;
        int condicao = 0;
        int entrarConta;
        List<Conta> listaContas = new List<Conta>();
        List<ContaPoupanca> listapoupanca = new List<ContaPoupanca>();
        List<ContaSalario> listaSalario = new List<ContaSalario>();
        List<ContaInvestimento> listaInvestimento = new List<ContaInvestimento>();
        
        do 
        {
            do
            {
                Console.Clear();
                System.Console.WriteLine("Olá, seja bem-vindo(a)!");
                System.Console.WriteLine("Escolha uma opcao :\n[1] - Criar uma conta\n[2] - Entrar na conta\n[3] - Sair");
                entrarConta = LerInteiro();
                if(entrarConta!=1 && entrarConta!=2 && entrarConta!=3){
                    System.Console.WriteLine("ERRO!Voce digitou um numero diferente das opcoes\nPressione qualquer tecla para prosseguir");
                    Console.ReadKey();
                }
            }while(entrarConta!=1 && entrarConta!=2 && entrarConta!=3);
            if(entrarConta==1){
                Cliente novoCliente = cadastroCliente();
                Console.WriteLine(CalculaIdade(novoCliente.DataNascimento));
                bool repetir = false;
                do {
                    Console.Clear();
                    repetir = false;


                    System.Console.WriteLine("Selecione o tipo de conta que deseja criar:\n[1] - Poupanca\n[2] - Salário\n[3] - Investimento\n[4] - Sair\n[5] - Voltar ao Menu Inicial");
                    condicao = LerInteiro();
                    
                    Console.Clear();
                    switch (condicao) {
                        case 1:
                            Console.WriteLine("Saldo mínimo para criação da Conta Poupança = R$100,00 ");
                            Console.Write("Digite o valor do depósito inicial: ");
                            double valor = LerDouble();
                            if (valor >= 100) 
                            {
                                
                                ContaPoupanca contaPoupanca = new ContaPoupanca(valor, novoCliente);
                            
                                listapoupanca.Add(contaPoupanca);
                                Console.WriteLine("Conta criada com sucesso!");
                                Menu(contaPoupanca,listapoupanca);
                                repetir = false;
                                
                            }
                            else 
                            {
                                Console.WriteLine("Saldo insuficiente para criação da Conta Poupança");
                                repetir = true;
                            }

                        break;

                        case 2: // Conta Salario
                            Holerite holerite = CadastrarHolerite(novoCliente);
                            
                            ContaSalario contaSalario = new ContaSalario(holerite, novoCliente);
                            
                            listaSalario.Add(contaSalario);
                            Menu(contaSalario, listapoupanca);
                            Console.ReadKey();
                            repetir = false;

                        break;

                        case 3:
                            if (CalculaIdade(novoCliente.DataNascimento) >= 18) {
                                perfil = PerfildoInvestidor();
                                
                                ContaInvestimento contaInvestimento =new(perfil,novoCliente);
                                
                                listaInvestimento.Add(contaInvestimento);
                                Menu(contaInvestimento, listapoupanca);
                                repetir = false;
                            } else {
                                Console.WriteLine("Conta Investimento é permitida apenas para maiores de 18 anos");
                                Console.ReadKey();
                                repetir = true;
                            }
                        break;
                        case 4 : // sair 
                            System.Console.WriteLine("Saindo...");
                            Thread.Sleep(2000);
                            condicao=-1;
                        break;
                        case 5: 
                            System.Console.WriteLine("Voltando...");
                            Thread.Sleep(2000);
                            repetir =false;
                        break;
                        default:
                            System.Console.WriteLine("Opcao Invalida,selecione somente o numero das opcões");
                            System.Console.WriteLine("Pressione qualquer tecla para continuar");
                            Console.ReadKey();
                            repetir =true;
                        break;
                    }
                    Console.Clear();
                } while (repetir);
            }
            else if(entrarConta==2)
            {
                int selecioneconta ;
                            do{
                                Console.Clear();
                                System.Console.WriteLine("Selecione o tipo de conta\n[1] - Poupanca\n[2] - Salário\n[3] - Investimento\n[4] - Voltar ao menu inicial");
                                
                                selecioneconta = LerInteiro();
                                if(selecioneconta<1 || selecioneconta>4){
                                    System.Console.WriteLine("Opcao Invalida,selecione somente o numero das opcões!");
                                    System.Console.WriteLine("Pressione qualquer tecla para continuar");
                                    Console.ReadKey();
                                }
                            }while(selecioneconta<1 || selecioneconta>4);
                            if(selecioneconta==4){
                                System.Console.WriteLine("Voltando...");
                                Thread.Sleep(2000);

                            }
                            else
                            {
                                System.Console.Write("Digite o numero da conta: ");
                                numeroAcessoConta = LerInteiro();
                                BuscaConta(numeroAcessoConta,selecioneconta,listapoupanca,listaSalario,listaInvestimento);
                            }
                            
            }
            else
            {   System.Console.WriteLine("Saindo...");
                Thread.Sleep(2000);

                condicao=-1;
            }
        } while(condicao != -1);
        
    }

    public static Cliente cadastroCliente() {
        
        ulong CPF;
        string nome;
        string email;
        ulong telefone;
        DateTime dataNasc = new();
        
        Console.WriteLine("Para criarmos sua conta serão necessários alguns dados:\n");
        
        
        do{
            Console.Write("Digite seu nome completo: ");
            nome = Console.ReadLine().ToUpper();
            if(nome==""){
                System.Console.WriteLine("Não reconhecemos o nome");
            }
        }while(nome=="");
        Console.Write("Digite seu CPF: ");
        CPF = LerLong();

        dataNasc = CriarDataNascimento();
        do{
            Console.Write("Digite seu e-mail: ");
            email = Console.ReadLine();
            if(email==""){
                    System.Console.WriteLine("Não reconhecemos o nome");
                }
        }while(email =="");
        Console.Write("Digite seu telefone: ");
        telefone = LerLong();

        return new Cliente(nome, CPF, dataNasc, email, telefone);
    }

    public static ulong LerLong()
    {
        bool verificacao;
        ulong num;
        do
        {
            verificacao = ulong.TryParse(Console.ReadLine(), out num);
        } while (!verificacao);

        return num;
    }
    public static int LerInteiro() 
    {
        bool verificacao;
        int num;
        do {
            verificacao = int.TryParse(Console.ReadLine(), out num);
        } while (!verificacao);

        return num;
    }
    public static double LerDouble() {
        bool verificacao;
        double num;
        do {
            verificacao = double.TryParse(Console.ReadLine(), out num);
        } while (!verificacao);

        return num;
    }
    public static int PesoPerfil(){
        int questao1;
        do{
            Console.Write("Digite sua opcao:");
            questao1 = LerInteiro();
            if(questao1>3 || questao1 <1){
                Console.WriteLine("Erro!Digite um numero dentro das opcões");
                                    
            }
            
        }while(questao1 > 3 || questao1 < 1);
        switch (questao1){
            case 1:
                return 1;
            break;
            case 2:
                return questao1 * 2;
            break;
            case 3:
                return questao1 * 3;
            break;

        }
        return 0;
        
    }
    public static string PerfildoInvestidor(){
            int soma=0;
            string perfil = "";
            //Questao 1
            Console.Clear();
            Console.WriteLine("========== Perfil do Investidor ==========");
            Console.WriteLine("1 - Como você classificaria sua aversão a risco?");
            Console.WriteLine("[1]- Alta");
            Console.WriteLine("[2]- Media");
            Console.WriteLine("[3]- Baixa");
            soma += PesoPerfil();
            //Questao 2
            Console.Clear();
            Console.WriteLine("2 - Dentre as opções, quais voce tem mais conhecimento ?");
            Console.WriteLine("[1]- Tesouro Direto, Renda Fixa,Fundos DI,Fundos de Renda fixa");
            Console.WriteLine("[2]- Fundos imobiliarios, Fundos Multimercados,Debentures,Fundos de acões");
            Console.WriteLine("[3]- Ações da Bolsa, Derivativos,COE,Criptomoedas");
            soma += PesoPerfil();
            //Questao 3
            Console.Clear();
            Console.WriteLine("3 - Quais são seus objetivos como investidor? ");
            Console.WriteLine("[1]- Proteger seu patrimonio");
            Console.WriteLine("[2]- Rentabilidade Moderada");
            Console.WriteLine("[3]- Alta Rentabilidade");
            soma += PesoPerfil();
            //Questao 4 
            Console.Clear();
            Console.WriteLine("4 - Qual é a sua Renda mensal? ");
            Console.WriteLine("[1]- Menos que R$5.000");
            Console.WriteLine("[2]- Entre R$ 5000 a 10000");
            Console.WriteLine("[3]-Mais que R$10000");
            soma+=PesoPerfil();
            //Questao 5 
            Console.Clear();
            Console.WriteLine("5 - Qual percentual da sua renda mensal voce investe regularmente?");
            Console.WriteLine("[1]- Menos que 20%");
            Console.WriteLine("[2]- Entre R$ 20% e 50%");
            Console.WriteLine("[3]-Mais que 50%");
            soma+=PesoPerfil();
            Console.Clear();
            if(soma>34)
            {
                perfil = "Agressivo";
                
            }
            else if(soma>14)
            {
                perfil = "Moderado";
                
            }
            else{
                perfil = "Conservador";
                
            }
            Console.WriteLine($"Seu perfil de {perfil} ");
            Console.WriteLine("Pressione qualquer tecla para continuar");
            Console.ReadKey();
            Console.Clear();
            return perfil;
    }
    public static Holerite CadastrarHolerite(Cliente cliente) 
    {
        
        ulong  CNPJ;
        string nomeEmpresa;
        double salario;

        Console.WriteLine("Para criarmos sua conta serão necessários alguns dados:\n");

        Console.Write("Digite o nome da empresa: ");
        nomeEmpresa = Console.ReadLine().ToUpper();

        Console.Write("Digite o CNPJ: ");
        CNPJ = LerLong();


        do {
            Console.WriteLine("Digite o salário: ");
            salario = LerDouble();
            if(salario < 0) 
            {
                Console.WriteLine("Informe valores positivos");
            }

        } while (salario < 0);

      

        return new Holerite(nomeEmpresa, CNPJ, salario, cliente.NomeCliente);

    }

    public static DateTime CriarDataNascimento()     
    {
        int dia, mes, ano;
        bool verificacao = true;
        
        do 
        {
            verificacao =true;
            Console.Write("Digite o dia do seu nascimento: ");
            dia = LerInteiro();

            Console.Write("Digite o mês do seu nascimento: ");
            mes = LerInteiro();

            Console.Write("Digite o ano do seu nascimento: ");
            ano = LerInteiro();
            if (mes < 13 && mes > 0) 
            { // Mes Valido

                if (DateTime.IsLeapYear(ano)) // Se bissexto
                {
                    if (mes == 2 && (dia > 29 || dia < 1))
                    {
                        verificacao = false;
                    }
                } 
                else 
                {
                    if (mes == 2 && (dia > 28 || dia < 1))
                    {
                        verificacao = false;
                    }
                }

                if (mes == 4 || mes == 6 || mes == 9 || mes == 11) // Mes com 30 dias
                {
                    if (dia > 30 || dia < 1)
                    {
                        verificacao = false;
                    }
                } 
               
                else if(mes == 1 || mes == 3 || mes == 5 || mes == 7 || mes == 8 || mes == 10 || mes == 12)// Mes com 31 dias
                {
                    if (dia > 31 || dia < 1)
                    {
                        verificacao = false;
                    }
                }

            }
            else 
            {
                verificacao = false;
            }

        } while (!verificacao) ;

        return new DateTime(ano, mes, dia);
    }
    public static int CalculaIdade(DateTime data) {
        // Carrega a data do dia para comparação caso data informada seja nula

        DateTime dataAtual = DateTime.Now;
      
        int idade = (dataAtual.Year - data.Year);

        if (dataAtual.Month < data.Month || (dataAtual.Month == data.Month && dataAtual.Day < data.Day)) {
            idade--;
        }

        return idade;
        
    }
    public static void Menu(ContaPoupanca contaPoupanca, List<ContaPoupanca>listaContaPoupanca) 
    {

        
        Console.WriteLine($"Seu número de conta é: {contaPoupanca.numeroConta}");
        Console.WriteLine("Digite qualquer tecla para prosseguir");
        Console.ReadKey();
        int opcao;

        do {
            Console.Clear();
            Console.Clear();
            Console.WriteLine("===== CONTA POUPANCA =====");
            Console.WriteLine("[1] - Saldo");
            Console.WriteLine("[2] - Depositar");
            Console.WriteLine("[3] - Sacar");
            Console.WriteLine("[4] - Extrato");
            Console.WriteLine("[5] - Informações da Conta");
            Console.WriteLine("[6] - Transferencia para poupanca");
            Console.WriteLine("[7] - Sair da conta");
            

            opcao = LerInteiro();
            
            switch (opcao) 
            {
                case 1:
                    Console.WriteLine("===== SALDO =====");
                    contaPoupanca.MostrarSaldo();
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                break;

                case 2: // Deposito
                    Console.WriteLine("===== DEPÓSITO =====");
                    Console.WriteLine("Digite o valor do depósito: ");
                    double valorDeposito = LerDouble();
                    contaPoupanca.AcessoDeposito(valorDeposito);
                    
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();

                    break;
                case 3: // Saque
                    Console.WriteLine("===== SAQUE =====");
                    Console.WriteLine("Digite o valor do saque: ");
                    double valorSaque = LerDouble();
                    contaPoupanca.AcessoSacar(valorSaque);
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                    break;
                case 4: // Extrato
                    Console.WriteLine("===== EXTRATO BANCÁRIO =====");
                    contaPoupanca.ExtratoBancario();
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                    break;
                case 5:
                    Console.WriteLine("===== INFORMAÇÕES DA CONTA =====");
                    contaPoupanca.InformacoesdaConta();
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                    break;
                case 6: 
                    System.Console.WriteLine("Digite o valor da transferencia:");
                    valorDeposito = LerDouble();
                    System.Console.WriteLine("Numero da Conta:");
                     int numConta = LerInteiro();
                    contaPoupanca.AcessoPoupanca(numConta,valorDeposito,listaContaPoupanca) ;

                    System.Console.WriteLine("Pressione qualquer tecla para continuar!");
                    Console.ReadKey();
                    break;

                case 7:
                    Console.WriteLine("Saindo da conta. Volte sempre!");
                    break;
                default:
                    Console.WriteLine("Valor inválido!!");
                    Thread.Sleep(2000);

                    break;
            }
        }while(opcao != 7 );
    }
    public static void Menu(ContaSalario contaSalario,  List<ContaPoupanca> listaContaPoupanca) {

        Console.WriteLine("Conta criada com sucesso!");
        Console.WriteLine($"Seu número de conta é: {contaSalario.numeroConta}");
        Console.WriteLine("pressione qualquer tecla para continuar...");
        Console.ReadKey();
        int opcao;
        int numConta;
        do {
            Console.Clear();
            Console.WriteLine("===== CONTA SALÁRIO =====");
            Console.WriteLine("[1] - Saldo");
            Console.WriteLine("[2] - Depositar");
            Console.WriteLine("[3] - Sacar");
            Console.WriteLine("[4] - Extrato");
            Console.WriteLine("[5] - Informações da Conta");
            Console.WriteLine("[6] - Transferir Poupanca");
            Console.WriteLine("[7] - Sair da conta");
            opcao = LerInteiro();
            
            switch (opcao) {
                case 1:
                    Console.WriteLine("===== SALDO =====");
                    contaSalario.MostrarSaldo();
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                    break;

                case 2: // Deposito
                    Console.WriteLine("===== DEPÓSITO =====");
                    Console.WriteLine("Digite o valor do depósito: ");
                    double valorDeposito = LerDouble();
                    contaSalario.AcessoDeposito(valorDeposito);
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                    break;
                case 3: // Saque
                    Console.WriteLine("===== SAQUE =====");
                    Console.WriteLine("Digite o valor do saque: ");
                    double valorSaque = LerDouble();
                    contaSalario.AcessoSacar(valorSaque);
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                    break;
                case 4: // Extrato
                    Console.WriteLine("===== EXTRATO BANCÁRIO =====");
                    contaSalario.ExtratoBancario();
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                    break;
                case 5:
                    Console.WriteLine("===== INFORMAÇÕES DA CONTA =====");
                    contaSalario.InformacoesdaConta();
                    Console.WriteLine($"Nome da Empresa:{contaSalario.Holerite.NomeEmpresa},CNPJ:{contaSalario.Holerite.CNPJ}");
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                     
                     
                    Console.ReadKey();
                    break;
                case 6:
                    System.Console.WriteLine("Digite o valor da transferencia:");
                    valorDeposito = LerDouble();
                    System.Console.WriteLine("Numero da Conta:");
                    numConta = LerInteiro();
                    contaSalario.AcessoPoupanca(numConta,valorDeposito,listaContaPoupanca) ;

                    System.Console.WriteLine("Pressione qualquer tecla para continuar!");
                    Console.ReadKey();
                    break;
                case 7:
                    Console.WriteLine("Saindo da conta. Volte sempre!");
                    break;
                default:
                    Console.WriteLine("Valor inválido!!");
                    Thread.Sleep(2000);

                    break;
            }
        } while (opcao != 7);
    }
    //Conta Investimento 
    public static void Menu(ContaInvestimento contaInvestimento, List<ContaPoupanca> listaContaPoupanca) 
    {

        Console.WriteLine("Conta criada com sucesso!");
        Console.WriteLine($"Seu número de conta é: {contaInvestimento.numeroConta}");
        Console.WriteLine("pressione qualquer tecla para continuar...");
        Console.ReadKey();
        string perfil;
        int opcao;
        string empresa;
        double quantiaInv;
        bool veri;
        double valorPoupanca;
        int numConta;
        do {
            
            Console.Clear();
            Console.WriteLine("===== CONTA INVESTIMENTO =====");
            Console.WriteLine("[1] - Saldo");
            Console.WriteLine("[2] - Depositar");
            Console.WriteLine("[3] - Sacar");
            Console.WriteLine("[4] - Extrato");
            Console.WriteLine("[5] - Informações da Conta");
            Console.WriteLine("[6] - Investir em ações");
            Console.WriteLine("[7] - Consultando o perfil do investidor");
            Console.WriteLine("[8] - Refazer perfil do investidor");
            Console.WriteLine("[9] - Transferencia para poupanca");
            Console.WriteLine("[10] - Sair");

            opcao = LerInteiro();
            
            switch (opcao) 
            {
                case 1:
                    Console.WriteLine("===== SALDO =====");
                    contaInvestimento.MostrarSaldo();
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                break;

                case 2: // Deposito
                    Console.WriteLine("===== DEPÓSITO =====");
                    Console.WriteLine("Digite o valor do depósito: ");
                    double valorDeposito = LerDouble();
                    contaInvestimento.AcessoDeposito(valorDeposito);
                    
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();

                    break;
                case 3: // Saque
                    Console.WriteLine("===== SAQUE =====");
                    Console.WriteLine("Digite o valor do saque: ");
                    double valorSaque = LerDouble();
                    contaInvestimento.AcessoSacar(valorSaque);
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                    break;
                case 4: // Extrato
                    Console.WriteLine("===== EXTRATO BANCÁRIO =====");
                    contaInvestimento.ExtratoBancario();
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                    break;
                case 5:
                    Console.WriteLine("===== INFORMAÇÕES DA CONTA =====");
                    contaInvestimento.InformacoesdaConta();
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                    
                    break;
                case 6:
                    Console.WriteLine("===== Investir em ações  =====");
                    do{
                        Console.WriteLine("Digite a sigla da empresa:");
                        
                        empresa = Console.ReadLine();
                        if(empresa==""){
                            Console.WriteLine("Campo 'sigla da empresa' esta vazio ");
                        }
                    }while(empresa=="");
                    Console.WriteLine("Digite a quantia que deseja investir");
                    quantiaInv = LerDouble();
                    veri = contaInvestimento.InvestirEmAcoes(quantiaInv,empresa);
                    System.Console.WriteLine("Pressione qualqer tecla para prosseguir!!");
                    Console.ReadKey();
                    break;
                case 7:
                    Console.WriteLine("===== Consultando o perfil do Investidor  =====");
                    Console.WriteLine($"Seu Perfil do Investidor é {contaInvestimento.perfilInvestidor}");
                    Console.WriteLine("Pressione qualqer tecla para prosseguir!!");
                    Console.ReadKey();
                    
                    break;
                case 8://Refazer perfil do Investidor
                    Console.WriteLine("===== Perfil do Investidor  =====");
                    perfil = PerfildoInvestidor();
                    contaInvestimento.AcessarPerfil(perfil);
                    
                    break;
                case 9://Transferencia Conta poupanca
                    System.Console.WriteLine("Digite o valor da transferencia:");
                    valorDeposito = int.Parse(Console.ReadLine());
                    System.Console.WriteLine("Numero da Conta:");
                    numConta = int.Parse(Console.ReadLine());
                    contaInvestimento.AcessoPoupanca(numConta,valorDeposito,listaContaPoupanca) ;

                    System.Console.WriteLine("Pressione qualquer tecla para continuar!");
                    Console.ReadKey();
                   
                    
                    //listaInvest.Add(contaInvestimento);
                    
                    break;
                case 10:
                    Console.WriteLine("Saindo da conta. Volte sempre!");
                    //listaInvest.Add(contaInvestimento);
                    
                    break;

                default:
                    Console.WriteLine("Valor inválido!!");
                    Thread.Sleep(2000);

                    break;
            }
        }while(opcao != 10 );
    }
    

    public static void BuscaConta(int numero,int opcao, List<ContaPoupanca>listaContaPoupanca,List<ContaSalario>listaSalario,List<ContaInvestimento> listaInvestimento)
    {       
            bool verificacao = false;
            string nomeConta = "";
            switch (opcao)
            {
                case 1 :
                    nomeConta = "Conta Poupanca";
                    foreach (ContaPoupanca conta in listaContaPoupanca )
                    {
                        if(conta.numeroConta== numero && nomeConta== conta.tipoConta )
                        {
                                Menu(conta,listaContaPoupanca);
                                verificacao =true;
                        }
                    }
                break;
                case 2:
                    nomeConta = "Conta Salario";
                    foreach (ContaSalario conta1 in listaSalario )
                    {
                        if(conta1.numeroConta== numero && nomeConta== conta1.tipoConta )
                        {
                                Menu(conta1,listaContaPoupanca);
                                verificacao =true;
                        }
                    }
                    break;
                case 3: 
                    nomeConta = "Conta Investimento";
                    foreach (ContaInvestimento conta2 in listaInvestimento )
                    {
                        if(conta2.numeroConta== numero && nomeConta== conta2.tipoConta )
                        {
                                Menu(conta2,listaContaPoupanca);
                                verificacao =true;
                        }
                    }
                break;
            }
            if(!verificacao)
            {
                Console.WriteLine("ERRO! Numero da Conta Inexistente!");
                System.Console.WriteLine("Pressione qualquer tecla para prosseguir");
                Console.ReadKey();
            }
            
    }


}


    

// Somos uma consultoria que presta serviços para o maior Banco da América Latina.
// Precisamos que vocês considerem o seguinte cenário:

// Eu como Product Manager.
// Desejo que, seja criado uma aplicação para
//     que nossos clientes bancários consigam criar suas contas através do celular.

// Nossos DoD(definition of done) serão:
// - Propiedades obrigatória: numeroConta, Saldo, tipoConta. ok
// - Os dados do cliente, poderá ser avaliado pelo time.      ok
// - Deverá ser considerado três tipos de conta: Poupança, Salário e Investimento  ok

//     - Ao criar a conta poupança deverá solicitar um saldo míninmo ok
//     - Ao criar a conta salário, deverá solicitar o holerite (simule um holerite)  ok
//     - Ao criar a conta investimento, deverá avaliar o perfil do investidor (simule um método que avalie o perfil do investidor)  ok

//     Dica: estamos solicitando essas validações ao criar, considere usar o construtor nesses cenários.  ok

// - Deverá ser possível visualizar o saldo da conta em todos os tipos de conta,   ok 
//   mas somente um tipo de Conta poderá modificar o saldo.  ok
//     Dica: avalie o uso do protected.

// - Implementar as seguintes funções:
//     - Depositar, Sacar    ok
//     - InvestirEmAcoes     ok
//     - TransferenciaParaPoupanca   ok
//     - DepositarSalario (o depositar salário deverá solicitar o CNPJ pagador)   ok
//     - ExtratoBancario ( o extrato bancaria deverá contemplar as movimentacoes da conta, independente do tipoConta)   ok

//     Dica: avalie quais deverão ser método da classe pai e quais deverão ser método da classe filha.


// - Implementar um método CalcularValorTarifaManutencao onde:  ok     
//     - Para a contaCliente: taxa de 0.3   nao existe
//     - Para a contaInvestimento: taxa de 0.8
//     - Para a contaPoupanca: taxa de 0.3,5
//     - Para a contaSalario: taxa de 0.3

//     Dica: utilize o polimorfismo para essas alterações.
//     Crie o diagrama de classe que represente o seguinte caso de uso:   

