using System;
using System.Collections.Generic;
using System.Linq;

namespace LerNumerosLoteria
{
    class Program
    {

        private const string CaminhoArquivo = "JogosLoteria.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("Bem vindo!");

            var sorteio = ReceberJogoDoUsuario();
            List<Jogo> jogosArquivo = ConverterArquivoEmJogos();

            ConfereJogos(jogosArquivo, sorteio);

            List<Jogo> jogosComAcerto = FiltraJogosVencedores(jogosArquivo);

            Console.WriteLine("\nNúmeros sorteados: {0}", string.Join(",", sorteio));
            if (jogosComAcerto.Count <= 0)
            {
                Console.WriteLine("\nInfelizmente você não teve nenhum jogo com acertos relevantes!!");
                ImprimeStatisticasJogo(sorteio, jogosArquivo);
            }
            else
            {
                Console.WriteLine("Parabéns!! Você teve {0} jogo(s) vencedor(es). Segue:\n", jogosComAcerto.Count);
                jogosComAcerto.ForEach(j => Console.WriteLine(j.ToString()));
            }
            Console.ReadLine();
        }

        static string[] ReadFileAsText()
        {
            return System.IO.File.ReadAllLines(CaminhoArquivo);
        }

        static List<Jogo> ConverterArquivoEmJogos()
        {
            string[] arquivoAsString = ReadFileAsText();
            List<Jogo> jogos = new List<Jogo>();

            for (int i = 0; i < arquivoAsString.Length; i += 6)
            {
                var idxTipoJogo = ConstantesJogo.JogosAceitos.FindIndex(tj => tj.Nome == arquivoAsString[i].Substring(6));
                if (idxTipoJogo >= 0)
                {
                    jogos.Add(new Jogo
                    {
                        Tipo = ConstantesJogo.JogosAceitos.ElementAt(idxTipoJogo),
                        Numeros = arquivoAsString[i + 3].Substring(17).Split(",").Select(ns => Convert.ToInt32(ns)).ToArray(),
                        LinhaArquivo = i + 1
                    });
                }
            }

            return jogos;
        }

        static int[] ReceberJogoDoUsuario()
        {
            Console.WriteLine("Favor digite os números do jogo separados por vírgula:");
            Console.WriteLine("Ex: 1,2,35,60,90,50");
            return Console.ReadLine()
                .Trim()
                .Split(",")
                .Select(ns => Convert.ToInt32(ns))
                .Distinct()
                .ToArray();
        }

        static List<Jogo> FiltraJogosVencedores(List<Jogo> listaJogos)
        {
            return listaJogos
                .Where(j => j.QtdeAcertos >= j.Tipo.QtdMinimaAcertos)
                .ToList();
        }

        static void ConfereJogos(List<Jogo> jogos, int[] numerosSorteados)
        {
            jogos.ForEach(j => j.QtdeAcertos = j.Numeros.Where(n => numerosSorteados.Contains(n)).Count());
        }

        static void ImprimeStatisticasJogo(int[] numerosSorteado, List<Jogo> jogosFeito)
        {
            Console.WriteLine("Estatísticas:");
            foreach (int qtdeAcertos in jogosFeito.Select(j => j.QtdeAcertos).Distinct().OrderByDescending(d => d))
            {
                var jogosDaVez = jogosFeito.Where(j => j.QtdeAcertos == qtdeAcertos);
                Console.WriteLine("{0}x jogos com {1} acerto(s). (linhas: {2})", jogosDaVez.Count(), qtdeAcertos, string.Join(", ", jogosDaVez.Select(j => j.LinhaArquivo)));
            }
        }
    }
}
