using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Sudoku
// by Daniel Cerman
// 2015-12-12
namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] rootSolution = {
                         {1, 2, 3, 4, 5, 6, 7, 8, 9},
                         {4, 5, 6, 7, 8, 9, 1, 2, 3},
                         {7, 8, 9, 1, 2, 3, 4, 5, 6},
                         {2, 3, 4, 5, 6, 7, 8, 9, 1},
                         {5, 6, 7, 8, 9, 1, 2, 3, 4},
                         {8, 9, 1, 2, 3, 4, 5, 6, 7},
                         {3, 4, 5, 6, 7, 8, 9, 1, 2},
                         {6, 7, 8, 9, 1, 2, 3, 4, 5},
                         {9, 1, 2, 3, 4, 5, 6, 7, 8}
                       };

            SudokuField sf = new SudokuField(rootSolution);
            Console.WriteLine(sf);

            Console.WriteLine("IsValidField: {0}", sf.IsValidField());
        }
    }
}
