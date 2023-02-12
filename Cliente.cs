using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BancoIntermediario
{
    public class Cliente
    {       // Dados do Cliente
        public string NomeCliente { get; private set; }
        public ulong CPF { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string email { get; private set; }
        public ulong Telefone { get; private set; }

        public Cliente(string NomeCliente,ulong CPF,DateTime DataNascimento,string email,ulong Telefone){
            this.NomeCliente = NomeCliente;
            this.CPF = CPF;
            this.DataNascimento= DataNascimento;
            this.email = email;
            this.Telefone = Telefone;

        }
    }
}