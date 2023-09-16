using APIPoke.Models;
using System.Collections.Generic;

namespace APIPoke.Functions
{
    public static class ExtractIdFromUrlFunction
    {
        public static List<Uri> ExtractEvolutionUrls(Chain chain)
        {
            var evolutionUrls = new List<Uri>();

            if (chain != null)
            {
                // Adicione a URL atual
                evolutionUrls.Add(chain.Species.Url);

                if (chain.EvolvesTo != null)
                {
                    foreach (var evolvesTo in chain.EvolvesTo)
                    {
                        // Chame a função recursivamente para cada evolução
                        evolutionUrls.AddRange(ExtractEvolutionUrls(evolvesTo));
                    }
                }
            }

            return evolutionUrls;
        }
        public static List<int> ExtractIdFromUrls(List<Uri> urls)
        {
            if (urls == null)
            {
                return null; // Trate o caso em que a lista de URLs é nula
            }

            List<int> ids = new List<int>();

            foreach (Uri url in urls)
            {
                if (url != null)
                {
                    string path = url.AbsolutePath;
                    string[] segments = path.Split('/');
                    string idStr = segments[4];

                    if (int.TryParse(idStr, out int id))
                    {
                        ids.Add(id);
                    }
                }
            }

            return ids;
        }

    }
}
