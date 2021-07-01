using System;
using System.Collections;
using System.Collections.Generic;

namespace ThreadPoolSample
{
    internal struct Matrix : IEnumerable<int>
    {
        private const string INDEX_OUT_OF_RANGE = "Index is out of range";

        private int[,] matrix;

        public Matrix(int[,] matrix) => this.matrix = matrix;
        public Matrix(int rows, int columns) => matrix = new int[rows, columns];
        public int[,] Array { get => matrix; set => matrix = value; }
        public int Rows => matrix.GetLength(0);
        public int Columns => matrix.GetLength(1);
        public int Length => matrix.Length;

        public int[] GetRow(int row)
        {
            CheckRange(row, Rows);
            var result = new int[Columns];
            for (int i = 0; i < Columns; i++)
                result[i] = matrix[row, i];
            return result;
        }

        public int[] GetColumn(int column)
        {
            CheckRange(column, Columns);
            var result = new int[Rows];
            for (int i = 0; i < Rows; i++)
                result[i] = matrix[i, column];
            return result;
        }

        private void CheckRange(int index, int upperBound)
        {
            if (index < 0 || index >= upperBound) throw new ArgumentOutOfRangeException(nameof(index), INDEX_OUT_OF_RANGE);
        }

        public int this[int index]
        {
            get
            {
                CheckRange(index, matrix.Length);
                var (row, column) = GetRowColumnFromIndex(index);
                return matrix[row, column];
            }

            set
            {
                CheckRange(index, matrix.Length);
                var (row, column) = GetRowColumnFromIndex(index);
                matrix[row, column] = value;
            }
        }

        private (int row, int column) GetRowColumnFromIndex(int index) => (index / Columns, index % Columns);

        public static Matrix GetRandomMatrix(int rows, int columns, int minValue, int maxValue, Func<int, int, int> generator)
        {
            var matrix = new Matrix(rows, columns);
            for (int i = 0; i < matrix.Length; i++)
                matrix[i] = generator.Invoke(minValue, maxValue);
            return matrix;
        }

        public IEnumerator<int> GetEnumerator() => new MatrixEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class MatrixEnumerator : IEnumerator<int>
        {
            private int index = -1;

            public Matrix Matrix { get; }

            public MatrixEnumerator(Matrix matrix) => Matrix = matrix;

            public int Current { get => Matrix[index]; }

            public bool MoveNext() => ++index < Matrix.Length;
            public void Reset() => index = -1;

            object IEnumerator.Current { get => Current; }

            public void Dispose() { }
        }
    }
}
