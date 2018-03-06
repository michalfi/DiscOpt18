namespace ColoringSolver
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Graph
    {
        public int VertexCount { get; private set; }

        public int EdgeCount { get; private set; }

        public Tuple<int, int>[] Edges { get; private set; }

        public int[] Degrees { get; private set; }

        public int[] Neighbors { get; private set; }

        public int[] NeighborIndices { get; private set; }

        public Graph(TextReader input)
        {
            string[] counts = input.ReadLine().Split();
            this.VertexCount = int.Parse(counts[0]);
            this.EdgeCount = int.Parse(counts[1]);
            this.Edges = new Tuple<int, int>[this.EdgeCount];
            this.Degrees = new int[this.VertexCount];

            for (int i = 0; i < this.EdgeCount; i++)
            {
                string[] ends = input.ReadLine().Split();
                this.Edges[i] = new Tuple<int, int>(int.Parse(ends[0]), int.Parse(ends[1]));
                this.Degrees[this.Edges[i].Item1]++;
                this.Degrees[this.Edges[i].Item2]++;
            }

            this.NeighborIndices = new int[this.VertexCount + 1];
            this.NeighborIndices[0] = 0;
            for (int i = 1; i <= this.VertexCount; i++)
                this.NeighborIndices[i] = this.NeighborIndices[i - 1] + this.Degrees[i - 1];

            var edgesTwice = this.Edges.Concat(this.Edges.Select(e => new Tuple<int, int>(e.Item2, e.Item1)));
            this.Neighbors = edgesTwice.OrderBy(e => e.Item1).Select(e => e.Item2).ToArray();
        }

        public IEnumerable<int> NeighborsOf(int v)
        {
            return this.Neighbors.Skip(this.NeighborIndices[v]).Take(this.Degrees[v]);
        }
    }
}
