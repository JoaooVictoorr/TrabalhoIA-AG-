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

        public static List<ItemPopulacao> CriarPopulacao(int quantidadePopulacao, int qtdDirecoes)
        {
            List<ItemPopulacao> itemPopulacao = new List<ItemPopulacao>();
            for (int i = 0; i < quantidadePopulacao; i++)
            {
                itemPopulacao.Add(Caminho(qtdDirecoes, Program.posicaoAtual));
                ReiniciarPosicaoAtual();
            }
            return itemPopulacao;
        }

        private static void ReiniciarPosicaoAtual()
        {
            Program.posicaoAtual.Linha = 1;
            Program.posicaoAtual.Coluna = 0;
        }

        public static List<ItemPopulacao> Avaliacao(List<ItemPopulacao> populacao)
        {
            ItemPopulacao caminhosAux;
            List<ItemPopulacao> caminhosAvaliados = new List<ItemPopulacao>();
            var labirinto = Labirinto.getLabirinto();

            foreach (var itemPopulacao in populacao)
            {
                foreach (var posicoes in itemPopulacao.caminhos)
                {
                    try
                    {
                        if (labirinto[posicoes.Linha, posicoes.Coluna].Contains("X"))
                        {
                            posicoes.valorFitness = -5;
                        }
                        else if (labirinto[posicoes.Linha, posicoes.Coluna].Contains(" "))
                        {
                            posicoes.valorFitness = +10;
                        }
                        else if (labirinto[posicoes.Linha, posicoes.Coluna].Contains("S"))
                        {
                            posicoes.valorFitness = +10000;
                        }
                    }
                    catch (Exception)
                    {
                        posicoes.valorFitness = -10;
                        //Se cair aqui, significa que o caminho seguiu pra fora do labirinto.      
                    }
                }
                caminhosAux = new ItemPopulacao();
                caminhosAux.caminhos = itemPopulacao.caminhos;
                caminhosAux.valorFitnessTotal = CalcularValorFitnessTotal(itemPopulacao.caminhos);
                caminhosAvaliados.Add(caminhosAux);
            }
            var populacaoAvaliada = caminhosAvaliados.OrderByDescending(x => x.valorFitnessTotal).ToList();
            populacaoAvaliada.RemoveRange(populacaoAvaliada.Count / 2, populacaoAvaliada.Count / 2);
            return populacaoAvaliada;
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

        public static ItemPopulacao Caminho(int quantDirecoes, Posicao posicaoAtual)
        {
            ItemPopulacao itemPopulacao = new ItemPopulacao();
            List<Posicao> posicoes = new List<Posicao>();
            for (int i = 0; i < quantDirecoes; i++)
            {
                posicoes.Add(Sorteio(posicaoAtual));
            }
            itemPopulacao.caminhos = posicoes;
            return itemPopulacao;
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

        private static int RandomNumber(int min = 0, int max = 4)
        {
            return r.Next(min, max);
        }

        public static void GerarFilhos(ref List<ItemPopulacao> listaPais)
        {
            int index;
            int tamanho = listaPais.Count;
            for (int i = 0; i < tamanho; i++)
            {
                index = RandomNumber(0, listaPais.Count);
                var filhoGerado = Cruzar(listaPais[i], listaPais[index]);
                listaPais.Add(filhoGerado);
            }
        }

        public static ItemPopulacao Cruzar(ItemPopulacao pai, ItemPopulacao mae)
        {
            ItemPopulacao filho = new ItemPopulacao();
            filho.caminhos = new List<Posicao>();

            for (int i = 0; i < pai.caminhos.Count / 4; i++)
            {
                filho.caminhos.Add(pai.caminhos[i]);
            }
            for (int i = 0; i < mae.caminhos.Count / 4; i++)
            {
                filho.caminhos.Add(mae.caminhos[i]);
            }

            for (int i = pai.caminhos.Count / 2; i < pai.caminhos.Count / 4; i++)
            {
                filho.caminhos.Add(pai.caminhos[i]);
            }
            for (int i = mae.caminhos.Count / 2; i < mae.caminhos.Count / 4; i++)
            {
                filho.caminhos.Add(mae.caminhos[i]);
            }
            return filho;
        }
    }
}
