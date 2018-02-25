using System.IO;

namespace KnapsackSolver
{
    public class Knapsack
    {
        public Knapsack(TextReader input)
        {
            string[] header = input.ReadLine().Split();
            Capacity = int.Parse(header[1]);
            int items = int.Parse(header[0]);
            Items = new Item[items];
            for (int i = 0; i < items; i++)
            {
                string[] itemInfo = input.ReadLine().Split();
                Items[i] = new Item()
                {
                    Index = i,
                    Size = int.Parse(itemInfo[1]),
                    Value = int.Parse(itemInfo[0])
                };
            }
        }

        public int Capacity { get; private set; }

        public Item[] Items { get; private set; }

        public class Item
        {
            public int Index;
            public int Size;
            public int Value;
        }
    }
}
