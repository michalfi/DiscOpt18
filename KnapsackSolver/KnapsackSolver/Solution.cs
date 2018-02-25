using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KnapsackSolver
{
    public class Solution
    {
        public int Value { get; set; }

        public bool IsOptimal { get; set; }

        public bool[] UsedItems { get; set; }

        public void Output(TextWriter output)
        {
            output.Write(Value);
            output.WriteLine(IsOptimal ? " 1" : " 0");
            output.WriteLine(string.Join(" ", UsedItems.Select(used => used ? "1" : "0")));
            output.Flush();
        }
    }
}
