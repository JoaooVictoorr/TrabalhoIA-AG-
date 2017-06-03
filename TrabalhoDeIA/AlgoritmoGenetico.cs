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
        public void Avaliacao(Queue<Posicao> caminho)
        {
            var labirinto = Labirinto.getLabirinto();
            Labirinto.Andar(labirinto, new Posicao { Linha = 1, Coluna = 0 }, caminho.Dequeue());

            // Código de Avaliação do caminho
        }

        public static Stack<Posicao> Caminho(int quantDirecoes, Posicao posicaoAtual, Stack<Posicao> populacao)
        {
            for (int i = 0; i < quantDirecoes; i++)
            {
                populacao.Push(Sorteio(posicaoAtual));
            }

            return populacao;
        }

        public static Posicao Sorteio(Posicao posicaoAtual)
        {
            int resultado = RandomNumber();

            if (resultado == 0)
            {
                Program.posicaoAtual = new Posicao(posicaoAtual.Linha, posicaoAtual.Coluna - 1);
                return new Posicao { Linha = posicaoAtual.Linha, Coluna = posicaoAtual.Coluna - 1 };
            }

            if (resultado == 1)
            {
                Program.posicaoAtual = new Posicao(posicaoAtual.Linha - 1, posicaoAtual.Coluna);
                return new Posicao { Linha = posicaoAtual.Linha - 1, Coluna = posicaoAtual.Coluna };
            }

            if (resultado == 2)
            {
                Program.posicaoAtual = new Posicao(posicaoAtual.Linha, posicaoAtual.Coluna + 1);
                return new Posicao { Linha = posicaoAtual.Linha, Coluna = posicaoAtual.Coluna + 1 };
            }

            if (resultado == 3)
            {
                Program.posicaoAtual = new Posicao(posicaoAtual.Linha + 1, posicaoAtual.Coluna);
                return new Posicao { Linha = posicaoAtual.Linha + 1, Coluna = posicaoAtual.Coluna };
            }

            return null;
        }

        private static int RandomNumber()
        {
            return r.Next(0, 3);
        }
    }
}
