using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoDeIA
{
    class AlgoritmoGenetico
    {
        static Random r = new Random();
        static int ultimaDirecao = -1;

        public static List<List<Posicao>> CriarPopulacao(int quantidadePopulacao, int qtdDirecoes)
        {
            List<List<Posicao>> populacao = new List<List<Posicao>>();
            List<Posicao> caminho = new List<Posicao>();
            for (int i = 0; i < quantidadePopulacao; i++)
            {
                caminho = new List<Posicao>();
                caminho = Caminho(qtdDirecoes, Program.posicaoAtual, caminho);
                populacao.Add(caminho);
                ReiniciarPosicaoAtual();
            }
            return populacao;
        }

        private static void ReiniciarPosicaoAtual()
        {
            Program.posicaoAtual.Linha = 1;
            Program.posicaoAtual.Coluna = 0;
        }

        public static void Avaliacao(List<List<Posicao>> populacao)
        {
            List<ListaCaminhos> caminhosAux;
            List<ListaCaminhos> caminhosAvaliados = new List<ListaCaminhos>(); ;
            var labirinto = Labirinto.getLabirinto();
            int i = -1;
            int j = 0;

            foreach (var caminho in populacao)
            {
                foreach (var direcoes in caminho)
                {
                    i++;
                    try
                    {
                        if (labirinto[caminho[i].Linha, caminho[i].Coluna].Contains("X"))
                        {
                            caminho[i].valorFitness = -5;
                        }
                        else if (labirinto[caminho[i].Linha, caminho[i].Coluna].Contains(" "))
                        {
                            caminho[i].valorFitness = +10;
                        }
                        else if (labirinto[caminho[i].Linha, caminho[i].Coluna].Contains("C"))
                        {
                            caminho[i].valorFitness = +100;
                        }
                    }
                    catch (Exception)
                    {
                        caminho[i].valorFitness = -10;
                        //Se cair aqui, significa que o caminho seguiu pra fora do labirinto.      
                    }
                }
                caminhosAux = new List<ListaCaminhos> { new ListaCaminhos { caminhos = caminho, valorFitnessTotal = CalcularValorFitnessTotal(caminho) } };
                caminhosAvaliados.Add(caminhosAux[0]);
                i = -1;
            }
            caminhosAvaliados.Sort((x, y) => x.valorFitnessTotal.CompareTo(y.valorFitnessTotal));
        }

        public static int CalcularValorFitnessTotal(List<Posicao> caminho)
        {
            int calculoValorFitness = 0;

            foreach (var direcao in caminho)
            {
                calculoValorFitness += direcao.valorFitness;
            }

            return calculoValorFitness;
        }

        public static List<Posicao> Caminho(int quantDirecoes, Posicao posicaoAtual, List<Posicao> populacao)
        {
            for (int i = 0; i < quantDirecoes; i++)
            {
                populacao.Add(Sorteio(posicaoAtual));
            }

            return populacao;
        }

        public static Posicao Sorteio(Posicao posicaoAtual)
        {
            int resultado = RandomNumber();

            if (ultimaDirecao == 0)
            {
                do
                {
                    resultado = RandomNumber();
                }
                while (resultado == 2);
            }

            else if (ultimaDirecao == 2)
            {
                do
                {
                    resultado = RandomNumber();
                }
                while (resultado == 0);
            }

            else if (ultimaDirecao == 1)
            {
                do
                {
                    resultado = RandomNumber();
                }
                while (resultado == 3);
            }

            else if (ultimaDirecao == 3)
            {
                do
                {
                    resultado = RandomNumber();
                }
                while (resultado == 1);
            }
            ultimaDirecao = resultado;

            if (resultado == 0)
            {
                Program.posicaoAtual.Linha = Program.posicaoAtual.Linha;
                Program.posicaoAtual.Coluna = Program.posicaoAtual.Coluna - 1;
                return new Posicao { Linha = Program.posicaoAtual.Linha, Coluna = Program.posicaoAtual.Coluna };
            }

            if (resultado == 1)
            {
                Program.posicaoAtual.Linha = Program.posicaoAtual.Linha - 1;
                Program.posicaoAtual.Coluna = Program.posicaoAtual.Coluna;
                return new Posicao { Linha = Program.posicaoAtual.Linha, Coluna = Program.posicaoAtual.Coluna };
            }

            if (resultado == 2)
            {
                Program.posicaoAtual.Linha = Program.posicaoAtual.Linha;
                Program.posicaoAtual.Coluna = Program.posicaoAtual.Coluna + 1;
                return new Posicao { Linha = Program.posicaoAtual.Linha, Coluna = Program.posicaoAtual.Coluna };
            }

            if (resultado == 3)
            {
                Program.posicaoAtual.Linha = Program.posicaoAtual.Linha + 1;
                Program.posicaoAtual.Coluna = Program.posicaoAtual.Coluna;
                return new Posicao { Linha = Program.posicaoAtual.Linha, Coluna = Program.posicaoAtual.Coluna };
            }

            return null;
        }

        private static int RandomNumber()
        {
            return r.Next(0, 4);
        }
    }
}
