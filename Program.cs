using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;


class Program
{
    interface INewNumber
    {
        int Get();
    }

    class NewNumber : INewNumber
    {
        static Random number;
        static NewNumber instance;

        static NewNumber() 
        {
            number = new Random();
            instance = new NewNumber();
        }

        private NewNumber() { }

        public static INewNumber CreateInstance() { return instance; }
        public int Get()
        {
            return number.Next(0, 4);
        }
    }
    class TestNewNumber : INewNumber
    {
        static readonly int[] arr = { 2, 2, 2, 5, 3, 0, 4, 4, 4, 4, 9, 0, 0, 7, 8, };
        static int num = 0;
        public int Get()
        {
            return arr[num++];
        }
    }

    void Starer()
    {
    }
    static void Main(string[] args)
    {
        var maxRows = 9;
        var maxColumns = 9;

        //var matrix = new int?[maxRows, maxColumns];
        var number = new Random();

        //CreateMatrix(matrix, number);

        //var matrix = new int[,]{ 
        //    { 9}, 
        //    { 9},
        //    { 9},
        //    { 7},
        //    { 4},
        //    { 4},
        //    { 4},
        //    { 5},
        //    { 0}
        //};
        var matrix = new int[,]{ { 9, 9, 9, 7, 4, 4, 4, 5, 0 } };

        Print(matrix, "First");
        var needReEdit = false;
        //do
        //{
        //    //EditRow(matrix, number, ref needReEdit);
        //    //Print(matrix, "After EditRow");

        //    EditColumn(matrix, number, ref needReEdit);
        //    Print(matrix, "After EditColumn");
        //} while (needReEdit);

        needReEdit = EditRow(matrix, new TestNewNumber());
        Print(matrix, "After EditRow");

        //needReEdit = EditColumn(matrix, new TestNewNumber()/*NewNumber.CreateInstance()*/);
        //Print(matrix, "After EditColumn");




        //new Program().Starer();
        Console.ReadLine();
    }


    static bool EditRow(int[,] matrix, INewNumber number)
    {
        bool needReEdit = false;
        for (int i = matrix.GetLength(0) - 1; i >= 0 ; i--)
        {
            int counter = 1;
            var element = matrix[i, 0];
            for (int j = 1; j < matrix.GetLength(1); j++)
            {
                if (element == matrix[i, j])
                {
                    counter++;
                }
                else
                {
                    if (counter >= 3)
                    {
                        needReEdit = true;
                        for (int n = 1; n <= counter; n++)
                        {
                            for (int k = i; k >= 0; k--)
                            {
                                if (k == 0)
                                {
                                    matrix[k, j - n] = number.Get();
                                    continue;
                                }

                                matrix[k, j - n] = matrix[k - 1, j - n];
                            }
                        }
                    }
                    element = matrix[i, j];
                    counter = 1;
                }

                if (j == matrix.GetLength(1) - 1 && counter >= 3)
                {
                    needReEdit = true;
                    for (int n = 0; n < counter; n++)
                    {
                        if (i == 0)
                        {
                            matrix[i, j - n] = number.Get();
                            continue;
                        }

                        matrix[i, j - n] = matrix[i - 1, j - n];
                    }
                }
            }
        }

        return needReEdit;
    }

    static bool EditColumn(int[,] matrix, INewNumber number)
    {
        bool needReEdit = false;
        for (int row = 0; row < matrix.GetLength(1); row++)
        {
            int counter = 1;
            var element = matrix[matrix.GetLength(0) - 1, row];
            for (int col = matrix.GetLength(0) - 2; col >= 0; col--)
            {
                if (element == matrix[col, row])
                {
                    counter++;
                }
                else
                {
                    if (counter >= 3)
                    {
                        needReEdit = true;
                        col = col + counter;
                        for (int fEditCol = col; fEditCol >= 0; fEditCol--)
                        {
                            int copyFrom = fEditCol - counter;
                            if (copyFrom < 0)
                            {
                                //matrix[fEditCol, row] = number.Next(0, 4);
                                matrix[fEditCol, row] = number.Get();
                                continue;
                            }

                            matrix[fEditCol, row] = matrix[copyFrom, row];
                        }

                    }

                    element = matrix[col, row];
                    counter = 1;
                }

                if (col == 0 && counter >= 3)
                {
                    col = counter - 1;
                    for (int fEditCol = col; fEditCol >= 0; fEditCol--)
                    {
                        matrix[fEditCol, row] = number.Get();
                    }

                    counter = 1;
                    needReEdit = true;
                }
            }
        }

        return needReEdit;
    }


    static void CreateMatrix(int[,] matrix, Random number)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int k = 0; k < matrix.GetLength(1); k++)
            {
                matrix[i, k] = number.Next(0, 4);
            }
        }
    }

    static void Print(int[,] matrix, string message = "")
    {
        Console.WriteLine(new string('-', 10) + message + new string('-', 10));
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int k = 0; k < matrix.GetLength(1); k++)
            {
                Console.Write(matrix[i, k] + " ");
            }
            Console.WriteLine();
        }

        Console.WriteLine();
    }
}