using System;


class Program
{
    interface INewNumber
    {
        int Get();
    }
    class NewNumber : INewNumber
    {
        static Random number = new Random();
        
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
        var maxRows = 9;
        var maxColumns = 9;
        var numbers = new NewNumber();
        var matrix = new int[maxRows, maxColumns];

        CreateMatrix(ref matrix, numbers);
        Print(matrix, "First");

        bool needReEdit;
        do
        {
            do
            {
                needReEdit = EditRow(ref matrix, numbers);
                Print(matrix, "After EditRow");
            } while (needReEdit);

            needReEdit = EditColumn(ref matrix, numbers);
            Print(matrix, "After EditColumn");

        } while (needReEdit);

        Print(matrix, "After All");
    }

    static void Main(string[] args)
    {
        new Program().Starer();
        Console.ReadLine();
    }


    static bool EditRow(ref int[,] matrix, INewNumber number)
    {
        bool needReEdit = false;
        for (int col = matrix.GetLength(0) - 1; col >= 0 ; col--)
        {
            int counter = 1;
            var element = matrix[col, 0];
            for (int row = 1; row < matrix.GetLength(1); row++)
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
                        for (int n = 1; n <= counter; n++)
                        {
                            for (int k = col; k >= 0; k--)
                            {
                                if (k == 0)
                                {
                                    matrix[k, row - n] = number.Get();
                                    continue;
                                }

                                matrix[k, row - n] = matrix[k - 1, row - n];
                            }
                        }
                    }
                    element = matrix[col, row];
                    counter = 1;
                }

                if (row == matrix.GetLength(1) - 1 && counter >= 3)
                {
                    needReEdit = true;
                    for (int n = 0; n < counter; n++)
                    {
                        for (int k = col; k >= 0; k--)
                        {
                            if (k == 0)
                            {
                                matrix[k, row - n] = number.Get();
                                continue;
                            }

                            matrix[k, row - n] = matrix[k - 1, row - n];
                        }
                    }
                }
            }
        }

        return needReEdit;
    }

    static bool EditColumn(ref int[,] matrix, INewNumber number)
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
    
    static void CreateMatrix(ref int[,] matrix, INewNumber number)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int k = 0; k < matrix.GetLength(1); k++)
            {
                matrix[i, k] = number.Get();
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