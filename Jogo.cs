using System;
using System.Collections.Generic;
using System.Linq;

namespace LerNumerosLoteria
{
    public class Jogo
    {
        public TipoJogo Tipo { get; set; }
        public int[] Numeros { get; set; }
        public int LinhaArquivo { get; set; }
        public int QtdeAcertos { get; set; }

        public override string ToString()
        {
            return "Linha: " + LinhaArquivo
                + "\nTipo Jogo: " + Tipo.Nome
                + "\nNumeros jogados: " + string.Join(", ", Numeros.Select(n => n.ToString()))
                + "\nQuantidade de Acertos: " + QtdeAcertos;
        }
    }

    public class TipoJogo
    {
        public string Nome { get; set; }
        public int QtdNumeros { get; set; }

        public int QtdMinimaAcertos { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TipoJogo jogo &&
                   Nome == jogo.Nome;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nome);
        }
    }

    public static class ConstantesJogo
    {
        public static List<TipoJogo> JogosAceitos = new List<TipoJogo>
            {
                new TipoJogo { Nome = "Mega Sena", QtdNumeros = 6, QtdMinimaAcertos = 4 },
                new TipoJogo { Nome = "Loto Fácil", QtdNumeros = 25, QtdMinimaAcertos = 15 }
            };
    }
}
