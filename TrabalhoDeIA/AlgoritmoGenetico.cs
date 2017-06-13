using System;
using System.Collections.Generic;
using System.Linq;

namespace TrabalhoDeIA
{
	class AlgoritmoGenetico
	{
		public static List<Individuo> GerarPopulacao(int quantidadeGeracao, int quantidadePosicoes)
		{

			List<Individuo> individuos = new List<Individuo>();

			for (int i = 0; i < quantidadeGeracao; i++)
			{
				individuos.Add(new Individuo
				{
					valorFitnessTotal = 0,
					posicoes = SorteioPosicao(quantidadePosicoes)
				});
			}
			return individuos;
		}

		public static List<Individuo> Avaliacao(List<Individuo> populacao)
		{

			var labirinto = Labirinto.getLabirinto();

			foreach (var individuo in populacao)
			{
				individuo.valorFitnessTotal = 0;
				foreach (var posicao in individuo.posicoes)
				{
					try
					{
						if (labirinto[posicao.Linha, posicao.Coluna].Contains("X"))
						{
							individuo.valorFitnessTotal -= 5;
						}
						else if (labirinto[posicao.Linha, posicao.Coluna].Contains(" "))
						{
							individuo.valorFitnessTotal += 10;
						}
						else if (labirinto[posicao.Linha, posicao.Coluna].Contains("S"))
						{
							individuo.valorFitnessTotal += 10000;
						}
						else
						{
							individuo.valorFitnessTotal += 10000;
						}
					}
					catch (Exception)
					{
						individuo.valorFitnessTotal -= 10;
					}
				}
			}

			var populacaoOrdenada = populacao.OrderByDescending(x => x.valorFitnessTotal).ToList();
			populacaoOrdenada.RemoveRange(populacao.Count / 2, populacao.Count / 2);
			return populacaoOrdenada;
		}


		public static List<Posicao> SorteioPosicao(int quantidadePosicao)
		{

			Posicao posicaoAtual = new Posicao(1, 0);

			List<Posicao> posicoes = new List<Posicao>();

			for (int i = 0; i < quantidadePosicao; i++)
			{

				int resultado = new Random().Next(0, 4);

				switch (resultado)
				{
					case 0:
						posicaoAtual.Coluna -= 1;
						break;
					case 1:
						posicaoAtual.Linha -= 1;
						break;
					case 2:
						posicaoAtual.Coluna = +1;
						break;
					case 3:
						posicaoAtual.Linha = +1;
						break;
				}

				posicoes.Add(new Posicao
				{
					Linha = posicaoAtual.Linha,
					Coluna = posicaoAtual.Coluna
				});
			}

			return posicoes;
		}

		public static void GerarFilhos(ref List<Individuo> listaPais)
		{
			int index;
			var tamanho = listaPais.Count;
			for (int i = 0; i < tamanho; i++)
			{
				index = new Random().Next(0, listaPais.Count);
				var filhoGerado = Cruzar(listaPais[i], listaPais[index]);
				listaPais.Add(filhoGerado);
			}
		}

		public static Individuo Cruzar(Individuo pai, Individuo mae)
		{
			Individuo filho = new Individuo();
			filho.posicoes = new List<Posicao>();

			for (int i = 0; i < pai.posicoes.Count / 4; i++)
			{
				filho.posicoes.Add(pai.posicoes[i]);
			}
			for (int i = 0; i < mae.posicoes.Count / 4; i++)
			{
				filho.posicoes.Add(mae.posicoes[i]);
			}

			for (int i = pai.posicoes.Count / 2; i < pai.posicoes.Count / 4; i++)
			{
				filho.posicoes.Add(pai.posicoes[i]);
			}
			for (int i = mae.posicoes.Count / 2; i < mae.posicoes.Count / 4; i++)
			{
				filho.posicoes.Add(mae.posicoes[i]);
			}
			return filho;
		}
	}
}
