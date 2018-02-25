using System.Linq;

namespace KnapsackSolver
{
    public class GreedySolver : ISolver
    {
        public Solution Solve(Knapsack k)
        {
            bool[] usedItems = new bool[k.Items.Length];
            usedItems.Populate(false);
            int value = 0;
            var sorted = SortedItems(k);
            int remainingCapacity = k.Capacity;

            foreach (Knapsack.Item i in sorted)
            {
                if (i.Size <= remainingCapacity)
                {
                    usedItems[i.Index] = true;
                    value += i.Value;
                    remainingCapacity -= i.Size;
                }
            }

            return new Solution()
            {
                IsOptimal = false,
                Value = value,
                UsedItems = usedItems
            };
        }

        public virtual IOrderedEnumerable<Knapsack.Item> SortedItems(Knapsack k)
        {
            var sorted = k.Items.OrderByDescending(x => x.Value);
            return sorted;
        }
    }
}
