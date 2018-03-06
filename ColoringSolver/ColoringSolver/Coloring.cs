namespace ColoringSolver
{
    using System.IO;
    using System.Linq;

    public class Coloring
    {
        public Coloring(int[] colorAssignment, bool isOptimal)
        {
            this.IsOptimal = isOptimal;
            this.ColorAssignment = colorAssignment;
            this.ColorClassSizes = CountClassSizes(colorAssignment);
        }

        private Coloring(int[] colorAssignment, int[] classSizes, bool isOptimal)
        {
            this.IsOptimal = isOptimal;
            this.ColorAssignment = colorAssignment;
            this.ColorClassSizes = classSizes;
        }

        public int ColorCount => this.ColorAssignment.Max() + 1;

        public int[] ColorAssignment { get; private set; }

        public int[] ColorClassSizes { get; private set; }

        public bool IsOptimal { get; set; }

        public static Coloring Normalize(int[] messyAssignment, bool isOptimal)
        {
            int len = messyAssignment.Length;
            int[] sizes = CountClassSizes(messyAssignment);

            int[] sortedColors = sizes.Select((size, index) => new { size, index }).OrderByDescending(x => x.size).Select(x => x.index).ToArray();
            int[] colorMapping = new int[len];
            for (int i = 0; i < len; i++)
                colorMapping[sortedColors[i]] = i;
            int[] colorAssignment = new int[len];
            int[] colorClassSizes = new int[len];
            for (int i = 0; i < len; i++)
            {
                colorAssignment[i] = colorMapping[messyAssignment[i]];
                colorClassSizes[i] = sizes[colorMapping[i]];
            }

            return new Coloring(colorAssignment, colorClassSizes, isOptimal);
        }

        private static int[] CountClassSizes(int[] colorAssignment)
        {
            int[] sizes = new int[colorAssignment.Length];
            foreach (int color in colorAssignment)
                sizes[color]++;
            return sizes;
        }

        public void Output(TextWriter target)
        {
            target.Write(this.ColorCount);
            target.WriteLine(this.IsOptimal ? " 1" : " 0");
            target.WriteLine(string.Join(" ", this.ColorAssignment));
            target.Flush();
        }
    }
}
