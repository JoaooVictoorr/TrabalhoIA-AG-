using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrabalhoDeIA
{
    class Program
    {
        public static Posicao posicaoAtual = new Posicao(1, 0);
        static void Main(string[] args)
        {
            var populacao = AlgoritmoGenetico.GerarPopulacao(10, 10);
            var populacaoSobrevivente = AlgoritmoGenetico.Avaliacao(populacao);

            while (true) //while para testar a redução da população
            {
                AlgoritmoGenetico.GerarFilhos(ref populacaoSobrevivente);
                populacaoSobrevivente = AlgoritmoGenetico.Avaliacao(populacaoSobrevivente);
                Labirinto.Andar(populacaoSobrevivente);
            }
        }
    }
}
