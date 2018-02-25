using System.Linq;

namespace KnapsackSolver
{
    public class EmptySolver : ISolver
    {
        public Solution Solve(Knapsack k)
        {
            return new Solution()
            {
                IsOptimal = false,
                Value = 0,
                UsedItems = k.Items.Select(x => false).ToArray()
            };
        }
    }
}
