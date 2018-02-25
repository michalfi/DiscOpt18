namespace KnapsackSolver
{
    public class HybridSolver : ISolver
    {
        public Solution Solve(Knapsack input)
        {
            ISolver solver;
            if (input.Capacity > 350000)
                solver = new RatioGreedySolver();
            else
                solver = new DynamicSolver();
            return solver.Solve(input);
        }
    }
}
