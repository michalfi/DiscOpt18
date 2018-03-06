namespace ColoringSolver
{
    using System.Linq;

    public class GreedySolver : ISolver
    {
        public Coloring Solve(Graph g)
        {
            int[] colors = new int[g.VertexCount];
            var availableColors = Enumerable.Range(0, g.VertexCount);
            colors.Populate(-1);
            int[] vertices = Enumerable.Range(0, g.VertexCount).OrderByDescending(i => g.Degrees[i]).ToArray();
            foreach (int v in vertices)
            {
                colors[v] = availableColors.Except(g.NeighborsOf(v).Select(n => colors[n])).Min();
            }

            return new Coloring(colors, false);
        }
    }
}
