using System;
using System.Linq;
using System.Threading.Tasks;

namespace ThreadPoolSample
{
    internal static class MatrixMath
    {
        public static Matrix Mul(this Matrix matrix, Matrix other)
        {
            ValidateMatrices(matrix, other);
            var result = new Matrix(matrix.Rows, other.Columns);
            for (int i = 0; i < result.Length; i++)
                result[i] = GetMul(matrix.GetRow(i / matrix.Columns), other.GetColumn(i % other.Columns));
            return result;
        }

        private static int GetMul(int[] array, int[] other)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
                sum += array[i] * other[i];
            return sum;
        }

        private static void ValidateMatrices(Matrix matrix, Matrix other)
        {
            if (matrix.Columns != other.Rows) throw new ArgumentException("Матрицы не согласованы");
        }

        public static Matrix MulParallel(this Matrix matrix, Matrix other)
        {
            ValidateMatrices(matrix, other);
            Matrix result = new(matrix.Rows, other.Columns);
            Parallel.For(0, result.Length, i =>
            {
                result[i] = GetMul(matrix.GetRow(i / matrix.Columns), other.GetColumn(i % other.Columns));
            });
            return result;
        }
        public static Matrix MulParallelWithRange(this Matrix matrix, Matrix other)
        {
            ValidateMatrices(matrix, other);
            Matrix result = new(matrix.Rows, other.Columns);
            Parallel.For(0, result.Length, i =>
            {
                var row = matrix.GetRow(i / matrix.Columns);
                var col = other.GetColumn(i % other.Columns);
                result[i] = Enumerable.Range(0, row.Length).Sum(j => row[j] * col[j]);
            });
            return result;
        }

        public static Matrix MulParallelWithZip(this Matrix matrix, Matrix other)
        {
            ValidateMatrices(matrix, other);
            Matrix result = new(matrix.Rows, other.Columns);
            Parallel.For(0, result.Length, i =>
            {
                var row = matrix.GetRow(i / matrix.Columns);
                var col = other.GetColumn(i % other.Columns);
                result[i] = row.Zip(col).Sum(t => t.First * t.Second);
            });
            return result;
        }
    }
}
