using System;
using System.Collections.Generic;
using System.Text;

namespace KnapsackSolver
{
    public interface ISolver
    {
        Solution Solve(Knapsack input);
    }
}
