using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Projeto_2
{
    internal class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string cpf;
            public string email;
        }

        static List<Cliente> clientes = new List<Cliente>();
        enum Menu {Listar = 1, Adicionar, Remover, Sair}
        static void Main(string[] args)
        {
            Carregar();
            bool saida = false;

            while (!saida)
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine("  SISTEMA DE CLIENTES  ");
                Console.WriteLine("-----------------------");
                Console.WriteLine(" [1] Listar Clientes\n [2] Adicionar Clientes\n [3] Remover Clientes\n [4] Sair do Sistema");
                Menu opcao = (Menu)int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case Menu.Listar:
                        Listar();
                        break;
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        saida = true;
                        break;
                }
                Console.Clear();
            }
        }
        
        static void Remover()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("  REMOVER CLIENTES  ");
            Console.WriteLine("--------------------");
            Listar();
            Console.WriteLine("Digite o ID do cliente que quer Remover: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < clientes.Count())
            {
                clientes.RemoveAt(id - 1);
                Salvar();
            }
            else
            {
                Console.WriteLine($"ID {id} digitado é inválido!");
                Console.WriteLine("Tente novamente, digitando um ID válido.");
                Console.ReadLine();
                Console.ReadLine();
            }
        }
        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("------------------------");
            Console.WriteLine("  CADASTRO DE CLIENTES  ");
            Console.WriteLine("------------------------");
            Console.Write("Nome: ");
            cliente.nome = Console.ReadLine();
            Console.Write("CPF: ");
            cliente.cpf = Console.ReadLine();
            Console.Write("Email: ");
            cliente.email = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro realizado com sucesso!");
            Console.WriteLine("Aperte ENTER para sair");
            Console.ReadLine();
            
        }
        static void Listar()
        {
            if (clientes.Count > 0)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("  LISTAGEM DE CADASTROS  ");
                Console.WriteLine("-------------------------");
                int indice = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {indice + 1}");
                    Console.WriteLine($" Nome: {cliente.nome}\n CPF: {cliente.cpf}\n Email: {cliente.email}");
                    Console.WriteLine("=========================");
                    indice++;
                }
            }
            else
            {
                Console.WriteLine("Ainda não temos nenhum cliente cadastrado.");
            }
            
            Console.WriteLine("Aperte ENTER para sair");
            Console.ReadLine();
        }
        static void Salvar()
        {
            FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);

            stream.Close();
        }
        static void Carregar()
        {
            FileStream stream = new FileStream("clientes.dat", FileMode.OpenOrCreate);
            try
            {
                
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);

                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }
            }
            catch(Exception e)
            {
                clientes = new List<Cliente>();
            }

            stream.Close();
        }
    }
}
