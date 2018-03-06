namespace ColoringSolver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Google.OrTools.ConstraintSolver;

    public class ConstraintSolver : ISolver
    {
        private const int TimeLimitSeconds = 300;

        private static TimeSpan TimeLimit { get; } = TimeSpan.FromSeconds(TimeLimitSeconds);

        private List<int[]> Cliques;

        public Coloring Solve(Graph g)
        {
            Coloring bestColoring = new GreedySolver().Solve(g);
            this.Cliques = new CliqueFinder(g).FindMaximalCliques();
            //int maxCliqueSize = this.Cliques.Max(c => c.Length);
            //Debug.WriteLine("Cliques found");
            while (true)
            {
                //Debug.WriteLine(bestColoring.ColorCount);
                /*if (bestColoring.ColorCount == maxCliqueSize)
                {
                    bestColoring.IsOptimal = true;
                    break;
                }*/

                var task = Task.Factory.StartNew(() => this.FindColoring(g, bestColoring.ColorCount - 1));
                if (!task.Wait(TimeLimit))
                    break;

                int[] coloring = task.Result;
                if (coloring == null)
                {
                    bestColoring.IsOptimal = true;
                    break;
                }
                bestColoring = new Coloring(coloring, false);
            }

            return bestColoring;
        }

        public int[] FindColoring(Graph g, int colorsCount)
        {
            Solver solver = new Solver("Coloring");
            IntVar[] vertexColors = solver.MakeIntVarArray(g.VertexCount, 0, colorsCount - 1, "coloring");
            foreach (var e in g.Edges)
                solver.Add(vertexColors[e.Item1] != vertexColors[e.Item2]);

            // symmetry breaking
            /*solver.Add(vertexColors[0] == 0);
            for (int v = 1; v < g.VertexCount; v++)
                solver.Add(vertexColors[v] <= solver.MakeMax(vertexColors.Take(v).ToArray()) + 1);*/

            foreach (var c in this.Cliques)
                solver.Add(solver.MakeAllDifferent(c.Select(v => vertexColors[v]).ToArray()));

            DecisionBuilder db = solver.MakePhase(vertexColors, Solver.CHOOSE_MIN_SIZE_LOWEST_MAX, Solver.ASSIGN_CENTER_VALUE);
            solver.NewSearch(db);
            if (!solver.NextSolution())
                return null;
            int[] coloring = vertexColors.Select(x => (int)(x.Value())).ToArray();
            return coloring;
        }
    }
}
