using System.Linq;

namespace KnapsackSolver
{
    public class BranchBoundSolver : ISolver
    {
        public Solution Solve(Knapsack k)
        {
            int itemCount = k.Items.Length;
            var sortedItems = k.Items.OrderByDescending(x => ((double)x.Value) / x.Size);
            int[] values = sortedItems.Select(x => x.Value).ToArray();
            int[] sizes = sortedItems.Select(x => x.Size).ToArray();
            int[] indices = sortedItems.Select(x => x.Index).ToArray();
            double[] ratios = sortedItems.Select(x => ((double)x.Value) / x.Size).ToArray();
            bool[] used = new bool[itemCount];

            int top = 0;
            int currentValue = 0;
            int currentSize = 0;

            int bestValue = 0;
            bool[] bestSolution = null;
            int bestSolutionTop = 0;

            while (true)
            {
                // add items as long as possible
                while (top < itemCount && currentSize + sizes[top] <= k.Capacity)
                {
                    used[top] = true;
                    currentSize += sizes[top];
                    currentValue += values[top];
                    top++;
                }
                // a solution
                //Console.WriteLine(String.Format("Trying solution: {0} - {1}", top, string.Join(" ", used.Take(top).Select(x => x ? "1" : "0"))));
                if (currentValue > bestValue)
                {
                    bestValue = currentValue;
                    bestSolution = (bool[])used.Clone();
                    bestSolutionTop = top;
                }
                // find the place to start next branch - locate and change the topmost nonterminal 1
                if (top == itemCount)
                {
                    top--;
                    currentSize -= sizes[top];
                    currentValue -= values[top];
                }
                if (top == itemCount - 1)
                {
                    do
                        top--;
                    while (top >= 0 && !used[top]);
                    if (top < 0)
                        break;
                    currentSize -= sizes[top];
                    currentValue -= values[top];
                }

                while (true)
                {
                    // check if branch can give any good solution
                    int estimateValue = currentValue;
                    int placeLeft = k.Capacity - currentSize;
                    for (int i = top + 1; i < itemCount; i++)
                    {
                        if (placeLeft >= sizes[i])
                        {
                            placeLeft -= sizes[i];
                            estimateValue += values[i];
                        }
                        else
                        {
                            estimateValue += (int)((double)placeLeft * values[i] / sizes[i]);
                            break;
                        }
                    }
                    if (estimateValue > bestValue)
                        break;
                    // continue up to find another
                    do
                        top--;
                    while (top >= 0 && !used[top]);
                    if (top < 0)
                        break;
                    currentSize -= sizes[top];
                    currentValue -= values[top];
                }

                if (top < 0)
                    break;

                used[top] = false;
                top++;
            }

            bool[] usedItems = new bool[itemCount];
            for (int i = 0; i < bestSolutionTop; i++)
                usedItems[indices[i]] = bestSolution[i];
            for (int i = bestSolutionTop; i < itemCount; i++)
                usedItems[indices[i]] = false;

            return new Solution()
            {
                IsOptimal = true,
                Value = bestValue,
                UsedItems = usedItems
            };
        }
    }
}
