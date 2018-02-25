using System;
using System.IO;
using Mono.Options;

namespace KnapsackSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            ISolver solver = new EmptySolver();
            Knapsack ks = new Knapsack(new StreamReader(Console.OpenStandardInput()));

            OptionSet options = new OptionSet() {
                {"s=", "Solver to use", solverName => {
                    switch (solverName) {
                        case "greedy":
                            solver = new GreedySolver();
                            break;
                        case "ratiogreedy":
                            solver = new RatioGreedySolver();
                            break;
                        case "dynamic":
                            solver = new DynamicSolver();
                            break;
                        case "hybrid":
                            solver = new HybridSolver();
                            break;
                        case "branch":
                            solver = new BranchBoundSolver();
                            break;
                        default:
                            throw new OptionException("Invalid solver name", "s");
                    }
                }}
            };
            options.Parse(args);


            Solution solution = solver.Solve(ks);
            solution.Output(new StreamWriter(Console.OpenStandardOutput()));
        }
    }
}
