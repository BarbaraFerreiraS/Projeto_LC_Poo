using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoIntermediario
{
    public class Holerite
    {
        public string NomeEmpresa { get; private set; }
        public ulong CNPJ { get; private set; }
        public double Salario { get; private set; }
        public string NomeFuncionario{ get; private set; }

        public Holerite(string Empresa,ulong CNPJ,double Salario,string Func) 
        {
            this.NomeEmpresa = Empresa;
            this.CNPJ = CNPJ;
            this.Salario = Salario;
            this.NomeFuncionario= Func;

            // Console.WriteLine("Entrou no Construtor :");
            // Console.WriteLine($"Nome da Empresa:  {this.NomeEmpresa}");
            // Console.WriteLine($"CNPJ:  {this.CNPJ}");
            // Console.WriteLine($"Nome do Salario: {this.Salario}");
            // Console.WriteLine($"Nome do Funcionario :{this.NomeFuncionario}");
        }

    }
}