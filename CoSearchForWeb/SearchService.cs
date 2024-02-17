using PokemonCoRNGLibrary;
using PokemonCoRNGLibrary.ProvidedData;
using System.Security.Cryptography;

namespace CoSearchForWeb
{
    public class SearchService
    {
        public SearchResult[] Search(string name, uint minH, uint maxH, uint minA, uint maxA, uint minB, uint maxB, uint minC, uint maxC, uint minD, uint maxD, uint minS, uint maxS)
        {
            var poke = ProvidedCoDarkPokemonData.Get(name);
            if (poke == null) return [];

            var slot = poke.GetGenerator();
            var res = new List<(uint, GCIndividual)>();
            for (uint h = minH; h <= maxH; h++)
                for (uint a = minA; a <= maxA; a++)
                    for (uint b = minB; b <= maxB; b++)
                        for (uint c = minC; c <= maxC; c++)
                            for (uint d = minD; d <= maxD; d++)
                                for (uint s = minS; s <= maxS; s++)
                                    res.AddRange(slot.CalcBack(h, a, b, c, d, s, true));

            return res.Select(_ => new SearchResult(_.Item1, _.Item2.PID, [.. _.Item2.IVs])).ToArray();
        }
    }

    public record struct SearchResult(uint Seed, uint PID, uint[] IVs);
}
