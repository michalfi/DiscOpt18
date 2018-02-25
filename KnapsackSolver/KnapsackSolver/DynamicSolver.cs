using System;

namespace KnapsackSolver
{
    public class DynamicSolver : ISolver
    {
        public Solution Solve(Knapsack k)
        {
            int[][] table = new int[k.Items.Length][];

            //first column
            table[0] = new int[k.Capacity + 1];
            int iSize = k.Items[0].Size;
            int iValue = k.Items[0].Value;
            for (int c = 0; c <= k.Capacity; c++)
            {
                table[0][c] = (iSize <= c) ? iValue : 0;
            }

            // the rest
            for (int i = 1; i < k.Items.Length; i++)
            {
                table[i] = new int[k.Capacity + 1];
                iSize = k.Items[i].Size;
                iValue = k.Items[i].Value;
                for (int c = 0; c <= k.Capacity; c++)
                {
                    if (iSize > c)
                        table[i][c] = table[i - 1][c];
                    else
                        table[i][c] = Math.Max(table[i - 1][c], table[i - 1][c - iSize] + iValue);
                }
            }

            // backtrack for optimal solution
            bool[] usedItems = new bool[k.Items.Length];
            int row = k.Capacity;
            for (int col = k.Items.Length - 1; col > 0; col--)
            {
                if (table[col][row] != table[col - 1][row])
                {
                    usedItems[col] = true;
                    row = row - k.Items[col].Size;
                }
                else
                    usedItems[col] = false;
            }
            usedItems[0] = table[0][row] != 0;

            return new Solution()
            {
                IsOptimal = true,
                Value = table[k.Items.Length - 1][k.Capacity],
                UsedItems = usedItems
            };
        }
    }
}
