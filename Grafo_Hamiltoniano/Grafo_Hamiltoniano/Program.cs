using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Grafo_Hamiltoniano
{
    class Program
    {
        //Método que carrega o txt para a matriz
        static int[,] Carrega_Matriz(string nome, int l, int c)
        {
            StreamReader LeArquivo = new StreamReader(nome);

            string linha = " ";

            int[,] Matriz = new int[l, c];

            int cont = 0;

            while ((linha = LeArquivo.ReadLine()) != null)
            {
                char[] array = linha.ToCharArray();

                for (int i = 0; i < array.Length; i++)
                {
                    Matriz[cont, i] = int.Parse(Convert.ToString(array[i]));
                }
                cont++;
            }
            return Matriz;
        }

        //Método que imprime a matriz
        static void Imprime_Matriz(int[,] M, int lh, int cl)
        {
            Console.Write("\n");
            for (int a = 0; a < lh; a++)
            {
                for (int b = 0; b < cl; b++)
                {
                    Console.Write(M[a, b] + " ");
                }
                Console.Write("\n");
            }
        }

        //Método que testa se o grafo é completo
        static void Grafo_Completo(int[,] M, int lh, int cl)
        {
            bool aux = true;

            for (int a = 0; a < lh; a++)
            {
                for (int b = 0; b < cl; b++)
                {
                    if (a == b)
                    {
                        if (M[a, b] != 0)
                            aux = false;
                    }
                    else
                    {
                        if (a != b && M[a, b] == 0)
                        {
                            aux = false;
                        }
                    }
                }
            }

            if (aux != false)
            {
                Console.Write("\n\nO Grafo é Completo");
            }
            else
            {
                Console.Write("\n\nO Grafo Nao é Completo");
            }
        }

        //Método que testa se o grafo é cíclico
        static void Grafo_Ciclico(int[,] M, int lh, int cl)
        {
            bool aux = true;
            int contador = 0;

            for (int a = 0; a < lh; a++)
            {
                for (int b = 0; b < cl; b++)
                {
                    if (M[a, b] == 1)
                    {
                        contador++;
                    }
                }

                if (contador != 2)
                {
                    aux = false;
                }

                contador = 0;
            }

            if (aux == true)
            {
                Console.Write("\n\nO Grafo é Cíclico");
            }
            else
            {
                Console.Write("\n\nO Grafo Nao é Cíclico");
            }
        }

        //Método que testa o teorema 01 - Teorema de Dirac 
        //Seja G um grafo simples com n vértices (n>=3). 
        //Se o grau de todo vértice for maior ou igual a n/2, então G é hamiltoniano
        static void Teorema_1(int[,] M, int lh, int cl)
        {
            bool aux = true;
            int n = 0;

            double valor;
            valor = lh / 2.0;

            //Verificando se o grafo simples com n vértices (n >= 3)            
            if (lh >= 3)
            {
                for (int a = 0; a < lh; a++)
                {
                    for (int b = 0; b < cl; b++)
                    {
                        if (M[a, b] == 1)
                        {
                            n++;
                        }
                    }

                    //Fazendo comparação se o grau de todo vértice for maior ou igual a n/2, para G ser hamiltoniano
                    if (n < valor)
                    {
                        aux = false;
                        n = 0;
                    }

                    //Restaurando variável com valor original
                    n = 0;
                }

                if (aux != false)
                {
                    Console.Write("\n\nO Grafo Antende o Teorema 01 (Teorema de Dirac)");
                }
                else
                {
                    Console.Write("\n\nO Grafo Nao Antende o Teorema 01 (Teorema de Dirac)");
                }
            }
            else
            {
                Console.Write("\n\nO Grafo Nao Antende o Teorema 01 (Teorema de Dirac)");
            }
        }

        //Método que testa o teorema 02 - Teorema de Ore
        //Seja G um grafo simples com n vértices (n>=3). 
        //Se para todo par de vértices não adjacentes v e w, a soma de seus graus for maior ou igual a n, então G é hamiltoniano
        static void Teorema_2(int[,] M, int lh, int cl)
        {
            bool aux = true;

            int quantidade = 0;

            int[] vetor = new int[lh];


            //Verificando se o grafo simples com n vértices (n >= 3)            
            if (lh >= 3)
            {

                //1° varredura para colocar o grau de cada vértice em uma posição do vetor
                for (int a1 = 0; a1 < lh; a1++)
                {
                    for (int b1 = 0; b1 < cl; b1++)
                    {
                        if (M[a1, b1] == 1)
                        {
                            quantidade++;
                        }
                    }

                    vetor[a1] = quantidade;

                    //Restaurando variável com valor original
                    quantidade = 0;
                }

                //2° Varredura para verificar se para todo par de vértices não adjacentes, a soma de seus graus for maior ou igual a n
                for (int a2 = 0; a2 < lh; a2++)
                {
                    for (int b2 = 0; b2 < cl; b2++)
                    {
                        if (a2 != b2)
                        {
                            if (M[a2, b2] == 0)
                            {
                                if ((vetor[a2] + vetor[b2]) < lh)
                                {
                                    aux = false;
                                }
                            }
                        }
                    }
                }


                if (aux == true)
                {
                    Console.Write("\n\nO Grafo Antende o Teorema 02 (Teorema de Ore)");
                }
                else
                {
                    Console.Write("\n\nO Grafo Nao antende o Teorema 02 (Teorema de Ore)");
                }
            }
            else
            {
                Console.Write("\n\nO Grafo Nao antende o Teorema 02 (Teorema de Ore)");
            }
        }


        static void Main(string[] args)
        {
            string nomearquivo;

            Console.Write("\t\t\t   *** GRAFOS HAMILTONIANOS ***");

            Console.Write("\n\nDigite o Caminho do Arquivo: ");
            nomearquivo = Console.ReadLine();

            //Contar as quantidade de linhas e jogar valor na matriz
            StreamReader R = new StreamReader(nomearquivo);

            int contador = 0;

            while (R.ReadLine() != null)
            {
                contador++;
            }

            R.Close();

            int[,] T = new int[contador, contador];


            //Verificando se o arquivo existe!

            if (File.Exists(nomearquivo))
            {
                T = Carrega_Matriz(nomearquivo, contador, contador);

                Imprime_Matriz(T, contador, contador);

                Grafo_Completo(T, contador, contador);

                Grafo_Ciclico(T, contador, contador);

                Teorema_1(T, contador, contador);

                Teorema_2(T, contador, contador);
            }
            else
            {
                Console.Write("\n\n\t\t     Arquivo não encontrado ou inexistente !");
            }

            Console.ReadKey();
        }
    }
}
