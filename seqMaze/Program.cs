using System;
using System.Collections.Generic;

namespace seqMaze
{
    class Program
    {
        static void Main(string[] args)
        {
            basic eslam = new basic();
            List<int[,]> pathToGoal = eslam.prog(@"C:\Users\Z51\Desktop\seqMaze\text2.txt");
        }
    }
}
