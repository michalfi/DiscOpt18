using System.Linq;

namespace KnapsackSolver
{
    public class RatioGreedySolver : GreedySolver
    {
        public override IOrderedEnumerable<Knapsack.Item> SortedItems(Knapsack k)
        {
            return k.Items.OrderByDescending(x => ((double)x.Value) / x.Size);
        }
    }
}
