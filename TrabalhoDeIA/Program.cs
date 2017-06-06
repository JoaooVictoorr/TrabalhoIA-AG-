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
            var labirinto = Labirinto.getLabirinto();
            Stack<String[,]> pilhaLabirinto = new Stack<String[,]>();
            Stack<Posicao> pilha = new Stack<Posicao>();
            List<List<Posicao>> populacao = new List<List<Posicao>>();

            populacao = AlgoritmoGenetico.CriarPopulacao(8, 10);

            //while (true) //while para testar a redução da população
            //{
                var caminhosAvaliados = AlgoritmoGenetico.Avaliacao(populacao);
                var paisSelecionados = AlgoritmoGenetico.melhoresPais(caminhosAvaliados);
                populacao = AlgoritmoGenetico.GerarFilhos(paisSelecionados);
            //}





            do
            {
                var caminhos = Labirinto.CaminhosLivres(labirinto, posicaoAtual);

                if (caminhos.Count > 0)
                {
                    var min = caminhos.Min(a => a.DistanciaHipotenusa);
                    var caminhoEscolhido = caminhos.Where(a => a.DistanciaHipotenusa == min).FirstOrDefault();

                    if (caminhos.Count >= 2)
                        pilhaLabirinto.Push(labirinto);

                    caminhos.Remove(caminhoEscolhido);

                    foreach (var caminho in caminhos)
                        pilha.Push(caminho);
                    Console.Clear();
                    Labirinto.Andar(labirinto, posicaoAtual, caminhoEscolhido);
                }
                else
                {
                    labirinto = pilhaLabirinto.Pop();
                    var caminhoEscolhido = pilha.Pop();
                    Console.Clear();
                    Labirinto.Andar(labirinto, posicaoAtual, caminhoEscolhido);
                }
                Thread.Sleep(500);

            } while (labirinto[19, 19] != "C");
        }
    }
}
