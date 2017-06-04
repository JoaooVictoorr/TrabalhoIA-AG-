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

        public static List<Stack<Posicao>> CriarPopulacao(int quantidadePopulacao, int qtdDirecoes)
        {
            List<Stack<Posicao>> populacao = new List<Stack<Posicao>>();
            Stack<Posicao> caminho = new Stack<Posicao>();
            for (int i = 0; i < quantidadePopulacao; i++)
            {
                caminho = new Stack<Posicao>();
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
