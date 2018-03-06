namespace ColoringSolver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CliqueFinder
    {
        private const int InterestingCliqueSize = 8;
        private const int CliqueLimit = 4000;
        private Graph G;
        private List<int[]> Cliques;
        private Random Randomizer;

        public CliqueFinder(Graph g)
        {
            this.G = g;
            this.Randomizer = new Random();
        }

        public List<int[]> FindMaximalCliques()
        {
            this.Cliques = new List<int[]>();
            this.FindInternal(new HashSet<int>(), new HashSet<int>(Enumerable.Range(0, this.G.VertexCount)), new HashSet<int>());
            return this.Cliques;
        }

        private void FindInternal(HashSet<int> R, HashSet<int> P, HashSet<int> X)
        {
            if (!P.Any() && !X.Any())
            {
                if (R.Count >= InterestingCliqueSize)
                    this.Cliques.Add(R.ToArray());
                return;
            }
            int pivotIndex = this.Randomizer.Next(P.Count + X.Count);
            int pivot = pivotIndex < P.Count ? P.ElementAt(pivotIndex) : X.ElementAt(pivotIndex - P.Count);
            HashSet<int> Ps = new HashSet<int>(P);
            Ps.ExceptWith(this.G.NeighborsOf(pivot));
            foreach (int v in Ps)
            {
                P.Remove(v);
                R.Add(v);
                HashSet<int> P1 = new HashSet<int>(P);
                P1.IntersectWith(this.G.NeighborsOf(v));
                HashSet<int> X1 = new HashSet<int>(X);
                X1.IntersectWith(this.G.NeighborsOf(v));
                this.FindInternal(R, P1, X1);
                if (this.Cliques.Count >= CliqueLimit)
                    return;
                R.Remove(v);
                X.Add(v);
            }
        }
    }
}
