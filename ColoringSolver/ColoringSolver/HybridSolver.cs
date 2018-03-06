namespace ColoringSolver
{
    public class HybridSolver : ISolver
    {
        public Coloring Solve(Graph g)
        {
            var solver = g.EdgeCount < 10000 ? (ISolver)new ConstraintSolver() : new GreedySolver();
            return solver.Solve(g);
        }
    }
}
