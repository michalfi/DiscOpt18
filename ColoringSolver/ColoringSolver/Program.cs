namespace ColoringSolver
{
    using System;
    using System.IO;
    using Mono.Options;

    public class Program
    {
        static void Main(string[] args)
        {
            Graph g = new Graph(new StreamReader(Console.OpenStandardInput()));
            ISolver solver = new HybridSolver();

            OptionSet os = new OptionSet() {
                { "s=", "Solver to use", solverName => {
                    switch (solverName)
                    {
                        case "greedy":
                            solver = new GreedySolver();
                            break;
                        case "cp":
                            solver = new ConstraintSolver();
                            break;
                    }
                }}
            };
            os.Parse(args);

            Coloring coloring = solver.Solve(g);
            coloring.Output(new StreamWriter(Console.OpenStandardOutput()));
        }
    }
}
