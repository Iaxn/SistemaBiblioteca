using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace sistemaBiblioteca
{
    class Program
    {
        public class Aluguel
        {
            public string NomeLivro { get; set; }
            public string NomePessoa { get; set; }
            public decimal Preco { get; set; }
            public int Dias { get; set; }
            public DateTime DataAquisicao { get; set; }

            public override string ToString()
            {
                DateTime dataDevolucao = DataAquisicao.AddDays(Dias);
                return $"Livro: {NomeLivro}, Alugado por: {NomePessoa}, Preço: R$ {Preco}, Por: {Dias} dias, Data de aquisição: {DataAquisicao.ToShortDateString()}, Data de devolução: {dataDevolucao.ToShortDateString()}.";
            }

            public string ToCsv()
            {
                return $"{NomeLivro};{NomePessoa};{Preco};{Dias};{DataAquisicao.ToShortDateString()}";
            }

            public static Aluguel FromCsv(string csvLine)
            {
                var values = csvLine.Split(';');
                if (values.Length < 5)
                {
                    throw new FormatException("A linha CSV não contém o número correto de campos.");
                }

                return new Aluguel
                {
                    NomeLivro = values[0],
                    NomePessoa = values[1],
                    Preco = decimal.Parse(values[2]),
                    Dias = int.Parse(values[3]),
                    DataAquisicao = DateTime.Parse(values[4])
                };
            }
        }

        static void Main()
        {
            List<Aluguel> alugueis = LoadAlugueis();

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("===================================================");
                Console.WriteLine("Sistema de cadastro bibliotecário");
                Console.WriteLine("===================================================");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("Menu:");
                Console.WriteLine("1 - Cadastrar Cliente");
                Console.WriteLine("2 - Revisar Clientes");
                Console.WriteLine("3 - Sair do programa");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarCliente(alugueis);
                        break;
                    case "2":
                        RevisarClientes(alugueis);
                        break;
                    case "3":
                        SaveAlugueis(alugueis);
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("========================");
                        Console.WriteLine("Saindo do programa...");
                        Console.WriteLine("========================");
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    default:
                        Console.WriteLine("Opção inválida! Pressione qualquer tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }

            } while (true);
        }

        static void CadastrarCliente(List<Aluguel> alugueis)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("===================================================");
            Console.WriteLine("Cadastro de cliente");
            Console.WriteLine("===================================================");
            Console.ForegroundColor = ConsoleColor.White;

            Aluguel aluguel = new Aluguel();
            Console.Write("Digite o nome do livro: ");
            aluguel.NomeLivro = Console.ReadLine();

            Console.Write("Digite o nome do locatário: ");
            aluguel.NomePessoa = Console.ReadLine();

            Console.Write("Digite o preço do aluguel: ");
            while (true)
            {
                string inputPreco = Console.ReadLine();
                if (decimal.TryParse(inputPreco, out decimal preco))
                {
                    aluguel.Preco = preco;
                    break;
                }
                else
                {
                    Console.Write("Valor inválido! Digite o preço do aluguel: ");
                }
            }

            Console.Write("Digite quanto tempo o locatário ficará com o livro (em dias): ");
            while (true)
            {
                string inputDias = Console.ReadLine();
                if (int.TryParse(inputDias, out int dias) && dias > 0)
                {
                    aluguel.Dias = dias;
                    break;
                }
                else
                {
                    Console.Write("Valor inválido! Digite a quantidade de dias: ");
                }
            }

            aluguel.DataAquisicao = DateTime.Now;

            alugueis.Add(aluguel);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("===================================================");
            Console.WriteLine("Aluguel cadastrado com sucesso! Pressione qualquer tecla para continuar.");
            Console.WriteLine("===================================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }

        static void RevisarClientes(List<Aluguel> alugueis)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("===================================================");
            Console.WriteLine("Alugueis registrados");
            Console.WriteLine("===================================================");
            Console.ForegroundColor = ConsoleColor.White;

            foreach (var a in alugueis)
            {
                Console.WriteLine(a);
            }

            Console.WriteLine("\nDigite o nome do cliente que deseja remover (ou 'sair' para voltar): ");
            var nomeParaRemover = Console.ReadLine();

            if (nomeParaRemover.ToLower() != "sair")
            {
                var aluguelARemover = alugueis.FirstOrDefault(a => a.NomePessoa.Equals(nomeParaRemover, StringComparison.OrdinalIgnoreCase));
                if (aluguelARemover != null)
                {
                    alugueis.Remove(aluguelARemover);

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("===================================================");
                    Console.WriteLine("Aluguel Removido com sucesso!");
                    Console.WriteLine("===================================================");
                    Console.ForegroundColor = ConsoleColor.White;

                }
                else
                {
                    Console.WriteLine("Cliente não encontrado.");
                }
            }

            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            Console.ReadKey();
        }

        static void SaveAlugueis(List<Aluguel> alugueis)
        {
            using (StreamWriter sw = new StreamWriter("alugueis.txt")) 
            {
                foreach (var aluguel in alugueis)
                {
                    sw.WriteLine(aluguel.ToCsv());
                }
            }
        }

        static List<Aluguel> LoadAlugueis()
        {
            var alugueis = new List<Aluguel>();
            if (File.Exists("alugueis.txt"))
            {
                var lines = File.ReadAllLines("alugueis.txt");
                foreach (var line in lines)
                {
                    try
                    {
                        alugueis.Add(Aluguel.FromCsv(line));
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Erro ao processar a linha: '{line}'. {ex.Message}");
                    }
                }
            }
            return alugueis;
        }
    }
}