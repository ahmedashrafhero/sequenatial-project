using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace seqMaze
{
    class basic
    {
        static List<string> lines = new List<string>();
        static int[] start, end;
        static async Task readfile(string path)
        {
            StreamReader file = File.OpenText(path);
            string line;
            while (!file.EndOfStream)
            {
                line = await file.ReadLineAsync();
                lines.Add(line);
            }
        }
        public List<int[,]> prog (string path)
        {
            List<int[]> blocks = new List<int[]>();
            int row = -1, col = -1;
            Task t = readfile(path);
            Task.WaitAll(t);
            foreach (String line in lines)
            {
                row++;
                col = -1;
                foreach (char c in line)
                {
                    col++;
                    if (c == '*')
                        blocks.Add(new[] { row, col });
                    else if (c == 'c')
                        start = new[] { row, col };
                    else if (c == 'e')
                        end = new[] { row, col };
                }

            }
            matrixElement[,] matrix = new matrixElement[3, 3];
            operations op = new operations();
            matrix = op.Create_matrix((row + 1), (col + 1), blocks, start, end);
            op.finding_path(matrix, start, start, new[] { (row + 1), (col + 1) });
            return op.smallest_list();
        }
    }
}
