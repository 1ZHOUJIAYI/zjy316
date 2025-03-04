using System;

class Program
{
    static void Main()
    {
        int[,] matrix = {
            {1, 2, 3, 4},
            {5, 1, 2, 3},
            {9, 5, 1, 2}
        };

        bool isToeplitz = IsToeplitzMatrix(matrix);
        Console.WriteLine("矩阵是否是托普利茨矩阵: " + isToeplitz);
    }

    static bool IsToeplitzMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows - 1; i++)
        {
            for (int j = 0; j < cols - 1; j++)
            {
                if (matrix[i, j] != matrix[i + 1, j + 1])
                {
                    return false;
                }
            }
        }

        return true; 
    }
}