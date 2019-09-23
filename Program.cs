using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth2
{
    class Program
    {
        //BOLORUNDURO VALIANT-JOSHUA
        //CPE/16/7824
        //OLOTUAH NELSON GBENGA
        //CPE/16/7864
        const string _maze = @"
 1   x    .
 xxx x xx .
       xx .
  xxxx xx .
  xxxx xx .
  xxxx xx .
         2";

        static int[][] _moves = {
        new int[] { -1, 0 },
        new int[] { 0, -1 },
        new int[] { 0, 1 },
        new int[] { 1, 0 } };

        static int[][] GetMazeArray(string maze)
        {
            // Split apart the maze string.
            string[] lines = maze.Split(new char[] { '.', '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries);
            // Create jagged array.
            int[][] array = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                // Create row.
                var row = new int[line.Length];
                for (int x = 0; x < line.Length; x++)
                {
                    // Set ints from chars.
                    switch (line[x])
                    {
                        case 'x':
                            row[x] = -1;
                            break;
                        case '1':
                            row[x] = 1;
                            break;
                        case '2':
                            row[x] = -3;
                            break;
                        default:
                            row[x] = 0;
                            break;
                    }
                }
                // Store row in jagged array.
                array[i] = row;
            }
            return array;
        }

        static void Display(int[][] array)
        {
            // Loop over int data and display as characters.
            for (int i = 0; i < array.Length; i++)
            {
                var row = array[i];
                for (int x = 0; x < row.Length; x++)
                {
                    switch (row[x])
                    {
                        case -1:
                            Console.Write('x');
                            break;
                        case 1:
                            Console.Write('1');
                            break;
                        case -3:
                            Console.Write('2');
                            break;
                        case 0:
                            Console.Write(' ');
                            break;
                        default:
                            Console.Write('.');
                            break;
                    }
                }
                // End line.
                Console.WriteLine();
            }
        }

        static bool IsValidPos(int[][] array, int row, int newRow, int newColumn)
        {
            // ... Ensure position is within the array bounds.
            if (newRow < 0) return false;
            if (newColumn < 0) return false;
            if (newRow >= array.Length) return false;
            if (newColumn >= array[row].Length) return false;
            return true;
        }

        static int ModifyPath(int[][] array)
        {
            // Loop over rows and then columns.
            for (int rowIndex = 0; rowIndex < array.Length; rowIndex++)
            {
                var row = array[rowIndex];
                for (int columnIndex = 0; columnIndex < row.Length; columnIndex++)
                {
                    // Find a square we have traveled to.
                    int value = array[rowIndex][columnIndex];
                    if (value >= 1)
                    {
                        // Try all possible moves from this square.
                        foreach (var movePair in _moves)
                        {
                            // Move to a valid square.
                            int newRow = rowIndex + movePair[0];
                            int newColumn = columnIndex + movePair[1];
                            if (IsValidPos(array, rowIndex, newRow, newColumn))
                            {
                                int testValue = array[newRow][newColumn];
                                if (testValue == 0)
                                {
                                    // Travel to a new square for the first time.
                                    // ... Record the count of total moves to it.
                                    array[newRow][newColumn] = value + 1;
                                    // Move has been performed.
                                    return 0;
                                }
                                else if (testValue == -3)
                                {
                                    // We are at our end point.
                                    return 1;
                                }
                            }
                        }
                    }
                }
            }
            // We cannot do anything.
            return -1;
        }

        static void Main()
        {
            Console.WriteLine("/'1 indicates the starting point, 2 indicates the endpoint, and x represents the walls/'");
            Console.WriteLine("The maze is displayed below:");
            // Parse our maze and display it.
            var array = GetMazeArray(_maze);
            Display(array);
            int count = 0;

            // Read user input and evaluate maze.
            while (true)
            {
                string line = Console.ReadLine();
                int result = ModifyPath(array);
                if (result == 1)
                {
                    Console.WriteLine($"DONE: {count} moves");
                    Console.ReadLine();
                    break;
                }
                else if (result == -1)
                {
                    Console.WriteLine($"FAIL: {count} moves");
                    Console.ReadLine();
                    break;
                }
                else
                {
                    Display(array);
                }
                count++;
            }

        }
    }

}




